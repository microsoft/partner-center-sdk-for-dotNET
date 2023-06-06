// -----------------------------------------------------------------------
// <copyright file="ProductCollectionByCountryByTargetViewByTargetSegmentByReservationScopeOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Products
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using Models.Products;
    using Network;
    using Utilities;

    /// <summary>
    /// Product operations by country, by collection id and by reservation scope implementation class.
    /// </summary>
    internal class ProductCollectionByCountryByTargetViewByTargetSegmentByReservationScopeOperations : BasePartnerComponent<Tuple<string, string, string, string>>, IProductCollectionByCountryByTargetViewByTargetSegmentByReservationScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCollectionByCountryByTargetViewByTargetSegmentByReservationScopeOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="targetView">The target view which contains the products.</param>
        /// <param name="country">The country on which to base the products.</param>
        /// <param name="targetSegment">The target segment used for filtering the products.</param>
        /// <param name="reservationScope">The reservation scope used for filtering the products.</param>
        public ProductCollectionByCountryByTargetViewByTargetSegmentByReservationScopeOperations(IPartner rootPartnerOperations, string targetView, string country, string targetSegment, string reservationScope) :
            base(rootPartnerOperations, new Tuple<string, string, string, string>(targetView, country, targetSegment, reservationScope))
        {
            ParameterValidator.Required(targetView, "targetView must be set");
            ParameterValidator.Required(targetSegment, "targetSegment must be set");
            ParameterValidator.Required(reservationScope, "reservationScope must be set");
            ParameterValidator.ValidateCountryCode(country);
        }

        /// <inheritdoc/>
        public ResourceCollection<Product> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Product>>(() => this.GetAsync());
        }

        /// <inheritdoc/>
        public async Task<ResourceCollection<Product>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<Product, ResourceCollection<Product>>(
                this.Partner,
                PartnerService.Instance.Configuration.Apis.GetProducts.Path);

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetProducts.Parameters.TargetView, this.Context.Item1));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetProducts.Parameters.Country, this.Context.Item2));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetProducts.Parameters.TargetSegment, this.Context.Item3));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetProducts.Parameters.ReservationScope, this.Context.Item4));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
