﻿// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace DirectoryTree;

/// <summary>
/// Handles printing directory structures.
/// </summary>
public class DirectoryPrinter
{
    private const char TreeRoot = '.';
    private const string TreeTab = "|   ";
    private const string TreeEntry = "|-- ";
    private const char DirSuffix = '/';

    /// <summary>
    /// Initializes a new instance of the <see cref="DirectoryPrinter"/> class.
    /// </summary>
    /// <param name="allFiles">Whether to include all files, including hidden files, in the output.</param>
    public DirectoryPrinter(bool allFiles = false)
    {
        this.AllFiles = allFiles;
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

        if (!Directory.Exists(path))
        {
            throw new ArgumentException("The path does not exist.", nameof(path));
        }

        using StreamWriter writer = new StreamWriter(outputStream);
        Console.WriteLine(TreeRoot);
        this.PrintTreeNode(writer, path, 0);
    }

    private void PrintTreeNode(StreamWriter writer, string path, int level)
    {
        try
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            string treeTabs = string.Concat(Enumerable.Repeat(TreeTab, level));

            foreach (DirectoryInfo subdir in dirInfo.GetDirectories().OrderBy(d => d.Name))
            {
                if (!this.AllFiles
                    && subdir.Attributes.HasFlag(FileAttributes.Hidden))
                {
                    continue;
                }

                writer.WriteLine(string.Concat(treeTabs, TreeEntry, subdir.Name, DirSuffix));
                this.PrintTreeNode(writer, subdir.FullName, level + 1);
            }

            foreach (FileInfo file in dirInfo.GetFiles().OrderBy(f => f.Name))
            {
                if (!this.AllFiles
                    && file.Attributes.HasFlag(FileAttributes.Hidden))
                {
                    continue;
                }

                writer.WriteLine(string.Concat(treeTabs, TreeEntry, file.Name));
            }
        }
        catch (UnauthorizedAccessException)
        {
            // Skip directories the user does not have access to.
        }
    }
}
