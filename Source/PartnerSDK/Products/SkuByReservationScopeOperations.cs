// -----------------------------------------------------------------------
// <copyright file="SkuByReservationScopeOperations.cs" company="Microsoft Corporation">
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
    using Models.Products;
    using Network;
    using Utilities;

    /// <summary>
    /// Single product by reservation scope operations implementation.
    /// </summary>
    internal class SkuByReservationScopeOperations : BasePartnerComponent<Tuple<string, string, string, string>>, ISkuByReservationScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SkuByReservationScopeOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="productId">The product Id.</param>
        /// <param name="skuId">The sku Id.</param>
        /// <param name="country">The country on which to base the product.</param>
        /// <param name="reservationScope">The reservation scope on which to base the product.</param>
        public SkuByReservationScopeOperations(IPartner rootPartnerOperations, string productId, string skuId, string country, string reservationScope) :
            base(rootPartnerOperations, new Tuple<string, string, string, string>(productId, skuId, country, reservationScope))
        {
            ParameterValidator.Required(productId, "productId has to be set.");
            ParameterValidator.Required(skuId, "skuId has to be set.");
            ParameterValidator.Required(reservationScope, "reservationScope has to be set.");
            ParameterValidator.ValidateCountryCode(country);
        }

        /// <inheritdoc/>
        public Sku Get()
        {
            return PartnerService.Instance.SynchronousExecute<Sku>(() => this.GetAsync());
        }

        /// <inheritdoc/>
        public async Task<Sku> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<Sku, Sku>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetSku.Path, this.Context.Item1, this.Context.Item2));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetSku.Parameters.Country, this.Context.Item3));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetSku.Parameters.ReservationScope, this.Context.Item4));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
