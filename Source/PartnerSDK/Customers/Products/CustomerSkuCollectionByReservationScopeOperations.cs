﻿// -----------------------------------------------------------------------
// <copyright file="CustomerSkuCollectionByReservationScopeOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers.Products
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models;
    using Models.Products;
    using Network;
    using PartnerCenter.Products;
    using Utilities;

    /// <summary>
    /// Implementation of customer sku collection operations by reservation scope.
    /// </summary>
    internal class CustomerSkuCollectionByReservationScopeOperations : BasePartnerComponent<Tuple<string, string, string>>, ISkuCollectionByReservationScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerSkuCollectionByReservationScopeOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer id for which to retrieve the skus.</param>
        /// <param name="productId">The product id for which to retrieve its skus.</param>
        /// <param name="reservationScope">The reservation scope used for filtering the skus.</param>
        public CustomerSkuCollectionByReservationScopeOperations(IPartner rootPartnerOperations, string customerId, string productId, string reservationScope) :
            base(rootPartnerOperations, new Tuple<string, string, string>(customerId, productId, reservationScope))
        {
            ParameterValidator.Required(customerId, "customerId must be set");
            ParameterValidator.Required(productId, "productId must be set");
            ParameterValidator.Required(reservationScope, "reservationScope must be set");
        }

        /// <inheritdoc/>
        public ResourceCollection<Sku> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Sku>>(() => this.GetAsync());
        }

        /// <inheritdoc/>
        public async Task<ResourceCollection<Sku>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<Sku, ResourceCollection<Sku>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCustomerSkus.Path, this.Context.Item1, this.Context.Item2));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetCustomerSkus.Parameters.ReservationScope, this.Context.Item3));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
