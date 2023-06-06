// -----------------------------------------------------------------------
// <copyright file="CustomerRelationshipCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Relationships
{
    using System;
    using System.Threading.Tasks;
    using Models;
    using Models.Relationships;
    using Network;

    /// <summary>
    /// For two-tier partner scenarios, holds the operations for retrieving partner relationships of a customer with respect to
    /// the logged in partner.
    /// </summary>
    internal class CustomerRelationshipCollectionOperations : BasePartnerComponent, ICustomerRelationshipCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerRelationshipCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        public CustomerRelationshipCollectionOperations(IPartner rootPartnerOperations, string customerId)
            : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set");
            }
        }

        /// <summary>
        /// Retrieves all the partners relationships associated to a specific customer.
        /// </summary>
        /// <returns>The partner relationships.</returns>
        public ResourceCollection<PartnerRelationship> Get()
        {
            return PartnerService.Instance.SynchronousExecute(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves all the partners relationships associated to a specific customer.
        /// </summary>
        /// <returns>The partner relationships.</returns>
        public async Task<ResourceCollection<PartnerRelationship>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<PartnerRelationship, ResourceCollection<PartnerRelationship>>(
               this.Partner,
               string.Format(PartnerService.Instance.Configuration.Apis.GetPartnerRelationshipsForCustomer.Path, this.Context));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
