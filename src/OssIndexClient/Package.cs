namespace OssIndexClient
{
    public class Package
    {
        public Package(EcoSystem ecoSystem, string id, string version)
        {
            Guard.AgainstNull(id, nameof(id));
            Guard.AgainstNull(version, nameof(version));
            EcoSystem = ecoSystem;
            Id = id;
            Version = version;
        }

        public EcoSystem EcoSystem { get; }
        public string Id { get; }
        public string Version { get; }
    }
}