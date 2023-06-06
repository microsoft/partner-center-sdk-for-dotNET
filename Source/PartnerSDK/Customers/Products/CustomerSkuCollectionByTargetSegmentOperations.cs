// -----------------------------------------------------------------------
// <copyright file="CustomerSkuCollectionByTargetSegmentOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers.Products
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models;
    using Models.Products;
    using Network;
    using PartnerCenter.Products;
    using Utilities;

    /// <summary>
    /// Implementation of customer sku collection operations by target segment.
    /// </summary>
    internal class CustomerSkuCollectionByTargetSegmentOperations : BasePartnerComponent<Tuple<string, string, string>>, ISkuCollectionByTargetSegment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerSkuCollectionByTargetSegmentOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer id for which to retrieve the skus.</param>
        /// <param name="productId">The product id for which to retrieve its skus.</param>
        /// <param name="targetSegment">The target segment used for filtering the skus.</param>
        public CustomerSkuCollectionByTargetSegmentOperations(IPartner rootPartnerOperations, string customerId, string productId, string targetSegment) :
            base(rootPartnerOperations, new Tuple<string, string, string>(customerId, productId, targetSegment))
        {
            ParameterValidator.Required(customerId, "customerId must be set");
            ParameterValidator.Required(productId, "productId must be set");
            ParameterValidator.Required(targetSegment, "targetSegment must be set");
        }

        /// <inheritdoc/>
        public ResourceCollection<Sku> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Sku>>(() => this.GetAsync());
        }

        /// <inheritdoc/>
        public async Task<ResourceCollection<Sku>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<Sku, ResourceCollection<Sku>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCustomerSkus.Path, this.Context.Item1, this.Context.Item2));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetCustomerSkus.Parameters.TargetSegment, this.Context.Item3));

            return await partnerServiceProxy.GetAsync();
        }

        /// <inheritdoc/>
        public ISkuCollectionByTargetSegmentByReservationScope ByReservationScope(string reservationScope)
        {
            return new CustomerSkuCollectionByTargetSegmentByReservationScopeOperations(this.Partner, this.Context.Item1, this.Context.Item2, this.Context.Item3, reservationScope);
        }
    }
}
