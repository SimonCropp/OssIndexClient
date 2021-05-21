# <img src="/src/icon.png" height="30px"> OssIndexClient

[![Build status](https://ci.appveyor.com/api/projects/status/41kf6ll7dbad35px?svg=true)](https://ci.appveyor.com/project/SimonCropp/ossindexclient)
[![NuGet Status](https://img.shields.io/nuget/v/OssIndexClient.svg)](https://www.nuget.org/packages/OssIndexClient/)

A .net client for OSSIndex (https://ossindex.sonatype.org/).


## NuGet package

https://nuget.org/packages/OssIndexClient/


## Usage

### Getting a report

<!-- snippet: GetReport -->
<a id='snippet-getreport'></a>
```cs
using OssIndex ossIndexClient = new();
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
<sup><a href='/src/Tests/Tests.cs#L59-L73' title='Snippet source file'>snippet source</a> | <a href='#snippet-getreport' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Getting multiple reports

<!-- snippet: GetReports -->
<a id='snippet-getreports'></a>
```cs
using OssIndex ossIndexClient = new();
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
<sup><a href='/src/Tests/Tests.cs#L31-L51' title='Snippet source file'>snippet source</a> | <a href='#snippet-getreports' title='Start of snippet'>anchor</a></sup>
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
      Id: 412e1f92-546e-465c-856b-40498da6fdeb,
      Title: [CVE-2017-0248] Microsoft .NET Framework 2.0, 3.5, 3.5.1, 4.5.2, 4.6, 4.6.1, 4.6.2 and 4.7 allow...,
      Description: Microsoft .NET Framework 2.0, 3.5, 3.5.1, 4.5.2, 4.6, 4.6.1, 4.6.2 and 4.7 allow an attacker to bypass Enhanced Security Usage taggings when they present a certificate that is invalid for a specific use, aka ".NET Security Feature Bypass Vulnerability.",
      CvssScore: 7.5,
      CvssVector: CVSS:3.0/AV:N/AC:L/PR:N/UI:N/S:U/C:N/I:H/A:N,
      Cve: CVE-2017-0248,
      Reference: https://ossindex.sonatype.org/vulnerability/412e1f92-546e-465c-856b-40498da6fdeb?component-type=nuget&component-name=System.Net.Http&utm_source=ossindexclient&utm_medium=integration
    },
    {
      Id: 1cc96f1c-2dac-4ec4-9a1b-56e63e27ce5f,
      Title: [CVE-2017-0256]  Improper Input Validation,
      Description: A spoofing vulnerability exists when the ASP.NET Core fails to properly sanitize web requests.,
      CvssScore: 5.3,
      CvssVector: CVSS:3.0/AV:N/AC:L/PR:N/UI:N/S:U/C:N/I:L/A:N,
      Cve: CVE-2017-0256,
      Reference: https://ossindex.sonatype.org/vulnerability/1cc96f1c-2dac-4ec4-9a1b-56e63e27ce5f?component-type=nuget&component-name=System.Net.Http&utm_source=ossindexclient&utm_medium=integration
    },
    {
      Id: 1b5f855f-0a6a-4163-9bd8-62ca43b32bca,
      Title: [CVE-2017-0249]  Improper Input Validation,
      Description: An elevation of privilege vulnerability exists when the ASP.NET Core fails to properly sanitize web requests.,
      CvssScore: 7.3,
      CvssVector: CVSS:3.0/AV:N/AC:L/PR:N/UI:N/S:U/C:L/I:L/A:L,
      Cve: CVE-2017-0249,
      Reference: https://ossindex.sonatype.org/vulnerability/1b5f855f-0a6a-4163-9bd8-62ca43b32bca?component-type=nuget&component-name=System.Net.Http&utm_source=ossindexclient&utm_medium=integration
    },
    {
      Id: aa6df1e3-b193-4780-89f0-5a6a14b514a7,
      Title: [CVE-2017-0247]  Improper Input Validation,
      Description: A denial of service vulnerability exists when the ASP.NET Core fails to properly validate web requests. NOTE: Microsoft has not commented on third-party claims that the issue is that the TextEncoder.EncodeCore function in the System.Text.Encodings.Web package in ASP.NET Core Mvc before 1.0.4 and 1.1.x before 1.1.3 allows remote attackers to cause a denial of service by leveraging failure to properly calculate the length of 4-byte characters in the Unicode Non-Character range.,
      CvssScore: 7.5,
      CvssVector: CVSS:3.0/AV:N/AC:L/PR:N/UI:N/S:U/C:N/I:H/A:N,
      Cve: CVE-2017-0247,
      Reference: https://ossindex.sonatype.org/vulnerability/aa6df1e3-b193-4780-89f0-5a6a14b514a7?component-type=nuget&component-name=System.Net.Http&utm_source=ossindexclient&utm_medium=integration
    }
  ]
}
```
<sup><a href='/src/Tests/Tests.GetReport.verified.txt#L1-L45' title='Snippet source file'>snippet source</a> | <a href='#snippet-Tests.GetReport.verified.txt' title='Start of snippet'>anchor</a></sup>
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
