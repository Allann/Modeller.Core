using System.Text;

namespace Hy.Modeller.Outputs
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder Indent(this StringBuilder sb, int indent = 1, int spaces = 4)
        {
            if (spaces < 1 || spaces > 8)
                spaces = 4;
            sb.Append(new string(' ', indent * spaces));
            return sb;
        }
    }
}
