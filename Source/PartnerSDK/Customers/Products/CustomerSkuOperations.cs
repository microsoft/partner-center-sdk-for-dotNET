// -----------------------------------------------------------------------
// <copyright file="CustomerSkuOperations.cs" company="Microsoft Corporation">
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
    /// Implements operations for a single customer sku.
    /// </summary>
    internal class CustomerSkuOperations : BasePartnerComponent<Tuple<string, string, string>>, ISku
    {
        /// <summary>
        /// The availabilities operations.
        /// </summary>
        private Lazy<IAvailabilityCollection> availabilities;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerSkuOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer id for which to retrieve the sku.</param>
        /// <param name="productId">The product id for which to retrieve its sku.</param>
        /// <param name="skuId">The sku id.</param>
        public CustomerSkuOperations(IPartner rootPartnerOperations, string customerId, string productId, string skuId) :
            base(rootPartnerOperations, new Tuple<string, string, string>(customerId, productId, skuId))
        {
            ParameterValidator.Required(customerId, "customerId must be set");
            ParameterValidator.Required(productId, "productId must be set");
            ParameterValidator.Required(skuId, "skuId must be set");

            this.availabilities = new Lazy<IAvailabilityCollection>(() => new CustomerAvailabilityCollectionOperations(this.Partner, customerId, productId, skuId));
        }

        /// <summary>
        /// TODO: implement availabilities for customer.
        /// </summary>
        public IAvailabilityCollection Availabilities
        {
            get
            {
                return this.availabilities.Value;
            }
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
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCustomerSku.Path, this.Context.Item1, this.Context.Item2, this.Context.Item3));

            return await partnerServiceProxy.GetAsync();
        }

        /// <inheritdoc/>
        public ISkuByReservationScope ByReservationScope(string reservationScope)
        {
            return new SkuByReservationScopeOperations(this.Partner, this.Context.Item1, this.Context.Item2, this.Context.Item3, reservationScope);
        }

        /// <inheritdoc/>
        public ICustomerSkuByReservationScope ByCustomerReservationScope(string reservationScope)
        {
            return new CustomerSkuByReservationScopeOperations(this.Partner, this.Context.Item1, this.Context.Item2, this.Context.Item3, reservationScope);
        }
    }
}
