// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace DirectoryTree;

/// <summary>
/// The exception thrown when an invalid program option is specified.
/// </summary>
/// <seealso cref="System.Exception" />
public class InvalidOptionException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidOptionException"/> class.
    /// </summary>
    public InvalidOptionException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidOptionException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public InvalidOptionException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidOptionException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The inner exception.</param>
    public InvalidOptionException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
