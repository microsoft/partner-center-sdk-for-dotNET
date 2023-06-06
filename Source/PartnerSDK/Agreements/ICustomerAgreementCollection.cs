// -----------------------------------------------------------------------
// <copyright file="ICustomerAgreementCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Agreements
{
    using System.Threading.Tasks;
    using Models;
    using Models.Agreements;

    /// <summary>
    /// Defines the operations available on a partner-customer agreement.
    /// </summary>
    public interface ICustomerAgreementCollection : IPartnerComponent
    {
        /// <summary>
        /// Retrieve the list of agreements between a partner and customer.
        /// </summary>
        /// <returns>The list of customer's agreements.</returns>
        ResourceCollection<Agreement> Get();

        /// <summary>
        /// Asynchronously retrieve the list of agreements between a partner and customer.
        /// </summary>
        /// <returns>The list of customer's agreements.</returns>
        Task<ResourceCollection<Agreement>> GetAsync();

        /// <summary>
        /// Create a new agreement on behalf of the customer.
        /// </summary>
        /// <param name="customerAgreement">Customer agreement details.</param>
        /// <returns>Newly created agreement.</returns>
        Agreement Create(Agreement customerAgreement);

        /// <summary>
        /// Asynchronously create a new agreement on behalf of the customer.
        /// </summary>
        /// <param name="customerAgreement">Customer agreement details.</param>
        /// <returns>Newly created agreement.</returns>
        Task<Agreement> CreateAsync(Agreement customerAgreement);

        /// <summary>
        /// Scopes customer agreements behavior to a specific agreement type.
        /// </summary>
        /// <param name="agreementType">The type to filter the customer agreement operations on.</param>
        /// <returns>The customer agreement collection operations customized for the given type.</returns>
        ICustomerAgreementCollection ByAgreementType(string agreementType);

        /// <summary>
        /// Retrieves the status of the direct acceptance of the Microsoft Customer Agreement by the customer.
        /// </summary>
        /// <returns>The list of customer's agreements.</returns>
        DirectSignedCustomerAgreementStatus GetDirectSignedCustomerAgreementStatus();

        /// <summary>
        /// Asynchronously retrieves the status of the direct acceptance of the Microsoft Customer Agreement by the customer.
        /// </summary>
        /// <returns>The list of customer's agreements.</returns>
        Task<DirectSignedCustomerAgreementStatus> GetDirectSignedCustomerAgreementStatusAsync();
    }
}
