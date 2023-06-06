// -----------------------------------------------------------------------
// <copyright file="DirectSignedCustomerAgreementStatus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Agreements
{
    /// <summary>
    /// Provides the direct sign status of a customer agreement.
    /// </summary>
    public class DirectSignedCustomerAgreementStatus
    {
        /// <summary>
        /// Gets or sets a value indicating whether the agreement is directly
        /// signed by the customer or not.
        /// </summary>
        public bool IsSigned { get; set; }
    }
}
