# Directory Tree

A command-line utility to print the structure of a directory.

## Usage

```shell
dirtree [options] [path]
```

Arguments:

```text
path  The directory path to print.
      If not provided, defaults to the current directory.
```

Options:

```text
-a, --all      Include hidden files in the output.
-g, --git      Include only files tracked in a git repository.
-v, --version  Display the application version.
-h, --help     Display the help.
```

## Example

Example command to print this repository:

```shell
dirtree --git
```

Output:

```text
.
|-- build/
|   |-- build.sh
|-- src/
|   |-- Common/
|   |   |-- Diagnostics/
|   |   |   |-- IProcessManager.cs
|   |   |   |-- ProcessManager.cs
|   |   |   |-- ProcessResult.cs
|   |   |-- Common.csproj
|   |-- DirectoryTree/
|   |   |-- Git/
|   |   |   |-- GitController.cs
|   |   |   |-- IGitController.cs
|   |   |-- Options/
|   |   |   |-- InvalidOptionException.cs
|   |   |   |-- OptionsParser.cs
|   |   |   |-- ProgramOptions.cs
|   |   |-- Properties/
|   |   |   |-- Resources.Designer.cs
|   |   |   |-- Resources.resx
|   |   |-- DirectoryPrinter.cs
|   |   |-- DirectoryTree.csproj
|   |   |-- Program.cs
|   |-- .editorconfig
|   |-- .globalconfig
|   |-- Common.Tests.props
|   |-- coverlet.runsettings
|   |-- Directory.Build.props
|   |-- DirectoryTree.sln
|   |-- stylecop.json
|-- .gitattributes
|-- .gitignore
|-- LICENSE.txt
|-- README.md
|-- version.json
```