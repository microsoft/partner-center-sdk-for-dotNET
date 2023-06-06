// -----------------------------------------------------------------------
// <copyright file="CustomerAvailabilityCollectionByTargetSegmentOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers.Products
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using Models.Products;
    using Network;
    using PartnerCenter.Products;
    using Utilities;

    /// <summary>
    /// Implementation of customer availabilities operations by target segment.
    /// </summary>
    internal class CustomerAvailabilityCollectionByTargetSegmentOperations : BasePartnerComponent<Tuple<string, string, string, string>>, IAvailabilityCollectionByTargetSegment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerAvailabilityCollectionByTargetSegmentOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer id for which to retrieve the availabilities.</param>
        /// <param name="productId">The product id for which to retrieve the availabilities.</param>
        /// <param name="skuId">The sku id for which to retrieve its availabilities.</param>
        /// <param name="targetSegment">The target segment used for filtering the availabilities.</param>
        public CustomerAvailabilityCollectionByTargetSegmentOperations(IPartner rootPartnerOperations, string customerId, string productId, string skuId, string targetSegment) :
            base(rootPartnerOperations, new Tuple<string, string, string, string>(customerId, productId, skuId, targetSegment))
        {
            ParameterValidator.Required(customerId, "customerId must be set");
            ParameterValidator.Required(productId, "productId must be set");
            ParameterValidator.Required(skuId, "skuId must be set");
            ParameterValidator.Required(targetSegment, "targetSegment must be set");
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
            
            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetCustomerAvailabilities.Parameters.TargetSegment, this.Context.Item4));

            return await partnerServiceProxy.GetAsync();
        }

        /// <inheritdoc/>
        public IAvailabilityCollectionByTargetSegmentByReservationScopeOperations ByReservationScope(string reservationScope)
        {
            return new CustomerAvailabilityCollectionByTargetSegmentByReservationScopeOperations(this.Partner, this.Context.Item1, this.Context.Item2, this.Context.Item3, this.Context.Item4, reservationScope);
        }
    }
}