using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OssIndexClient;

static class TempPath
{
    static char[] invalidPathChars = Path.GetInvalidPathChars();

    static string tempDir = Path.Combine(Path.GetTempPath(), "OssIndexClient");
    static string combinedDir = Path.Combine(tempDir, "combined");

    static TempPath()
    {
        Directory.CreateDirectory(combinedDir);
    }

    public static string GetPath(IEnumerable<Package> packages)
    {
        var codes = new List<int>();

        foreach (var package in packages)
        {
            codes.Add(package.Id.GetStableHashCode());
            codes.Add(package.Type.GetStableHashCode());
            codes.Add(package.Version.GetStableHashCode());
        }

        codes.Sort();
        var hash = 0;
        foreach (var code in codes)
        {
            unchecked
            {
                hash *= 251; // multiply by a prime number
                hash += code; // add next hash code
            }
        }

        return Path.Combine(combinedDir, hash + ".json");
    }

    public static string GetPath(Package package)
    {
        var packageDir = Path.Combine(tempDir, "aa", package.Id);
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