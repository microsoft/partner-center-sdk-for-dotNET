// -----------------------------------------------------------------------
// <copyright file="IPartnerProfileCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Profiles
{
    /// <summary>
    /// Represents the behavior of a partner's profiles.
    /// </summary>
    public interface IPartnerProfileCollection : IPartnerComponent
    {
        /// <summary>
        /// Gets the operations available for the legal business profile.
        /// </summary>
        ILegalBusinessProfile LegalBusinessProfile { get; }

        /// <summary>
        /// Gets the operations available for the Mpn profile.
        /// </summary>
        IMpnProfile MpnProfile { get; }

        /// <summary>
        /// Gets the operations available for the partner support profile.
        /// </summary>
        ISupportProfile SupportProfile { get; }

        /// <summary>
        /// Gets the operations available for the organization profile.
        /// </summary>
        IOrganizationProfile OrganizationProfile { get; }

        /// <summary>
        /// Gets the operations available for the partner billing profile.
        /// </summary>
        IBillingProfile BillingProfile { get; }
    }
}
