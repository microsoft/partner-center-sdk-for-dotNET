// -----------------------------------------------------------------------
// <copyright file="CustomerSkuByReservationScopeOperations.cs" company="Microsoft Corporation">
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
    /// Single customer's product by reservation scope operations implementation.
    /// </summary>
    internal class CustomerSkuByReservationScopeOperations : BasePartnerComponent<Tuple<string, string, string, string>>, ICustomerSkuByReservationScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerSkuByReservationScopeOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="productId">The product Id.</param>
        /// <param name="skuId">The sku Id.</param>
        /// <param name="reservationScope">The reservation scope on which to base the product.</param>
        public CustomerSkuByReservationScopeOperations(IPartner rootPartnerOperations, string customerId, string productId, string skuId, string reservationScope) :
            base(rootPartnerOperations, new Tuple<string, string, string, string>(customerId, productId, skuId, reservationScope))
        {
            ParameterValidator.Required(customerId, "customerId has to be set.");
            ParameterValidator.Required(productId, "productId has to be set.");
            ParameterValidator.Required(skuId, "skuId has to be set.");
            ParameterValidator.Required(reservationScope, "reservationScope has to be set.");
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

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetCustomerSku.Parameters.ReservationScope, this.Context.Item4));

            return await partnerServiceProxy.GetAsync();
        }
    }
}