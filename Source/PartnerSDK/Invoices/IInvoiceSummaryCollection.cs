// -----------------------------------------------------------------------
// <copyright file="IInvoiceSummaryCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    using System.Threading.Tasks;
    using GenericOperations;
    using Microsoft.Store.PartnerCenter.Models;
    using Models.Invoices;

    /// <summary>
    /// Represents all the operations that can be done on invoice summary collection.
    /// </summary>
    public interface IInvoiceSummaryCollection : IPartnerComponent, IEntityGetOperations<ResourceCollection<InvoiceSummary>>
    {
        /// <summary>
        /// Retrieves the invoice summary collection. This operation is currently only supported for user based credentials.
        /// </summary>
        /// <returns>The invoice summary collection.</returns>
        new ResourceCollection<InvoiceSummary> Get();

        /// <summary>
        /// Asynchronously retrieves the invoice summary collection. This operation is currently only supported for user based credentials.
        /// </summary>
        /// <returns>The invoice summary collection.</returns>
        new Task<ResourceCollection<InvoiceSummary>> GetAsync();
    }
}