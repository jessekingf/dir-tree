// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace Common.Diagnostics;

    /// <summary>
    /// Provides functionality to start system processes.
    /// </summary>
public interface IProcessManager
{
    /// <summary>
    /// Starts a process resource and waits for it to exit.
    /// </summary>
    /// <param name="fileName">The name of the application or document to start.</param>
    /// <param name="arguments">The set of command-line arguments to use when starting the application.</param>
    /// <param name="workingDirectory">The working directory for the process to be started.</param>
    /// <returns>The result of the process execution.</returns>
    ProcessResult Run(string fileName, string arguments, string workingDirectory = null);
}
