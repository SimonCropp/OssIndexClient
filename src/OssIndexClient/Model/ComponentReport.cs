using System.Collections.Generic;

namespace OssIndexClient
{
    public class ComponentReport
    {
        public ComponentReport(
            string type,
            string id,
            string version,
            string? description,
            string reference,
            IReadOnlyList<Vulnerability> vulnerabilities)
        {
            Guard.AgainstNull(type, nameof(type));
            Guard.AgainstNull(id, nameof(id));
            Guard.AgainstNull(version, nameof(version));
            Guard.AgainstNullOrEmpty(reference, nameof(reference));
            Guard.AgainstNull(vulnerabilities, nameof(vulnerabilities));
            Type = type;
            Id = id;
            Version = version;
            Description = description;
            Reference = reference;
            Vulnerabilities = vulnerabilities;
        }

        public string Type { get; }
        public string Id { get; }
        public string Version { get; }
        public string? Description { get; }
        public string Reference { get; }
        public IReadOnlyList<Vulnerability> Vulnerabilities { get; }
    }
}