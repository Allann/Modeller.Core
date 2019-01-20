namespace Hy.Modeller.Interfaces
{
    public interface IBuilder
    {
        IContext Context { get; }

        void Create();
    }
}