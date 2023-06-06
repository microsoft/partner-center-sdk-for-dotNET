// -----------------------------------------------------------------------
// <copyright file="CustomerAvailabilityCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers.Products
{
    using System;
    using System.Threading.Tasks;
    using Models;
    using Models.Products;
    using Network;
    using PartnerCenter.Products;
    using Utilities;

    /// <summary>
    /// Implementation of customer availabilities operations.
    /// </summary>
    internal class CustomerAvailabilityCollectionOperations : BasePartnerComponent<Tuple<string, string, string>>, IAvailabilityCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerAvailabilityCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer id for which to retrieve the availabilities.</param>
        /// <param name="productId">The product id for which to retrieve the availabilities.</param>
        /// <param name="skuId">The sku id for which to retrieve its availabilities.</param>
        public CustomerAvailabilityCollectionOperations(IPartner rootPartnerOperations, string customerId, string productId, string skuId) :
            base(rootPartnerOperations, new Tuple<string, string, string>(customerId, productId, skuId))
        {
            ParameterValidator.Required(customerId, "customerId must be set");
            ParameterValidator.Required(productId, "productId must be set");
            ParameterValidator.Required(skuId, "skuId must be set");
        }

        /// <inheritdoc/>
        public IAvailability this[string availabilityId]
        {
            get
            {
                return this.ById(availabilityId);
            }
        }

        /// <inheritdoc/>
        public IAvailability ById(string availabilityId)
        {
            return new CustomerAvailabilityOperations(this.Partner, this.Context.Item1, this.Context.Item2, this.Context.Item3, availabilityId);
        }

        /// <inheritdoc/>
        public ResourceCollection<Availability> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Availability>>(() => this.GetAsync());
        }

        /// <inheritdoc/>
        public async Task<ResourceCollection<Availability>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<Availability, ResourceCollection<Availability>>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.GetCustomerAvailabilities.Path, this.Context.Item1, this.Context.Item2, this.Context.Item3));
            
            return await partnerServiceProxy.GetAsync();
        }

        /// <inheritdoc/>
        public IAvailabilityCollectionByTargetSegment ByTargetSegment(string targetSegment)
        {
            return new CustomerAvailabilityCollectionByTargetSegmentOperations(this.Partner, this.Context.Item1, this.Context.Item2, this.Context.Item3, targetSegment);
        }

        /// <inheritdoc/>
        public IAvailabilityCollectionByReservationScopeOperations ByReservationScope(string reservationScope)
        {
            return new CustomerAvailabilityCollectionByReservationScopeOperations(this.Partner, this.Context.Item1, this.Context.Item2, this.Context.Item3, reservationScope);
        }
    }
}