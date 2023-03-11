namespace OssIndexClient;

public class Package
{
    public Package(EcoSystem ecoSystem, string name, string version)
    {
        EcoSystem = ecoSystem;
        Name = name;
        Version = version;
    }

    public Package(EcoSystem ecoSystem, string @namespace, string name, string version)
    {
        EcoSystem = ecoSystem;
        Namespace = @namespace;
        Name = name;
        Version = version;
    }

    public EcoSystem EcoSystem { get; }
    public string? Namespace { get; }
    public string Name { get; }
    public string Version { get; }
}