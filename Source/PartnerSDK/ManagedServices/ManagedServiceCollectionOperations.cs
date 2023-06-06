// -----------------------------------------------------------------------
// <copyright file="ManagedServiceCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.ManagedServices
{
    using System;
    using System.Threading.Tasks;
    using Models;
    using Models.ManagedServices;
    using Network;

    /// <summary>
    /// Implements a customer's managed services operations.
    /// </summary>
    internal class ManagedServiceCollectionOperations : BasePartnerComponent, IManagedServiceCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedServiceCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        public ManagedServiceCollectionOperations(IPartner rootPartnerOperations, string customerId)
            : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set");
            }
        }

        /// <summary>
        /// Gets managed services for a customer.
        /// </summary>
        /// <returns>The customer's managed services.</returns>
        public ResourceCollection<ManagedService> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<ManagedService>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously gets managed services for a customer.
        /// </summary>
        /// <returns>The customer's managed services.</returns>
        public async Task<ResourceCollection<ManagedService>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<ManagedService, ResourceCollection<ManagedService>>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.GetCustomerManagedServices.Path, this.Context));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
