// -----------------------------------------------------------------------
// <copyright file="IRequestContext.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.RequestContext
{
    using System;

    /// <summary>
    /// Bundles context information which is amended to the partner SDK operations.
    /// </summary>
    public interface IRequestContext
    {
        /// <summary>
        /// Gets the request Id. Uniquely identifies the partner service operation.
        /// </summary>
        Guid RequestId { get; }

        /// <summary>
        /// Gets the correlation Id. This Id is used to group logical operations together.
        /// </summary>
        Guid CorrelationId { get; }

        /// <summary>
        /// Gets the Locale.
        /// </summary>
        string Locale { get; }
    }
}
