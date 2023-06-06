// -----------------------------------------------------------------------
// <copyright file="OverageCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Subscriptions
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Subscriptions;
    using Microsoft.Store.PartnerCenter.Network;

    /// <summary>
    /// The overage implementation.
    /// </summary>
    internal class OverageCollectionOperations : BasePartnerComponent, IOverageCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OverageCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id to whom the subscriptions belong.</param>
        public OverageCollectionOperations(IPartner rootPartnerOperations, string customerId)
            : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set");
            }
        }

        /// <summary>
        /// Gets the overage.
        /// </summary>
        /// <returns>The overage.</returns>
        public ResourceCollection<Overage> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Overage>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously gets the overage.
        /// </summary>
        /// <returns>The overage.</returns>
        public async Task<ResourceCollection<Overage>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<Overage, ResourceCollection<Overage>>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.GetOverage.Path, this.Context));

            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Creates or updates the overage.
        /// </summary>
        /// <param name="entity">The overage.</param>
        /// <returns>The updated overage.</returns>
        public Overage Put(Overage entity)
        {
            return PartnerService.Instance.SynchronousExecute<Overage>(() => this.PutAsync(entity));
        }

        /// <summary>
        /// Asynchronously creates or updates the overage.
        /// </summary>
        /// <param name="entity">The overage.</param>
        /// <returns>The updated overage.</returns>
        public async Task<Overage> PutAsync(Overage entity)
        {
            var partnerServiceProxy = new PartnerServiceProxy<Overage, Overage>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.GetOverage.Path, this.Context));

            return await partnerServiceProxy.PutAsync(entity);
        }
    }
}
