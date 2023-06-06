// -----------------------------------------------------------------------
// <copyright file="ILogger.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Logging
{
    /// <summary>
    /// Defines logger behavior.
    /// </summary>
    internal interface ILogger
    {
        /// <summary>
        /// Logs a piece of information.
        /// </summary>
        /// <param name="message">The informational message.</param>
        void Information(string message);

        /// <summary>
        /// Logs a warning.
        /// </summary>
        /// <param name="message">The warning message.</param>
        void Warning(string message);

        /// <summary>
        /// Logs an error.
        /// </summary>
        /// <param name="message">The error message.</param>
        void Error(string message);
    }
}