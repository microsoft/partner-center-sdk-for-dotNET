﻿// -----------------------------------------------------------------------
// <copyright file="LegalBusinessProfile.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Partners
{
    /// <summary>
    /// Represents a partner's legal business profile.
    /// </summary>
    public sealed class LegalBusinessProfile : ResourceBaseWithLinks<StandardResourceLinks>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LegalBusinessProfile"/> class.
        /// </summary>
        public LegalBusinessProfile()
        {
            this.Address = new Address();
            this.PrimaryContact = new Contact();
            this.CompanyApproverAddress = new Address();
        }

        /// <summary>
        /// Gets or sets the legal company name.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// Gets or sets the primary contact.
        /// </summary>
        public Contact PrimaryContact { get; set; }

        /// <summary>
        /// Gets or sets the company approver address.
        /// </summary>
        public Address CompanyApproverAddress { get; set; }

        /// <summary>
        /// Gets or sets the company approver email.
        /// </summary>
        public string CompanyApproverEmail { get; set; }

        /// <summary>
        /// Gets or sets the vetting status.
        /// </summary>
        public VettingStatus VettingStatus { get; set; }

        /// <summary>
        /// Gets or sets the vetting sub status.
        /// </summary>
        public VettingSubStatus VettingSubStatus { get; set; }
    }
}
