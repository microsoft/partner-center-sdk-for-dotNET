// -----------------------------------------------------------------------
// <copyright file="ICustomerQualification.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Qualification
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the operations available on a customer's qualification.
    /// </summary>
    public interface ICustomerQualification : IPartnerComponent
    {
        /// <summary>
        /// Retrieves the customer qualifications in the V2 contract.
        /// </summary>
        /// <returns>the list of CustomerQualification.</returns>
        IEnumerable<Models.Customers.V2.CustomerQualification> GetQualifications();

        /// <summary>
        /// Asynchronously retrieves the customer qualifications in the V2 contract.
        /// </summary>
        /// <returns>the list of CustomerQualification.</returns>
        Task<IEnumerable<Models.Customers.V2.CustomerQualification>> GetQualificationsAsync();

        /// <summary>
        /// Creates the customer qualification synchronously using the POST Qualifications API. Use for GovernmentCommunityCloud with validation code after successful registration through Microsoft.
        /// </summary>
        /// <param name="customerQualificationRequest">Customer qualification to be created.</param>
        /// <returns>The updated customer qualification in the V2 contract.</returns>
        Models.Customers.V2.CustomerQualification CreateQualifications(Models.Customers.V2.CustomerQualificationRequest customerQualificationRequest);

        /// <summary>
        /// Creates the customer qualification asynchronously using the POST Qualifications API. Use for GovernmentCommunityCloud with validation code after successful registration through Microsoft.
        /// </summary>
        /// <param name="customerQualificationRequest">Customer qualification to be created.</param>
        /// <returns>The updated customer qualification in the V2 contract.</returns>
        Task<Models.Customers.V2.CustomerQualification> CreateQualificationsAsync(Models.Customers.V2.CustomerQualificationRequest customerQualificationRequest);
    }
}
