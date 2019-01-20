namespace Hy.Modeller.Interfaces
{
    public interface ICodeGenerator
    {
        IOutput Create(IContext context);
    }
}