// -----------------------------------------------------------------------
// <copyright file="ProductPromotionCollectionByCountryBySegmentOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Products
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Products;
    using Microsoft.Store.PartnerCenter.Network;
    using Microsoft.Store.PartnerCenter.Utilities;

    /// <summary>
    /// Product promotion operations by country and by segment implementation class.
    /// </summary>
    internal class ProductPromotionCollectionByCountryBySegmentOperations : BasePartnerComponent<Tuple<string, string>>, IProductPromotionCollectionByCountryBySegment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductPromotionCollectionByCountryBySegmentOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="segment">The segment on which to base the product promotions.</param>
        /// <param name="country">The country on which to base the product promotions.</param>
        public ProductPromotionCollectionByCountryBySegmentOperations(IPartner rootPartnerOperations, string segment, string country)
            : base(rootPartnerOperations, new Tuple<string, string>(segment, country))
        {
            ParameterValidator.Required(segment, "segment must be set");
            ParameterValidator.ValidateCountryCode(country);
        }

        /// <inheritdoc/>
        public ResourceCollection<ProductPromotion> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<ProductPromotion>>(() => this.GetAsync());
        }

        /// <inheritdoc/>
        public async Task<ResourceCollection<ProductPromotion>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<ProductPromotion, ResourceCollection<ProductPromotion>>(
                this.Partner,
                PartnerService.Instance.Configuration.Apis.GetProductPromotions.Path);

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetProductPromotions.Parameters.Segment, this.Context.Item1));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetProductPromotions.Parameters.Country, this.Context.Item2));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
