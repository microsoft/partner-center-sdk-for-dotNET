// -----------------------------------------------------------------------
// <copyright file="IReconciliationLineItemCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models.Query;
    using Models;
    using Models.Invoices;

    /// <summary>
    /// Represents the operations that can be done on partner's reconciliation line items.
    /// </summary>
    public interface IReconciliationLineItemCollection : IPartnerComponent
    {
        /// <summary>
        /// Retrieves reconciliation line items for a specific billing provider and invoice line item type 
        /// </summary>
        /// <returns>The collection of reconciliation line items.</returns>
        SeekBasedResourceCollection<InvoiceLineItem> Get();

        /// <summary>
        /// Asynchronously retrieves reconciliation line items collection of the partner
        /// </summary>
        /// <returns>The collection of reconciliation line items.</returns>
        Task<SeekBasedResourceCollection<InvoiceLineItem>> GetAsync();

        /// <summary>
        /// Seeks pages of reconciliation line items collection  of the partner
        /// </summary>
        /// <param name="continuationToken">The continuation token from the previous results.</param>
        /// <param name="seekOperation">The seek operation to perform. Next is only supported.</param>
        /// <returns>The next page of utilization records.</returns>
        SeekBasedResourceCollection<InvoiceLineItem> Seek(string continuationToken, SeekOperation seekOperation = SeekOperation.Next);

        /// <summary>
        /// Asynchronously seeks pages of reconciliation line items collection of the partner
        /// </summary>
        /// <param name="continuationToken">The continuation token from the previous results.</param>
        /// <param name="seekOperation">The seek operation to perform. Next is only supported.</param>
        /// <returns>A task that returns the next page of utilization records.</returns>
        Task<SeekBasedResourceCollection<InvoiceLineItem>> SeekAsync(string continuationToken, SeekOperation seekOperation = SeekOperation.Next);
    }
}