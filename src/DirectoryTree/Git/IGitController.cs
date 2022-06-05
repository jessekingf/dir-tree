// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace DirectoryTree.Git;

/// <summary>
/// Provides functionality for interacting with Git.
/// </summary>
public interface IGitController
{
    /// <summary>
    /// Gets whether the specified path is a Git repository.
    /// </summary>
    /// <param name="path">The path to check.</param>
    /// <returns>True if the path is a Git repository; false otherwise.</returns>
    bool IsRepository(string path);

    /// <summary>
    /// Gets whether the specified file or directory is tracked by Git.
    /// </summary>
    /// <param name="path">The path to check.</param>
    /// <returns>True if the path is tracked by Git; false otherwise.</returns>
    bool IsTracked(string path);
}
