// -----------------------------------------------------------------------
// <copyright file="IInvoiceCollection.cs" company="Microsoft Corporation">
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
    using Models.Query;

    /// <summary>
    /// Represents the operations that can be done on the partner's invoices.
    /// </summary>
    public interface IInvoiceCollection : IPartnerComponent, IEntireEntityCollectionRetrievalOperations<Invoice, ResourceCollection<Invoice>>, IEntitySelector<IInvoice>
    {
        /// <summary>
        /// Gets an invoice summary operations.
        /// </summary>
        /// <returns>The invoice summary operations.</returns>
        IInvoiceSummary Summary { get; }

        /// <summary>
        /// Gets invoice summary collection operations.
        /// </summary>
        /// <returns>The invoice summary collection.</returns>
        IInvoiceSummaryCollection Summaries { get; }

        /// <summary>
        /// Gets an estimates operations.
        /// </summary>
        IEstimateCollection Estimates { get; }

        /// <summary>
        /// Gets a single invoice operations.
        /// </summary>
        /// <param name="invoiceId">The invoice id.</param>
        /// <returns>The invoice operations.</returns>
        new IInvoice this[string invoiceId] { get; }

        /// <summary>
        /// Gets a single invoice operations.
        /// </summary>
        /// <param name="invoiceId">The invoice id.</param>
        /// <returns>The invoice operations.</returns>
        new IInvoice ById(string invoiceId);

        /// <summary>
        /// Retrieves all the invoices.
        /// </summary>
        /// <returns>The invoices.</returns>
        new ResourceCollection<Invoice> Get();

        /// <summary>
        /// Asynchronously retrieves all the invoices.
        /// </summary>
        /// <returns>The invoices.</returns>
        new Task<ResourceCollection<Invoice>> GetAsync();

        /// <summary>
        /// Queries invoices associated to the partner.
        /// </summary>
        /// <param name="query">The invoice query.
        /// The <see cref="Microsoft.Store.PartnerCenter.Models.Query.QueryFactory"/> can be used to build queries.
        /// Invoice queries support simple and indexed queries. You can retrieve subsets of invoices by providing an indexed query and specifying the index to start from
        /// and the page size to retrieve.
        /// You can also filter invoices using their dates. <see cref="Microsoft.Store.PartnerCenter.Models.Invoices.InvoiceSearchField"/> lists
        /// the supported search fields. You can use the <see cref="Microsoft.Store.PartnerCenter.Models.Query.FieldFilterOperation" /> enumeration to specify filtering operations.
        /// Supported filtering operations are: greater than, greater than or equals, less than, less than or equals, equals, and not equals. You can also compose compound filters
        /// that use the AND and OR field filter operations to retrieve a specific invoice subset.
        /// </param>
        /// <returns>The invoices that match the given query.</returns>
        ResourceCollection<Invoice> Query(IQuery query);

        /// <summary>
        /// Asynchronously queries invoices associated to the partner.
        /// </summary>
        /// <param name="query">The invoice query.
        /// The <see cref="Microsoft.Store.PartnerCenter.Models.Query.QueryFactory"/> can be used to build queries.
        /// Invoice queries support simple and indexed queries. You can retrieve subsets of invoices by providing an indexed query and specifying the index to start from
        /// and the page size to retrieve.
        /// You can also filter invoices using their dates. <see cref="Microsoft.Store.PartnerCenter.Models.Invoices.InvoiceSearchField"/> lists
        /// the supported search fields. You can use the <see cref="Microsoft.Store.PartnerCenter.Models.Query.FieldFilterOperation" /> enumeration to specify filtering operations.
        /// Supported filtering operations are: greater than, greater than or equals, less than, less than or equals, equals, and not equals. You can also compose compound filters
        /// that use the AND and OR field filter operations to retrieve a specific invoice subset.
        /// </param>
        /// <returns>The invoices that match the given query.</returns>
        Task<ResourceCollection<Invoice>> QueryAsync(IQuery query);
    }
}
