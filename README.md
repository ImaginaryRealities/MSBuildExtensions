MSBuild Extensions
==================
This project implements custom MSBuild tasks and other extensions that are used by ImaginaryRealities to build our software products. We are releasing the source code under an open source license so that they can be used by others who are developing Windows or .NET software products.

Building the MSBuild Extensions
-------------------------------
The MSBuild Extensions project uses an automated build system controlled by Microsoft's MSBuild build tool that is distributed with the .NET Framework. The MSBuild Extensions project is compatible with both the .NET 4.0 and 4.5 frameworks.

The MSBuild Extensions project utilizes NuGet's package restore feature to automatically download dependencies that are deployed using NuGet. Dependencies that are not available through NuGet are included in the Git repository for the project.

In order to make building the project as easy as possible, two command scripts have been created in the root of the project workspace:

* **BUILD.cmd** will build the source code, run the unit tests, and will generate the NuGet package for redistributing the project to consumers.
* **CLEAN.cmd** will remove all build-generated files and directories from the project workspace. The **CLEAN.cmd** script will also remove the NuGet packages directory and any packages that were downloaded by the build process.

###Strong Naming the Assemblies
ImaginaryRealities always strong names the assemblies that we release, however, for security reasons we do not redistribute our stong name key. In order to build the project, you will need to create a strong name key named **ImaginaryRealities.snk** in the **src** directory.

    cd <project-root>\src
    sn -k ImaginaryRealities.snk

If you do not create the strong name key, the project will fail to build and you will receive an error that the strong name key file could not be found.

Project Team
------------
<table width="100%">
	<tr>
		<th>Name</th>
		<th>Role</th>
		<th>Blog</th>
		<th>Twitter</th>
	</tr>
	<tr>
		<td><a href="mailto:michael@imaginaryrealities.com">Michael Collins</a></td>
		<td>Project coordinator</td>
		<td><a href="http://www.michaelfcollins3.me">Blog</a></td>
		<td><a href="https://twitter.com/mfcollins3">@mfcollins3</a></td>
	</tr>
</table>

License
-------
Copyright &copy; 2013 ImaginaryRealities, LLC

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the right to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.