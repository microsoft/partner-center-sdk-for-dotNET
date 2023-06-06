// -----------------------------------------------------------------------
// <copyright file="CustomerQualificationOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Qualification
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Network;
    using V2Models = Microsoft.Store.PartnerCenter.Models.Customers.V2;

    /// <summary>
    /// This class implements the operations available on a customer's qualification.
    /// </summary>
    internal class CustomerQualificationOperations : BasePartnerComponent, ICustomerQualification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerQualificationOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        public CustomerQualificationOperations(IPartner rootPartnerOperations, string customerId) : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }
        }

        /// <summary>
        /// Gets the customer qualification in the V2 contract.
        /// </summary>
        /// <returns>The customer qualification in the V2 contract.</returns>
        public IEnumerable<V2Models.CustomerQualification> GetQualifications()
        {
            return PartnerService.Instance.SynchronousExecute<IEnumerable<V2Models.CustomerQualification>>(() => this.GetQualificationsAsync());
        }

        /// <summary>
        /// Asynchronously gets the customer qualification in the V2 contract.
        /// </summary>
        /// <returns>The customer qualification in the V2 contract.</returns>
        public async Task<IEnumerable<V2Models.CustomerQualification>> GetQualificationsAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<IEnumerable<V2Models.CustomerQualification>, IEnumerable<V2Models.CustomerQualification>>(
               this.Partner,
               string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCustomerQualifications.Path, this.Context));

            return await partnerApiServiceProxy.GetAsync();
        }

        /// <summary>
        /// Updates the customer qualification using the asynchronous API. Use for GovernmentCommunityCloud with validation code after successful registration through Microsoft.
        /// </summary>
        /// <param name="customerQualificationRequest">Customer qualification to be created.</param>
        /// <returns>The updated customer qualification in the V2 contract.</returns>
        public V2Models.CustomerQualification CreateQualifications(V2Models.CustomerQualificationRequest customerQualificationRequest)
        {
            return PartnerService.Instance.SynchronousExecute<V2Models.CustomerQualification>(() => this.CreateQualificationsAsync(customerQualificationRequest));
        }

        /// <summary>
        /// Asynchronously updates the customer qualification.  Use for GovernmentCommunityCloud with validation code after successful registration through Microsoft.
        /// </summary>
        /// <param name="customerQualificationRequest">Customer qualification to be created.</param>
        /// <returns>The updated customer qualification.</returns>
        public async Task<V2Models.CustomerQualification> CreateQualificationsAsync(V2Models.CustomerQualificationRequest customerQualificationRequest)
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<V2Models.CustomerQualificationRequest, V2Models.CustomerQualification>(
                                            this.Partner,
                                            string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.CreateCustomerQualifications.Path, this.Context));

            return await partnerApiServiceProxy.PostAsync(customerQualificationRequest);
        }
    }
}
