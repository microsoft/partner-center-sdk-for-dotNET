// -----------------------------------------------------------------------
// <copyright file="CustomerProductCollectionByTargetViewOperations.cs" company="Microsoft Corporation">
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
    /// Product operations by customer id and by target view implementation class.
    /// </summary>
    internal class CustomerProductCollectionByTargetViewOperations : BasePartnerComponent<Tuple<string, string>>, ICustomerProductCollectionByTargetView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerProductCollectionByTargetViewOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer id for which to retrieve the products.</param>
        /// <param name="targetView">The target view which contains the products.</param>
        public CustomerProductCollectionByTargetViewOperations(IPartner rootPartnerOperations, string customerId, string targetView) :
            base(rootPartnerOperations, new Tuple<string, string>(customerId, targetView))
        {
            ParameterValidator.Required(customerId, "customerId must be set");
            ParameterValidator.Required(targetView, "targetView must be set");
        }
        
        /// <inheritdoc/>
        public ICustomerProductCollectionByTargetViewByTargetSegment ByTargetSegment(string targetSegment)
        {
            return new CustomerProductCollectionByTargetViewByTargetSegmentOperations(this.Partner, this.Context.Item1, this.Context.Item2, targetSegment);
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
                PartnerService.Instance.Configuration.Apis.GetCustomerProducts.Parameters.TargetView, this.Context.Item2));
            
            return await partnerServiceProxy.GetAsync();
        }

        /// <inheritdoc/>
        public ICustomerProductCollectionByTargetViewByReservationScope ByReservationScope(string reservationScope)
        {
            return new CustomerProductCollectionByTargetViewByReservationScopeOperations(this.Partner, this.Context.Item1, this.Context.Item2, reservationScope);
        }
    }
}
