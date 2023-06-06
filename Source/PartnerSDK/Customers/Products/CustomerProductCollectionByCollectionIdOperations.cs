// -----------------------------------------------------------------------
// <copyright file="CustomerProductCollectionByCollectionIdOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers.Products
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using Models.Products;
    using Network;
    using Utilities;

    /// <summary>
    /// Product operations by customer id and by collection id implementation class.
    /// </summary>
    internal class CustomerProductCollectionByCollectionIdOperations : BasePartnerComponent<Tuple<string, string>>, ICustomerProductCollectionByCollectionId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerProductCollectionByCollectionIdOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer id for which to retrieve the products.</param>
        /// <param name="collectionId">The collection ID which contains the products.</param>
        public CustomerProductCollectionByCollectionIdOperations(IPartner rootPartnerOperations, string customerId, string collectionId) :
            base(rootPartnerOperations, new Tuple<string, string>(customerId, collectionId))
        {
            ParameterValidator.Required(customerId, "customerId must be set");
            ParameterValidator.Required(collectionId, "collectionId must be set");
        }
        
        /// <inheritdoc/>
        public ICustomerProductCollectionByCollectionIdByTargetSegment ByTargetSegment(string targetSegment)
        {
            return new CustomerProductCollectionByCollectionIdByTargetSegmentOperations(this.Partner, this.Context.Item1, this.Context.Item2, targetSegment);
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
                string.Format(PartnerService.Instance.Configuration.Apis.GetCustomerProducts.Path, this.Context.Item1));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetCustomerProducts.Parameters.CollectionId, this.Context.Item2));
            
            return await partnerServiceProxy.GetAsync();
        }
    }
}
