using System.Collections.Generic;

class ComponentReportDto
{
    public string coordinates { get; set; }= null!;
    public string description { get; set; } = null!;
    public string reference { get; set; }= null!;
    public List<VulnerabilityDto> vulnerabilities { get; set; }= null!;
}