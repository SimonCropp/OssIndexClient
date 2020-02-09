using System.IO;
using System.Linq;
using System.Text;

static class TempPath
{
    static char[] invalidPathChars = Path.GetInvalidPathChars();

    static string tempDir = Path.Combine(Path.GetTempPath(), "OSSIndexClient");

    public static string GetPath(Package package)
    {
        var packageDir = Path.Combine(tempDir, package.Type, package.Id);
        Directory.CreateDirectory(packageDir);
        var builder = new StringBuilder(packageDir + @"\");
        foreach (var ch in package.Version.Where(ch => !invalidPathChars.Contains(ch)))
        {
            builder.Append(ch);
        }

        builder.Append(".json");
        return builder.ToString();
    }
}