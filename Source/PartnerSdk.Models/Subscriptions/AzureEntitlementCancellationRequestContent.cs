// <copyright file="AzureEntitlementCancellationRequestContent.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AzureEntitlementCancellationRequestContent"/> class.
    /// </summary>
    public class AzureEntitlementCancellationRequestContent
    {
        /// <summary>
        /// Gets or sets the cancellation reason.
        /// </summary>
        public string CancellationReason { get; set; }
    }
}
