// -----------------------------------------------------------------------
// <copyright file="AzureResource.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Utilizations
{
    /// <summary>
    /// Represents an Azure resource being metered.
    /// </summary>
    public class AzureResource
    {
        /// <summary>
        /// Gets or sets the unique identifier of the Azure resource that was consumed. Also known as resourceID or resourceGUID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the friendly name of the Azure resource being consumed.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the category of the consumed Azure resource.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the sub-category of the consumed Azure resource.
        /// </summary>
        public string Subcategory { get; set; }

        /// <summary>
        /// Gets or sets the region of the consumed Azure resource.
        /// </summary>
        public string Region { get; set; }
    }
}
