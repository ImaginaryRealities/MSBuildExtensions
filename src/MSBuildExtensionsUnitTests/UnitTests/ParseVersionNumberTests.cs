//-----------------------------------------------------------------------------
// <copyright file="ParseVersionNumberTests.cs" company="ImaginaryRealities">
// Copyright 2013 ImaginaryRealities, LLC
// </copyright>
// <summary>
// This file implements the unit tests for the ParseVersionNumber class.
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
    using Xunit;

    /// <summary>
    /// Unit tests the <see cref="ParseVersionNumber"/> class.
    /// </summary>
    public class ParseVersionNumberTests
    {
        /// <summary>
        /// The <see cref="ParseVersionNumber"/> object to test.
        /// </summary>
        private readonly ParseVersionNumber task = new ParseVersionNumber();

        /// <summary>
        /// Tests that the <see cref="ParseVersionNumber"/> task fails when
        /// presented with an invalid semantic version number.
        /// </summary>
        [Fact]
        public void TaskFailsForInvalidSemanticVersionNumber()
        {
            this.task.VersionNumber = "1.0.0.0";
            var result = this.task.Execute();
            Assert.False(result);
        }

        /// <summary>
        /// Tests that the <see cref="ParseVersionNumber"/> task succeeds when
        /// only the major, minor, and patch components are provided.
        /// </summary>
        [Fact]
        public void TaskParsesBasicVersionNumberSuccessfully()
        {
            this.task.VersionNumber = "11.22.33";
            var result = this.task.Execute();
            Assert.True(result);
            Assert.Equal("11", this.task.MajorVersion);
            Assert.Equal("22", this.task.MinorVersion);
            Assert.Equal("33", this.task.PatchVersion);
            Assert.Null(this.task.PrereleaseVersion);
            Assert.Null(this.task.BuildVersion);
        }

        /// <summary>
        /// Tests that the <see cref="ParseVersionNumber"/> task successfully
        /// parses a version number.
        /// </summary>
        [Fact]
        public void TaskParsesFullVersionNumberSuccessfully()
        {
            this.task.VersionNumber = "1.2.3-alpha.1+build.25";
            var result = this.task.Execute();
            Assert.True(result);
            Assert.Equal("1", this.task.MajorVersion);
            Assert.Equal("2", this.task.MinorVersion);
            Assert.Equal("3", this.task.PatchVersion);
            Assert.Equal("alpha.1", this.task.PrereleaseVersion);
            Assert.Equal("build.25", this.task.BuildVersion);
        }
    }
}
