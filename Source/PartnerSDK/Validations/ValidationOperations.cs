// -----------------------------------------------------------------------
// <copyright file="ValidationOperations.cs" company="Microsoft Corporation">
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
    using Microsoft.Store.PartnerCenter.Network;
    using Microsoft.Store.PartnerCenter.Utilities;

    /// <summary>
    /// Represents the behavior of a validation operations.
    /// </summary>
    internal class ValidationOperations : BasePartnerComponent, IValidations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public ValidationOperations(IPartner rootPartnerOperations)
            : base(rootPartnerOperations)
        {
        }

        /// <summary>
        /// Checks if the address is valid or not.
        /// </summary>
        /// <param name="address">The address to be validated.</param>
        /// <returns>True if the address is valid, false otherwise.</returns>
        public AddressValidationResponse IsAddressValid(Address address)
        {
            return PartnerService.Instance.SynchronousExecute<AddressValidationResponse>(() => this.IsAddressValidAsync(address));
        }

        /// <summary>
        /// Asynchronously checks if the address is valid or not.
        /// </summary>
        /// <param name="address">The address to be validated.</param>
        /// <returns>The address validation response object.</returns>
        public async Task<AddressValidationResponse> IsAddressValidAsync(Address address)
        {
            ParameterValidator.Required(address, "address can't be null");

            var partnerServiceProxy = new PartnerServiceProxy<Address, AddressValidationResponse>(
               this.Partner,
               PartnerService.Instance.Configuration.Apis.AddressValidation.Path);

            return await partnerServiceProxy.PostAsync(address);
        }

        /// <summary>
        /// Gets validation code.  Used for Government Community Cloud.
        /// </summary>
        /// <returns>List of validation codes.</returns>
        public IEnumerable<ValidationCode> GetValidationCodes()
        {
            return PartnerService.Instance.SynchronousExecute<IEnumerable<ValidationCode>>(() => this.GetValidationCodesAsync());
        }

        /// <summary>
        /// Asynchronously gets validation codes. Used for Government Community Cloud.
        /// </summary>
        /// <returns>List of validation codes.</returns>
        public async Task<IEnumerable<ValidationCode>> GetValidationCodesAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<IEnumerable<ValidationCode>, IEnumerable<ValidationCode>>(
                this.Partner,
                PartnerService.Instance.Configuration.Apis.GetValidationCodes.Path);

            return await partnerServiceProxy.GetAsync();
        }
    }
}