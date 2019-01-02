using Hy.Modeller.Interfaces;

namespace Hy.Modeller.CoreTests
{
    internal class TestFileWriter : IFileWriter
    {
        public string Output { get; private set; }

        void IFileWriter.Write(IFile file)
        {
            Output = file.Content;
        }
    }
}
