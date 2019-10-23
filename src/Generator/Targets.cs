using System.Collections.Generic;
using System.Linq;

namespace Hy.Modeller.Generator
{
    public class Targets
    {
        private ICollection<string> _supported = new List<string> { "netstandard2.0", "netstandard2.1" };

        public static Targets Shared { get; } = new Targets();

        public Targets()
        {
            Reset();
        }

        public string Default => _supported.First();

        public void RegisterTarget(string target)
        {
            var t = target.ToLowerInvariant();
            if (_supported.Contains(t)) return;
            _supported.Add(t);
        }

        public void Reset()
        {
            _supported = new List<string> { "netstandard2.0","netstandard2.1" };
        }

        public IEnumerable<string> Supported => _supported;
    }

}
