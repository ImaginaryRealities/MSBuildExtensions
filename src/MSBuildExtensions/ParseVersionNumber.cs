//-----------------------------------------------------------------------------
// <copyright file="ParseVersionNumber.cs" company="ImaginaryRealities">
// Copyright 2013 ImaginaryRealities, LLC
// </copyright>
// <summary>
// This file implements the ParseVersionNumber class. The ParseVersionNumber
// class implements a custom MSBuild task that will take a semantic version
// number and will output the individual components of the version number.
// </summary>
//-----------------------------------------------------------------------------

namespace ImaginaryRealities.MSBuild
{
    using System;
    using System.Text.RegularExpressions;

    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    /// MSBuild task that will accept as input a semantic version number and
    /// will parse the version number and output the components of the version
    /// number.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <b>ParseVersionNumber</b> task can be used in an MSBuild script to
    /// accept a <a href="http://semver.org">semantic version number</a> and
    /// return its components. This can be useful to automatically generate the
    /// .NET version numbers for the product and assembly versions that appear
    /// in the file and assembly metadata.
    /// </para>
    /// </remarks>
    public class ParseVersionNumber : Task
    {
        /// <summary>
        /// A regular expression that is used to match a decimal number for the
        /// build number in a semantic version number.
        /// </summary>
        private static readonly Regex BuildNumberRegex = new Regex(
            @"^\d+$", RegexOptions.Compiled | RegexOptions.Singleline);

        /// <summary>
        /// The regular expression to use to parse semantic version numbers.
        /// </summary>
        private static readonly Regex VersionNumberRegex =
            new Regex(
                @"^(?<major>\d+)\.(?<minor>\d+)\.(?<patch>\d+)(-(?<prerelease>[A-Za-z0-9\.\-]+))?(\+(?<build>[A-Za-z0-9\.\-]+))?$",
                RegexOptions.Compiled | RegexOptions.Singleline);

        /// <summary>
        /// Gets the optional build number component from the version number.
        /// </summary>
        /// <value>
        /// The build number from the build version of the semantic version
        /// number.
        /// </value>
        /// <remarks>
        /// <para>
        /// The build number is retrieved by parsing the build version
        /// component of the semantic version and returning the first part that
        /// is a decimal number.
        /// </para>
        /// </remarks>
        [Output]
        public string BuildNumber { get; private set; }

        /// <summary>
        /// Gets the optional build version component from the version number.
        /// </summary>
        /// <value>
        /// The build number from the semantic version number, or <b>null</b>
        /// if the build version is not present.
        /// </value>
        [Output]
        public string BuildVersion { get; private set; }

        /// <summary>
        /// Gets the major version component from the version number.
        /// </summary>
        /// <value>
        /// The major version number from the semantic version number.
        /// </value>
        [Output]
        public string MajorVersion { get; private set; }

        /// <summary>
        /// Gets the minor version component from the version number.
        /// </summary>
        /// <value>
        /// The minor version number from the semantic version number.
        /// </value>
        [Output]
        public string MinorVersion { get; private set; }

        /// <summary>
        /// Gets the patch version component from the version number.
        /// </summary>
        /// <value>
        /// The patch version number from the semantic version number.
        /// </value>
        [Output]
        public string PatchVersion { get; private set; }

        /// <summary>
        /// Gets the optional pre-release version component from the semantic
        /// version number.
        /// </summary>
        /// <value>
        /// The pre-release version component if present, or <b>null</b> if the
        /// pre-release version is not part of the semantic version number.
        /// </value>
        [Output]
        public string PrereleaseVersion { get; private set; }

        /// <summary>
        /// Gets or sets the semantic version number to be parsed.
        /// </summary>
        /// <value>
        /// The semantic version number to parse.
        /// </value>
        [Required]
        public string VersionNumber { get; set; }

        /// <summary>
        /// Parses the semantic version number and outputs the individual
        /// components.
        /// </summary>
        /// <returns>
        /// <b>True</b> if the semantic version number was parsed successfully,
        /// or <b>false</b> if the version number was invalid or an error
        /// occurred.
        /// </returns>
        public override bool Execute()
        {
            var match = VersionNumberRegex.Match(this.VersionNumber);
            if (!match.Success)
            {
                return false;
            }

            this.MajorVersion = match.Groups["major"].Value;
            this.MinorVersion = match.Groups["minor"].Value;
            this.PatchVersion = match.Groups["patch"].Value;
            var prereleaseGroup = match.Groups["prerelease"];
            this.PrereleaseVersion = prereleaseGroup.Success ? prereleaseGroup.Value : null;
            var buildGroup = match.Groups["build"];
            this.BuildNumber = "0";
            if (buildGroup.Success)
            {
                this.BuildVersion = buildGroup.Value;
                var parts = this.BuildVersion.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var part in parts)
                {
                    match = BuildNumberRegex.Match(part);
                    if (!match.Success)
                    {
                        continue;
                    }

                    this.BuildNumber = part;
                    break;
                }
            }
            else
            {
                this.BuildVersion = null;
            }

            return true;
        }
    }
}
