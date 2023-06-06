// -----------------------------------------------------------------------
// <copyright file="ResourceCollectionEnumeratorContainer.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Enumerators
{
    using System;
    using Factory;
    using Models;
    using Models.Auditing;
    using Models.Customers;
    using Models.Invoices;
    using Models.Offers;
    using Models.ServiceRequests;
    using Models.Users;

    /// <summary>
    /// Contains supported resource collection enumerators.
    /// </summary>
    internal class ResourceCollectionEnumeratorContainer : BasePartnerComponent, IResourceCollectionEnumeratorContainer
    {
        /// <summary>
        /// A lazy reference to an offer enumerator factory.
        /// </summary>
        private Lazy<IndexBasedCollectionEnumeratorFactory<Offer, ResourceCollection<Offer>>> offerEnumeratorFactory;

        /// <summary>
        /// A lazy reference to a customer enumerator factory.
        /// </summary>
        private Lazy<IndexBasedCollectionEnumeratorFactory<Customer, SeekBasedResourceCollection<Customer>>> customerEnumeratorFactory;

        /// <summary>
        /// A lazy reference to a customer user enumerator factory.
        /// </summary>
        private Lazy<IndexBasedCollectionEnumeratorFactory<CustomerUser, SeekBasedResourceCollection<CustomerUser>>> customerUserEnumeratorFactory;

        /// <summary>
        /// A lazy reference to an invoice enumerator factory.
        /// </summary>
        private Lazy<IndexBasedCollectionEnumeratorFactory<Invoice, ResourceCollection<Invoice>>> invoiceEnumeratorFactory;

        /// <summary>
        /// A lazy reference to a service request enumerator factory.
        /// </summary>
        private Lazy<IndexBasedCollectionEnumeratorFactory<ServiceRequest, ResourceCollection<ServiceRequest>>> serviceRequestEnumeratorFactory;

        /// <summary>
        /// A lazy reference to an invoice line enumerator factory.
        /// </summary>
        private Lazy<IndexBasedCollectionEnumeratorFactory<InvoiceLineItem, ResourceCollection<InvoiceLineItem>>> invoiceLineItemEnumeratorFactory;

        /// <summary>
        /// A lazy reference to an reconciliation line enumerator factory.
        /// </summary>
        private Lazy<IndexBasedCollectionEnumeratorFactory<InvoiceLineItem, ResourceCollection<InvoiceLineItem>>> reconciliationLineItemEnumeratorFactory;

        /// <summary>
        /// A lazy reference to an audit record enumerator factory.
        /// </summary>
        private Lazy<IndexBasedCollectionEnumeratorFactory<AuditRecord, SeekBasedResourceCollection<AuditRecord>>> auditRecordEnumeratorFactory;

        /// <summary>
        /// A lazy reference to a utilization record enumerator factory.
        /// </summary>
        private Lazy<IUtilizationCollectionEnumeratorContainer> utilizationRecordEnumeratorContainer;

        /// <summary>
        /// A lazy reference to a product enumerator factory.
        /// </summary>
        private Lazy<IndexBasedCollectionEnumeratorFactory<Models.Products.Product, ResourceCollection<Models.Products.Product>>> productEnumeratorFactory;

        /// <summary>
        /// A lazy reference to a product promotion enumerator factory.
        /// </summary>
        private Lazy<IndexBasedCollectionEnumeratorFactory<Models.Products.ProductPromotion, ResourceCollection<Models.Products.ProductPromotion>>> productPromotionEnumeratorFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceCollectionEnumeratorContainer"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public ResourceCollectionEnumeratorContainer(IPartner rootPartnerOperations)
            : base(rootPartnerOperations)
        {
            this.offerEnumeratorFactory = new Lazy<IndexBasedCollectionEnumeratorFactory<Offer, ResourceCollection<Offer>>>(() => new IndexBasedCollectionEnumeratorFactory<Offer, ResourceCollection<Offer>>(this.Partner));
            this.customerEnumeratorFactory = new Lazy<IndexBasedCollectionEnumeratorFactory<Customer, SeekBasedResourceCollection<Customer>>>(() => new IndexBasedCollectionEnumeratorFactory<Customer, SeekBasedResourceCollection<Customer>>(this.Partner));
            this.invoiceEnumeratorFactory = new Lazy<IndexBasedCollectionEnumeratorFactory<Invoice, ResourceCollection<Invoice>>>(() => new IndexBasedCollectionEnumeratorFactory<Invoice, ResourceCollection<Invoice>>(this.Partner));
            this.serviceRequestEnumeratorFactory = new Lazy<IndexBasedCollectionEnumeratorFactory<ServiceRequest, ResourceCollection<ServiceRequest>>>(() => new IndexBasedCollectionEnumeratorFactory<ServiceRequest, ResourceCollection<ServiceRequest>>(this.Partner));
            this.invoiceLineItemEnumeratorFactory = new Lazy<IndexBasedCollectionEnumeratorFactory<InvoiceLineItem, ResourceCollection<InvoiceLineItem>>>(() => new IndexBasedCollectionEnumeratorFactory<InvoiceLineItem, ResourceCollection<InvoiceLineItem>>(this.Partner));
            this.reconciliationLineItemEnumeratorFactory = new Lazy<IndexBasedCollectionEnumeratorFactory<InvoiceLineItem, ResourceCollection<InvoiceLineItem>>>(() => new IndexBasedCollectionEnumeratorFactory<InvoiceLineItem, ResourceCollection<InvoiceLineItem>>(this.Partner));
            this.customerUserEnumeratorFactory = new Lazy<IndexBasedCollectionEnumeratorFactory<CustomerUser, SeekBasedResourceCollection<CustomerUser>>>(() => new IndexBasedCollectionEnumeratorFactory<CustomerUser, SeekBasedResourceCollection<CustomerUser>>(this.Partner));
            this.auditRecordEnumeratorFactory = new Lazy<IndexBasedCollectionEnumeratorFactory<AuditRecord, SeekBasedResourceCollection<AuditRecord>>>(() => new IndexBasedCollectionEnumeratorFactory<AuditRecord, SeekBasedResourceCollection<AuditRecord>>(this.Partner));
            this.utilizationRecordEnumeratorContainer = new Lazy<IUtilizationCollectionEnumeratorContainer>(() => new UtilizationCollectionEnumeratorContainer(this.Partner));
            this.productEnumeratorFactory = new Lazy<IndexBasedCollectionEnumeratorFactory<Models.Products.Product, ResourceCollection<Models.Products.Product>>>(() => new IndexBasedCollectionEnumeratorFactory<Models.Products.Product, ResourceCollection<Models.Products.Product>>(this.Partner));
            this.productPromotionEnumeratorFactory = new Lazy<IndexBasedCollectionEnumeratorFactory<Models.Products.ProductPromotion, ResourceCollection<Models.Products.ProductPromotion>>>(() => new IndexBasedCollectionEnumeratorFactory<Models.Products.ProductPromotion, ResourceCollection<Models.Products.ProductPromotion>>(this.Partner));
        }

        /// <summary>
        /// Gets a factory that creates offer collection enumerators.
        /// </summary>
        public IResourceCollectionEnumeratorFactory<ResourceCollection<Offer>> Offers
        {
            get
            {
                return this.offerEnumeratorFactory.Value;
            }
        }

        /// <summary>
        /// Gets a factory that creates customer collection enumerators.
        /// </summary>
        public IResourceCollectionEnumeratorFactory<SeekBasedResourceCollection<Customer>> Customers
        {
            get
            {
                return this.customerEnumeratorFactory.Value;
            }
        }

        /// <summary>
        /// Gets a factory that creates invoice collection enumerators.
        /// </summary>
        public IResourceCollectionEnumeratorFactory<ResourceCollection<Invoice>> Invoices
        {
            get
            {
                return this.invoiceEnumeratorFactory.Value;
            }
        }

        /// <summary>
        /// Gets a factory that creates service request collection enumerators.
        /// </summary>
        public IResourceCollectionEnumeratorFactory<ResourceCollection<ServiceRequest>> ServiceRequests
        {
            get
            {
                return this.serviceRequestEnumeratorFactory.Value;
            }
        }

        /// <summary>
        /// Gets a factory that creates invoice line item collection enumerators.
        /// </summary>        
        public IResourceCollectionEnumeratorFactory<ResourceCollection<InvoiceLineItem>> InvoiceLineItems
        {
            get
            {
                return this.invoiceLineItemEnumeratorFactory.Value;
            }
        }

        /// <summary>
        /// Gets a factory that creates customer user collection enumerators.
        /// </summary>
        public IResourceCollectionEnumeratorFactory<SeekBasedResourceCollection<CustomerUser>> CustomerUsers
        {
            get
            {
                return this.customerUserEnumeratorFactory.Value;
            }
        }

        /// <summary>
        /// Gets a factory that creates an enumerator for audit records.
        /// </summary>
        public IResourceCollectionEnumeratorFactory<SeekBasedResourceCollection<AuditRecord>> AuditRecords
        {
            get
            {
                return this.auditRecordEnumeratorFactory.Value;
            }
        }

        /// <summary>
        /// Gets factories that create enumerators for utilization records for different subscriptions.
        /// </summary>
        public IUtilizationCollectionEnumeratorContainer Utilization
        {
            get
            {
                return this.utilizationRecordEnumeratorContainer.Value;
            }
        }

        /// <summary>
        /// Gets a factory that creates product collection enumerators.
        /// </summary>
        public IResourceCollectionEnumeratorFactory<ResourceCollection<Models.Products.Product>> Products
        {
            get
            {
                return this.productEnumeratorFactory.Value;
            }
        }

        /// <summary>
        /// Gets a factory that creates product promotion collection enumerators.
        /// </summary>
        public IResourceCollectionEnumeratorFactory<ResourceCollection<Models.Products.ProductPromotion>> ProductPromotions
        {
            get
            {
                return this.productPromotionEnumeratorFactory.Value;
            }
        }
    }
}
