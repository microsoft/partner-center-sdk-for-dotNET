// -----------------------------------------------------------------------
// <copyright file="SkuAttestationProperties.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Products
{
    /// <summary>
    /// Class that represents the sku attestation properties.
    /// </summary>
    public class SkuAttestationProperties
    {
        /// <summary>
        /// Gets or sets a value indicating whether attestation is required.
        /// </summary>
        public bool EnforceAttestation { get; set; }

        /// <summary>
        /// Gets or sets the type of the attestation.
        /// </summary>
        public string AttestationType { get; set; }
    }
}
