// <copyright file="AzureEntitlementCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Subscriptions
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Subscriptions;
    using Microsoft.Store.PartnerCenter.Network;

    /// <summary>
    /// The Azure entitlement operations.
    /// </summary>
    internal class AzureEntitlementCollectionOperations : BasePartnerComponent<Tuple<string, string>>, IAzureEntitlementCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureEntitlementCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="subscriptionId">The subscription identifier.</param>
        public AzureEntitlementCollectionOperations(IPartner rootPartnerOperations, string customerId, string subscriptionId)
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, subscriptionId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentNullException($"'{nameof(customerId)}' cannot be null or whitespace.", nameof(customerId));
            }

            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                throw new ArgumentNullException($"'{nameof(subscriptionId)}' cannot be null or whitespace.", nameof(subscriptionId));
            }
        }

        /// <summary>
        /// Gets the behavior for an entity using the entity's ID.
        /// </summary>
        /// <param name="azureEntitlementId">The azure entitlement identifier.</param>
        /// <returns>The Azure entitlement operations.</returns>
        public IAzureEntitlement this[string azureEntitlementId]
        {
            get
            {
                return this.ById(azureEntitlementId);
            }
        }

        /// <summary>
        /// Retrieves the behavior for an entity using the entity's ID.
        /// </summary>
        /// <param name="azureEntitlementId">The Azure entitlement Id.</param>
        /// <returns>The Azure entitlement operation.</returns>
        public IAzureEntitlement ById(string azureEntitlementId)
        {
            return new AzureEntitlementOpertions(this.Partner, this.Context.Item1, this.Context.Item2, azureEntitlementId);
        }

        /// <summary>
        /// Gets the specified azure entitlement identifier.
        /// </summary>
        /// <returns>
        /// Azure entitlement.
        /// </returns>
        public ResourceCollection<AzureEntitlement> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<AzureEntitlement>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously gets the specified azure entitlement identifier.
        /// </summary>
        /// <returns>
        /// Azure entitlement.
        /// </returns>
        public async Task<ResourceCollection<AzureEntitlement>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<ResourceCollection<AzureEntitlement>, ResourceCollection<AzureEntitlement>>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.GetAzureEntitlements.Path, this.Context.Item1, this.Context.Item2));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
