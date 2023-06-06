// -----------------------------------------------------------------------
// <copyright file="ICustomer.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers
{
    using System.Threading.Tasks;
    using Agreements;
    using Analytics;
    using Carts;
    using CustomerDirectoryRoles;
    using CustomerUsers;
    using DevicesDeployment;
    using Entitlements;
    using GenericOperations;
    using ManagedServices;
    using PromotionEligibilities;
    using Models.Customers;
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
    using ValidationStatus;

    /// <summary>
    /// Groups operations that can be performed on a single partner customer.
    /// </summary>
    public interface ICustomer : IPartnerComponent, IEntityGetOperations<Customer>, IEntityDeleteOperations<Customer>, IEntityPatchOperations<Customer>
    {
        /// <summary>
        /// Gets the orders behavior for the customer.
        /// </summary>
        /// <returns>The customer orders.</returns>
        IOrderCollection Orders { get; }

        /// <summary>
        /// Obtains the profiles behavior for the customer.
        /// </summary>
        /// <returns>The customer profiles.</returns>
        ICustomerProfileCollection Profiles { get; }

        /// <summary>
        /// Obtains the service costs behavior for the customer.
        /// </summary>
        /// <returns>The customer's service costs.</returns>
        ICustomerServiceCostsCollection ServiceCosts { get; }

        /// <summary>
        /// Obtains the agreements behavior for the customer.
        /// </summary>
        /// <returns>The customer agreements.</returns>
        ICustomerAgreementCollection Agreements { get; }

        /// <summary>
        /// Obtains the subscriptions behavior for the customer.
        /// </summary>
        /// <returns>The customer subscriptions.</returns>
        ISubscriptionCollection Subscriptions { get; }

        /// <summary>
        /// Obtains the service requests behavior for the customer.
        /// </summary>
        IServiceRequestCollection ServiceRequests { get; }

        /// <summary>
        /// Obtains the configuration policies behavior for the customer.
        /// </summary>
        IConfigurationPolicyCollection ConfigurationPolicies { get; }

        /// <summary>
        /// Obtains the devices batch behavior of the customer.
        /// </summary>
        IDevicesBatchCollection DeviceBatches { get; }

        /// <summary>
        /// Obtains the device policy behavior of the customer.
        /// </summary>
        ICustomerDeviceCollection DevicePolicy { get; }

        /// <summary>
        /// Obtains the devices batch upload status behavior of the customer.
        /// </summary>
        IBatchJobStatusCollection BatchUploadStatus { get; }

        /// <summary>
        /// Obtains the managed services behavior for the customer.
        /// </summary>
        IManagedServiceCollection ManagedServices { get; }

        /// <summary>
        /// Obtains the Offer Categories behavior for the customer.
        /// </summary>
        ICustomerOfferCategoryCollection OfferCategories { get; }

        /// <summary>
        /// Obtains the Offers behavior for the customer.
        /// </summary>
        ICustomerOfferCollection Offers { get; }

        /// <summary>
        /// Obtains the Products behavior for the customer.
        /// </summary>
        ICustomerProductCollection Products { get; }

        /// <summary>
        /// Obtains the usage summary behavior for the customer.
        /// </summary>
        ICustomerUsageSummary UsageSummary { get; }

        /// <summary>
        /// Obtains the usage spending budget behavior for the customer.
        /// </summary>
        ICustomerUsageSpendingBudget UsageBudget { get; }

        /// <summary>
        /// Obtains the qualification behavior of the customer.
        /// </summary>
        ICustomerQualification Qualification { get; }

        /// <summary>
        /// Obtains the users behavior for the customer.
        /// </summary>
        ICustomerUserCollection Users { get; }

        /// <summary>
        /// Obtains the directory role behavior for the customer.
        /// </summary>
        IDirectoryRoleCollection DirectoryRoles { get; }

        /// <summary>
        /// Obtains the subscribed SKUs collection behavior for the customer.
        /// </summary>
        ICustomerSubscribedSkuCollection SubscribedSkus { get; }

        /// <summary>
        /// Obtains the relationship collection behavior for the customer.
        /// </summary>
        ICustomerRelationshipCollection Relationships { get; }

        /// <summary>
        /// Obtains the analytics collection behavior for the customer.
        /// </summary>
        ICustomerAnalyticsCollection Analytics { get; }

        /// <summary>
        /// Obtains the Cart collection behavior for the customer.
        /// </summary>
        ICartCollection Carts { get; }

        /// <summary>
        /// Obtains the New-Commerce migration collection behavior for the customer.
        /// </summary>
        INewCommerceMigrationCollection NewCommerceMigrations { get; }

        /// <summary>
        /// Gets the New-Commerce migration schedule collection behavior for the customer.
        /// </summary>
        INewCommerceMigrationScheduleCollection NewCommerceMigrationSchedules { get; }

        /// <summary>
        /// Obtains the entitlement collection behavior for the customer.
        /// </summary>
        IEntitlementCollection Entitlements { get; }

        /// <summary>
        /// Obtains the validation status behavior for the customer.
        /// </summary>
        IValidationStatus ValidationStatus { get; }

        /// <summary>
        /// Obtains the promotion eligibilities collection behavior for the customer.
        /// </summary>
        IPromotionEligibilitiesCollection PromotionEligibilities { get; }

        /// <summary>
        /// Retrieves the customer information.
        /// </summary>
        /// <returns>The customer information.</returns>
        new Customer Get();

        /// <summary>
        /// Asynchronously retrieves the customer information.
        /// </summary>
        /// <returns>The customer information.</returns>
        new Task<Customer> GetAsync();

        /// <summary>
        /// Deletes the customer from a testing in production account. This won't work for real accounts.
        /// </summary>
        new void Delete();

        /// <summary>
        /// Asynchronously deletes the customer from a testing in production account. This won't work for real accounts.
        /// </summary>
        /// <returns>A task which completes when the customer is deleted.</returns>
        new Task DeleteAsync();

        /// <summary>
        /// Removes Customer Partner relationship when RelationshipToPartner == CustomerPartnerRelationship.None.
        /// </summary>
        /// <param name="customer">A Customer with RelationshipToPartner == CustomerPartnerRelationship.None. </param>
        /// <returns>The customer information.</returns>
        new Customer Patch(Customer customer);

        /// <summary>
        /// Asynchronously removes Customer Partner relationship when RelationshipToPartner == CustomerPartnerRelationship.None.
        /// </summary>
        /// <param name="customer">A Customer with RelationshipToPartner == CustomerPartnerRelationship.None. </param>
        /// <returns>The customer information.</returns>
        new Task<Customer> PatchAsync(Customer customer);
    }
}
