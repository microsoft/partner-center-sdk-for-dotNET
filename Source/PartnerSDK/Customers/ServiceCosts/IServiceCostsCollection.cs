// -----------------------------------------------------------------------
// <copyright file="IServiceCostsCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers.ServiceCosts
{
    using System;

    /// <summary>
    ///  Holds the operations related to a customer's service costs.
    /// </summary>
    public interface IServiceCostsCollection : IPartnerComponent<Tuple<string, string>>
    {
        /// <summary>
        /// Gets the customer's service cost summary.
        /// </summary>
        IServiceCostSummary Summary { get; }

        /// <summary>
        /// Gets the customer's service cost line items.
        /// </summary>
        IServiceCostLineItemsCollection LineItems { get; }
    }
}
