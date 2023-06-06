// -----------------------------------------------------------------------
// <copyright file="ValidationStatusOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.ValidationStatus
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models.ValidationStatus;
    using Microsoft.Store.PartnerCenter.Models.ValidationStatus.Enums;
    using Microsoft.Store.PartnerCenter.Network;

    /// <summary>
    /// This class implements the operations available on a customer's validation status.
    /// </summary>
    internal class ValidationStatusOperations : BasePartnerComponent, IValidationStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationStatusOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        public ValidationStatusOperations(IPartner rootPartnerOperations, string customerId) : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("The customer ID must be set.");
            }
        }

        /// <inheritdoc/>
        public ValidationStatus GetValidationStatus(ValidationType type)
        {
            return PartnerService.Instance.SynchronousExecute<ValidationStatus>(() => this.GetValidationStatusAsync(type));
        }

        /// <inheritdoc/>
        public async Task<ValidationStatus> GetValidationStatusAsync(ValidationType type)
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<ValidationStatus, ValidationStatus>(
                                            this.Partner,
                                            string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetValidationStatus.Path, this.Context));

            partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                        PartnerService.Instance.Configuration.Apis.GetValidationStatus.Parameters.ValidationType,
                        type.ToString()));

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
