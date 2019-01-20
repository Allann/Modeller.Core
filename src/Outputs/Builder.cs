using Hy.Modeller.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace Hy.Modeller.Outputs
{
    public class Builder : IBuilder
    {
        private readonly ICodeGenerator _codeGenerator;
        private readonly IOutputStrategy _outputStrategy;
        private readonly ILogger<IBuilder> _logger;

        public Builder(IContext context, ICodeGenerator codeGenerator, IOutputStrategy outputStrategy, ILogger<IBuilder> logger)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            _codeGenerator = codeGenerator ?? throw new ArgumentNullException(nameof(codeGenerator));
            _outputStrategy = outputStrategy ?? throw new ArgumentNullException(nameof(outputStrategy));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IContext Context { get; }

        public void Create()
        {
            var outputPath = Context.GeneratorConfiguration.OutputPath;

            _logger.LogInformation($"Creating: {outputPath}");

            var output = _codeGenerator.Create(Context);
            if(output!=null)
                _outputStrategy.Create(output, Context.GeneratorConfiguration);

            _logger.LogInformation("Generation complete.");
        }
    }
}