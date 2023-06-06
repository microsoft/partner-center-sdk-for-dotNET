// -----------------------------------------------------------------------
// <copyright file="SkuOperations.cs" company="Microsoft Corporation">
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
    /// Implements a single sku operations.
    /// </summary>
    internal class SkuOperations : BasePartnerComponent<Tuple<string, string, string>>, ISku
    {
        /// <summary>
        /// The availability operations.
        /// </summary>
        private Lazy<IAvailabilityCollection> availabilities;

        /// <summary>
        /// Initializes a new instance of the <see cref="SkuOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="productId">The product id.</param>
        /// <param name="skuId">The sku id.</param>
        /// <param name="country">The country on which to base the sku.</param>
        public SkuOperations(IPartner rootPartnerOperations, string productId, string skuId, string country) :
            base(rootPartnerOperations, new Tuple<string, string, string>(productId, skuId, country))
        {
            ParameterValidator.Required(productId, "productId must be set");
            ParameterValidator.Required(skuId, "skuId must be set");
            ParameterValidator.ValidateCountryCode(country);

            this.availabilities = new Lazy<IAvailabilityCollection>(() => new AvailabilityCollectionOperations(this.Partner, productId, skuId, country));
        }

        /// <inheritdoc/>
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
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetSku.Path, this.Context.Item1, this.Context.Item2));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetSku.Parameters.Country, this.Context.Item3));

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
