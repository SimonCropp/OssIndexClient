namespace OssIndexClient
{
    public class Package
    {
        public Package(EcoSystem ecoSystem, string name, string version)
        {
            Guard.AgainstNull(name, nameof(name));
            Guard.AgainstNull(version, nameof(version));
            EcoSystem = ecoSystem;
            Name = name;
            Version = version;
        }

        public Package(EcoSystem ecoSystem, string @namespace, string name, string version)
        {
            Guard.AgainstNull(@namespace, nameof(@namespace));
            Guard.AgainstNull(name, nameof(name));
            Guard.AgainstNull(version, nameof(version));
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
}