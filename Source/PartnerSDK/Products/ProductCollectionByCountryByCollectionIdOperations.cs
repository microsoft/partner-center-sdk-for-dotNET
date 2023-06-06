// -----------------------------------------------------------------------
// <copyright file="ProductCollectionByCountryByCollectionIdOperations.cs" company="Microsoft Corporation">
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
    /// Product operations by country and by collection id implementation class.
    /// </summary>
    internal class ProductCollectionByCountryByCollectionIdOperations : BasePartnerComponent<Tuple<string, string>>, IProductCollectionByCountryByCollectionId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCollectionByCountryByCollectionIdOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="collectionId">The collection ID which contains the products.</param>
        /// <param name="country">The country on which to base the products.</param>
        public ProductCollectionByCountryByCollectionIdOperations(IPartner rootPartnerOperations, string collectionId, string country) :
            base(rootPartnerOperations, new Tuple<string, string>(collectionId, country))
        {
            ParameterValidator.Required(collectionId, "collectionId must be set");
            ParameterValidator.ValidateCountryCode(country);
        }
        
        /// <inheritdoc/>
        public IProductCollectionByCountryByCollectionIdByTargetSegment ByTargetSegment(string targetSegment)
        {
            return new ProductCollectionByCountryByCollectionIdByTargetSegmentOperations(this.Partner, this.Context.Item1, this.Context.Item2, targetSegment);
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
                PartnerService.Instance.Configuration.Apis.GetProducts.Parameters.CollectionId, this.Context.Item1));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetProducts.Parameters.Country, this.Context.Item2));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
