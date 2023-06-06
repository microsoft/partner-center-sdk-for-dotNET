// -----------------------------------------------------------------------
// <copyright file="CustomerProductOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers.Products
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models.Products;
    using Network;
    using PartnerCenter.Products;
    using Utilities;

    /// <summary>
    /// Single product by customer id operations implementation.
    /// </summary>
    internal class CustomerProductOperations : BasePartnerComponent<Tuple<string, string>>, IProduct
    {
        /// <summary>
        /// The product skus operations.
        /// </summary>
        private readonly Lazy<ISkuCollection> skus;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerProductOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer id for which to retrieve the product.</param>
        /// <param name="productId">The product id.</param>
        public CustomerProductOperations(IPartner rootPartnerOperations, string customerId, string productId) :
            base(rootPartnerOperations, new Tuple<string, string>(customerId, productId))
        {
            ParameterValidator.Required(customerId, "customerId has to be set.");
            ParameterValidator.Required(productId, "productId has to be set.");

            this.skus = new Lazy<ISkuCollection>(() => new CustomerSkuCollectionOperations(this.Partner, this.Context.Item1, this.Context.Item2));
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
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCustomerProduct.Path, this.Context.Item1, this.Context.Item2));

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
