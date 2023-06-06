// -----------------------------------------------------------------------
// <copyright file="IValidationStatus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.ValidationStatus
{
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models.ValidationStatus;
    using Microsoft.Store.PartnerCenter.Models.ValidationStatus.Enums;

    /// <summary>
    /// Defines the operations available on a customer's validation status.
    /// </summary>
    public interface IValidationStatus : IPartnerComponent
    {
        /// <summary>
        /// Retrieves the customer's validation status.
        /// </summary>
        /// <param name="type">The <see cref="ValidationType"/> to retrive a status for.</param>
        /// <returns>The customer's <see cref="ValidationStatus"/>.</returns>
        ValidationStatus GetValidationStatus(ValidationType type);

        /// <summary>
        /// Asynchronously retrieves the customer's validation status.
        /// </summary>
        /// <param name="type">The <see cref="ValidationType"/> to retrive a status for.</param>
        /// <returns>the customer's <see cref="ValidationStatus"/>.</returns>
        Task<ValidationStatus> GetValidationStatusAsync(ValidationType type);
    }
}
