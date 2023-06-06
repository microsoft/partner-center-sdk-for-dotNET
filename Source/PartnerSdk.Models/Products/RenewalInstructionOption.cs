// --------------------------------------------------------------------
// <copyright file="RenewalInstructionOption.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Products
{
    /// <summary>
    /// The renewal instruction option model.
    /// </summary>
    public class RenewalInstructionOption
    {
        /// <summary>
        /// Gets or sets the renew to id.
        /// </summary>
        public string RenewToId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not it is autorenewable.
        /// </summary>
        public bool IsAutoRenewable { get; set; }
    }
}
