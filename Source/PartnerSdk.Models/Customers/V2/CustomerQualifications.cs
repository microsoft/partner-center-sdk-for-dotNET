// -----------------------------------------------------------------------
// <copyright file="CustomerQualifications.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Customers.V2
{
    using System.Collections.Generic;

    /// <summary>
    /// V2.CustomerQualifications
    /// Represents the object within which exists the qualifications of a customer.
    /// </summary>
    public class CustomerQualifications
    {
        /// <summary>
        /// Gets or sets the enumeration of qualifications.
        /// </summary>
        public IEnumerable<CustomerQualification> Qualifications { get; set; }
    }
}
