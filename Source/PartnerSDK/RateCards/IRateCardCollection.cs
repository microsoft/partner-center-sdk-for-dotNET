// -----------------------------------------------------------------------
// <copyright file="IRateCardCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.RateCards
{
    /// <summary>
    /// Holds operations that apply to rate cards.
    /// </summary>
    public interface IRateCardCollection : IPartnerComponent
    {
        /// <summary>
        /// Gets the Azure rate card operations.
        /// </summary>
        IAzureRateCard Azure { get; }
    }
}
