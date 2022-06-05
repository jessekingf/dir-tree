// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace DirectoryTree.Git;

using System.IO.Abstractions;
using Common.Diagnostics;

/// <summary>
/// Handles git operations.
/// </summary>
public class GitController : IGitController
{
    /// <summary>
    /// Provides access to the file system.
    /// </summary>
    private readonly IFileSystem fileSystem;

    /// <summary>
    /// The process manager to use to run Git commands.
    /// </summary>
    private readonly IProcessManager processManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="GitController"/> class.
    /// </summary>
    public GitController()
        : this(new FileSystem(), new ProcessManager())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GitController"/> class.
    /// </summary>
    /// <param name="fileSystem">Provides access to the file system.</param>
    /// <param name="processManager">The process manager.</param>
    public GitController(IFileSystem fileSystem, IProcessManager processManager)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        this.processManager = processManager ?? throw new ArgumentNullException(nameof(processManager));
    }

    /// <inheritdoc />
    public bool IsRepository(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(path));
        }

        if (!this.fileSystem.Directory.Exists(path))
        {
            throw new ArgumentException($"Directory not found: {path}");
        }

        ProcessResult result = this.processManager.Run(@"git", @"rev-parse", path);
        return result.ExitCode == 0;
    }

    /// <inheritdoc />
    public bool IsTracked(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(path));
        }

        if (!this.fileSystem.File.Exists(path)
            && !this.fileSystem.Directory.Exists(path))
        {
            throw new ArgumentException($"Path not found: {path}");
        }

        ProcessResult result = this.processManager.Run(@"git", $"ls-files --error-unmatch {path}");
        return result.ExitCode == 0;
    }
}
