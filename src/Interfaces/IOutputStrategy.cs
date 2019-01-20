namespace Hy.Modeller.Interfaces
{
    public interface IOutputStrategy
    {
        void Create(IOutput output, IGeneratorConfiguration generatorConfiguration);
    }
}
