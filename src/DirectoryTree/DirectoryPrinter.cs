// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace DirectoryTree;

using System.IO.Abstractions;
using DirectoryTree.Git;

/// <summary>
/// Handles printing directory structures.
/// </summary>
public class DirectoryPrinter
{
    /// <summary>
    /// The character to print to represent the root directory.
    /// </summary>
    private const char TreeRoot = '.';

    /// <summary>
    /// The characters to print to tab over in the tree before printing a node.
    /// </summary>
    private const string TreeTab = "|   ";

    /// <summary>
    /// The prefix to print for a tree node.
    /// </summary>
    private const string TreeEntryPrefix = "|-- ";

    /// <summary>
    /// The suffix to print at the end of a directory node.
    /// </summary>
    private const char DirSuffix = '/';

    /// <summary>
    /// Provides access to the file system.
    /// </summary>
    private readonly IFileSystem fileSystem;

    /// <summary>
    /// Provides access to Git source control.
    /// </summary>
    private readonly IGitController git = new GitController();

    /// <summary>
    /// Initializes a new instance of the <see cref="DirectoryPrinter"/> class.
    /// </summary>
    public DirectoryPrinter()
        : this(new FileSystem(), new GitController())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DirectoryPrinter"/> class.
    /// </summary>
    /// <param name="fileSystem">Provides access to the file system.</param>
    /// <param name="git">Handles access to Git.</param>
    public DirectoryPrinter(IFileSystem fileSystem, IGitController git)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        this.git = git ?? throw new ArgumentNullException(nameof(git));
    }

    /// <summary>
    /// Gets or sets a value indicating whether to include all files,
    /// including hidden files, in the output.
    /// </summary>
    public bool AllFiles
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to only include
    /// files tracked in a git repository.
    /// </summary>
    public bool GitOnly
    {
        get;
        set;
    }

    /// <summary>
    /// Prints the directory tree.
    /// </summary>
    /// <param name="outputStream">The output stream to print to.</param>
    /// <param name="path">The directory path to print.</param>
    public void PrintTree(Stream outputStream, string path)
    {
        if (outputStream == null)
        {
            throw new ArgumentNullException(nameof(outputStream));
        }

        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentException("The path cannot be null or empty.", nameof(path));
        }

        if (!this.fileSystem.Directory.Exists(path))
        {
            throw new ArgumentException($"Path not found: {path}");
        }

        if (this.GitOnly
            && !this.git.IsRepository(path))
        {
            throw new ArgumentException($"Not a git repository: {path}");
        }

        using StreamWriter writer = new StreamWriter(outputStream);
        Console.WriteLine(TreeRoot);
        this.PrintTreeNode(writer, path, 0);
    }

    private void PrintTreeNode(StreamWriter writer, string path, int level)
    {
        try
        {
            IDirectoryInfo dirInfo = this.fileSystem.DirectoryInfo.FromDirectoryName(path);
            string treeTabs = string.Concat(Enumerable.Repeat(TreeTab, level));

            foreach (IDirectoryInfo subdir in dirInfo.GetDirectories().OrderBy(d => d.Name))
            {
                if (!this.AllFiles
                    && subdir.Attributes.HasFlag(FileAttributes.Hidden))
                {
                    continue;
                }

                if (this.GitOnly
                    && (subdir.Name.Equals(".git", StringComparison.Ordinal)
                        || !this.git.IsTracked(subdir.FullName)))
                {
                    continue;
                }

                writer.WriteLine(string.Concat(treeTabs, TreeEntryPrefix, subdir.Name, DirSuffix));
                this.PrintTreeNode(writer, subdir.FullName, level + 1);
            }

            foreach (IFileInfo file in dirInfo.GetFiles().OrderBy(f => f.Name))
            {
                if (!this.AllFiles
                    && file.Attributes.HasFlag(FileAttributes.Hidden))
                {
                    continue;
                }

                if (this.GitOnly
                    && !this.git.IsTracked(file.FullName))
                {
                    continue;
                }

                writer.WriteLine(string.Concat(treeTabs, TreeEntryPrefix, file.Name));
            }
        }
        catch (UnauthorizedAccessException)
        {
            // Skip directories the user does not have access to.
        }
    }
}
