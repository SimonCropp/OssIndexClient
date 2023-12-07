# <img src="/src/icon.png" height="30px"> OssIndexClient

[![Build status](https://ci.appveyor.com/api/projects/status/41kf6ll7dbad35px?svg=true)](https://ci.appveyor.com/project/SimonCropp/ossindexclient)
[![NuGet Status](https://img.shields.io/nuget/v/OssIndexClient.svg)](https://www.nuget.org/packages/OssIndexClient/)

A .net client for OSSIndex (https://ossindex.sonatype.org/).

**See [Milestones](../../milestones?state=closed) for release notes.**


## NuGet package

https://nuget.org/packages/OssIndexClient/


## Usage

### Getting a report

<!-- snippet: GetReport -->
<a id='snippet-getreport'></a>
```cs
using var ossIndexClient = new OssIndex();
var report = await ossIndexClient.GetReport(
    new(
        ecoSystem: EcoSystem.nuget,
        name: "System.Net.Http",
        version: "4.3.1"));

foreach (var vulnerability in report.Vulnerabilities)
{
    Debug.WriteLine(vulnerability.Title);
}
```
<sup><a href='/src/Tests/Tests.cs#L54-L68' title='Snippet source file'>snippet source</a> | <a href='#snippet-getreport' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Getting multiple reports

<!-- snippet: GetReports -->
<a id='snippet-getreports'></a>
```cs
using var ossIndexClient = new OssIndex();
var reports = await ossIndexClient.GetReports(
    new(
        ecoSystem: EcoSystem.nuget,
        name: "System.Net.Http",
        version: "4.3.1"),
    new(
        ecoSystem: EcoSystem.npm,
        name: "jquery",
        version: "1.11.3"));
foreach (var report in reports)
{
    foreach (var vulnerability in report.Vulnerabilities)
    {
        Debug.WriteLine(vulnerability.Title);
    }
}
```
<sup><a href='/src/Tests/Tests.cs#L26-L46' title='Snippet source file'>snippet source</a> | <a href='#snippet-getreports' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Example report contents

<!-- snippet: Tests.GetReport.verified.txt -->
<a id='snippet-Tests.GetReport.verified.txt'></a>
```txt
{
  EcoSystem: nuget,
  Name: System.Net.Http,
  Version: 4.3.1,
  Description: This package provides a programming interface for modern HTTP applications. This package includes HttpClient for sending requests over HTTP, as well as HttpRequestMessage and HttpResponseMessage for processing HTTP messages.,
  Reference: https://ossindex.sonatype.org/component/pkg:nuget/System.Net.Http@4.3.1?utm_source=ossindexclient&utm_medium=integration,
  Vulnerabilities: [
    {
      Id: CVE-2017-0248,
      Title: [CVE-2017-0248] CWE-295: Improper Certificate Validation,
      Description: Microsoft .NET Framework 2.0, 3.5, 3.5.1, 4.5.2, 4.6, 4.6.1, 4.6.2 and 4.7 allow an attacker to bypass Enhanced Security Usage taggings when they present a certificate that is invalid for a specific use, aka ".NET Security Feature Bypass Vulnerability.",
      CvssScore: 7.5,
      CvssVector: CVSS:3.0/AV:N/AC:L/PR:N/UI:N/S:U/C:N/I:H/A:N,
      Cve: CVE-2017-0248,
      Cwe: CWE-295,
      Reference: https://ossindex.sonatype.org/vulnerability/CVE-2017-0248?component-type=nuget&component-name=System.Net.Http&utm_source=ossindexclient&utm_medium=integration,
      ExternalReferences: [
        http://web.nvd.nist.gov/view/vuln/detail?vulnId=CVE-2017-0248,
        https://github.com/dotnet/corefx/issues/19535,
        https://portal.msrc.microsoft.com/en-US/security-guidance/advisory/CVE-2017-0248
      ]
    },
    {
      Id: CVE-2017-0249,
      Title: [CVE-2017-0249] CWE-20: Improper Input Validation,
      Description: An elevation of privilege vulnerability exists when the ASP.NET Core fails to properly sanitize web requests.,
      CvssScore: 7.3,
      CvssVector: CVSS:3.0/AV:N/AC:L/PR:N/UI:N/S:U/C:L/I:L/A:L,
      Cve: CVE-2017-0249,
      Cwe: CWE-20,
      Reference: https://ossindex.sonatype.org/vulnerability/CVE-2017-0249?component-type=nuget&component-name=System.Net.Http&utm_source=ossindexclient&utm_medium=integration,
      ExternalReferences: [
        http://web.nvd.nist.gov/view/vuln/detail?vulnId=CVE-2017-0249,
        https://github.com/aspnet/Announcements/issues/239
      ]
    },
    {
      Id: CVE-2017-0256,
      Title: [CVE-2017-0256] CWE-20: Improper Input Validation,
      Description: A spoofing vulnerability exists when the ASP.NET Core fails to properly sanitize web requests.,
      CvssScore: 5.3,
      CvssVector: CVSS:3.0/AV:N/AC:L/PR:N/UI:N/S:U/C:N/I:L/A:N,
      Cve: CVE-2017-0256,
      Cwe: CWE-20,
      Reference: https://ossindex.sonatype.org/vulnerability/CVE-2017-0256?component-type=nuget&component-name=System.Net.Http&utm_source=ossindexclient&utm_medium=integration,
      ExternalReferences: [
        http://web.nvd.nist.gov/view/vuln/detail?vulnId=CVE-2017-0256,
        https://github.com/aspnet/Announcements/issues/239
      ]
    },
    {
      Id: CVE-2018-8292,
      Title: [CVE-2018-8292] CWE-200: Information Exposure,
      Description: An information disclosure vulnerability exists in .NET Core when authentication information is inadvertently exposed in a redirect, aka ".NET Core Information Disclosure Vulnerability." This affects .NET Core 2.1, .NET Core 1.0, .NET Core 1.1, PowerShell Core 6.0.,
      CvssScore: 7.5,
      CvssVector: CVSS:3.0/AV:N/AC:L/PR:N/UI:N/S:U/C:H/I:N/A:N,
      Cve: CVE-2018-8292,
      Cwe: CWE-200,
      Reference: https://ossindex.sonatype.org/vulnerability/CVE-2018-8292?component-type=nuget&component-name=System.Net.Http&utm_source=ossindexclient&utm_medium=integration,
      ExternalReferences: [
        http://web.nvd.nist.gov/view/vuln/detail?vulnId=CVE-2018-8292,
        https://github.com/dotnet/announcements/issues/88,
        https://github.com/dotnet/corefx/issues/32730
      ]
    }
  ]
}
```
<sup><a href='/src/Tests/Tests.GetReport.verified.txt#L1-L67' title='Snippet source file'>snippet source</a> | <a href='#snippet-Tests.GetReport.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Package Ecosystems

The supported [OSSIndex Package Ecosystems](https://ossindex.sonatype.org/doc/coordinates) are represented by an enum.

<!-- snippet: EcoSystem -->
<a id='snippet-ecosystem'></a>
```cs
public enum EcoSystem
{
    /// <summary>https://alpinelinux.org</summary>
    alpine,

    /// <summary>https://bower.io</summary>
    bower,

    /// <summary>https://crates.io</summary>
    cargo,

    /// <summary>https://chocolatey.org</summary>
    chocolatey,

    /// <summary>https://clojars.org</summary>
    clojars,

    /// <summary>https://getcomposer.org</summary>
    composer,

    /// <summary>https://conan.io</summary>
    conan,

    /// <summary>https://conda.io</summary>
    conda,

    /// <summary>https://cran.r-project.org</summary>
    cran,

    /// <summary>https://www.debian.org</summary>
    deb,

    /// <summary>https://www.drupal.org</summary>
    drupal,

    /// <summary>https://golang.org/pkg</summary>
    golang,

    /// <summary>https://maven.apache.org</summary>
    maven,

    /// <summary>https://www.npmjs.com</summary>
    npm,

    /// <summary>https://www.nuget.org</summary>
    nuget,

    /// <summary>https://pypi.org</summary>
    pypi,

    /// <summary>https://rpm.org</summary>
    rpm,

    /// <summary>https://rubygems.org</summary>
    gem,
}
```
<sup><a href='/src/OssIndexClient/EcoSystem.cs#L7-L66' title='Snippet source file'>snippet source</a> | <a href='#snippet-ecosystem' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Notes

 * https://ossindex.sonatype.org/api/v3/component-report/pkg:nuget/System.Net.Http@4.3.1


## Icon

[Security](https://thenounproject.com/term/security/1264523/) designed by [Made](https://thenounproject.com/elki/) from [The Noun Project](https://thenounproject.com/creativepriyanka).
