// -----------------------------------------------------------------------
// <copyright file="CartError.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Carts
{
    using System.Collections.Generic;
    using Microsoft.Store.PartnerCenter.Models.Carts.Enums;

    /// <summary>
    /// Represents an error associated to a cart line item.
    /// </summary>
    public class CartError
    {
        /// <summary>
        /// Gets or sets a cart error code.
        /// </summary>
        public CartErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets an error description.
        /// </summary>
        /// <value>An error description.</value>
        public string ErrorDescription { get; set; }

        /// <summary>
        /// Gets or sets the additional information property bag.
        /// </summary>
        public Dictionary<string, object> AdditionalInformation { get; set; }
    }
}