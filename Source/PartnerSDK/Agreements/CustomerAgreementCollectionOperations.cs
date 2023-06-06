// -----------------------------------------------------------------------
// <copyright file="CustomerAgreementCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Agreements
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models;
    using Models.Agreements;
    using Network;
    using Utilities;

    /// <summary>
    /// Customer's agreement collection operations implementations class.
    /// </summary>
    internal class CustomerAgreementCollectionOperations : BasePartnerComponent, ICustomerAgreementCollection
    {
        /// <summary>
        /// The agreement type to filter the result set on.
        /// </summary>
        private readonly string agreementTypeFilter;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerAgreementCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance. </param>
        /// <param name="customerId">The customer tenant Id.</param>
        /// <param name="agreementType">(Optional) The type of the agreement to filter on.</param>
        public CustomerAgreementCollectionOperations(IPartner rootPartnerOperations, string customerId, string agreementType = null) : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }

            this.agreementTypeFilter = agreementType;
        }

        /// <inheritdoc/>
        public ResourceCollection<Agreement> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Agreement>>(() => this.GetAsync());
        }

        /// <inheritdoc/>
        public async Task<ResourceCollection<Agreement>> GetAsync()
        {
            var resourcePath = string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCustomerAgreements.Path, this.Context);

            var partnerApiServiceProxy = new PartnerServiceProxy<Agreement, ResourceCollection<Agreement>>(this.Partner, resourcePath);
            if (!string.IsNullOrWhiteSpace(this.agreementTypeFilter))
            {
                partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>("agreementType", this.agreementTypeFilter));
            }

            return await partnerApiServiceProxy.GetAsync();
        }

        /// <inheritdoc/>
        public Agreement Create(Agreement customerAgreement)
        {
            return PartnerService.Instance.SynchronousExecute<Agreement>(() => this.CreateAsync(customerAgreement));
        }

        /// <inheritdoc/>
        public async Task<Agreement> CreateAsync(Agreement customerAgreement)
        {
            ParameterValidator.Required(customerAgreement, "customerAgreement can't be null");

            var partnerApiServiceProxy = new PartnerServiceProxy<Agreement, Agreement>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCustomerAgreements.Path, this.Context));

            return await partnerApiServiceProxy.PostAsync(customerAgreement);
        }

        /// <inheritdoc/>
        public ICustomerAgreementCollection ByAgreementType(string agreementType)
        {
            ParameterValidator.Required(agreementType, $"{nameof(agreementType)} is required.");

            return new CustomerAgreementCollectionOperations(this.Partner, this.Context, agreementType);
        }

        /// <inheritdoc/>
        public DirectSignedCustomerAgreementStatus GetDirectSignedCustomerAgreementStatus()
        {
            return PartnerService.Instance.SynchronousExecute<DirectSignedCustomerAgreementStatus>(() => this.GetDirectSignedCustomerAgreementStatusAsync());
        }

        /// <inheritdoc/>
        public async Task<DirectSignedCustomerAgreementStatus> GetDirectSignedCustomerAgreementStatusAsync()
        {
            var resourcePath = string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetDirectSignedCustomerAgreementStatus.Path, this.Context);

            var partnerApiServiceProxy = new PartnerServiceProxy<DirectSignedCustomerAgreementStatus, DirectSignedCustomerAgreementStatus>(this.Partner, resourcePath);

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
