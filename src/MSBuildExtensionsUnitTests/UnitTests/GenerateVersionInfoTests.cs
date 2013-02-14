//-----------------------------------------------------------------------------
// <copyright file="GenerateVersionInfoTests.cs" company="ImaginaryRealities">
// Copyright 2013 ImaginaryRealities, LLC
// </copyright>
// <summary>
// This file implements the unit tests for the GenerateVersionInfo class.
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

namespace ImaginaryRealities.MSBuild.UnitTests
{
    using System.IO;
    using System.Text.RegularExpressions;

    using Xunit;

    /// <summary>
    /// Unit tests the <see cref="GenerateVersionInfo"/> class.
    /// </summary>
    public class GenerateVersionInfoTests
    {
        /// <summary>
        /// The <see cref="GenerateVersionInfo"/> task to test.
        /// </summary>
        private readonly GenerateVersionInfo task = new GenerateVersionInfo();

        /// <summary>
        /// Tests that the <see cref="GenerateVersionInfo"/> task successfully
        /// generates the version information source code file.
        /// </summary>
        [Fact]
        public void VersionInfoFileIsGeneratedSuccessfully()
        {
            string path = null;
            try
            {
                path = Path.GetTempFileName();
                this.task.OutputPath = path;
                this.task.AssemblyConfiguration = "Release";
                this.task.AssemblyFileVersion = "2.1.3.5";
                this.task.AssemblyInformationalVersion = "2.1.3.5";
                this.task.AssemblyVersion = "2.0.0.0";
                this.task.SemanticVersion = "2.1.3+build.5";
                var result = this.task.Execute();
                Assert.True(result);

                string sourceCode;
                using (var reader = File.OpenText(path))
                {
                    sourceCode = reader.ReadToEnd();
                }

                Assert.True(
                    Regex.IsMatch(
                        sourceCode,
                        "\\[assembly: System.Reflection.AssemblyConfigurationAttribute\\(\"Release\"\\)\\]",
                        RegexOptions.Multiline));
                Assert.True(
                    Regex.IsMatch(
                        sourceCode,
                        "\\[assembly: System.Reflection.AssemblyFileVersionAttribute\\(\"2\\.1\\.3\\.5\"\\)\\]",
                        RegexOptions.Multiline));
                Assert.True(
                    Regex.IsMatch(
                        sourceCode,
                        "\\[assembly: System.Reflection.AssemblyInformationalVersionAttribute\\(\"2\\.1\\.3\\.5\"\\)\\]",
                        RegexOptions.Multiline));
                Assert.True(
                    Regex.IsMatch(
                        sourceCode,
                        "\\[assembly: System.Reflection.AssemblyVersionAttribute\\(\"2\\.0\\.0\\.0\"\\)\\]",
                        RegexOptions.Multiline));
                Assert.True(
                    Regex.IsMatch(
                        sourceCode,
                        "\\[assembly: ImaginaryRealities.Framework.SemanticVersionAttribute\\(\"2\\.1\\.3\\+build\\.5\"\\)\\]",
                        RegexOptions.Multiline));
            }
            finally
            {
                if (null != path && File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }
    }
}
