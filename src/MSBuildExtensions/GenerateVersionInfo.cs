//-----------------------------------------------------------------------------
// <copyright file="GenerateVersionInfo.cs" company="ImaginaryRealities">
// Copyright 2013 ImaginaryRealities, LLC
// </copyright>
// <summary>
// This file implements the GenerateVersionInfo class. The GenerateVersionInfo
// class is a custom MSBuild task that will generate a source code file
// containing the name of the build configuration and the product and assembly
// version numbers.
// </summary>
// <license>
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// right to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN N ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// </license>
//-----------------------------------------------------------------------------

namespace ImaginaryRealities.MSBuild
{
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.IO;
    using System.Reflection;

    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    /// MSBuild task that will generate a source code file containing the
    /// build-specific metadata for the project's assemblies.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If you look at the metadata in the typical <b>AssemblyInfo.cs</b> file,
    /// you will usually find metadata attributes that are both static and
    /// dynamic in nature. For example, the <b>AssemblyTitleAttribute</b>
    /// attribute is likely to be static, as the title of the assembly will not
    /// change frequently (if at all) during the lifetime of the project.
    /// However, other attributes such as <b>AssemblyConfigurationAttribute</b>
    /// and <b>AssemblyFileVersion</b> attribute will tend to change with each
    /// product build. The <b>GenerateVersionInfo</b> task is a custom MSBuild
    /// task that will help you to generate a source code file containing the
    /// non-static attributes for the assembly.
    /// </para>
    /// <para>
    /// The <b>GenerateVersionInfo</b> task will generate a metadata file
    /// containing the following attributes:
    /// </para>
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// <see cref="AssemblyConfigurationAttribute"/>
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// <see cref="AssemblyFileVersionAttribute"/>
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// <see cref="AssemblyInformationalVersionAttribute"/>
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// <see cref="AssemblyVersionAttribute"/>
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// <b>SemanticVersionNumberAttribute</b> (Optional; only necessary if you
    /// are using the ImaginaryRealities Common library)
    /// </description>
    /// </item>
    /// </list>
    /// <example>
    /// In this example, we will use the <b>GenerateVersionInfo</b> task to
    /// generate the dynamic metadata file for an assembly as part of an
    /// automated build process. In order to use the task in an MSBuild script,
    /// you will first need to import the task. Next, you call the task by
    /// passing the path of the file that will be created and the values for
    /// each of the attributes to be set.
    /// <code>
    /// &lt;UsingTask TaskName="ImaginaryRealities.MSBuild.GenerateVersionInfo"
    ///            AssemblyFile="ImaginaryRealities.MSBuild.dll"/&gt;
    /// &lt;Target Name="_GenerateVersionInfoFile"&gt;
    ///   &lt;GenerateVersionInfo OutputPath="VersionInfo.cs"
    ///                        AssemblyConfiguration="Release"
    ///                        AssemblyFileVersion="1.1.2.3"
    ///                        AssemblyInformationalVersion="1.1.2.3"
    ///                        AssemblyVersion="1.0.0.0"
    ///                        SemanticVersion="1.1.2+build.3"/&gt;
    /// &lt;/Target&gt;
    /// </code>
    /// </example>
    /// </remarks>
    public class GenerateVersionInfo : Task
    {
        /// <summary>
        /// Gets or sets the name of the build configuration for the assembly.
        /// </summary>
        /// <value>
        /// The name of the build configuration (typically Debug or Release).
        /// </value>
        [Required]
        public string AssemblyConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the version number for the assembly file.
        /// </summary>
        /// <value>
        /// The file version number for the assembly.
        /// </value>
        [Required]
        public string AssemblyFileVersion { get; set; }

        /// <summary>
        /// Gets or sets the informational version number for the assembly.
        /// </summary>
        /// <value>
        /// The informational version number for the assembly.
        /// </value>
        [Required]
        public string AssemblyInformationalVersion { get; set; }

        /// <summary>
        /// Gets or sets the version number for the assembly.
        /// </summary>
        /// <value>
        /// The version number for the assembly.
        /// </value>
        [Required]
        public string AssemblyVersion { get; set; }

        /// <summary>
        /// Gets or sets the path of the source code file to create.
        /// </summary>
        /// <value>
        /// The path of the source code file that the task will generate.
        /// </value>
        [Required]
        public string OutputPath { get; set; }

        /// <summary>
        /// Gets or sets the semantic version number for the assembly.
        /// </summary>
        /// <value>
        /// The semantic version number that will be added to the assembly
        /// metadata if this property is specified.
        /// </value>
        public string SemanticVersion { get; set; }

        /// <summary>
        /// Generates the assembly metadata file with the build-specific
        /// metadata values.
        /// </summary>
        /// <returns>
        /// <b>True</b> if the source code file was successfully generated, or
        /// <b>false</b> if an error occurred.
        /// </returns>
        public override bool Execute()
        {
            var codeCompileUnit = new CodeCompileUnit();
            codeCompileUnit.AssemblyCustomAttributes.Add(
                new CodeAttributeDeclaration(
                    new CodeTypeReference(typeof(AssemblyConfigurationAttribute)),
                    new CodeAttributeArgument(new CodePrimitiveExpression(this.AssemblyConfiguration))));
            codeCompileUnit.AssemblyCustomAttributes.Add(
                new CodeAttributeDeclaration(
                    new CodeTypeReference(typeof(AssemblyFileVersionAttribute)),
                    new CodeAttributeArgument(new CodePrimitiveExpression(this.AssemblyFileVersion))));
            codeCompileUnit.AssemblyCustomAttributes.Add(
                new CodeAttributeDeclaration(
                    new CodeTypeReference(typeof(AssemblyInformationalVersionAttribute)),
                    new CodeAttributeArgument(new CodePrimitiveExpression(this.AssemblyInformationalVersion))));
            codeCompileUnit.AssemblyCustomAttributes.Add(
                new CodeAttributeDeclaration(
                    new CodeTypeReference(typeof(AssemblyVersionAttribute)),
                    new CodeAttributeArgument(new CodePrimitiveExpression(this.AssemblyVersion))));
            if (!string.IsNullOrEmpty(this.SemanticVersion))
            {
                codeCompileUnit.AssemblyCustomAttributes.Add(
                    new CodeAttributeDeclaration(
                        "ImaginaryRealities.Framework.SemanticVersionAttribute",
                        new CodeAttributeArgument(new CodePrimitiveExpression(this.SemanticVersion))));
            }

            using (var provider = CodeDomProvider.CreateProvider("csharp"))
            using (var writer = File.CreateText(this.OutputPath))
            {
                provider.GenerateCodeFromCompileUnit(codeCompileUnit, writer, new CodeGeneratorOptions());
            }

            return true;
        }
    }
}
