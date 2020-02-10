using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

static class TempPath
{
    static char[] invalidPathChars = Path.GetInvalidPathChars();

    static string tempDir = Path.Combine(Path.GetTempPath(), "OSSIndexClient");

    public static string GetPath(IEnumerable<Package> packages)
    {
        var codes = new List<int>();

        foreach (var package in packages)
        {
            codes.Add(package.Id.GetHashCode());
            codes.Add(package.Type.GetHashCode());
            codes.Add(package.Version.GetHashCode());
        }
        codes.Sort();
        var hash = 0;
        foreach (var code in codes) {
            unchecked {
                hash *= 251; // multiply by a prime number
                hash += code; // add next hash code
            }
        }
        var packageDir = Path.Combine(tempDir, "combined", hash+".json");
        Directory.CreateDirectory(packageDir);
        return packageDir;
    }




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