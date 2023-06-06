// -----------------------------------------------------------------------
// <copyright file="CustomerAvailabilityOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers.Products
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models.Products;
    using Network;
    using PartnerCenter.Products;
    using Utilities;

    /// <summary>
    /// Single customer availability operations implementation.
    /// </summary>
    internal class CustomerAvailabilityOperations : BasePartnerComponent<Tuple<string, string, string, string>>, IAvailability
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerAvailabilityOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The corresponding customer id.</param>
        /// <param name="productId">The corresponding product id.</param>
        /// <param name="skuId">The corresponding sku id.</param>
        /// <param name="availabilityId">The availability id.</param>
        public CustomerAvailabilityOperations(IPartner rootPartnerOperations, string customerId, string productId, string skuId, string availabilityId) :
            base(rootPartnerOperations, new Tuple<string, string, string, string>(customerId, productId, skuId, availabilityId))
        {
            ParameterValidator.Required(customerId, "customerId has to be set.");
            ParameterValidator.Required(productId, "productId has to be set.");
            ParameterValidator.Required(skuId, "skuId has to be set.");
            ParameterValidator.Required(availabilityId, "availabilityId has to be set.");
        }

        /// <inheritdoc/>
        public Availability Get()
        {
            return PartnerService.Instance.SynchronousExecute<Availability>(() => this.GetAsync());
        }

        /// <inheritdoc/>
        public async Task<Availability> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<Availability, Availability>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCustomerAvailability.Path, this.Context.Item1, this.Context.Item2, this.Context.Item3, this.Context.Item4));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
