// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace DirectoryTree;

/// <summary>
/// The entry class of the application.
/// </summary>
internal class Program
{
    /// <summary>
    /// Defines the entry point of the application.
    /// </summary>
    public static void Main()
    {
        string path = Directory.GetCurrentDirectory();
        Stream outputStream = Console.OpenStandardOutput();
        DirectoryPrinter.PrintTree(path, outputStream);
    }
}
