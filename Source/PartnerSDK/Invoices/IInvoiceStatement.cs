// -----------------------------------------------------------------------
// <copyright file="IInvoiceStatement.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    using System.IO;
    using System.Threading.Tasks;
    using GenericOperations;

    /// <summary>
    /// Represents the operations available to an invoice statement.
    /// </summary>
    public interface IInvoiceStatement : IPartnerComponent, IEntityGetOperations<Stream>
    {
        /// <summary>
        /// Retrieves the PDF invoice statement.
        /// </summary>
        /// <returns>The invoice statement.</returns>
        new Stream Get();

        /// <summary>
        /// Asynchronously retrieves the PDF invoice statement.
        /// </summary>
        /// <returns>The invoice statement.</returns>
        new Task<Stream> GetAsync();
    }
}