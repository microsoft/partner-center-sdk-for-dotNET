// -----------------------------------------------------------------------
// <copyright file="CustomerSkuCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers.Products
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models;
    using Models.Products;
    using Network;
    using PartnerCenter.Products;
    using Utilities;

    /// <summary>
    /// Implementation of customer sku collection operations.
    /// </summary>
    internal class CustomerSkuCollectionOperations : BasePartnerComponent<Tuple<string, string>>, ISkuCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerSkuCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer id for which to retrieve the skus.</param>
        /// <param name="productId">The product id for which to retrieve its skus.</param>
        public CustomerSkuCollectionOperations(IPartner rootPartnerOperations, string customerId, string productId) :
            base(rootPartnerOperations, new Tuple<string, string>(customerId, productId))
        {
            ParameterValidator.Required(customerId, "customerId must be set");
            ParameterValidator.Required(productId, "productId must be set");
        }

        /// <inheritdoc/>
        public ISku this[string skuId]
        {
            get
            {
                return this.ById(skuId);
            }
        }

        /// <inheritdoc/>
        public ISku ById(string skuId)
        {
            return new CustomerSkuOperations(this.Partner, this.Context.Item1, this.Context.Item2, skuId);
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
                
            return await partnerServiceProxy.GetAsync();
        }

        /// <inheritdoc/>
        public ISkuCollectionByTargetSegment ByTargetSegment(string targetSegment)
        {
            return new CustomerSkuCollectionByTargetSegmentOperations(this.Partner, this.Context.Item1, this.Context.Item2, targetSegment);
        }

        /// <inheritdoc/>
        public ISkuCollectionByReservationScope ByReservationScope(string reservationScope)
        {
            return new CustomerSkuCollectionByReservationScopeOperations(this.Partner, this.Context.Item1, this.Context.Item2, reservationScope);
        }
    }
}
