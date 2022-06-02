// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace DirectoryTree.Git;

using Common.Diagnostics;

/// <summary>
/// Handles git operations.
/// </summary>
public class GitController
{
    /// <summary>
    /// The process manager to use to run Git commands.
    /// </summary>
    private readonly IProcessManager processManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="GitController"/> class.
    /// </summary>
    public GitController()
        : this(new ProcessManager())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GitController"/> class.
    /// </summary>
    /// <param name="processManager">The process manager.</param>
    public GitController(IProcessManager processManager)
    {
        this.processManager = processManager ?? throw new ArgumentNullException(nameof(processManager));
    }

    /// <summary>
    /// Gets whether the specified path is a Git repository.
    /// </summary>
    /// <param name="path">The path to check.</param>
    /// <returns>True if the path is a Git repository; false otherwise.</returns>
    public bool IsRepository(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(path));
        }

        if (!Directory.Exists(path))
        {
            throw new ArgumentException($"Path not found: {path}");
        }

        ProcessResult result = this.processManager.Run(@"git", @"rev-parse", path);
        return result.ExitCode == 0;
    }

    /// <summary>
    /// Gets whether the specified file or directory is tracked by Git.
    /// </summary>
    /// <param name="path">The path to check.</param>
    /// <returns>True if the path is tracked by Git; false otherwise.</returns>
    public bool IsTracked(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(path));
        }

        if (!File.Exists(path)
            && !Directory.Exists(path))
        {
            throw new ArgumentException($"Path not found: {path}");
        }

        ProcessResult result = this.processManager.Run(@"git", $"ls-files --error-unmatch {path}");
        return result.ExitCode == 0;
    }
}
