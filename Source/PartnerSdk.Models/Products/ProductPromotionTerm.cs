// -----------------------------------------------------------------------
// <copyright file="ProductPromotionTerm.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Products
{
    /// <summary>
    /// Class that represents product promotion eligible item term.
    /// </summary>
    public class ProductPromotionTerm
    {
        /// <summary>
        /// Gets or sets the product promotion term's duration in IS08601 format.
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// Gets or sets the product promotion term's billing cycle.
        /// </summary>
        public string BillingCycle { get; set; }
    }
}
