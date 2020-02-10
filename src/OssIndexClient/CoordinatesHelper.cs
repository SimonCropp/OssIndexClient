using System;
using OssIndexClient;

static class CoordinatesHelper
{
    public static string Coordinates(this Package package)
    {
        if (package.Namespace == null)
        {
            return $"pkg:{package.EcoSystem}/{package.Name}@{package.Version}";
        }
        return $"pkg:{package.EcoSystem}/{package.Namespace}/{package.Name}@{package.Version}";
    }

    public static Package Parse(string coordinates)
    {
        var split = coordinates.Split('/', '@');
        var ecoSystem = (EcoSystem) Enum.Parse(typeof(EcoSystem), split[0].Substring(4));
        if (split.Length == 3)
        {
            var name = split[1];
            var version = split[2];
            return new Package(ecoSystem, name, version);
        }
        else
        {
            var @namespace = split[1];
            var name = split[2];
            var version = split[3];
            return new Package(ecoSystem, @namespace, name, version);
        }
    }
}