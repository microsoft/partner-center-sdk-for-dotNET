﻿// -----------------------------------------------------------------------
// <copyright file="AzureInstanceData.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Utilizations
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Holds Azure instance level details.
    /// </summary>
    public class AzureInstanceData
    {
        /// <summary>
        /// Gets or sets the fully qualified Azure resource ID, which includes the resource groups and the instance name.
        /// </summary>
        public Uri ResourceUri { get; set; }

        /// <summary>
        /// Gets or sets the region in which the this service was run.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the unique namespace used to identify the resource for Azure Marketplace 3rd party usage.
        /// </summary>
        public string PartNumber { get; set; }

        /// <summary>
        /// Gets or sets the unique namespace used to identify the 3rd party order for Azure Marketplace.
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the the resource tags specified by the user.
        /// </summary>
        public IDictionary<string, string> Tags { get; set; }

        /// <summary>
        /// Gets or sets the the additional info fields.
        /// </summary>
        public IDictionary<string, string> AdditionalInfo { get; set; }
    }
}