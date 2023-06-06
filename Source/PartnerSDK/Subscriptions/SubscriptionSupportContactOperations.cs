// -----------------------------------------------------------------------
// <copyright file="SubscriptionSupportContactOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Subscriptions
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models.Subscriptions;
    using Microsoft.Store.PartnerCenter.Network;

    /// <summary>
    /// This class implements the operations for a customer's subscription support contact.
    /// </summary>
    internal class SubscriptionSupportContactOperations : BasePartnerComponent<Tuple<string, string>>, ISubscriptionSupportContact
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionSupportContactOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="subscriptionId">The subscription id.</param>
        public SubscriptionSupportContactOperations(IPartner rootPartnerOperations, string customerId, string subscriptionId)
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, subscriptionId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }

            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                throw new ArgumentException("subscriptionId must be set.");
            }
        }

        /// <summary>
        /// Retrieves the support contact of the customer's subscription.
        /// </summary>
        /// <returns>The support contact.</returns>
        public SupportContact Get()
        {
            return PartnerService.Instance.SynchronousExecute(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the support contact of the customer's subscription.
        /// </summary>
        /// <returns>The support contact.</returns>
        public async Task<SupportContact> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<SupportContact, SupportContact>(
             this.Partner,
             string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetSubscriptionSupportContact.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.GetAsync();
        }

        /// <summary>
        /// Updates the support contact of the customer's subscription.
        /// </summary>
        /// <param name="supportContact">The support contact.</param>
        /// <returns>The updated support contact.</returns>
        public SupportContact Update(SupportContact supportContact)
        {
            return PartnerService.Instance.SynchronousExecute(() => this.UpdateAsync(supportContact));
        }

        /// <summary>
        /// Asynchronously updates the support contact of the customer's subscription.
        /// </summary>
        /// <param name="supportContact">The support contact.</param>
        /// <returns>The updated support contact.</returns>
        public async Task<SupportContact> UpdateAsync(SupportContact supportContact)
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<SupportContact, SupportContact>(
              this.Partner,
              string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.UpdateSubscriptionSupportContact.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.PutAsync(supportContact);
        }
    }
}
