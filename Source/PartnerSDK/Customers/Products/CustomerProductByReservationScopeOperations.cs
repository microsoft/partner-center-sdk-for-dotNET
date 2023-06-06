// -----------------------------------------------------------------------
// <copyright file="CustomerProductByReservationScopeOperations.cs" company="Microsoft Corporation">
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
    internal class CustomerProductByReservationScopeOperations : BasePartnerComponent<Tuple<string, string, string>>, ICustomerProductByReservationScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerProductByReservationScopeOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="productId">The product Id.</param>
        /// <param name="reservationScope">The reservation scope on which to base the product.</param>
        public CustomerProductByReservationScopeOperations(IPartner rootPartnerOperations, string customerId, string productId, string reservationScope) :
            base(rootPartnerOperations, new Tuple<string, string, string>(customerId, productId, reservationScope))
        {
            ParameterValidator.Required(customerId, "customerId has to be set.");
            ParameterValidator.Required(productId, "productId has to be set.");
            ParameterValidator.Required(reservationScope, "reservationScope has to be set.");
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

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetCustomerProduct.Parameters.ReservationScope, this.Context.Item3));

            return await partnerServiceProxy.GetAsync();
        }
    }
}