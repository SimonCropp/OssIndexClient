using System.Collections.Generic;

namespace OssIndexClient
{
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
            Guard.AgainstNull(name, nameof(name));
            Guard.AgainstNull(version, nameof(version));
            Guard.AgainstNullOrEmpty(reference, nameof(reference));
            Guard.AgainstNull(vulnerabilities, nameof(vulnerabilities));
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
}