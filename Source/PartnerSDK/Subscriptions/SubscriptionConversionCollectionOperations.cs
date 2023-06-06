// -----------------------------------------------------------------------
// <copyright file="SubscriptionConversionCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Subscriptions
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Microsoft.Store.PartnerCenter.Models.Subscriptions;
    using Microsoft.Store.PartnerCenter.Network;

    /// <summary>
    /// The customer subscription conversion implementation.
    /// </summary>
    internal class SubscriptionConversionCollectionOperations : BasePartnerComponent<Tuple<string, string>>, ISubscriptionConversionCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionConversionCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id to whom the subscriptions belong.</param>
        /// <param name="subscriptionId">The subscription Id where the upgrade is occurring.</param>
        public SubscriptionConversionCollectionOperations(IPartner rootPartnerOperations, string customerId, string subscriptionId)
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, subscriptionId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId should be set.", nameof(customerId));
            }

            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                throw new ArgumentException("subscriptionId should be set.", nameof(subscriptionId));
            }
        }

        /// <summary>
        /// Submits a subscription conversion.
        /// </summary>
        /// <param name="conversion">The new subscription conversion information.</param>
        /// <returns>The subscription conversion results.</returns>       
        public ConversionResult Create(Conversion conversion) => PartnerService.Instance.SynchronousExecute(() => this.CreateAsync(conversion));

        /// <summary>
        /// Asynchronously submits a subscription conversion.
        /// </summary>
        /// <param name="conversion">The new subscription conversion information.</param>
        /// <returns>The subscription conversion results.</returns>       
        public async Task<ConversionResult> CreateAsync(Conversion conversion)
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<Conversion, ConversionResult>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.PostSubscriptionConversion.Path, this.Context.Item1, this.Context.Item2),
                jsonConverter: new ResourceCollectionConverter<Conversion>());

            return await partnerApiServiceProxy.PostAsync(conversion);
        }

        /// <summary>
        /// Retrieves all conversions for the trial subscription.
        /// </summary>
        /// <returns>The subscription conversions.</returns>
        public ResourceCollection<Conversion> Get() => PartnerService.Instance.SynchronousExecute(this.GetAsync);

        /// <summary>
        /// Asynchronously retrieves all conversions for the trial subscription.
        /// </summary>
        /// <returns>The subscription conversions.</returns>
        public async Task<ResourceCollection<Conversion>> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<Conversion, ResourceCollection<Conversion>>(
              this.Partner,
              string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetSubscriptionConversions.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
