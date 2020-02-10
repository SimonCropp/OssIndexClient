using System.Collections.Generic;

namespace OssIndexClient
{
    public class ComponentReport
    {
        public ComponentReport(
            EcoSystem ecoSystem,
            string id,
            string version,
            string? description,
            string reference,
            IReadOnlyList<Vulnerability> vulnerabilities)
        {
            Guard.AgainstNull(id, nameof(id));
            Guard.AgainstNull(version, nameof(version));
            Guard.AgainstNullOrEmpty(reference, nameof(reference));
            Guard.AgainstNull(vulnerabilities, nameof(vulnerabilities));
            EcoSystem = ecoSystem;
            Id = id;
            Version = version;
            Description = description;
            Reference = reference;
            Vulnerabilities = vulnerabilities;
        }

        public EcoSystem EcoSystem { get; }
        public string Id { get; }
        public string Version { get; }
        public string? Description { get; }
        public string Reference { get; }
        public IReadOnlyList<Vulnerability> Vulnerabilities { get; }
    }
}