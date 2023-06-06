// -----------------------------------------------------------------------
// <copyright file="IRetryableOperation.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Retries
{
    using System;

    /// <summary>
    /// Defines a retryable operation.
    /// </summary>
    /// <typeparam name="T">The return type of the operation.</typeparam>
    internal interface IRetryableOperation<T>
    {
        /// <summary>
        /// Executes the operation with retries.
        /// </summary>
        /// <param name="operation">The operation to execute.</param>
        /// <returns>The operation's result.</returns>
        T Execute(Func<T> operation);
    }
}
