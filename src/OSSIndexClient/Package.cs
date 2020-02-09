public class Package
{
    public Package(string type, string id, string version)
    {
        Guard.AgainstNull(type, nameof(type));
        Guard.AgainstNull(id, nameof(id));
        Guard.AgainstNull(version, nameof(version));
        Type = type;
        Id = id;
        Version = version;
    }

    public string Type { get; }
    public string Id { get; }
    public string Version { get; }
}