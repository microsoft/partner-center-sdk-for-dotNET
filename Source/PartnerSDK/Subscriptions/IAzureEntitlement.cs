// <copyright file="IAzureEntitlement.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Subscriptions
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.GenericOperations;
    using Microsoft.Store.PartnerCenter.Models.Subscriptions;

    /// <summary>
    /// This interface defines the operations available on customer's Azure entitlements.
    /// </summary>
    public interface IAzureEntitlement : IPartnerComponent<Tuple<string, string, string>>, IEntityGetOperations<AzureEntitlement>
    {
        /// <summary>
        /// Cancels the specified Azure entitlement.
        /// </summary>
        /// <param name="azureEntitlementCancellationRequestContent">Content of the azure entitlement cancellation request.</param>
        /// <returns>Canceled Azure entitlement.</returns>
        AzureEntitlement Cancel(AzureEntitlementCancellationRequestContent azureEntitlementCancellationRequestContent);

        /// <summary>
        /// Asynchronously cancels the specified Azure entitlement.
        /// </summary>
        /// <param name="azureEntitlementCancellationRequestContent">Content of the azure entitlement cancellation request.</param>
        /// <returns>Canceled Azure entitlement.</returns>
        Task<AzureEntitlement> CancelAsync(AzureEntitlementCancellationRequestContent azureEntitlementCancellationRequestContent);
    }
}
