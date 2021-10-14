namespace OssIndexClient;

/// <summary>
/// Package manager ecosystem. https://ossindex.sonatype.org/ecosystems
/// </summary>

#region EcoSystem

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

#endregion