// <copyright file="RenewalOption.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Models.Products
{
    /// <summary>
    /// Class that represents a conversion option.
    /// </summary>
    public class RenewalOption
    {
        /// <summary>
        /// Gets or sets the ISO standard representation of this term's duration.
        /// </summary>
        public string TermDuration { get; set; }

        /// <summary>
        /// Gets or sets the renewal option term duration description.
        /// </summary>
        public string Description { get; set; }
    }
}
