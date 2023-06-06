// -----------------------------------------------------------------------
// <copyright file="CustomerSubscribedSkuCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.SubscribedSkus
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models;
    using Models.JsonConverters;
    using Models.Licenses;
    using Network;

    /// <summary>
    /// Customer subscribed products collection operations class.
    /// </summary>
    internal class CustomerSubscribedSkuCollectionOperations : BasePartnerComponent, ICustomerSubscribedSkuCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerSubscribedSkuCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer tenant Id.</param>
        public CustomerSubscribedSkuCollectionOperations(IPartner rootPartnerOperations, string customerId) : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }
        }

        /// <summary>
        /// Retrieves all the customer subscribed products.
        /// </summary>
        /// <param name="licenseGroupIds"> License group id.</param>
        /// <returns>All the customer subscribed products.</returns>
        public ResourceCollection<SubscribedSku> Get(List<LicenseGroupId> licenseGroupIds = null)
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<SubscribedSku>>(() => this.GetAsync(licenseGroupIds));
        }

        /// <summary>
        /// Asynchronously retrieves all the customer subscribed products.
        /// </summary>
        /// <param name="licenseGroupIds"> License group id.</param>
        /// <returns>All the customer subscribed products.</returns>
        public async Task<ResourceCollection<SubscribedSku>> GetAsync(List<LicenseGroupId> licenseGroupIds = null)
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<SubscribedSku, ResourceCollection<SubscribedSku>>(this.Partner, string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCustomerSubscribedSkus.Path, this.Context), jsonConverter: new ResourceCollectionConverter<SubscribedSku>());

            if (licenseGroupIds != null)
            {
                if (licenseGroupIds.Contains(LicenseGroupId.Group1))
                {
                    // Get subscribed skus from license group id Group1.
                    partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                                PartnerService.Instance.Configuration.Apis.GetCustomerSubscribedSkus.Parameters.licenseGroupIds, LicenseGroupId.Group1.ToString()));
                }

                if (licenseGroupIds.Contains(LicenseGroupId.Group2))
                {
                    // Get subscribed skus from license group id Group2.
                    partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                                PartnerService.Instance.Configuration.Apis.GetCustomerSubscribedSkus.Parameters.licenseGroupIds, LicenseGroupId.Group2.ToString()));
                }
            }

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
