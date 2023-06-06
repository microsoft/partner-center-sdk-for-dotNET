// -----------------------------------------------------------------------
// <copyright file="ProductByReservationScopeOperations.cs" company="Microsoft Corporation">
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
    internal class ProductByReservationScopeOperations : BasePartnerComponent<Tuple<string, string, string>>, IProductByReservationScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductByReservationScopeOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="productId">The product Id.</param>
        /// <param name="country">The country on which to base the product.</param>
        /// <param name="reservationScope">The reservation scope on which to base the product.</param>
        public ProductByReservationScopeOperations(IPartner rootPartnerOperations, string productId, string country, string reservationScope) :
            base(rootPartnerOperations, new Tuple<string, string, string>(productId, country, reservationScope))
        {
            ParameterValidator.Required(productId, "productId has to be set.");
            ParameterValidator.Required(reservationScope, "reservationScope has to be set.");
            ParameterValidator.ValidateCountryCode(country);
        }

        /// <inheritdoc/>
        public Product Get()
        {
            return PartnerService.Instance.SynchronousExecute<Product>(() => this.GetAsync());
        }

        /// <inheritdoc/>
        public async Task<Product> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<Product, Product>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetProduct.Path, this.Context.Item1));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetProduct.Parameters.Country, this.Context.Item2));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetProduct.Parameters.ReservationScope, this.Context.Item3));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
