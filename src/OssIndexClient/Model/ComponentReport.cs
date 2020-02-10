using System.Collections.Generic;

namespace OssIndexClient
{
    public class ComponentReport
    {
        public ComponentReport(
            string coordinates,
            string? description,
            string reference,
            IReadOnlyList<Vulnerability> vulnerabilities)
        {
            Guard.AgainstNullOrEmpty(coordinates, nameof(coordinates));
            Guard.AgainstNullOrEmpty(reference, nameof(reference));
            Guard.AgainstNull(vulnerabilities, nameof(vulnerabilities));
            Coordinates = coordinates;
            Description = description;
            Reference = reference;
            Vulnerabilities = vulnerabilities;
        }

        public string Coordinates { get; }
        public string? Description { get; }
        public string Reference { get; }
        public IReadOnlyList<Vulnerability> Vulnerabilities { get; }
    }
}