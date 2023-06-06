// -----------------------------------------------------------------------
// <copyright file="CustomerRelationshipRequestOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.RelationshipRequests
{
    using System.Threading.Tasks;
    using Models.RelationshipRequests;
    using Network;

    /// <summary>
    /// Customer relationship request implementation class.
    /// </summary>
    internal class CustomerRelationshipRequestOperations : BasePartnerComponent, ICustomerRelationshipRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerRelationshipRequestOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public CustomerRelationshipRequestOperations(IPartner rootPartnerOperations) : base(rootPartnerOperations)
        {
        }

        /// <summary>
        /// Retrieves the customer relationship request which can be used by a customer to establish a relationship with the current partner.
        /// </summary>
        /// <returns>A customer relationship request.</returns>
        public CustomerRelationshipRequest Get()
        {
            return PartnerService.Instance.SynchronousExecute<CustomerRelationshipRequest>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the customer relationship request which can be used by a customer to establish a relationship with the current partner.
        /// </summary>
        /// <returns>A customer relationship request.</returns>
        public async Task<CustomerRelationshipRequest> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<CustomerRelationshipRequest, CustomerRelationshipRequest>(
                this.Partner,
                PartnerService.Instance.Configuration.Apis.GetCustomerRelationshipRequest.Path);

            return await partnerServiceProxy.GetAsync();
        }
    }
}
