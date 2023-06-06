// -----------------------------------------------------------------------
// <copyright file="ProductOperations.cs" company="Microsoft Corporation">
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
    using Microsoft.Store.PartnerCenter.Customers.Products;
    using Models.Products;
    using Network;
    using Utilities;

    /// <summary>
    /// Single product operations implementation.
    /// </summary>
    internal class ProductOperations : BasePartnerComponent<Tuple<string, string>>, IProduct
    {
        /// <summary>
        /// The product skus operations.
        /// </summary>
        private readonly Lazy<ISkuCollection> skus;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="productId">The product Id.</param>
        /// <param name="country">The country on which to base the product.</param>
        public ProductOperations(IPartner rootPartnerOperations, string productId, string country) :
            base(rootPartnerOperations, new Tuple<string, string>(productId, country))
        {
            ParameterValidator.Required(productId, "productId has to be set.");
            ParameterValidator.ValidateCountryCode(country);
            
            this.skus = new Lazy<ISkuCollection>(() => new SkuCollectionOperations(this.Partner, this.Context.Item1, this.Context.Item2));
        }

        /// <inheritdoc/>
        public ISkuCollection Skus
        {
            get
            {
                return this.skus.Value;
            }
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

            return await partnerServiceProxy.GetAsync();
        }

        /// <inheritdoc/>
        public IProductByReservationScope ByReservationScope(string reservationScope)
        {
            return new ProductByReservationScopeOperations(this.Partner, this.Context.Item1, this.Context.Item2, reservationScope);
        }

        /// <inheritdoc/>
        public ICustomerProductByReservationScope ByCustomerReservationScope(string reservationScope)
        {
            return new CustomerProductByReservationScopeOperations(this.Partner, this.Context.Item1, this.Context.Item2, reservationScope);
        }
    }
}
