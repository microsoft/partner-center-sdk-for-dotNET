// -----------------------------------------------------------------------
// <copyright file="IEstimateLink.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    /// <summary>
    /// Represents the operations available to an estimate link.
    /// </summary>
    public interface IEstimateLink : IPartnerComponent
    {
        /// <summary>
        /// Retrieves the operations that can be applied on products from a given country.
        /// </summary>
        /// <param name="currencyCode">The country name.</param>
        /// <returns>The product collection operations by country.</returns>
        IEstimateLinkCollectionByCurrency ByCurrency(string currencyCode);
    }
}