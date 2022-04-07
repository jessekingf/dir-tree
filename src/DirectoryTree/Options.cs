// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace DirectoryTree;

/// <summary>
/// Represents the program options.
/// </summary>
public class Options
{
    /// <summary>
    /// Gets or sets a value indicating whether to display the application help documentation.
    /// </summary>
    public bool DisplayHelp
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to display the application version.
    /// </summary>
    public bool DisplayVersion
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the path to print.
    /// </summary>
    public string Path
    {
        get;
        set;
    }
}
