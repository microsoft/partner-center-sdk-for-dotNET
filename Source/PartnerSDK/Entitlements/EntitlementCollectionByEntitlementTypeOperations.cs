// -----------------------------------------------------------------------
// <copyright file="EntitlementCollectionByEntitlementTypeOperations.cs" company="Microsoft Corporation">
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
    using Microsoft.Store.PartnerCenter.Utilities;

    /// <summary>
    /// Entitlement collection by entitlement type operations class.
    /// </summary>
    internal class EntitlementCollectionByEntitlementTypeOperations : BasePartnerComponent<Tuple<string, string>>, IEntitlementCollectionByEntitlementType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntitlementCollectionByEntitlementTypeOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer id.</param>
        /// <param name="entitlementType">The entitlement type.</param>
        public EntitlementCollectionByEntitlementTypeOperations(IPartner rootPartnerOperations, string customerId, string entitlementType) :
            base(rootPartnerOperations, new Tuple<string, string>(customerId, entitlementType))
        {
            ParameterValidator.Required(customerId, "customer Id must be set.");
            ParameterValidator.Required(entitlementType, "entitlement Type must be set");
        }

        /// <summary>
        /// Retrieves entitlement collection with the given entitlement type.
        /// </summary>
        /// <returns>The collection of entitlements corresponding to a specific entitlement type for the customer.</returns>
        public ResourceCollection<Entitlement> Get()
        {
            return PartnerService.Instance.SynchronousExecute(this.GetAsync);
        }

        /// <summary>
        /// Asynchronously retrieves entitlement collection with the given entitlement type.
        /// </summary>
        /// <returns>The collection of entitlements corresponding to a specific entitlement type for the customer.</returns>
        public async Task<ResourceCollection<Entitlement>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<Entitlement, ResourceCollection<Entitlement>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetEntitlements.Path, this.Context.Item1));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetEntitlements.Parameters.EntitlementType, this.Context.Item2));

            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Retrieves entitlement collection with the given entitlement type.
        /// </summary>
        /// <param name="showExpiry">A flag to indicate if the expiry date is required to be returned along with the entitlement (if applicable).</param>
        /// <returns>The collection of entitlements corresponding to a specific entitlement type for the customer.</returns>
        public ResourceCollection<Entitlement> Get(bool showExpiry)
        {
            return PartnerService.Instance.SynchronousExecute(() => this.GetAsync(showExpiry));
        }

        /// <summary>
        /// Asynchronously retrieves entitlement collection with the given entitlement type.
        /// </summary>
        /// <param name="showExpiry">A flag to indicate if the expiry date is required to be returned along with the entitlement (if applicable).</param>
        /// <returns>The collection of entitlements corresponding to a specific entitlement type for the customer.</returns>
        public async Task<ResourceCollection<Entitlement>> GetAsync(bool showExpiry)
        {
            var partnerServiceProxy = new PartnerServiceProxy<Entitlement, ResourceCollection<Entitlement>>(
                                            this.Partner,
                                            string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetEntitlements.Path, this.Context.Item1));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetEntitlements.Parameters.EntitlementType, this.Context.Item2));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetEntitlements.Parameters.ShowExpiry, showExpiry.ToString()));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
