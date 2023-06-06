// -----------------------------------------------------------------------
// <copyright file="PartnerProfileCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Profiles
{
    using System;

    /// <summary>
    /// The partner profile collection operations implementation.
    /// </summary>
    internal class PartnerProfileCollectionOperations : BasePartnerComponent, IPartnerProfileCollection
    {
        /// <summary>
        /// The legal business profile operations.
        /// </summary>
        private readonly Lazy<ILegalBusinessProfile> legalBusinessProfile;

        /// <summary>
        /// The Mpn profile operations.
        /// </summary>
        private readonly Lazy<IMpnProfile> mpnProfileOperations;

        /// <summary>
        /// The support profile operations.
        /// </summary>
        private readonly Lazy<ISupportProfile> supportProfileOperations;

        /// <summary>
        /// The organization profile operations.
        /// </summary>
        private readonly Lazy<IOrganizationProfile> organizationProfile;

        /// <summary>
        /// The billing profile operations.
        /// </summary>
        private readonly Lazy<IBillingProfile> billingProfile;

        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerProfileCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public PartnerProfileCollectionOperations(IPartner rootPartnerOperations)
            : base(rootPartnerOperations)
        {
            this.legalBusinessProfile = new Lazy<ILegalBusinessProfile>(() => new LegalBusinessProfileOperations(this.Partner));
            this.mpnProfileOperations = new Lazy<IMpnProfile>(() => new MpnProfileOperations(this.Partner));
            this.supportProfileOperations = new Lazy<ISupportProfile>(() => new SupportProfileOperations(this.Partner));
            this.organizationProfile = new Lazy<IOrganizationProfile>(() => new OrganizationProfileOperations(this.Partner));
            this.billingProfile = new Lazy<IBillingProfile>(() => new BillingProfileOperations(this.Partner));
        }

        /// <summary>
        /// Gets the operations available for the legal business profile.
        /// </summary>
        public ILegalBusinessProfile LegalBusinessProfile
        {
            get { return this.legalBusinessProfile.Value; }
        }

        /// <summary>
        /// Gets the operations available for the Mpn profile.
        /// </summary>
        public IMpnProfile MpnProfile
        {
            get { return this.mpnProfileOperations.Value;  }
        }

        /// <summary>
        /// Gets the operations available for the support profile.
        /// </summary>
        public ISupportProfile SupportProfile
        {
            get { return this.supportProfileOperations.Value; }
        }

        /// <summary>
        /// Gets the operations available for the organization profile.
        /// </summary>
        public IOrganizationProfile OrganizationProfile
        {
            get
            {
                return this.organizationProfile.Value;
            }
        }

        /// <summary>
        /// Gets the operations available for the billing profile.
        /// </summary>
        public IBillingProfile BillingProfile
        {
            get
            {
                return this.billingProfile.Value;
            }
        }
    }
}
