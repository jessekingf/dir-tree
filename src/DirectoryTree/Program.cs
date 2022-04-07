// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace DirectoryTree;

using System.Reflection;
using DirectoryTree.Properties;

/// <summary>
/// The entry class of the application.
/// </summary>
internal class Program
{
    /// <summary>
    /// Defines the entry point of the application.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    public static void Main(string[] args)
    {
        try
        {
            // Parse the command line arguments.
            Options options = OptionsParser.Parse(args);

            // Process the options.
            ProcessOptions(options);
        }
        catch (InvalidOptionException ex)
        {
            // Log the error and display the help text.
            Console.Error.WriteLine(ex.Message);
            DisplayHelp();
            Environment.Exit(1);
        }

        Environment.Exit(0);
    }

    /// <summary>
    /// Processes and executes the specified program options.
    /// </summary>
    /// <param name="options">The options parsed from the program arguments.</param>
    private static void ProcessOptions(Options options)
    {
        if (options.DisplayHelp)
        {
            DisplayHelp();
        }
        else if (options.DisplayVersion)
        {
            DisplayVersion();
        }
        else
        {
            PrintDirectoryTree(options.Path);
        }
    }

    /// <summary>
    /// Prints the directory tree.
    /// </summary>
    /// <param name="path">The path.</param>
    private static void PrintDirectoryTree(string path)
    {
        Stream outputStream = Console.OpenStandardOutput();
        DirectoryPrinter.PrintTree(path, outputStream);
    }

    /// <summary>
    /// Displays the help text for this application.
    /// </summary>
    private static void DisplayHelp()
    {
        Console.WriteLine(Resources.ProgramHelp);
    }

    /// <summary>
    /// Gets the current application version.
    /// </summary>
    private static void DisplayVersion()
    {
        Version version = Assembly.GetEntryAssembly().GetName().Version;
        Console.WriteLine(version.ToString());
    }
}
