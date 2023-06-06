// -----------------------------------------------------------------------
// <copyright file="CustomerCompanyProfile.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Customers
{
    /// <summary>
    /// Holds customer company profile information.
    /// </summary>
    public sealed class CustomerCompanyProfile : ResourceBaseWithLinks<StandardResourceLinks>
    {
        /// <summary>
        /// Gets or sets the azure active directory tenant identifier for the customer's tenant.
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// Gets or sets the customer's azure active directory domain.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the organization registration number.
        /// </summary>
        public string OrganizationRegistrationNumber { get; set; }
    }
}