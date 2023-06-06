// <copyright file="AzureEntitlementOpertions.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Subscriptions
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models.Subscriptions;
    using Microsoft.Store.PartnerCenter.Network;
    using Microsoft.Store.PartnerCenter.Utilities;

    /// <summary>
    /// The Azure entitlement operations.
    /// </summary>
    internal class AzureEntitlementOpertions : BasePartnerComponent<Tuple<string, string, string>>, IAzureEntitlement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureEntitlementOpertions"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="subscriptionId">The subscription identifier.</param>
        /// <param name="azureEntitlementId">The Azure entitlement Id.</param>
        public AzureEntitlementOpertions(IPartner rootPartnerOperations, string customerId, string subscriptionId, string azureEntitlementId)
            : base(rootPartnerOperations, new Tuple<string, string, string>(customerId, subscriptionId, azureEntitlementId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentNullException($"'{nameof(customerId)}' cannot be null or whitespace.", nameof(customerId));
            }

            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                throw new ArgumentNullException($"'{nameof(subscriptionId)}' cannot be null or whitespace.", nameof(subscriptionId));
            }

            if (string.IsNullOrWhiteSpace(azureEntitlementId))
            {
                throw new ArgumentNullException($"'{nameof(azureEntitlementId)}' cannot be null or whitespace.", nameof(azureEntitlementId));
            }
        }

        /// <summary>
        /// Gets the specified azure entitlement identifier.
        /// </summary>
        /// <returns>
        /// Azure entitlement.
        /// </returns>
        public AzureEntitlement Get()
        {
            return PartnerService.Instance.SynchronousExecute<AzureEntitlement>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously gets the specified azure entitlement identifier.
        /// </summary>
        /// <returns>
        /// Azure entitlement.
        /// </returns>
        public async Task<AzureEntitlement> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<AzureEntitlement, AzureEntitlement>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.GetAzureEntitlement.Path, this.Context.Item1, this.Context.Item2, this.Context.Item3));

            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Cancels the specified azure entitlement cancellation request content.
        /// </summary>
        /// <param name="azureEntitlementCancellationRequestContent">Content of the azure entitlement cancellation request.</param>
        /// <returns>
        /// Canceled Azure entitlement.
        /// </returns>
        public AzureEntitlement Cancel(AzureEntitlementCancellationRequestContent azureEntitlementCancellationRequestContent)
        {
            return PartnerService.Instance.SynchronousExecute<AzureEntitlement>(() => this.CancelAsync(azureEntitlementCancellationRequestContent));
        }

        /// <summary>
        /// Asynchronously cancels the asynchronous.
        /// </summary>
        /// <param name="azureEntitlementCancellationRequestContent">Content of the azure entitlement cancellation request.</param>
        /// <returns>
        /// Canceled Azure entitlement.
        /// </returns>
        public async Task<AzureEntitlement> CancelAsync(AzureEntitlementCancellationRequestContent azureEntitlementCancellationRequestContent)
        {
            ParameterValidator.Required(azureEntitlementCancellationRequestContent, "azureEntitlementCancellationRequestContent can't be null.");

            var partnerApiServiceProxy = new PartnerServiceProxy<AzureEntitlementCancellationRequestContent, AzureEntitlement>(
                this.Partner,
                string.Format(
                    CultureInfo.InvariantCulture,
                    PartnerService.Instance.Configuration.Apis.CancelAzureEntitlement.Path,
                    this.Context.Item1,
                    this.Context.Item2,
                    this.Context.Item3));

            return await partnerApiServiceProxy.PostAsync(azureEntitlementCancellationRequestContent);
        }
    }
}
