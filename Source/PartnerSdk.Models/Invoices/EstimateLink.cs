// -----------------------------------------------------------------------
// <copyright file="EstimateLink.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Invoices
{
    /// <summary>
    /// EstimateLink represents a URI and the HTTP method which indicates the desired action for accessing the resource.
    /// </summary>
    public class EstimateLink
    {
        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>        
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Period.
        /// </summary>
        public string Period { get; set; }

        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        public Link Link { get; set; }
    }
}