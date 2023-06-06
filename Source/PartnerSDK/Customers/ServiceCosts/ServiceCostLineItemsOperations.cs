// -----------------------------------------------------------------------
// <copyright file="ServiceCostLineItemsOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers.ServiceCosts
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models;
    using Models.JsonConverters;
    using Models.ServiceCosts;
    using Network;   

    /// <summary>
    /// Represents the behavior of the customer service cost line items as a whole.
    /// </summary>
    internal class ServiceCostLineItemsOperations : BasePartnerComponent<Tuple<string, string>>, IServiceCostLineItemsCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceCostLineItemsOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="context">The context, including customer id and billing period.</param>
        public ServiceCostLineItemsOperations(IPartner rootPartnerOperations, Tuple<string, string> context)
            : base(rootPartnerOperations, context)
        {
            if (context == null)
            {
                throw new ArgumentException("context must be set.");
            }
        }

        /// <summary>
        /// Retrieves a customer's service cost line items.
        /// </summary>
        /// <returns>The service cost line items.</returns>
        public ResourceCollection<ServiceCostLineItem> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<ServiceCostLineItem>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves a customer's service cost line items.
        /// </summary>
        /// <returns>The service cost line items.</returns>
        public async Task<ResourceCollection<ServiceCostLineItem>> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<ServiceCostLineItem, ResourceCollection<ServiceCostLineItem>>(
              this.Partner,
              string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCustomerServiceCostLineItems.Path, this.Context.Item1, this.Context.Item2),
              jsonConverter: new ResourceCollectionConverter<ServiceCostLineItem>());

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
