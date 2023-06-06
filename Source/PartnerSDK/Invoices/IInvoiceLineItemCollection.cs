// -----------------------------------------------------------------------
// <copyright file="IInvoiceLineItemCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.Invoices;

    /// <summary>
    /// Represents the operations that can be done on partner's invoice line items.
    /// </summary>
    public interface IInvoiceLineItemCollection : IPartnerComponent, IEntityCollectionRetrievalOperations<InvoiceLineItem, ResourceCollection<InvoiceLineItem>>
    {
        /// <summary>
        /// Retrieves invoice line items for a specific billing provider and invoice line item type 
        /// </summary>
        /// <returns>The collection of invoice line items.</returns>
       new ResourceCollection<InvoiceLineItem> Get();

        /// <summary>
        /// Asynchronously retrieves invoice line items for a specific billing provider and invoice line item type 
        /// </summary>
        /// <returns>The collection of invoice line items.</returns>
        new Task<ResourceCollection<InvoiceLineItem>> GetAsync();

        /// <summary>
        /// Retrieves a subset of invoice line items for a specific billing provider and invoice line item type 
        /// </summary>
        /// <param name="size">The maximum number of invoice line items to return.</param>
        /// <param name="offset">The page offset.</param>
        /// <returns>The subset of invoice line items.</returns>
        new ResourceCollection<InvoiceLineItem> Get(int size, int offset);

        /// <summary>
        ///  Asynchronously retrieves a subset of invoice line items for a specific billing provider and invoice line item type 
        /// </summary>
        /// <param name="size">The maximum number of invoice line items to return.</param>
        /// <param name="offset">The page offset.</param>
        /// <returns>The subset of invoice line items.</returns>
        new Task<ResourceCollection<InvoiceLineItem>> GetAsync(int size, int offset);
    }
}