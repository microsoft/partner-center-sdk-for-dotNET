// -----------------------------------------------------------------------
// <copyright file="IAgreementDetailsCollection.cs" company="Microsoft Corporation">
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
    /// This interface represents the agreement details behavior.
    /// </summary>
    public interface IAgreementDetailsCollection : IPartnerComponent
    {
        /// <summary>
        /// Retrieves the agreement details.
        /// </summary>
        /// <returns>List of details about agreements.</returns>
        ResourceCollection<AgreementMetaData> Get();

        /// <summary>
        /// Asynchronously retrieves the agreement details.
        /// </summary>
        /// <returns>List of details about agreements.</returns>
        Task<ResourceCollection<AgreementMetaData>> GetAsync();

        /// <summary>
        /// Scopes agreements behavior to a specific agreement type.
        /// </summary>
        /// <param name="agreementType">The agreement type to filter the agreement operations on.</param>
        /// <returns>The agreement collection operations customized for the given agreement type.</returns>
        IAgreementDetailsCollection ByAgreementType(string agreementType);
    }
}
