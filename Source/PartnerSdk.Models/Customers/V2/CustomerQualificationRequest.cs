// -----------------------------------------------------------------------
// <copyright file="CustomerQualificationRequest.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Customers.V2
{
    using Newtonsoft.Json;

    /// <summary>
    /// V2.CustomerQualificationRequest
    /// Represents the object used to create a customer's qualifications.
    /// </summary>
    public class CustomerQualificationRequest
    {
        /// <summary>
        /// Gets or sets the qualification.
        /// </summary>
        [JsonProperty("qualification")]
        public string Qualification { get; set; }

        /// <summary>
        /// Gets or sets the educational segment.
        /// </summary>
        [JsonProperty("educationSegment", NullValueHandling = NullValueHandling.Ignore)]
        public string EducationSegment { get; set; }

        /// <summary>
        /// Gets or sets the GCC validation code.
        /// </summary>
        [JsonProperty("validationCode", NullValueHandling = NullValueHandling.Ignore)]
        public string ValidationCode { get; set; }

        /// <summary>
        /// Gets or sets the entity's website.
        /// </summary>
        [JsonProperty("website", NullValueHandling = NullValueHandling.Ignore)]
        public string Website { get; set; }
    }
}
