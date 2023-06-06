// -----------------------------------------------------------------------
// <copyright file="CustomerProductCollectionByTargetViewByReservationScopeOperations.cs" company="Microsoft Corporation">
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
    /// Product operations by customer id and by reservation scope implementation class.
    /// </summary>
    internal class CustomerProductCollectionByTargetViewByReservationScopeOperations : BasePartnerComponent<Tuple<string, string, string>>, ICustomerProductCollectionByTargetViewByReservationScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerProductCollectionByTargetViewByReservationScopeOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer id for which to retrieve the products.</param>
        /// <param name="targetView">The target view which contains the products.</param>
        /// <param name="reservationScope">The reservation scope which contains the products.</param>
        public CustomerProductCollectionByTargetViewByReservationScopeOperations(IPartner rootPartnerOperations, string customerId, string targetView, string reservationScope) :
            base(rootPartnerOperations, new Tuple<string, string, string>(customerId, targetView, reservationScope))
        {
            ParameterValidator.Required(customerId, "customerId must be set");
            ParameterValidator.Required(targetView, "targetView must be set");
        }

        /// <inheritdoc/>
        public ICustomerProductCollectionByTargetViewByTargetSegmentByReservationScope ByReservationScope(string reservationScope)
        {
            return new CustomerProductCollectionByTargetViewByTargetSegmentByReservationScopeOperations(this.Partner, this.Context.Item1, this.Context.Item2, this.Context.Item3, reservationScope);
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

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetCustomerProducts.Parameters.ReservationScope, this.Context.Item3));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
