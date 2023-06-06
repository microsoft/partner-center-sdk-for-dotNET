﻿// -----------------------------------------------------------------------
// <copyright file="IInvoiceTaxReceiptStatement.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using GenericOperations;

    /// <summary>
    /// Represents the operations available to an invoice tax receipt statement.
    /// </summary>
    public interface IInvoiceTaxReceiptStatement : IPartnerComponent<Tuple<string, string>>, IEntityGetOperations<Stream>
    {
        /// <summary>
        /// Retrieves the PDF invoice tax receipt statement.
        /// </summary>
        /// <returns>The invoice tax receipt statement.</returns>
        new Stream Get();

        /// <summary>
        /// Asynchronously retrieves the PDF invoice tax receipt statement.
        /// </summary>
        /// <returns>The invoice tax receipt statement.</returns>
        new Task<Stream> GetAsync();
    }
}