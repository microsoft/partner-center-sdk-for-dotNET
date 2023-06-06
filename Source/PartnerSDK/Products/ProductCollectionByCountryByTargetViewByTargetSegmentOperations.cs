// -----------------------------------------------------------------------
// <copyright file="ProductCollectionByCountryByTargetViewByTargetSegmentOperations.cs" company="Microsoft Corporation">
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
    /// Product operations by country, by target view and by target segment implementation class.
    /// </summary>
    internal class ProductCollectionByCountryByTargetViewByTargetSegmentOperations : BasePartnerComponent<Tuple<string, string, string>>, IProductCollectionByCountryByTargetViewByTargetSegment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCollectionByCountryByTargetViewByTargetSegmentOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="targetView">The target view which contains the products.</param>
        /// <param name="country">The country on which to base the products.</param>
        /// <param name="targetSegment">The target segment used for filtering the products.</param>
        public ProductCollectionByCountryByTargetViewByTargetSegmentOperations(IPartner rootPartnerOperations, string targetView, string country, string targetSegment) :
            base(rootPartnerOperations, new Tuple<string, string, string>(targetView, country, targetSegment))
        {
            ParameterValidator.Required(targetView, "targetView must be set");
            ParameterValidator.Required(targetSegment, "targetSegment must be set");
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

            return await partnerServiceProxy.GetAsync();
        }

        /// <inheritdoc/>
        public IProductCollectionByCountryByTargetViewByTargetSegmentByReservationScope ByReservationScope(string reservationScope)
        {
            return new ProductCollectionByCountryByTargetViewByTargetSegmentByReservationScopeOperations(this.Partner, this.Context.Item1, this.Context.Item2, this.Context.Item3, reservationScope);
        }
    }
}
