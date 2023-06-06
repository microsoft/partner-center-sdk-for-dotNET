// -----------------------------------------------------------------------
// <copyright file="AddressValidationResponse.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Customers
{
    using System.Collections.Generic;

    /// <summary>
    /// The address validation response object.
    /// </summary>
    public sealed class AddressValidationResponse : ResourceBaseWithLinks<StandardResourceLinks>
    {
        /// <summary>
        /// Gets or sets the original address.
        /// </summary>
        public Address OriginalAddress { get; set; }

        /// <summary>
        /// Gets or sets the suggested addresses.
        /// </summary>
        public List<Address> SuggestedAddresses { get; set; }

        /// <summary>
        /// Gets or sets the validation status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the validation message.
        /// </summary>
        public string ValidationMessage { get; set; }
    }
}
