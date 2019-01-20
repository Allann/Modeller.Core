using Hy.Modeller.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Hy.Modeller.Generator
{
    public class CodeGenerator : ICodeGenerator
    {
        private readonly ILogger<ICodeGenerator> _logger;

        public CodeGenerator(ILogger<ICodeGenerator> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IOutput Create(IContext context)
        {
            var result = context.ProcessConfiguration();
            if (!result.IsValid)
            {
                foreach (var item in result.Errors)
                    _logger.LogError($"{item.ErrorMessage}");
                return null;
            }
        
            _logger.LogInformation($"Module: {context.Module.Namespace}");
            _logger.LogInformation($"Supports regen: {context.Settings.SupportRegen}");
            _logger.LogInformation($"Generator: {context.Generator.Metadata.Name}");

            var type = context.Generator.Metadata.EntryPoint;
            var cis = type.GetConstructors();
            if (cis.Length != 1)
            {
                _logger.LogError("Modeller only supports single public constructors for a generator.");
                return null;
            }
            var ci = cis[0];
            var args = new List<object>();
            foreach (var p in ci.GetParameters())
            {
                if (p.ParameterType.FullName == "Hy.Modeller.Interfaces.ISettings")
                {
                    args.Add(context.Settings);
                }
                else if (p.ParameterType.FullName == "Hy.Modeller.Models.Module")
                {
                    args.Add(context.Module);
                }
                else if (p.ParameterType.FullName == "Hy.Modeller.Models.Model")
                {
                    args.Add(context.Model);
                }
                else
                {
                    _logger.LogError($"{p.ParameterType.ToString()} is not a supported argument type on Generator constructors.");
                    return null;
                }
            }

            return (ci.Invoke(args.ToArray()) as IGenerator).Create();
        }
    }
}
