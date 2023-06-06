// -----------------------------------------------------------------------
// <copyright file="CustomerQualification.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Customers.V2
{
    using System;

    /// <summary>
    /// V2.CustomerQualification
    /// Represents the qualification of a customer.
    /// </summary>
    public class CustomerQualification
    {
        /// <summary>
        /// Gets or sets the qualification.
        /// </summary>
        public string Qualification { get; set; }

        /// <summary>
        /// Gets or sets the vetting status.
        /// </summary>
        public string VettingStatus { get; set; }

        /// <summary>
        /// Gets or sets vetting reason.
        /// </summary>
        public string VettingReason { get; set; }

        /// <summary>
        /// Gets or sets vetting create date.
        /// </summary>
        public DateTime? VettingCreateDate { get; set; }
    }
}
