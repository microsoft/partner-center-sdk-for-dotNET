// -----------------------------------------------------------------------
// <copyright file="OrderError.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Carts
{
    using Microsoft.Store.PartnerCenter.Models;

    /// <summary>
    /// Gets type of order error
    /// </summary>
    /// <value>The order group Id with failure.</value>
    public class OrderError : ResourceBase
    {
        /// <summary>
        /// Gets or sets the order group Id with failure.
        /// </summary>
        /// <value>The order group Id with failure.</value>
        public string OrderGroupId { get; set; }

        /// <summary>
        /// Gets or sets the error code associated with the issue.
        /// </summary>
        /// <value>The error code associated with the failure.</value>
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the description of the issue.
        /// </summary>
        /// <value>The description of the issue</value>
        public string Description { get; set; }
    }
}
