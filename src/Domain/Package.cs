using Hy.Modeller.Interfaces;

namespace Hy.Modeller.Domain
{
    public class Package:IPackage
    {
        public Package(string name, string version)
        {
            Name = name;
            Version = version;
        }

        public string Name { get; set; }

        public string Version { get; set; }
    }

}