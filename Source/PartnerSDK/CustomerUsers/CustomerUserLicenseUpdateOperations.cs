// -----------------------------------------------------------------------
// <copyright file="CustomerUserLicenseUpdateOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.CustomerUsers
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models.Licenses;
    using Network;

    /// <summary>
    /// Customer user license update collection operations class.
    /// </summary>
    internal class CustomerUserLicenseUpdateOperations : BasePartnerComponent<Tuple<string, string>>, ICustomerUserLicenseUpdates
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerUserLicenseUpdateOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="userId">The user Id or UPN depending upon the method which is consuming it.</param>
        public CustomerUserLicenseUpdateOperations(IPartner rootPartnerOperations, string customerId, string userId)
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
        /// Assign licenses to a user.
        /// This method serves three scenarios:
        /// 1. Add license to a customer user.
        /// 2. Remove license from a customer user.
        /// 3. Update existing license for a customer user.
        /// </summary>
        /// <param name="entity">License update object.</param>
        /// <returns>Returned license update object.</returns>
        public LicenseUpdate Create(LicenseUpdate entity)
        {
            return PartnerService.Instance.SynchronousExecute(() => this.CreateAsync(entity));
        }

        /// <summary>
        /// Asynchronously assign licenses to a user.
        /// This method serves three scenarios:
        /// 1. Add license to a customer user.
        /// 2. Remove license from a customer user.
        /// 3. Update existing license for a customer user.
        /// </summary>
        /// <param name="entity">License update object.</param>
        /// <returns>Returned license update object.</returns>
        public async Task<LicenseUpdate> CreateAsync(LicenseUpdate entity)
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<LicenseUpdate, LicenseUpdate>(this.Partner, string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.SetCustomerUserLicenseUpdates.Path, this.Context.Item1, this.Context.Item2));
            return await partnerApiServiceProxy.PostAsync(entity);
        }
    }
}