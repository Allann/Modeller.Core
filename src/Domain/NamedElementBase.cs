using Newtonsoft.Json;

namespace Hy.Modeller.Domain
{
    public abstract class NamedElementBase
    {
        protected NamedElementBase(string name)
        {
            Name = new Name(name);
        }

        protected void OverrideName(string name)
        {
            Name.SetName(name);
        }

        [JsonProperty]
        public Name Name { get; protected set; }
    }
}