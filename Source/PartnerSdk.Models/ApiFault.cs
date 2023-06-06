// -----------------------------------------------------------------------
// <copyright file="ApiFault.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models
{
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents API failures.
    /// </summary>
    public sealed class ApiFault : ResourceBase
    {
        /// <summary>
        /// Gets or sets the API error code.
        /// </summary>
        [JsonProperty("code")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        [JsonProperty("description")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets additional fault information if present.
        /// </summary>
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<object> ErrorData { get; set; }

        /// <summary>
        /// Returns a meaningful summary about the API fault.
        /// </summary>
        /// <returns>The API fault summary.</returns>
        public override string ToString()
        {
            StringBuilder apiFaultDescription = new StringBuilder();

            apiFaultDescription.AppendLine().AppendFormat("Error code: {0}", this.ErrorCode).AppendLine();
            apiFaultDescription.AppendFormat("Error message: {0}", this.ErrorMessage).AppendLine();

            if (this.ErrorData != null)
            {
                apiFaultDescription.AppendLine("Error data:");

                foreach (var errorData in this.ErrorData)
                {
                    if (errorData != null)
                    {
                        apiFaultDescription.AppendLine(errorData.ToString());
                    }
                }

                apiFaultDescription.AppendLine();
            }

            return apiFaultDescription.ToString();
        }
    }
}