// -----------------------------------------------------------------------
// <copyright file="CustomerUserLicenseCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.CustomerUsers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models.Licenses;
    using Network;
    using PartnerCenter.Models;
    using PartnerCenter.Models.JsonConverters;

    /// <summary>
    /// Customer user license collection operations class.
    /// </summary>
    internal class CustomerUserLicenseCollectionOperations : BasePartnerComponent<Tuple<string, string>>, ICustomerUserLicenseCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerUserLicenseCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="userId">The user Id or UPN depending upon the method which is consuming it.</param>
        public CustomerUserLicenseCollectionOperations(IPartner rootPartnerOperations, string customerId, string userId)
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, userId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("userId must be set.");
            }
        }

        /// <summary>
        /// Retrieves the customer user all licenses.
        /// </summary>
        /// <param name="licenseGroupIds"> License group id.</param>
        /// <returns>The customer user all licenses.</returns>
        public ResourceCollection<License> Get(List<LicenseGroupId> licenseGroupIds = null)
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<License>>(() => this.GetAsync(licenseGroupIds));
        }

        /// <summary>
        /// Asynchronously retrieves the customer user all licenses.
        /// </summary>
        /// <param name="licenseGroupIds"> License group id.</param>
        /// <returns>The customer user all licenses.</returns>
        public async Task<ResourceCollection<License>> GetAsync(List<LicenseGroupId> licenseGroupIds = null)
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<License, ResourceCollection<License>>(this.Partner, string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCustomerUserAssignedLicenses.Path, this.Context.Item1, this.Context.Item2), jsonConverter: new ResourceCollectionConverter<License>());

            if (licenseGroupIds != null)
            {
                if (licenseGroupIds.Contains(LicenseGroupId.Group1))
                {
                    // Get Group1 licenses
                    partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                                PartnerService.Instance.Configuration.Apis.GetCustomerUserAssignedLicenses.Parameters.licenseGroupIds, LicenseGroupId.Group1.ToString()));
                }

                if (licenseGroupIds.Contains(LicenseGroupId.Group2))
                {
                    // Get Group2 licenses
                    partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                                PartnerService.Instance.Configuration.Apis.GetCustomerUserAssignedLicenses.Parameters.licenseGroupIds, LicenseGroupId.Group2.ToString()));
                }
            }

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}