// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace DirectoryTree;

using DirectoryTree.Properties;

/// <summary>
/// Parses the command line arguments.
/// </summary>
public static class OptionsParser
{
    /// <summary>
    /// Parses the program options from the command-line arguments.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    /// <returns>The program options.</returns>
    public static Options Parse(string[] args)
    {
        Options options = new Options();
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
            options.Path = Directory.GetCurrentDirectory();
        }

        if (!Directory.Exists(options.Path))
        {
            throw new InvalidOptionException(string.Format(Resources.InvalidPath, options.Path));
        }

        return options;
    }
}
