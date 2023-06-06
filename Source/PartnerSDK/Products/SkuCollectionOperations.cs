// -----------------------------------------------------------------------
// <copyright file="SkuCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Products
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models;
    using Models.Products;
    using Network;
    using Utilities;

    /// <summary>
    /// Sku Collection operations implementation.
    /// </summary>
    internal class SkuCollectionOperations : BasePartnerComponent<Tuple<string, string>>, ISkuCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SkuCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="productId">The product id.</param>
        /// <param name="country">The country on which to base the product.</param>
        public SkuCollectionOperations(IPartner rootPartnerOperations, string productId, string country) :
            base(rootPartnerOperations, new Tuple<string, string>(productId, country))
        {
            ParameterValidator.Required(productId, "productId must be set");
            ParameterValidator.ValidateCountryCode(country);
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
            return new SkuOperations(this.Partner, this.Context.Item1, skuId, this.Context.Item2);
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
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetSkus.Path, this.Context.Item1));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetSkus.Parameters.Country, this.Context.Item2));

            return await partnerServiceProxy.GetAsync();
        }

        /// <inheritdoc/>
        public ISkuCollectionByTargetSegment ByTargetSegment(string targetSegment)
        {
            return new SkuCollectionByTargetSegmentOperations(this.Partner, this.Context.Item1, this.Context.Item2, targetSegment);
        }

        /// <inheritdoc/>
        public ISkuCollectionByReservationScope ByReservationScope(string reservationScope)
        {
            return new SkuCollectionByReservationScopeOperations(this.Partner, this.Context.Item1, this.Context.Item2, reservationScope);
        }
    }
}
