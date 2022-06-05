// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace DirectoryTree.Options;

using System.IO.Abstractions;
using DirectoryTree.Git;
using DirectoryTree.Properties;

/// <summary>
/// Parses the command line arguments.
/// </summary>
public class OptionsParser
{
    /// <summary>
    /// Provides access to the file system.
    /// </summary>
    private readonly IFileSystem fileSystem;

    /// <summary>
    /// Provides access to Git source control.
    /// </summary>
    private readonly IGitController git = new GitController();

    /// <summary>
    /// Initializes a new instance of the <see cref="OptionsParser"/> class.
    /// </summary>
    public OptionsParser()
        : this(new FileSystem(), new GitController())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OptionsParser"/> class.
    /// </summary>
    /// <param name="fileSystem">Provides access to the file system.</param>
    /// <param name="git">Handles access to Git.</param>
    public OptionsParser(IFileSystem fileSystem, IGitController git)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        this.git = git ?? throw new ArgumentNullException(nameof(git));
    }

    /// <summary>
    /// Parses the program options from the command-line arguments.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    /// <returns>The program options.</returns>
    public ProgramOptions Parse(string[] args)
    {
        ProgramOptions options = new();
        foreach (string arg in args ?? Array.Empty<string>())
        {
            if (string.IsNullOrEmpty(arg))
            {
                continue;
            }

            if (arg.StartsWith("-", StringComparison.InvariantCulture))
            {
                switch (arg.ToLowerInvariant())
                {
                    case "--all":
                    case "-a":
                        options.AllFiles = true;
                        continue;
                    case "--git":
                    case "-g":
                        options.GitOnly = true;
                        continue;
                    case "--help":
                    case "-h":
                        options.DisplayHelp = true;
                        continue;
                    case "--version":
                    case "-v":
                        options.DisplayVersion = true;
                        continue;
                    default:
                        throw new InvalidOptionException(string.Format(Resources.InvalidOption, arg));
                }
            }

            // Not a switch, assume it is the directory path.
            // If the path is already set, too many arguments have been passed.
            if (!string.IsNullOrEmpty(options.Path))
            {
                throw new InvalidOptionException(string.Format(Resources.InvalidOption, arg));
            }

            options.Path = arg;
        }

        if (string.IsNullOrEmpty(options.Path))
        {
            // Default to the current working directory.
            options.Path = this.fileSystem.Directory.GetCurrentDirectory();
        }

        if (!this.fileSystem.Directory.Exists(options.Path))
        {
            throw new InvalidOptionException(string.Format(Resources.InvalidPath, options.Path));
        }

        if (options.GitOnly)
        {
            if (!this.git.IsRepository(options.Path))
            {
                throw new InvalidOptionException(string.Format(Resources.InvalidGitRepo, options.Path));
            }
        }

        return options;
    }
}
