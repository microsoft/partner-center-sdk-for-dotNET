// -----------------------------------------------------------------------
// <copyright file="IValidations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Validations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Customers;
    using Microsoft.Store.PartnerCenter.Models.ValidationCodes;

    /// <summary>
    /// Represents the behavior of a validation operations.
    /// </summary>
    public interface IValidations : IPartnerComponent
    {
        /// <summary>
        /// Checks if the address is valid or not.
        /// </summary>
        /// <param name="address">The address to be validated.</param>
        /// <returns>The address validation response object.</returns>
        AddressValidationResponse IsAddressValid(Address address);

        /// <summary>
        /// Asynchronously checks if the address is valid or not.
        /// </summary>
        /// <param name="address">The address to be validated.</param>
        /// <returns>The address validation response object.</returns>
        Task<AddressValidationResponse> IsAddressValidAsync(Address address);

        /// <summary>
        /// Gets validation code which is used for Government Community Cloud customers qualification.
        /// </summary>
        /// <returns>List of validation codes.</returns>
        IEnumerable<ValidationCode> GetValidationCodes();

        /// <summary>
        /// Asynchronously gets validation code which is used for Government Community Cloud customers qualification.
        /// </summary>
        /// <returns>List of validation codes.</returns>
        Task<IEnumerable<ValidationCode>> GetValidationCodesAsync();
    }
}