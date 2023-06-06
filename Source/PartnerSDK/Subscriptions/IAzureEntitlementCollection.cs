// <copyright file="IAzureEntitlementCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Subscriptions
{
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.GenericOperations;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Subscriptions;

    /// <summary>
    /// This interface defines the Azure entitlement collection behavior.
    /// </summary>
    public interface IAzureEntitlementCollection : IEntireEntityCollectionRetrievalOperations<AzureEntitlement, ResourceCollection<AzureEntitlement>>, IEntitySelector<IAzureEntitlement>
    {
        /// <summary>
        /// Gets the behavior for an entity using the entity's ID.
        /// </summary>
        /// <param name="azureEntitlementId">The azure entitlement identifier.</param>
        /// <returns>The Azure entitlement operation.</returns>
        new IAzureEntitlement this[string azureEntitlementId] { get; }

        /// <summary>
        /// Retrieves the behavior for an entity using the entity's ID.
        /// </summary>
        /// <param name="azureEntitlementId">The Azure entitlement Id.</param>
        /// <returns>The Azure entitlement operation.</returns>
        new IAzureEntitlement ById(string azureEntitlementId);

        /// <summary>
        /// Gets the specified azure entitlement identifier.
        /// </summary>
        /// <returns>Azure entitlements.</returns>
        new ResourceCollection<AzureEntitlement> Get();

        /// <summary>
        /// Asynchronously gets the specified azure entitlement identifier.
        /// </summary>
        /// <returns>Azure entitlements.</returns>
        new Task<ResourceCollection<AzureEntitlement>> GetAsync();
    }
}
