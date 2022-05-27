// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace Common.Diagnostics;

using System;
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// Handles starting system processes.
/// </summary>
/// <seealso cref="Common.Diagnostics.IProcessManager" />
public class ProcessManager : IProcessManager
{
    /// <summary>
    /// Starts a process resource and waits for it to exit.
    /// </summary>
    /// <param name="fileName">The name of the application or document to start.</param>
    /// <param name="arguments">The set of command-line arguments to use when starting the application.</param>
    /// <param name="workingDirectory">The working directory for the process to be started.</param>
    /// <returns>The result of the process execution.</returns>
    public ProcessResult Run(string fileName, string arguments, string workingDirectory = null)
    {
        if (fileName == null)
        {
            throw new ArgumentNullException(nameof(fileName));
        }

        if (arguments == null)
        {
            throw new ArgumentNullException(nameof(arguments));
        }

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = fileName,
            Arguments = arguments,
            WorkingDirectory = workingDirectory,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        return this.Run(startInfo);
    }

    /// <summary>
    /// Starts a process resource and waits for it to exit.
    /// </summary>
    /// <param name="startInfo">The information used to start the process.</param>
    /// <returns>The result of the process execution.</returns>
    private ProcessResult Run(ProcessStartInfo startInfo)
    {
        using Process process = new Process()
        {
            StartInfo = startInfo,
        };

        IList<string> standardOutput = new List<string>();
        process.OutputDataReceived += (s, e) =>
        {
            if (e?.Data != null)
            {
                standardOutput.Add(e.Data);
            }
        };

        IList<string> standardError = new List<string>();
        process.ErrorDataReceived += (s, e) =>
        {
            if (e?.Data != null)
            {
                standardError.Add(e.Data);
            }
        };

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        process.WaitForExit();

        return new ProcessResult()
        {
            ExitCode = process.ExitCode,
            StandardOutput = standardOutput,
            StandardError = standardError,
        };
    }
}
