namespace OssIndexClient;

public class ComponentReport
{
    public ComponentReport(
        EcoSystem ecoSystem,
        string? @namespace,
        string name,
        string version,
        string? description,
        string reference,
        IReadOnlyList<Vulnerability> vulnerabilities)
    {
        Guard.AgainstNullOrEmpty(reference, nameof(reference));
        EcoSystem = ecoSystem;
        Namespace = @namespace;
        Name = name;
        Version = version;
        Description = description;
        Reference = reference;
        Vulnerabilities = vulnerabilities;
    }

    public EcoSystem EcoSystem { get; }
    public string? Namespace { get; }
    public string Name { get; }
    public string Version { get; }
    public string? Description { get; }
    public string Reference { get; }
    public IReadOnlyList<Vulnerability> Vulnerabilities { get; }
}