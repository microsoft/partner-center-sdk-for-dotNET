﻿// -----------------------------------------------------------------------
// <copyright file="SkuCollectionByTargetSegmentByReservationScopeOperations.cs" company="Microsoft Corporation">
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
    using Models;
    using Models.Products;
    using Network;
    using Utilities;

    /// <summary>
    /// Sku Collection operations implementation.
    /// </summary>
    internal class SkuCollectionByTargetSegmentByReservationScopeOperations : BasePartnerComponent<Tuple<string, string, string, string>>, ISkuCollectionByTargetSegmentByReservationScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SkuCollectionByTargetSegmentByReservationScopeOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="productId">The product id.</param>
        /// <param name="country">The country on which to base the product.</param>
        /// <param name="targetSegment">The target segment used for filtering the skus.</param>
        /// <param name="reservationScope">The reservation scope used for filtering the skus.</param>
        public SkuCollectionByTargetSegmentByReservationScopeOperations(IPartner rootPartnerOperations, string productId, string country, string targetSegment, string reservationScope) :
            base(rootPartnerOperations, new Tuple<string, string, string, string>(productId, country, targetSegment, reservationScope))
        {
            ParameterValidator.Required(productId, "productId must be set");
            ParameterValidator.ValidateCountryCode(country);
            ParameterValidator.Required(targetSegment, "targetSegment must be set");
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
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetSkus.Path, this.Context.Item1));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetSkus.Parameters.Country, this.Context.Item2));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetSkus.Parameters.TargetSegment, this.Context.Item3));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetSkus.Parameters.ReservationScope, this.Context.Item4));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
