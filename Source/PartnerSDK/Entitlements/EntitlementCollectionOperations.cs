// -----------------------------------------------------------------------
// <copyright file="EntitlementCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Entitlements
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Entitlements;
    using Microsoft.Store.PartnerCenter.Network;

    /// <summary>
    /// Entitlement collection operations implementation class.
    /// </summary>
    internal class EntitlementCollectionOperations : BasePartnerComponent, IEntitlementCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntitlementCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        public EntitlementCollectionOperations(IPartner rootPartnerOperations, string customerId) :
            base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set");
            }
        }

        /// <inheritdoc/>
        public IEntitlementCollectionByEntitlementType ByEntitlementType(string entitlementType)
        {
            return new EntitlementCollectionByEntitlementTypeOperations(this.Partner,  this.Context, entitlementType);
        }

        /// <inheritdoc/>
        public ResourceCollection<Entitlement> Get()
        {
            return PartnerService.Instance.SynchronousExecute(this.GetAsync);
        }

        /// <inheritdoc/>
        public async Task<ResourceCollection<Entitlement>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<Entitlement, ResourceCollection<Entitlement>>(
                                            this.Partner,
                                            string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetEntitlements.Path, this.Context));

            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Retrieves the entitlements for a customer.
        /// </summary>
        /// <param name="showExpiry">A flag to indicate if the expiry date is required to be returned along with the entitlement (if applicable).</param>
        /// <returns>The collection of entitlements for the customer.</returns>
        public ResourceCollection<Entitlement> Get(bool showExpiry)
        {
            return PartnerService.Instance.SynchronousExecute(() => this.GetAsync(showExpiry));
        }

        /// <summary>
        /// An asynchronous operation to retrieve the entitlements for a customer.
        /// </summary>
        /// <param name="showExpiry">A flag to indicate if the expiry date is required to be returned along with the entitlement (if applicable).</param>
        /// <returns>The collection of entitlements for the customer.</returns>
        public async Task<ResourceCollection<Entitlement>> GetAsync(bool showExpiry)
        {
            var partnerServiceProxy = new PartnerServiceProxy<Entitlement, ResourceCollection<Entitlement>>(
                                            this.Partner,
                                            string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetEntitlements.Path, this.Context));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetEntitlements.Parameters.ShowExpiry, showExpiry.ToString()));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
