// -----------------------------------------------------------------------
// <copyright file="PartnerOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter
{
    using System;
    using Agreements;
    using Analytics;
    using AuditRecords;
    using Compliance;
    using CountryValidationRules;
    using Customers;
    using Domains;
    using Enumerators;
    using Extensions;
    using GenericOperations;
    using Invoices;
    using Microsoft.Store.PartnerCenter.SelfServePolicies;
    using Offers;
    using Products;
    using ProductUpgrades;
    using Profiles;
    using RateCards;
    using Relationships;
    using RequestContext;
    using ServiceIncidents;
    using ServiceRequests;
    using Usage;
    using Utilities;
    using Validations;

    /// <summary>
    /// The partner implementation class.
    /// </summary>
    internal class PartnerOperations : IPartner
    {
        /// <summary>
        /// The resource collection enumerator container.
        /// </summary>
        private readonly Lazy<IResourceCollectionEnumeratorContainer> enumeratorContainer;

        /// <summary>
        /// The partner customers operations.
        /// </summary>
        private readonly Lazy<ICustomerCollection> customers;

        /// <summary>
        /// The offer categories.
        /// </summary>
        private readonly Lazy<ICountrySelector<IOfferCategoryCollection>> offerCategories;

        /// <summary>
        /// The offers operation.
        /// </summary>
        private readonly Lazy<ICountrySelector<IOfferCollection>> offers;

        /// <summary>
        /// The products operation.
        /// </summary>
        private readonly Lazy<IProductCollection> products;

        /// <summary>
        /// The product promotions operation.
        /// </summary>
        private readonly Lazy<IProductPromotionCollection> productPromotions;

        /// <summary>
        /// The product upgrades operation.
        /// </summary>
        private readonly Lazy<IProductUpgradesCollection> productUpgrades;

        /// <summary>
        /// The extensions operation.
        /// </summary>
        private readonly Lazy<IExtensions> extensions;

        /// <summary>
        /// The profile operations.
        /// </summary>
        private readonly Lazy<IPartnerProfileCollection> profiles;

        /// <summary>
        /// The compliance operations.
        /// </summary>
        private readonly Lazy<IComplianceCollection> compliance;

        /// <summary>
        /// The partner invoices.
        /// </summary>
        private readonly Lazy<IInvoiceCollection> invoices;

        /// <summary>
        /// The service request operations.
        /// </summary>
        private readonly Lazy<IPartnerServiceRequestCollection> serviceRequests;

        /// <summary>
        /// The agreement details operations.
        /// </summary>
        private readonly Lazy<IAgreementDetailsCollection> agreementDetails;

        /// <summary>
        /// The agreement template operations.
        /// </summary>
        private readonly Lazy<IAgreementTemplateCollection> agreementTemplates;

        /// <summary>
        /// The service incidents operations.
        /// </summary>
        private readonly Lazy<IServiceIncidentCollection> serviceIncidents;

        /// <summary>
        /// The country validation rules operations.
        /// </summary>
        private readonly ICountryValidationRulesCollection countryValidationRules;

        /// <summary>
        /// The partner usage summary operations.
        /// </summary>
        private readonly Lazy<IPartnerUsageSummary> usageSummary;

        /// <summary>
        /// The domains operations.
        /// </summary>
        private readonly IDomainCollection domains;

        /// <summary>
        /// The audit records collection operations.
        /// </summary>
        private readonly Lazy<IAuditRecordsCollection> auditRecords;

        /// <summary>
        /// The rate cards collection operations.
        /// </summary>
        private readonly Lazy<IRateCardCollection> rateCards;

        /// <summary>
        /// The relationship collection operations.
        /// </summary>
        private readonly Lazy<IRelationshipCollection> relationships;

        /// <summary>
        /// The analytics collection operations.
        /// </summary>
        private readonly Lazy<IPartnerAnalyticsCollection> analytics;

        /// <summary>
        /// The validation operations.
        /// </summary>
        private readonly Lazy<IValidations> validations;

        /// <summary>
        /// The self serve policies operations.
        /// </summary>
        private readonly Lazy<ISelfServePoliciesCollection> selfServePolicies;

        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerOperations"/> class.
        /// </summary>
        /// <param name="credentials">The partner credentials.</param>
        /// <param name="context">A partner context.</param>
        public PartnerOperations(IPartnerCredentials credentials, IRequestContext context)
        {
            ParameterValidator.Required(credentials, "credentials can't be null");
            ParameterValidator.Required(context, "context can't be null");

            this.Credentials = credentials;
            this.RequestContext = context;

            this.enumeratorContainer = new Lazy<IResourceCollectionEnumeratorContainer>(() => new ResourceCollectionEnumeratorContainer(this));
            this.customers = new Lazy<ICustomerCollection>(() => new CustomerCollectionOperations(this));
            this.offerCategories = new Lazy<ICountrySelector<IOfferCategoryCollection>>(() => new OfferCountrySelector(this));
            this.offers = new Lazy<ICountrySelector<IOfferCollection>>(() => new OfferCountrySelector(this));
            this.products = new Lazy<IProductCollection>(() => new ProductCollectionOperations(this));
            this.productPromotions = new Lazy<IProductPromotionCollection>(() => new ProductPromotionCollectionOperations(this));
            this.productUpgrades = new Lazy<IProductUpgradesCollection>(() => new ProductUpgradesCollectionOperations(this));
            this.extensions = new Lazy<IExtensions>(() => new ExtensionsOperations(this));
            this.invoices = new Lazy<IInvoiceCollection>(() => new InvoiceCollectionOperations(this));
            this.serviceRequests = new Lazy<IPartnerServiceRequestCollection>(() => new PartnerServiceRequestCollectionOperations(this));
            this.agreementDetails = new Lazy<IAgreementDetailsCollection>(() => new AgreementDetailsCollectionOperations(this));
            this.agreementTemplates = new Lazy<IAgreementTemplateCollection>(() => new AgreementTemplateCollectionOperations(this));
            this.serviceIncidents = new Lazy<IServiceIncidentCollection>(() => new ServiceIncidentCollectionOperations(this));
            this.profiles = new Lazy<IPartnerProfileCollection>(() => new PartnerProfileCollectionOperations(this));
            this.compliance = new Lazy<IComplianceCollection>(() => new ComplianceCollectionOperations(this));
            this.countryValidationRules = new CountryValidationRulesCollectionOperations(this);
            this.usageSummary = new Lazy<IPartnerUsageSummary>(() => new PartnerUsageSummaryOperations(this));
            this.domains = new DomainCollectionOperations(this);
            this.auditRecords = new Lazy<IAuditRecordsCollection>(() => new AuditRecordsCollection(this));
            this.rateCards = new Lazy<IRateCardCollection>(() => new RateCardCollectionOperations(this));
            this.relationships = new Lazy<IRelationshipCollection>(() => new RelationshipCollectionOperations(this));
            this.analytics = new Lazy<IPartnerAnalyticsCollection>(() => new PartnerAnalyticsCollectionOperations(this));
            this.validations = new Lazy<IValidations>(() => new ValidationOperations(this));
            this.selfServePolicies = new Lazy<ISelfServePoliciesCollection>(() => new SelfServePoliciesCollectionOperations(this));
        }

        /// <summary>
        /// Gets the partner credentials.
        /// </summary>
        public IPartnerCredentials Credentials { get; private set; }

        /// <summary>
        /// Gets the partner context.
        /// </summary>
        public IRequestContext RequestContext { get; private set; }

        /// <summary>
        /// Gets the collection enumerators available for traversing through results.
        /// </summary>
        public IResourceCollectionEnumeratorContainer Enumerators
        {
            get
            {
                return this.enumeratorContainer.Value;
            }
        }

        /// <summary>
        /// Gets the offer categories operations available to the partner.
        /// </summary>
        public ICountrySelector<IOfferCategoryCollection> OfferCategories
        {
            get
            {
                return this.offerCategories.Value;
            }
        }

        /// <summary>
        /// Gets the partner profiles operations.
        /// </summary>
        public IPartnerProfileCollection Profiles
        {
            get
            {
                return this.profiles.Value;
            }
        }

        /// <summary>
        /// Gets the compliance operations.
        /// </summary>
        public IComplianceCollection Compliance
        {
            get
            {
                return this.compliance.Value;
            }
        }

        /// <summary>
        /// Gets the partner customers operations.
        /// </summary>
        public ICustomerCollection Customers
        {
            get
            {
                return this.customers.Value;
            }
        }

        /// <summary>
        /// Gets the offer operations available to the partner.
        /// </summary>
        public ICountrySelector<IOfferCollection> Offers
        {
            get
            {
                return this.offers.Value;
            }
        }

        /// <summary>
        /// Gets the product operations available to the partner.
        /// </summary>
        public IProductCollection Products
        {
            get
            {
                return this.products.Value;
            }
        }

        /// <summary>
        /// Gets the product promotion operations available to the partner.
        /// </summary>
        public IProductPromotionCollection ProductPromotions
        {
            get
            {
                return this.productPromotions.Value;
            }
        }

        /// <summary>
        /// Gets the product upgrades operations available to the partner.
        /// </summary>
        public IProductUpgradesCollection ProductUpgrades
        {
            get
            {
                return this.productUpgrades.Value;
            }
        }

        /// <summary>
        /// Gets the extensions operations available to the partner.
        /// </summary>
        public IExtensions Extensions
        {
            get
            {
                return this.extensions.Value;
            }
        }

        /// <summary>
        /// Gets the partner's invoices.
        /// </summary>
        public IInvoiceCollection Invoices
        {
            get
            {
                return this.invoices.Value;
            }
        }

        /// <summary>
        /// Gets the Service Request operations available.
        /// </summary>
        public IPartnerServiceRequestCollection ServiceRequests
        {
            get
            {
                return this.serviceRequests.Value;
            }
        }

        /// <summary>
        /// Gets the agreement details operations available.
        /// </summary>
        public IAgreementDetailsCollection AgreementDetails
        {
            get
            {
                return this.agreementDetails.Value;
            }
        }

        /// <summary>
        /// Gets the agreement template operations available.
        /// </summary>
        public IAgreementTemplateCollection AgreementTemplates
        {
            get
            {
                return this.agreementTemplates.Value;
            }
        }

        /// <summary>
        /// Gets the Service incidents operations available.
        /// </summary>
        public IServiceIncidentCollection ServiceIncidents
        {
            get
            {
                return this.serviceIncidents.Value;
            }
        }

        /// <summary>
        /// Gets the country validation rules operations available.
        /// </summary>
        public ICountryValidationRulesCollection CountryValidationRules
        {
            get
            {
                return this.countryValidationRules;
            }
        }

        /// <summary>
        /// Gets the usage summary operations available to the partner.
        /// </summary>
        public IPartnerUsageSummary UsageSummary
        {
            get
            {
                return this.usageSummary.Value;
            }
        }

        /// <summary>
        /// Gets the Domains operations available.
        /// </summary>
        public IDomainCollection Domains
        {
            get
            {
                return this.domains;
            }
        }

        /// <summary>
        /// Gets the audit records operations available to the partner.
        /// </summary>
        public IAuditRecordsCollection AuditRecords
        {
            get
            {
                return this.auditRecords.Value;
            }
        }

        /// <summary>
        /// Gets the rate card operations available to the partner.
        /// </summary>
        public IRateCardCollection RateCards
        {
            get
            {
                return this.rateCards.Value;
            }
        }

        /// <summary>
        /// Gets the relationship collection operations available to the partner.
        /// </summary>
        public IRelationshipCollection Relationships
        {
            get
            {
                return this.relationships.Value;
            }
        }

        /// <summary>
        /// Gets the analytics collection operations available to the partner.
        /// </summary>
        public IPartnerAnalyticsCollection Analytics
        {
            get
            {
                return this.analytics.Value;
            }
        }

        /// <summary>
        /// Gets the validation operations available to the partner.
        /// </summary>
        public IValidations Validations
        {
            get
            {
                return this.validations.Value;
            }
        }

        /// <summary>
        /// Gets the self serve policies collection operations available to the partner.
        /// </summary>
        public ISelfServePoliciesCollection SelfServePolicies
        {
            get
            {
                return this.selfServePolicies.Value;
            }
        }
    }
}