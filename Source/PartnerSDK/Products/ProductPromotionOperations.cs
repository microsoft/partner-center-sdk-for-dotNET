// -----------------------------------------------------------------------
// <copyright file="ProductPromotionOperations.cs" company="Microsoft Corporation">
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
    using Microsoft.Store.PartnerCenter.Models.Products;
    using Microsoft.Store.PartnerCenter.Network;
    using Microsoft.Store.PartnerCenter.Utilities;

    /// <summary>
    /// Single product promotion operations implementation.
    /// </summary>
    internal class ProductPromotionOperations : BasePartnerComponent<Tuple<string, string>>, IProductPromotion
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductPromotionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="productPromotionId">The product promotion Id.</param>
        /// <param name="country">The country on which to base the product promotion.</param>
        public ProductPromotionOperations(IPartner rootPartnerOperations, string productPromotionId, string country)
            : base(rootPartnerOperations, new Tuple<string, string>(productPromotionId, country))
        {
            ParameterValidator.Required(productPromotionId, "productPromotionId has to be set.");
            ParameterValidator.ValidateCountryCode(country);
        }

        /// <inheritdoc/>
        public ProductPromotion Get()
        {
            return PartnerService.Instance.SynchronousExecute(() => this.GetAsync());
        }

        /// <inheritdoc/>
        public async Task<ProductPromotion> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<ProductPromotion, ProductPromotion>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetProductPromotion.Path, this.Context.Item1));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetProductPromotion.Parameters.Country, this.Context.Item2));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
