// -----------------------------------------------------------------------
// <copyright file="LogManager.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Logging
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Use this class for logging messages. This class supports pluggable loggers.
    /// </summary>
    internal class LogManager : ILogger
    {
        /// <summary>
        /// The singleton log manager instance.
        /// </summary>
        private static readonly Lazy<LogManager> LogManagerSingleton = new Lazy<LogManager>(() => new LogManager());

        /// <summary>
        /// The registered loggers collection.
        /// </summary>
        private readonly IList<ILogger> registeredLoggers = new List<ILogger>();

        /// <summary>
        /// Prevents a default instance of the <see cref="LogManager"/> class from being created.
        /// </summary>
        private LogManager()
        {
        }

        /// <summary>
        /// Gets the singleton log manager instance.
        /// </summary>
        public static LogManager Instance
        {
            get
            {
                return LogManagerSingleton.Value;
            }
        }

        /// <summary>
        /// Gets the registered loggers collection. You can add your custom loggers here.
        /// </summary>
        public IList<ILogger> Loggers
        {
            get
            {
                return this.registeredLoggers;
            }
        }

        /// <summary>
        /// Logs a piece of information.
        /// </summary>
        /// <param name="message">The informational message.</param>
        public void Information(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                foreach (var logger in this.registeredLoggers)
                {
                    logger.Information(message);
                }
            }
        }

        /// <summary>
        /// Logs a warning.
        /// </summary>
        /// <param name="message">The warning message.</param>
        public void Warning(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                foreach (var logger in this.registeredLoggers)
                {
                    logger.Warning(message);
                }
            }
        }

        /// <summary>
        /// Logs an error.
        /// </summary>
        /// <param name="message">The error message.</param>
        public void Error(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                foreach (var logger in this.registeredLoggers)
                {
                    logger.Error(message);
                }
            }
        }
    }
}
