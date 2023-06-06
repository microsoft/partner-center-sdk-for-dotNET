// -----------------------------------------------------------------------
// <copyright file="RenewalInstruction.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Products
{
    using System.Collections.Generic;

    /// <summary>
    /// The renewal instruction model.
    /// </summary>
    public class RenewalInstruction
    {
        /// <summary>
        /// Gets or sets the term ids to which this instruction apply.
        /// </summary>
        public IEnumerable<string> ApplicableTermIds { get; set; }

        /// <summary>
        /// Gets or sets the renewal options.
        /// </summary>
        public IEnumerable<RenewalInstructionOption> RenewalOptions { get; set; }
    }
}
