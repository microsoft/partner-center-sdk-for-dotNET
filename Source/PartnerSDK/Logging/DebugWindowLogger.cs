// -----------------------------------------------------------------------
// <copyright file="DebugWindowLogger.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Logging
{
    using System.Diagnostics;

    /// <summary>
    /// A logger that writes to the debug window to aid in debugging.
    /// </summary>
    internal class DebugWindowLogger : ILogger
    {
        /// <summary>
        /// Logs a piece of information.
        /// </summary>
        /// <param name="message">The informational message.</param>
        public void Information(string message)
        {
            Debug.WriteLine("INFO: " + message);
        }

        /// <summary>
        /// Logs a warning.
        /// </summary>
        /// <param name="message">The warning message.</param>
        public void Warning(string message)
        {
            Debug.WriteLine("WARNING: " + message);
        }

        /// <summary>
        /// Logs an error.
        /// </summary>
        /// <param name="message">The error message.</param>
        public void Error(string message)
        {
            Debug.WriteLine("ERROR: " + message);
        }
    }
}
