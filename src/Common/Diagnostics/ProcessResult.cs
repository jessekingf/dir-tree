// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace Common.Diagnostics;

using System.Collections.Generic;

/// <summary>
/// The execution results of a system process.
/// </summary>
public class ProcessResult
{
    /// <summary>
    /// Gets or sets the value that the associated process specified when it terminated.
    /// </summary>
    public int ExitCode
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the lines directed to the standard output.
    /// </summary>
    public IList<string> StandardOutput
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the lines directed to the standard error output.
    /// </summary>
    public IList<string> StandardError
    {
        get;
        set;
    }
}
