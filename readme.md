<!--
GENERATED FILE - DO NOT EDIT
This file was generated by [MarkdownSnippets](https://github.com/SimonCropp/MarkdownSnippets).
Source File: /readme.source.md
To change this file edit the source file and then run MarkdownSnippets.
-->

# <img src="/src/icon.png" height="30px"> OssIndexClient

[![Build status](https://ci.appveyor.com/api/projects/status/41kf6ll7dbad35px?svg=true)](https://ci.appveyor.com/project/SimonCropp/ossindexclient)
[![NuGet Status](https://img.shields.io/nuget/v/OssIndexClient.svg)](https://www.nuget.org/packages/OssIndexClient/)

A .net client for OSSIndex (https://ossindex.sonatype.org/).


<!-- toc -->
## Contents

  * [Usage](#usage)
  * [Notes](#notes)
  * [Security contact information](#security-contact-information)<!-- endtoc -->


## NuGet package

https://nuget.org/packages/OssIndexClient/


## Usage

<!-- snippet: GetReport -->
<a id='snippet-getreport'/></a>
```cs
using var ossIndexClient = new OssIndex();
var report = await ossIndexClient.GetReport(
    new Package(
        type: "nuget",
        id: "System.Net.Http",
        version: "4.3.1"));
```
<sup><a href='/src/Tests/Tests.cs#L30-L39' title='File snippet `getreport` was extracted from'>snippet source</a> | <a href='#snippet-getreport' title='Navigate to start of snippet `getreport`'>anchor</a></sup>
<!-- endsnippet -->

<!-- snippet: Tests.GetReport.verified.txt -->
<a id='snippet-Tests.GetReport.verified.txt'/></a>
```txt
{
  Coordinates: 'pkg:nuget/System.Net.Http@4.3.1',
  Description: 'This package provides a programming interface for modern HTTP applications. This package includes HttpClient for sending requests over HTTP, as well as HttpRequestMessage and HttpResponseMessage for processing HTTP messages.',
  Reference: 'https://ossindex.sonatype.org/component/pkg:nuget/System.Net.Http@4.3.1',
  Vulnerabilities: [
    {
      Id: '412e1f92-546e-465c-856b-40498da6fdeb',
      Title: '[CVE-2017-0248] Microsoft .NET Framework 2.0, 3.5, 3.5.1, 4.5.2, 4.6, 4.6.1, 4.6.2 and 4.7 allow...',
      Description: 'Microsoft .NET Framework 2.0, 3.5, 3.5.1, 4.5.2, 4.6, 4.6.1, 4.6.2 and 4.7 allow an attacker to bypass Enhanced Security Usage taggings when they present a certificate that is invalid for a specific use, aka ".NET Security Feature Bypass Vulnerability."',
      CvssScore: 7.5,
      CvssVector: 'CVSS:3.0/AV:N/AC:L/PR:N/UI:N/S:U/C:N/I:H/A:N',
      Cve: 'CVE-2017-0248',
      Reference: 'https://ossindex.sonatype.org/vuln/412e1f92-546e-465c-856b-40498da6fdeb'
    },
    {
      Id: '1cc96f1c-2dac-4ec4-9a1b-56e63e27ce5f',
      Title: '[CVE-2017-0256]  Improper Input Validation',
      Description: 'A spoofing vulnerability exists when the ASP.NET Core fails to properly sanitize web requests.',
      CvssScore: 5.3,
      CvssVector: 'CVSS:3.0/AV:N/AC:L/PR:N/UI:N/S:U/C:N/I:L/A:N',
      Cve: 'CVE-2017-0256',
      Reference: 'https://ossindex.sonatype.org/vuln/1cc96f1c-2dac-4ec4-9a1b-56e63e27ce5f'
    },
    {
      Id: '1b5f855f-0a6a-4163-9bd8-62ca43b32bca',
      Title: '[CVE-2017-0249]  Improper Input Validation',
      Description: 'An elevation of privilege vulnerability exists when the ASP.NET Core fails to properly sanitize web requests.',
      CvssScore: 7.3,
      CvssVector: 'CVSS:3.0/AV:N/AC:L/PR:N/UI:N/S:U/C:L/I:L/A:L',
      Cve: 'CVE-2017-0249',
      Reference: 'https://ossindex.sonatype.org/vuln/1b5f855f-0a6a-4163-9bd8-62ca43b32bca'
    },
    {
      Id: 'aa6df1e3-b193-4780-89f0-5a6a14b514a7',
      Title: '[CVE-2017-0247]  Improper Input Validation',
      Description: 'A denial of service vulnerability exists when the ASP.NET Core fails to properly validate web requests. NOTE: Microsoft has not commented on third-party claims that the issue is that the TextEncoder.EncodeCore function in the System.Text.Encodings.Web package in ASP.NET Core Mvc before 1.0.4 and 1.1.x before 1.1.3 allows remote attackers to cause a denial of service by leveraging failure to properly calculate the length of 4-byte characters in the Unicode Non-Character range.',
      CvssScore: 7.5,
      CvssVector: 'CVSS:3.0/AV:N/AC:L/PR:N/UI:N/S:U/C:N/I:H/A:N',
      Cve: 'CVE-2017-0247',
      Reference: 'https://ossindex.sonatype.org/vuln/aa6df1e3-b193-4780-89f0-5a6a14b514a7'
    }
  ]
}
```
<sup><a href='/src/Tests/Tests.GetReport.verified.txt#L1-L43' title='File snippet `Tests.GetReport.verified.txt` was extracted from'>snippet source</a> | <a href='#snippet-Tests.GetReport.verified.txt' title='Navigate to start of snippet `Tests.GetReport.verified.txt`'>anchor</a></sup>
<!-- endsnippet -->


## Notes

 * https://ossindex.sonatype.org/api/v3/component-report/pkg:nuget/System.Net.Http@4.3.1


## Security contact information

To report a security vulnerability, use the [Tidelift security contact](https://tidelift.com/security). Tidelift will coordinate the fix and disclosure.


## Icon

[Database](https://thenounproject.com/term/database/310841/) designed by [Creative Stall](https://thenounproject.com/creativestall/) from [The Noun Project](https://thenounproject.com/creativepriyanka).