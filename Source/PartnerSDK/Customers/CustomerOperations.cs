// -----------------------------------------------------------------------
// <copyright file="CustomerOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers
{
    using System;
    using System.Threading.Tasks;
    using Agreements;
    using Analytics;
    using Carts;
    using CustomerDirectoryRoles;
    using CustomerUsers;
    using DevicesDeployment;
    using Entitlements;
    using ManagedServices;
    using PromotionEligibilities;
    using Models.Customers;
    using Network;
    using NewCommerceMigrations;
    using NewCommerceMigrationSchedules;
    using Offers;
    using Orders;
    using Products;
    using Profiles;
    using Qualification;
    using Relationships;
    using ServiceCosts;
    using ServiceRequests;
    using SubscribedSkus;
    using Subscriptions;
    using Usage;
    using Utilities;
    using ValidationStatus;

    /// <summary>
    /// Implements a single customer operations.
    /// </summary>
    internal class CustomerOperations : BasePartnerComponent, ICustomer
    {
        /// <summary>
        /// The customer subscriptions operations.
        /// </summary>
        private readonly Lazy<ISubscriptionCollection> subscriptions;

        /// <summary>
        /// The customer orders operations.
        /// </summary>
        private readonly Lazy<IOrderCollection> orders;

        /// <summary>
        /// The customer profiles operations.
        /// </summary>
        private readonly Lazy<ICustomerProfileCollection> profiles;

        /// <summary>
        /// The customer's service costs operations.
        /// </summary>
        private readonly Lazy<ICustomerServiceCostsCollection> serviceCosts;

        /// <summary>
        /// The customer's agreements.
        /// </summary>
        private readonly Lazy<ICustomerAgreementCollection> agreements;

        /// <summary>
        /// The customer service requests operations.
        /// </summary>
        private readonly Lazy<IServiceRequestCollection> serviceRequests;

        /// <summary>
        /// The customer configuration policies operations.
        /// </summary>
        private readonly Lazy<IConfigurationPolicyCollection> configurationPolicies;

        /// <summary>
        /// The customer device collection operations.
        /// </summary>
        private readonly Lazy<ICustomerDeviceCollection> devices;

        /// <summary>
        /// The customer devices batch upload job status operations.
        /// </summary>
        private readonly Lazy<IBatchJobStatusCollection> batchUploadStatusCollection;

        /// <summary>
        /// The customer managed services.
        /// </summary>
        private readonly Lazy<IManagedServiceCollection> managedServices;

        /// <summary>
        /// A lazy reference to the offer operations.
        /// </summary>
        private readonly Lazy<ICustomerOfferCollection> offers;

        /// <summary>
        /// A lazy reference to the product operations.
        /// </summary>
        private readonly Lazy<ICustomerProductCollection> products;

        /// <summary>
        /// A lazy reference to the cart operations.
        /// </summary>
        private readonly Lazy<ICartCollection> cart;

        /// <summary>
        /// A lazy reference to the New-Commerce migration operations.
        /// </summary>
        private readonly Lazy<INewCommerceMigrationCollection> newCommerceMigrations;

        /// <summary>
        /// A lazy reference to the New-Commerce migration schedule operations.
        /// </summary>
        private readonly Lazy<INewCommerceMigrationScheduleCollection> newCommerceMigrationSchedules;

        /// <summary>
        /// A lazy reference to the offer category operations.
        /// </summary>
        private readonly Lazy<ICustomerOfferCategoryCollection> offerCategories;

        /// <summary>
        /// The customer summary for usage-based subscriptions operations.
        /// </summary>
        private readonly Lazy<ICustomerUsageSummary> usageSummary;

        /// <summary>
        /// The operations for the spending budget allocated to the customer by the partner.
        /// </summary>
        private readonly Lazy<ICustomerUsageSpendingBudget> usageBudget;

        /// <summary>
        /// A lazy reference to the customer qualification operations.
        /// </summary>
        private readonly Lazy<ICustomerQualification> customerQualification;

        /// <summary>
        /// A lazy reference to the devices batch operations.
        /// </summary>
        private readonly Lazy<IDevicesBatchCollection> deviceBatches;

        /// <summary>
        /// A lazy reference to the customer user license collection operations.
        /// </summary>
        private readonly Lazy<ICustomerUserCollection> customerUserCollectionOperations;

        /// <summary>
        /// A lazy reference to the directory role collection operations.
        /// </summary>
        private readonly Lazy<IDirectoryRoleCollection> directoryRoleCollectionOperations;

        /// <summary>
        /// A lazy reference to the subscribed SKU collection operations.
        /// </summary>
        private readonly Lazy<ICustomerSubscribedSkuCollection> subscribedSkuCollectionOperations;

        /// <summary>
        /// A lazy reference to the customer relationship collection operations.
        /// </summary>
        private readonly Lazy<ICustomerRelationshipCollection> customerRelationshipCollectionOperations;

        /// <summary>
        /// A lazy reference to the customer analytics collection operations.
        /// </summary>
        private readonly Lazy<ICustomerAnalyticsCollection> analytics;

        /// <summary>
        /// A lazy reference to the entitlement collection operations.
        /// </summary>
        private readonly Lazy<IEntitlementCollection> entitlements;

        /// <summary>
        /// A lazy reference to the validation status operations.
        /// </summary>
        private readonly Lazy<IValidationStatus> validationStatus;

        /// <summary>
        /// A lazy reference to the promotion eligibilities collection operations.
        /// </summary>
        private readonly Lazy<IPromotionEligibilitiesCollection> promotionEligibilities;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        public CustomerOperations(IPartner rootPartnerOperations, string customerId)
            : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set");
            }

            this.subscriptions = new Lazy<ISubscriptionCollection>(() => new SubscriptionCollectionOperations(this.Partner, this.Context));
            this.orders = new Lazy<IOrderCollection>(() => new OrderCollectionOperations(this.Partner, this.Context));
            this.profiles = new Lazy<ICustomerProfileCollection>(() => new CustomerProfileCollectionOperations(this.Partner, this.Context));
            this.serviceCosts = new Lazy<ICustomerServiceCostsCollection>(() => new CustomerServiceCostsCollectionOperations(this.Partner, this.Context));
            this.serviceRequests = new Lazy<IServiceRequestCollection>(() => new CustomerServiceRequestCollectionOperations(this.Partner, this.Context));
            this.agreements = new Lazy<ICustomerAgreementCollection>(() => new CustomerAgreementCollectionOperations(this.Partner, this.Context));
            this.configurationPolicies = new Lazy<IConfigurationPolicyCollection>(() => new ConfigurationPolicyCollectionOperations(this.Partner, this.Context));
            this.deviceBatches = new Lazy<IDevicesBatchCollection>(() => new DevicesBatchCollectionOperations(this.Partner, this.Context));
            this.devices = new Lazy<ICustomerDeviceCollection>(() => new CustomerDevicesCollectionOperations(this.Partner, this.Context));
            this.batchUploadStatusCollection = new Lazy<IBatchJobStatusCollection>(() => new BatchJobStatusCollectionOperations(this.Partner, this.Context));
            this.managedServices = new Lazy<IManagedServiceCollection>(() => new ManagedServiceCollectionOperations(this.Partner, this.Context));
            this.offers = new Lazy<ICustomerOfferCollection>(() => new CustomerOfferCollectionOperations(this.Partner, this.Context));
            this.products = new Lazy<ICustomerProductCollection>(() => new CustomerProductCollectionOperations(this.Partner, this.Context));
            this.offerCategories = new Lazy<ICustomerOfferCategoryCollection>(() => new CustomerOfferCategoryCollectionOperations(this.Partner, this.Context));
            this.usageSummary = new Lazy<ICustomerUsageSummary>(() => new CustomerUsageSummaryOperations(this.Partner, this.Context));
            this.usageBudget = new Lazy<ICustomerUsageSpendingBudget>(() => new CustomerUsageSpendingBudgetOperations(this.Partner, this.Context));
            this.customerQualification = new Lazy<ICustomerQualification>(() => new CustomerQualificationOperations(this.Partner, this.Context));
            this.customerUserCollectionOperations = new Lazy<ICustomerUserCollection>(() => new CustomerUsersCollectionOperations(this.Partner, this.Context));
            this.directoryRoleCollectionOperations = new Lazy<IDirectoryRoleCollection>(() => new DirectoryRoleCollectionOperations(this.Partner, this.Context));
            this.subscribedSkuCollectionOperations = new Lazy<ICustomerSubscribedSkuCollection>(() => new CustomerSubscribedSkuCollectionOperations(this.Partner, this.Context));
            this.customerRelationshipCollectionOperations = new Lazy<ICustomerRelationshipCollection>(() => new CustomerRelationshipCollectionOperations(this.Partner, this.Context));
            this.analytics = new Lazy<ICustomerAnalyticsCollection>(() => new CustomerAnalyticsCollectionOperations(this.Partner, this.Context));
            this.cart = new Lazy<ICartCollection>(() => new CartCollectionOperations(this.Partner, this.Context));
            this.newCommerceMigrations = new Lazy<INewCommerceMigrationCollection>(() => new NewCommerceMigrationCollectionOperations(this.Partner, this.Context));
            this.newCommerceMigrationSchedules = new Lazy<INewCommerceMigrationScheduleCollection>(() => new NewCommerceMigrationScheduleCollectionOperations(this.Partner, this.Context));
            this.entitlements = new Lazy<IEntitlementCollection>(() => new EntitlementCollectionOperations(this.Partner, this.Context));
            this.validationStatus = new Lazy<IValidationStatus>(() => new ValidationStatusOperations(this.Partner, this.Context));
            this.promotionEligibilities = new Lazy<IPromotionEligibilitiesCollection>(() => new PromotionEligibilitiesCollectionOperations(this.Partner, this.Context));
        }

        /// <summary>
        /// Gets the orders behavior for the customer.
        /// </summary>
        /// <returns>The customer orders.</returns>
        public IOrderCollection Orders
        {
            get
            {
                return this.orders.Value;
            }
        }

        /// <summary>
        /// Obtains the profiles behavior for the customer.
        /// </summary>
        /// <returns>The customer profiles.</returns>
        public ICustomerProfileCollection Profiles
        {
            get
            {
                return this.profiles.Value;
            }
        }

        /// <summary>
        /// Obtains the service costs behavior for the customer.
        /// </summary>
        /// <returns>The customer's service costs.</returns>
        public ICustomerServiceCostsCollection ServiceCosts
        {
            get
            {
                return this.serviceCosts.Value;
            }
        }

        /// <summary>
        /// Obtains the agreements behavior for the customer.
        /// </summary>
        /// <returns>The customer's agreements.</returns>
        public ICustomerAgreementCollection Agreements
        {
            get
            {
                return this.agreements.Value;
            }
        }

        /// <summary>
        /// Obtains the subscriptions behavior for the customer.
        /// </summary>
        /// <returns>The customer subscriptions.</returns>
        public ISubscriptionCollection Subscriptions
        {
            get
            {
                return this.subscriptions.Value;
            }
        }

        /// <summary>
        /// Obtains the service requests behavior for the customer.
        /// </summary>
        /// <returns>The customer service request operations.</returns>
        public IServiceRequestCollection ServiceRequests
        {
            get
            {
                return this.serviceRequests.Value;
            }
        }

        /// <summary>
        /// Obtains the configuration policies behavior for the customer. 
        /// </summary>
        /// <returns>The customer configuration policies.</returns>
        public IConfigurationPolicyCollection ConfigurationPolicies
        {
            get
            {
                return this.configurationPolicies.Value;
            }
        }

        /// <summary>
        /// Obtains the Devices Batches behavior of the customer.
        /// </summary>
        public IDevicesBatchCollection DeviceBatches
        {
            get
            {
                return this.deviceBatches.Value;
            }
        }

        /// <summary>
        /// Obtains the Device policy behavior of the customer.
        /// </summary>
        public ICustomerDeviceCollection DevicePolicy
        {
            get
            {
                return this.devices.Value;
            }
        }

        /// <summary>
        /// Obtains the devices batch upload job status behavior of the customer.
        /// </summary>
        public IBatchJobStatusCollection BatchUploadStatus
        {
            get
            {
                return this.batchUploadStatusCollection.Value;
            }
        }                    

        /// <summary>
        /// Obtains the managed services behavior for the customer.
        /// </summary>
        /// <returns>The customer managed services operations.</returns>
        public IManagedServiceCollection ManagedServices
        {
            get
            {
                return this.managedServices.Value;
            }
        }

        /// <summary>
        /// Obtains the Offer Categories behavior for the customer.
        /// </summary>
        public ICustomerOfferCategoryCollection OfferCategories
        {
            get
            {
                return this.offerCategories.Value;
            }
        }

        /// <summary>
        /// Obtains the Offers behavior for the customer.
        /// </summary>
        public ICustomerOfferCollection Offers
        {
            get
            {
                return this.offers.Value;
            }
        }

        /// <summary>
        /// Obtains the Cart behavior for the customer.
        /// </summary>
        public ICartCollection Carts
        {
            get
            {
                return this.cart.Value;
            }
        }

        /// <summary>
        /// Gets the New-Commerce migration behavior for the customer.
        /// </summary>
        public INewCommerceMigrationCollection NewCommerceMigrations
        {
            get
            {
                return this.newCommerceMigrations.Value;
            }
        }

        /// <summary>
        /// Gets the New-Commerce migration schedule behavior for the customer.
        /// </summary>
        public INewCommerceMigrationScheduleCollection NewCommerceMigrationSchedules
        {
            get
            {
                return this.newCommerceMigrationSchedules.Value;
            }
        }

        /// <summary>
        /// Obtains the Products behavior for the customer.
        /// </summary>
        public ICustomerProductCollection Products
        {
            get
            {
                return this.products.Value;
            }
        }

        /// <summary>
        /// Obtains the usage summary behavior for the customer.
        /// </summary>
        /// <returns>The customer usage summary operations.</returns>
        public ICustomerUsageSummary UsageSummary 
        {
            get
            {
                return this.usageSummary.Value;
            }
        }

        /// <summary>
        /// Obtains the usage spending budget behavior for the customer.
        /// </summary>
        /// <returns>The customer usage spending budget operations.</returns>
        public ICustomerUsageSpendingBudget UsageBudget
        {
            get
            {
                return this.usageBudget.Value;
            }
        }

        /// <summary>
        /// Obtains the Customer qualification.
        /// </summary>
        public ICustomerQualification Qualification
        {
            get
            {
                return this.customerQualification.Value;
            }
        }

        /// <summary>
        /// Obtains the customer user collection object.
        /// </summary>
        /// <returns> The customer user collection operations instance.</returns>
        public ICustomerUserCollection Users
        {
            get
            {
                return this.customerUserCollectionOperations.Value;
            }
        }

        /// <summary>
        /// Obtains the directory role collection object.
        /// </summary>
        /// <returns> The directory role operations instance.</returns>
        public IDirectoryRoleCollection DirectoryRoles
        {
            get
            {
                return this.directoryRoleCollectionOperations.Value;
            }
        }

        /// <summary>
        /// Obtains the subscribed SKU collection object.
        /// </summary>
        /// <returns> The subscribed sku collection operations instance.</returns>
        public ICustomerSubscribedSkuCollection SubscribedSkus
        {
            get
            {
                return this.subscribedSkuCollectionOperations.Value;
            }
        }

        /// <summary>
        /// Obtains the customer relationship collection object.
        /// </summary>
        /// <returns> The customer relationship collection operations instance.</returns>
        public ICustomerRelationshipCollection Relationships
        {
            get
            {
                return this.customerRelationshipCollectionOperations.Value;
            }
        }

        /// <summary>
        /// Obtains the customer level analytics collection object.
        /// </summary>
        /// <returns> The customer level analytics collection operations instance.</returns>
        public ICustomerAnalyticsCollection Analytics
        {
            get
            {
                return this.analytics.Value;
            }
        }

        /// <summary>
        /// Obtains the entitlement collection object.
        /// </summary>
        /// <returns> The entitlement collection operations instance.</returns>
        public IEntitlementCollection Entitlements
        {
            get
            {
                return this.entitlements.Value;
            }
        }

        /// <summary>
        /// Obtains the validation status object.
        /// </summary>
        /// <returns> The validation status operations instance.</returns>
        public IValidationStatus ValidationStatus
        {
            get
            {
                return this.validationStatus.Value;
            }
        }

        public IPromotionEligibilitiesCollection PromotionEligibilities
        {
            get
            {
                return this.promotionEligibilities.Value;
            }
        }

        /// <summary>
        /// Retrieves information of a specific customer.
        /// </summary>
        /// <returns>The customer object.</returns>
        public Customer Get()
        {
            return PartnerService.Instance.SynchronousExecute<Customer>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves information of a specific customer.
        /// </summary>
        /// <returns>The customer object.</returns>
        public async Task<Customer> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<Customer, Customer>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.GetCustomer.Path, this.Context));

            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Deletes the customer from a testing in production account. This won't work for real accounts.
        /// </summary>
        public void Delete()
        {
            PartnerService.Instance.SynchronousExecute(() => this.DeleteAsync());
        }

        /// <summary>
        /// Asynchronously deletes the customer from a testing in production account. This won't work for real accounts.
        /// </summary>
        /// <returns>A task which completes when the customer is deleted.</returns>
        public async Task DeleteAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<Customer, Customer>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.DeleteCustomer.Path, this.Context));

            await partnerServiceProxy.DeleteAsync();
        }

        /// <summary>
        /// Removes Customer Partner relationship when RelationshipToPartner == CustomerPartnerRelationship.None.
        /// </summary>
        /// <param name="customer">A Customer with RelationshipToPartner == CustomerPartnerRelationship.None. </param>
        /// <returns>The customer information.</returns>
        public Customer Patch(Customer customer)
        {
            return PartnerService.Instance.SynchronousExecute<Customer>(() => this.PatchAsync(customer));
        }

        /// <summary>
        /// Asynchronously removes Customer Partner relationship when RelationshipToPartner == CustomerPartnerRelationship.None.
        /// </summary>
        /// <param name="customer">A Customer with RelationshipToPartner == CustomerPartnerRelationship.None. </param>
        /// <returns>The customer information.</returns>
        public async Task<Customer> PatchAsync(Customer customer)
        {
            ParameterValidator.Required(customer, "customer is required.");

            var partnerServiceProxy = new PartnerServiceProxy<Customer, Customer>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.RemoveCustomerRelationship.Path, this.Context));

            return await partnerServiceProxy.PatchAsync(customer);
        }
    }
}
