namespace Hy.Modeller.Interfaces
{
    public interface IOutputStrategy
    {
        void Create(IOutput output, string path = null, bool overwrite = false);
    }
}
