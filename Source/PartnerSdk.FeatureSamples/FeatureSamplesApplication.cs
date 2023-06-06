// -----------------------------------------------------------------------
// <copyright file="FeatureSamplesApplication.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Security;
    using System.Threading.Tasks;
    using Extensions;
    using Features;
    using Features.Agreements;
    using Features.Analytics;
    using Features.AzureUtilization;
    using Features.Cart;
    using Features.CountryValidationRules;
    using Features.CustomerDirectoryRoles;
    using Features.CustomerProducts;
    using Features.Customers;
    using Features.CustomerServiceCosts;
    using Features.CustomerSubscribedSkus;
    using Features.CustomerUser;
    using Features.DeviceDeployment;
    using Features.DistiVar;
    using Features.Domains;
    using Features.Entitlements;
    using Features.Invoices;
    using Features.NewCommerceMigrations;
    using Features.Offers;
    using Features.Orders;
    using Features.Products;
    using Features.ProductUpgrades;
    using Features.Profiles;
    using Features.PromotionEligibilities;
    using Features.Qualification;
    using Features.RateCards;
    using Features.Relationships;
    using Features.SelfServePolicies;
    using Features.ServiceIncidents;
    using Features.ServiceRequests;
    using Features.Subscriptions;
    using Features.ValidationStatus;
    using Microsoft.Identity.Client;
    using Microsoft.Store.PartnerCenter.FeatureSamples.Features.NewCommerceMigrationSchedules;
    using Microsoft.Store.PartnerCenter.FeatureSamples.Features.OrderAttachments;
    using Microsoft.Store.PartnerCenter.Models.Orders;

    /// <summary>
    /// A sample application that tests the partner SDK's functionality.
    /// </summary>
    public static class FeatureSamplesApplication
    {
        /// <summary>
        /// The state hash table key used to access the customers list.
        /// </summary>
        public const string CustomersKey = "CUSTOMERS";

        /// <summary>
        /// The state hash table key used to access a customer object.
        /// </summary>
        public const string SelectedCustomerKey = "SELECTED_CUSTOMER";

        /// <summary>
        /// The state hash table key used to access a customer for order service.
        /// </summary>
        public const string SelectedCustomerForOrderSvc = "SELECTED_CUSTOMER_FOR_ORDER_SVC";

        /// <summary>
        /// The state hash table key used to access a product upgrade customer object.
        /// </summary>
        public const string SelectedProductUpgradeCustomerKey = "SELECTED_PRODUCT_UPGRADE_CUSTOMER";

        /// <summary>
        /// The state hash table key used to access a product upgrade product name.
        /// </summary>
        public const string SelectedProductName = "SELECTED_PRODUCT_UPGRADE_NAME";

        /// <summary>
        /// The state hash table key used to access a customer object.
        /// </summary>
        public const string ActivationLinksCustomerKey = "ACTIVATIONLINKS_CUSTOMER";

        /// <summary>
        /// The state hash table key used to access a customer object.
        /// </summary>
        public const string ActivationLinksSubscripitonKey = "ACTIVATIONLINKS_SUBSCRIPTION";

        /// <summary>
        /// The state hash table key used to access a cart object.
        /// </summary>
        public const string SelectedCartKey = "SELECTED_CART";

        /// <summary>
        /// The state hash table key used to access a customer with usage data
        /// </summary>
        public const string CustomerForUsageDemo = "CUSTOMER_FOR_USAGE_DEMO";

        /// <summary>
        /// The state hash table key used to access a subscription with usage data
        /// </summary>
        public const string SubscriptionForUsageDemo = "SUBSCRIPTION_FOR_USAGE_DEMO";

        /// <summary>
        /// The state hash table key used to access a customer object with service requests.
        /// </summary>
        public const string CustomerForServiceRequests = "CUSTOMER_WITH_SERVICE_REQUESTS";

        /// <summary>
        /// The state hash table key used to access a customer with service costs.
        /// </summary>
        public const string CustomerWithServiceCosts = "CUSTOMER_WITH_SERVICE_COSTS";

        /// <summary>
        /// The state hash table key used to access a customer with trial offer.
        /// </summary>
        public const string CustomerWithTrialOffer = "CUSTOMER_WITH_TRIAL_OFFER";

        /// <summary>
        /// The trial offer id.
        /// </summary>
        public const string TrialOfferId = "TRIAL_OFFER_ID";

        /// <summary>
        /// The state hash table key used to access a customer object with service requests.
        /// </summary>
        public const string CustomerServiceRequest = "CUSTOMER_SERVICE_REQUEST_ID";

        /// <summary>
        /// The state hash table key used to access a partner network profile object.
        /// </summary>
        public const string SelectedMpnId = "SELECTED_MPNID";

        /// <summary>
        /// The state hash table key used to access a tenant Id.
        /// </summary>
        public const string SelectedTenantId = "SELECTED_TENANTID";

        /// <summary>
        /// The state hash table key used to access a customer object with devices.
        /// </summary>
        public const string CustomerForDeviceDeployment = "CUSTOMER_WITH_DEVICE_DEPLOYMENT";

        /// <summary>
        /// The state hash table key used to access a customer object with device collection.
        /// </summary>
        public const string CustomerDeviceBatchId = "DEVICE_BATCH_ID";

        /// <summary>
        /// The state hash table key used to access a customer object with devices.
        /// </summary>
        public const string CustomerDeviceId = "CUSTOMER_DEVICE_ID";

        /// <summary>
        /// The state hash table key used to access a customer object with devices.
        /// </summary>
        public const string CustomerWithProducts = "CUSTOMER_WITH_PRODUCTS";

        /// <summary>
        /// Direct Reseller Integration Test Account User.
        /// </summary>
        public const string DirectResellerIntegrationTestAccountUser = "DIRECT_RESELLER_INTEGRATION_TEST_ACCOUNT_USER";

        /// <summary>
        /// The state hash table key used to access a customer object with configuration policies.
        /// </summary>
        public const string CustomerConfigurationPolicyId = "CUSTOMER_CONFIGURATION_POLICY_ID";

        /// <summary>
        /// The state hash table key used to access a customer object with configuration policy to be deleted.
        /// </summary>
        public const string CustomerConfigurationPolicyIdForDelete = "CUSTOMER_CONFIGURATION_POLICY_ID_FOR_DELETE";

        /// <summary>
        /// The Id to track to the status of batch upload of the devices.
        /// </summary>
        public const string TrackingIdForDeviceDeployment = "TRACKINGID_FOR_BATCHUPLOAD";

        /// <summary>
        /// The customer with agreements.
        /// </summary>
        public const string CustomerWithAgreements = "CUSTOMER_WITH_AGREEMENTS";

        /// <summary>
        /// The partner's user Id for agreement creation.
        /// </summary>
        public const string PartnerUserForAgreement = "PARTNER_USER_ID_FOR_AGREEMENT";

        /// <summary>
        /// The state hash table key used to access the offer category list.
        /// </summary>
        public const string OfferCategoriesKey = "OFFER_CATEGORIES";

        /// <summary>
        /// The state hash table key used to access the offers.
        /// </summary>
        public const string OffersKey = "OFFERS";

        /// <summary>
        /// The state hash table key used to access the products.
        /// </summary>
        public const string ProductsKey = "PRODUCTS";

        /// <summary>
        /// The state hash table key used to access the product promotions.
        /// </summary>
        public const string ProductPromotionsKey = "PRODUCT_PROMOTIONS";

        /// <summary>
        /// The state hash table key used to access the default catalog view.
        /// </summary>
        public const string ProductTargetViewKey = "PRODUCT_TARGET_VIEW";

        /// <summary>
        /// The state hash table key used to access the default segment.
        /// </summary>
        public const string ProductPromotionSegmentKey = "PRODUCT_PROMOTION_SEGMENT";

        /// <summary>
        /// The state hash table key used to access the customer products.
        /// </summary>
        public const string CustomerProductsKey = "CUSTOMER_PRODUCTS";

        /// <summary>
        /// The state hash table key used to access the skus.
        /// </summary>
        public const string SkusKey = "SKUS";

        /// <summary>
        /// The state hash table key used to access the created cart
        /// </summary>
        public const string CartsKey = "CARTS";

        /// <summary>
        /// The state hash table key used to access the Inventory
        /// </summary>
        public const string InventoryKey = "Inventory";

        /// <summary>
        /// The state hash table key used to access the availabilities.
        /// </summary>
        public const string AvailabilitiesKey = "AVAILABILITIES";

        /// <summary>
        /// The state hash table key used to access the skus.
        /// </summary>
        public const string CustomerSkusKey = "CUSTOMER_SKUS";

        /// <summary>
        /// The state hash table key used to access the subscriptions.
        /// </summary>
        public const string SubscriptionKey = "SUBSCRIPTIONS";

        /// <summary>
        /// The state hash table key used to access the orders.
        /// </summary>
        public const string OrdersKey = "ORDERS";

        /// <summary>
        /// The state hash table key used to access the selected invoice
        /// </summary>
        public const string SelectedInvoiceKey = "SELECTED_INVOICE";

        /// <summary>
        /// The state hash table key used to access the selected receipt
        /// </summary>
        public const string SelectedReceiptKey = "SELECTED_RECEIPT";

        /// <summary>
        /// The state hash table key used to access the selected currency code key.
        /// </summary>
        public const string SelectedCurrencyCodeKey = "SELECTED_CURRENCYCODE";

        /// <summary>
        /// The state hash table key used to access the customers list.
        /// </summary>
        public const string CustomersList = "CUSTOMERSLIST";

        /// <summary>
        /// The state hash table key used to access the customers user list.
        /// </summary>
        public const string CustomerUsersCollection = "CUSTOMERUSERS";

        /// <summary>
        /// The state hash table key used to access the selected customers user key.
        /// </summary>
        public const string CustomersUserKey = "CUSTOMERUSERKEY";

        /// <summary>
        /// The state hash table key used to access the selected subscription key.
        /// </summary>
        public const string SelectedSubscriptionKey = "SUBSCRIPTIONUSERKEY";

        /// <summary>
        /// The state hash table key used to access the selected azure subscription key.
        /// </summary>
        public const string LegacyAzureSubscriptionKey = "AZURE_SUBSCRIPTION_ID_KEY";

        /// <summary>
        /// The state hash table key used to access a customer with registration
        /// </summary>
        public const string CustomerForRegistrationStatusDemo = "CUSTOMER_FOR_REGISTRATION_DEMO";

        /// <summary>
        /// The state hash table key used to access a subscription with registration
        /// </summary>
        public const string SubscriptionForRegistrationStatusDemo = "SUBSCRIPTION_FOR_REGISTRATION_DEMO";

        /// <summary>
        /// The state hash table key used to access the selected billing cycle type key.
        /// </summary>
        public const string BillingCycleTypeKey = "BILLINGCYCLETYPEKEY";

        /// <summary>
        /// The state hash table key used to access an offer having add on items with service requests.
        /// </summary>
        public const string OfferWithAddonId = "OFFER_WITH_ADDON_ID";

        /// <summary>
        /// The state hash table key used to access an offer add on item with service requests.
        /// </summary>
        public const string OfferAddonOneId = "OFFER_ADDON_ONE_ID";

        /// <summary>
        /// The state hash table key used to access another offer add on item with service requests.
        /// </summary>
        public const string OfferAddonTwoId = "OFFER_ADDON_TWO_ID";

        /// <summary>
        /// The state hash table key used to access a product family
        /// </summary>
        public const string ProductFamily = "PRODUCT_FAMILY";

        /// <summary>
        /// The state hash table key used to access the product upgrade id
        /// </summary>
        public const string UpgradeId = "PRODUCT_UPGRADE_ID";

        /// <summary>
        /// The state hash table key used to access the Azure Plan product id
        /// </summary>
        public const string AzurePlanProductId = "AZURE_PLAN_PRODUCT_ID";

        /// <summary>
        /// The state hash table key used to access the Azure Plan sku id
        /// </summary>
        public const string AzurePlanSkuId = "AZURE_PLAN_SKU_ID";

        /// <summary>
        /// The state hash table key used for Direct Reseller Test Client Id
        /// </summary>
        public const string DirectResellerTestAccountClientIdKey = "directResellerTestAccount.clientId";

        /// <summary>
        /// The state hash table key used for Direct Reseller Test User Name
        /// </summary>
        public const string DirectResellerTestAccountUserNameKey = "directResellerTestAccount.userName";

        /// <summary>
        /// The state hash table key used for Direct Reseller Test Password
        /// </summary>
        public const string DirectResellerTestAccountPasswordKey = "directResellerTestAccount.passWord";

        /// <summary>
        /// The Direct Reseller Test Account Customer Tenant Id
        /// </summary>
        public const string DirectResellerTestAccountCustomerTenantId = "DIRECT_RESELLER_TEST_ACCOUNT_CUSTOMER_TENANT_ID";

        /// <summary>
        /// The Direct Reseller Test Account User Id
        /// </summary>
        public const string DirectResellerTestAccountUserId = "DIRECT_RESELLER_TEST_ACCOUNT_USER_ID";

        /// <summary>
        /// The state hash table key used to access a order for attachments.
        /// </summary>
        public const string OrderIdForAttachments = "ORDERID_ATTACHMENTS";

        /// <summary>
        /// The state hash table key used to access a attachment id for a order.
        /// </summary>
        public const string OrderAttachmentId = "ORDER_ATTACHMENTID";

        /// <summary>
        /// Holds the application state.
        /// </summary>
        private static IDictionary<string, object> applicationState = new Dictionary<string, object>();

        /// <summary>
        /// The registered features which will be run.
        /// </summary>
        private static ICollection<IUnitOfWork> features = new List<IUnitOfWork>();

        /// <summary>
        /// The registered features for CustomerUserLicense API which will be run.
        /// </summary>
        private static ICollection<IUnitOfWork> customerUserLicenseFeatures = new List<IUnitOfWork>();

        /// <summary>
        /// Main entry point to the application.
        /// </summary>
        public static void Main()
        {
            try
            {
                // register units of work
                FeatureSamplesApplication.RegisterUnitsOfWork();

                // override the service API URL to the configured endpoint
                PartnerService.Instance.ApiRootUrl = ConfigurationManager.AppSettings["partnerServiceApiRoot"];

                IPartnerCredentials credentials = FeatureSamplesApplication.Authenticate();
                IPartnerCredentials credentialsForCustomerUserLicense = FeatureSamplesApplication.Authenticate(
                    null,
                    DirectResellerTestAccountClientIdKey,
                    DirectResellerTestAccountUserNameKey,
                    DirectResellerTestAccountPasswordKey);

                // create the partner operations
                IAggregatePartner partnerOperations = PartnerService.Instance.CreatePartnerOperations(credentials);
                IAggregatePartner partnerOperationsForCustomerUserLicense = PartnerService.Instance.CreatePartnerOperations(credentialsForCustomerUserLicense);

                ////List of tuples that map between the partnerOperations of choice for the appropriate features list. This is to support the usage of different testaccounts for testing features
                List<Tuple<IAggregatePartner, ICollection<IUnitOfWork>>> partnerOperationsFeaturesPair = new List<Tuple<IAggregatePartner, ICollection<IUnitOfWork>>>();

                partnerOperationsFeaturesPair.Add(new Tuple<IAggregatePartner, ICollection<IUnitOfWork>>(partnerOperations, FeatureSamplesApplication.features));
                partnerOperationsFeaturesPair.Add(new Tuple<IAggregatePartner, ICollection<IUnitOfWork>>(partnerOperationsForCustomerUserLicense, FeatureSamplesApplication.customerUserLicenseFeatures));

                foreach (Tuple<IAggregatePartner, ICollection<IUnitOfWork>> tuple in partnerOperationsFeaturesPair)
                {
                    IAggregatePartner operation = tuple.Item1;
                    ICollection<IUnitOfWork> featureList = tuple.Item2;

                    // execute the registered features
                    foreach (IUnitOfWork feature in featureList)
                    {
                        do
                        {
                            Console.Clear();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(feature.Title);
                            Console.ForegroundColor = ConsoleColor.White;

                            try
                            {
                                feature.Execute(operation, FeatureSamplesApplication.applicationState);
                            }
                            catch (Exception unitOfWorkFailure)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(unitOfWorkFailure);
                            }

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(
                                "Press Enter to view the next feature, to repeat the current feature press r...");
                        }
                        while (Console.ReadKey().KeyChar == 'r');
                    }
                }
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exception);
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Authenticates with the partner API service.
        /// </summary>
        /// <param name="applicationSignInEnabled">Application Sign In Enabling.</param>
        /// <param name="accountClientId">The Account Client Id.</param>
        /// <param name="accountUserName">The Account User Name.</param>
        /// <param name="accountPassWord">The Account Password.</param>
        /// <returns>The service credentials.</returns>
        private static IPartnerCredentials Authenticate(string applicationSignInEnabled = null, string accountClientId = null, string accountUserName = null, string accountPassWord = null)
        {
            IPartnerCredentials credentials = null;
            bool isApplicationSignInEnabled = applicationSignInEnabled != null ? bool.Parse(ConfigurationManager.AppSettings[applicationSignInEnabled]) : bool.Parse(ConfigurationManager.AppSettings["applicationSignIn"]);

            if (isApplicationSignInEnabled)
            {
                // This is an application sign in
                string aadAuthority = ConfigurationManager.AppSettings["aad.authority"];
                string graphEndpoint = ConfigurationManager.AppSettings["aad.graphEndpoint"];
                string applicationId = ConfigurationManager.AppSettings["aad.applicationId"];
                string applicationSecret = ConfigurationManager.AppSettings["aad.applicationSecret"];
                string applicationDomain = ConfigurationManager.AppSettings["aad.applicationDomain"];

                credentials = PartnerCredentials.Instance.GenerateByApplicationCredentials(applicationId, applicationSecret, applicationDomain, aadAuthority, graphEndpoint);

                // applications can't query customers, therefore let's set a default customer Id for the other features to work
                FeatureSamplesApplication.applicationState[FeatureSamplesApplication.SelectedCustomerKey] = ConfigurationManager.AppSettings["defaultCustomerId"];
                FeatureSamplesApplication.applicationState[FeatureSamplesApplication.SelectedCustomerForOrderSvc] = ConfigurationManager.AppSettings["customerIdForOrderSvc"];
                FeatureSamplesApplication.applicationState[FeatureSamplesApplication.SelectedCartKey] = ConfigurationManager.AppSettings["defaultCartId"];
            }
            else
            {
                // This is a user sign in, Login to Azure Active Directory
                var authenticationResult = FeatureSamplesApplication.LoginToAad(accountClientId, accountUserName, accountPassWord).Result;

                // Authenticate by user context with the partner service
                credentials = PartnerCredentials.Instance.GenerateByUserCredentials(
                    ConfigurationManager.AppSettings[accountClientId ?? "aad.clientId"],
                    new AuthenticationToken(
                        authenticationResult.AccessToken,
                        authenticationResult.ExpiresOn),
                    async delegate(AuthenticationToken expireAuthToken)
                    {
                        // token has expired, re-Login to Azure Active Directory
                        var aadToken = await FeatureSamplesApplication.LoginToAad(accountClientId, accountUserName, accountPassWord);
                        return new AuthenticationToken(aadToken.AccessToken, aadToken.ExpiresOn);
                    });
            }

            return credentials;
        }

        /// <summary>
        /// Registers units of work the sample application will execute.
        /// </summary>
        private static void RegisterUnitsOfWork()
        {
            bool testMultiTier = bool.Parse(ConfigurationManager.AppSettings["TestMultiTierScenario"]);
            bool testDevicesScenarios = bool.Parse(ConfigurationManager.AppSettings["TestDevicesScenario"]);
            bool testModernScenarios = bool.Parse(ConfigurationManager.AppSettings["TestModernScenarios"]);
            bool testRICases = bool.Parse(ConfigurationManager.AppSettings["TestRIScenarios"]);
            bool testModernAzureScenarios = bool.Parse(ConfigurationManager.AppSettings["TestModernAzureScenarios"]);
            bool testModernOfficeScenarios = bool.Parse(ConfigurationManager.AppSettings["TestModernOfficeScenarios"]);

            // set a default customer Id for device collections
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.CustomerForDeviceDeployment] = ConfigurationManager.AppSettings["customerWithDevices"];

            // set a default device collection Id for device collection operations
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.CustomerDeviceBatchId] = ConfigurationManager.AppSettings["deviceBatchId"];

            // set a default device Id for device operations
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.CustomerDeviceId] = ConfigurationManager.AppSettings["deviceId"];

            // set a default policy Id for configuration policy operations
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.CustomerConfigurationPolicyId] = ConfigurationManager.AppSettings["configurationPolicyId"];

            // set a default policy Id for configuration delete policy operations
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.CustomerConfigurationPolicyIdForDelete] = ConfigurationManager.AppSettings["configurationPolicyId"];

            // set a default tracking Id for batch upload
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.TrackingIdForDeviceDeployment] = ConfigurationManager.AppSettings["trackingIdForDeviceDeployment"];
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.BillingCycleTypeKey] = ConfigurationManager.AppSettings["defaultBillingCycleType"];
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.SelectedCustomerKey] = ConfigurationManager.AppSettings["defaultCustomerId"];
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.SelectedCustomerForOrderSvc] = ConfigurationManager.AppSettings["customerIdForOrderSvc"];
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.SelectedProductUpgradeCustomerKey] = ConfigurationManager.AppSettings["productUpgradeCustomerId"];
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.SelectedProductName] = ConfigurationManager.AppSettings["productResourceName"];
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.AzurePlanProductId] = ConfigurationManager.AppSettings["azurePlanProductId"];
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.AzurePlanSkuId] = ConfigurationManager.AppSettings["azurePlanSkuId"];
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.ActivationLinksCustomerKey] = ConfigurationManager.AppSettings["customerForActivationLinksDemo"];
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.SelectedSubscriptionKey] = ConfigurationManager.AppSettings["defaultSubscriptionId"];
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.LegacyAzureSubscriptionKey] = ConfigurationManager.AppSettings["azuresubscriptionId"];

            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.ActivationLinksSubscripitonKey] = ConfigurationManager.AppSettings["subscriptionForActivationLinksDemo"];
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.OrdersKey] = ConfigurationManager.AppSettings["modernOrderIdDemo"];

            // Set default values for agreements scenario
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.CustomerWithAgreements] = ConfigurationManager.AppSettings["customerWithAgreements"];
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.PartnerUserForAgreement] = ConfigurationManager.AppSettings["partnerUserIdForAgreement"];

            // Set default values for offer add on scenario
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.OfferWithAddonId] = ConfigurationManager.AppSettings["offerWithAddonId"];
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.OfferAddonOneId] = ConfigurationManager.AppSettings["offerAddonOneId"];
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.OfferAddonTwoId] = ConfigurationManager.AppSettings["offerAddonTwoId"];
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.SelectedInvoiceKey] = ConfigurationManager.AppSettings["selectedInvoiceKey"];
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.SelectedReceiptKey] = ConfigurationManager.AppSettings["selectedReceiptKey"];

            // Set default value for product family for product upgrades
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.ProductFamily] = ConfigurationManager.AppSettings["defaultProductFamily"];

            // Set default value for product family for product upgrades
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.OrderIdForAttachments] = ConfigurationManager.AppSettings["orderIdForAttachments"];
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.OrderAttachmentId] = ConfigurationManager.AppSettings["orderAttachmentId"];

            FeatureSamplesApplication.features.Add(new DownloadOrderAttachment());

            FeatureSamplesApplication.features.Add(new UploadOrderAttachments());

            FeatureSamplesApplication.features.Add(new GetOrderAttachments());

            FeatureSamplesApplication.features.Add(new CreateAndUpdateSelfServePolicies());
            FeatureSamplesApplication.features.Add(new GetSelfServePolicies());
            FeatureSamplesApplication.features.Add(new DeleteSelfServePolicies());
            FeatureSamplesApplication.features.Add(new TipActivateSubscription());
            FeatureSamplesApplication.features.Add(new GovernmentCommunityCloudCustomerCreation());

            if (testModernScenarios)
            {
                FeatureSamplesApplication.features.Add(new GetAgreementDetails());
                FeatureSamplesApplication.features.Add(new GetAgreementDocument());
                FeatureSamplesApplication.features.Add(new GetCustomerAgreements());
                FeatureSamplesApplication.features.Add(new GetDirectSignedCustomerAgreementStatus());
                FeatureSamplesApplication.features.Add(new CreateCustomerAgreement());
                FeatureSamplesApplication.features.Add(new GetOrdersByBillingCycleType());
                FeatureSamplesApplication.features.Add(new CreateAzureReservationOrder());
                FeatureSamplesApplication.features.Add(new GetOrderProvisioningStatus());
                FeatureSamplesApplication.features.Add(new GetOrderActivationLinks());
            }

            if (testDevicesScenarios)
            {
                FeatureSamplesApplication.features.Add(new GetAllConfigurationPolicies());
                FeatureSamplesApplication.features.Add(new CreateConfigurationPolicy());
                FeatureSamplesApplication.features.Add(new UpdateConfigurationPolicy());
                FeatureSamplesApplication.features.Add(new DeleteConfigurationPolicy());
                FeatureSamplesApplication.features.Add(new CreateDeviceBatch());
                FeatureSamplesApplication.features.Add(new CreateDevices());
                FeatureSamplesApplication.features.Add(new GetDevicesBatch());
                FeatureSamplesApplication.features.Add(new GetDevices());
                FeatureSamplesApplication.features.Add(new UpdateDevice());
                FeatureSamplesApplication.features.Add(new DeleteDevice());
                FeatureSamplesApplication.features.Add(new GetBatchUploadStatus());
            }

            if (testRICases)
            {
                FeatureSamplesApplication.features.Add(new GetOrder());
                FeatureSamplesApplication.features.Add(new CustomerPaging());
                FeatureSamplesApplication.features.Add(new CustomerFiltering());
                FeatureSamplesApplication.features.Add(new CustomerInformation());
                FeatureSamplesApplication.features.Add(new CustomerQualificationOperations());
            }

            if (testModernOfficeScenarios)
            {
                FeatureSamplesApplication.features.Add(new UpdateSubscriptionScheduleChange());
                FeatureSamplesApplication.features.Add(new UpdateSubscriptionNextChargeInstruction());
                FeatureSamplesApplication.features.Add(new UpdateOverage());
                FeatureSamplesApplication.features.Add(new PostPromotionEligibilities());
                FeatureSamplesApplication.features.Add(new CreateNewCommerceMigration());
                FeatureSamplesApplication.features.Add(new CreateNewCommerceMigrationSchedule());
                FeatureSamplesApplication.features.Add(new UpdateNewCommerceMigrationSchedule());
                FeatureSamplesApplication.features.Add(new CancelNewCommerceMigrationSchedule());
                FeatureSamplesApplication.features.Add(new CreateOrderWithCustomTermEndDate());
                FeatureSamplesApplication.features.Add(new CreateCartModernOffice());
                FeatureSamplesApplication.features.Add(new CheckoutCartWithCustomerUserUpn());
                FeatureSamplesApplication.features.Add(new CreateOrderWithCustomerUserUpn());
            }

            // Set default values for subscription registration
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.CustomerForRegistrationStatusDemo] = ConfigurationManager.AppSettings["customerForRegistrationDemo"];
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.SubscriptionForRegistrationStatusDemo] = ConfigurationManager.AppSettings["subscriptionForRegistrationDemo"];
            FeatureSamplesApplication.features.Add(new GetSubscriptionRegistrationStatus());
            FeatureSamplesApplication.features.Add(new RegisterSubscription());

            FeatureSamplesApplication.features.Add(new GetAllConfigurationPolicies());
            FeatureSamplesApplication.features.Add(new CreateConfigurationPolicy());
            FeatureSamplesApplication.features.Add(new UpdateConfigurationPolicy());
            FeatureSamplesApplication.features.Add(new DeleteConfigurationPolicy());
            FeatureSamplesApplication.features.Add(new CreateDeviceBatch());
            FeatureSamplesApplication.features.Add(new CreateDevices());
            FeatureSamplesApplication.features.Add(new GetDevicesBatch());
            FeatureSamplesApplication.features.Add(new GetDevices());
            FeatureSamplesApplication.features.Add(new UpdateDevice());
            FeatureSamplesApplication.features.Add(new DeleteDevice());
            FeatureSamplesApplication.features.Add(new GetBatchUploadStatus());

            FeatureSamplesApplication.features.Add(new GetOrders());

            if (testMultiTier)
            {
                FeatureSamplesApplication.features.Add(new GetPartnerRelationships(testMultiTier));
                FeatureSamplesApplication.features.Add(new PlaceOrderForVar(testMultiTier));
                FeatureSamplesApplication.features.Add(new CreateCustomerVar(testMultiTier));
            }

            FeatureSamplesApplication.features.Add(new GetAzureRateCard());
            FeatureSamplesApplication.features.Add(new GetAzureSharedRateCard());

            FeatureSamplesApplication.features.Add(new GetAuditRecords());
            FeatureSamplesApplication.features.Add(new SearchAuditRecords());
            FeatureSamplesApplication.features.Add(new SearchAuditRecordsByResourceType());
            FeatureSamplesApplication.features.Add(new SearchAuditRecordsByCustomerId());

            FeatureSamplesApplication.features.Add(new ServiceIncidentCollectionOperations());

            FeatureSamplesApplication.features.Add(new DeletePartnerCustomerRelationship());
            FeatureSamplesApplication.features.Add(new DeleteCustomerFromTipAccount());
            FeatureSamplesApplication.features.Add(new GetCustomerRelationshipRequest());
            FeatureSamplesApplication.features.Add(new GetMarketSpecificValidationDataByCountry());

            FeatureSamplesApplication.features.Add(new CheckDomainAvailability());

            FeatureSamplesApplication.features.Add(new GetCustomerBillingProfile());
            FeatureSamplesApplication.features.Add(new UpdateCustomerBillingProfile());
            FeatureSamplesApplication.features.Add(new GetCustomerCompanyProfile());

            // set a default catalog view
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.ProductTargetViewKey] = ConfigurationManager.AppSettings["defaultProductTargetView"];

            // set a default segment
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.ProductPromotionSegmentKey] = ConfigurationManager.AppSettings["defaultProductPromotionsSegment"];

            if (testRICases)
            {
                FeatureSamplesApplication.features.Add(new ProductPromotions());
                FeatureSamplesApplication.features.Add(new ProductPromotionDetails());
                FeatureSamplesApplication.features.Add(new ProductPromotionPaging());
                FeatureSamplesApplication.features.Add(new Products());
                FeatureSamplesApplication.features.Add(new ProductsByTargetSegment());
                FeatureSamplesApplication.features.Add(new ProductDetails());
                FeatureSamplesApplication.features.Add(new ProductPaging());
                FeatureSamplesApplication.features.Add(new Skus());
                FeatureSamplesApplication.features.Add(new SkusByTargetSegment());
                FeatureSamplesApplication.features.Add(new SkuDetails());
                FeatureSamplesApplication.features.Add(new Availabilities());
                FeatureSamplesApplication.features.Add(new AvailabilitiesByTargetSegment());
                FeatureSamplesApplication.features.Add(new AvailabilityDetails());
                FeatureSamplesApplication.features.Add(new InventoryCheck());

                //// set a default customer id who is eligible for some products

                FeatureSamplesApplication.applicationState[FeatureSamplesApplication.CustomerWithProducts] = ConfigurationManager.AppSettings["customerWithProducts"];

                FeatureSamplesApplication.features.Add(new CustomerProducts());
                FeatureSamplesApplication.features.Add(new CustomerProductsByTargetSegment());
                FeatureSamplesApplication.features.Add(new CustomerProductDetails());
                FeatureSamplesApplication.features.Add(new CustomerSkus());
                FeatureSamplesApplication.features.Add(new CustomerSkusByTargetSegment());
                FeatureSamplesApplication.features.Add(new CustomerSkuDetails());
                FeatureSamplesApplication.features.Add(new CustomerAvailabilities());
                FeatureSamplesApplication.features.Add(new CustomerAvailabilitiesByTargetSegment());
                FeatureSamplesApplication.features.Add(new CustomerAvailabilityDetails());
            }

            FeatureSamplesApplication.features.Add(new OfferCategories());
            FeatureSamplesApplication.features.Add(new Offers());
            FeatureSamplesApplication.features.Add(new CreateOrder());

            FeatureSamplesApplication.features.Add(new GetOrder());
            FeatureSamplesApplication.features.Add(new UpdateOrder());
            FeatureSamplesApplication.features.Add(new UpdateOrderForAddonOfferPurchase());

            FeatureSamplesApplication.features.Add(new Subscriptions());
            FeatureSamplesApplication.features.Add(new SubscriptionsByOrder());
            FeatureSamplesApplication.features.Add(new SubscriptionsByPartner());
            FeatureSamplesApplication.features.Add(new GetSubscription());
            FeatureSamplesApplication.features.Add(new SubscriptionAddons());
            FeatureSamplesApplication.features.Add(new UpgradeSubscription());
            FeatureSamplesApplication.features.Add(new TransitionSubscription());
            FeatureSamplesApplication.features.Add(new GetSubscriptionTransitions());
            FeatureSamplesApplication.features.Add(new UpdateSubscription());
            FeatureSamplesApplication.features.Add(new Entitlements());

            FeatureSamplesApplication.features.Add(new OfferPaging());
            FeatureSamplesApplication.features.Add(new OffersByCategory());
            FeatureSamplesApplication.features.Add(new OffersByCategoryPaged());
            FeatureSamplesApplication.features.Add(new OfferDetails());

            FeatureSamplesApplication.features.Add(new CustomerManagedServices());
            FeatureSamplesApplication.features.Add(new CustomerCreation());
            FeatureSamplesApplication.features.Add(new GovernmentCommunityCloudCustomerCreation());

            // set default MPN Id for Partner Profiles
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.SelectedMpnId] = ConfigurationManager.AppSettings["defaultMpnId"];
            FeatureSamplesApplication.features.Add(new GetMpnProfile());
            FeatureSamplesApplication.features.Add(new GetLegalBusinessProfile());
            FeatureSamplesApplication.features.Add(new GetSupportProfile());
            FeatureSamplesApplication.features.Add(new GetOrganizationProfile());
            FeatureSamplesApplication.features.Add(new GetBillingProfile());
            FeatureSamplesApplication.features.Add(new UpdateSupportProfile());
            FeatureSamplesApplication.features.Add(new UpdateOrganizationProfile());
            FeatureSamplesApplication.features.Add(new UpdateBillingProfile());
            FeatureSamplesApplication.features.Add(new UpdateLegalBusinessProfile());
            FeatureSamplesApplication.features.Add(new PartnerServiceRequestGet());
            FeatureSamplesApplication.features.Add(new PartnerServiceRequestsGetAll());
            FeatureSamplesApplication.features.Add(new PartnerServiceRequestUpdate());
            FeatureSamplesApplication.features.Add(new PartnerServiceRequestsSimpleQuerySearch());
            FeatureSamplesApplication.features.Add(new PartnerServiceRequestsPagination());
            FeatureSamplesApplication.features.Add(new PartnerServiceRequestsFiltering());

            // set Tenant Id for Compliance
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.SelectedTenantId] =
                ConfigurationManager.AppSettings["defaultTenantId"];
            FeatureSamplesApplication.features.Add(new GetAgreementSignatureStatus());

            // set a default customer id who has service requests for customer service request operations
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.CustomerForServiceRequests] = ConfigurationManager.AppSettings["customerWithServiceRequests"];

            // set a default service request id for customer service request operations
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.CustomerServiceRequest] = ConfigurationManager.AppSettings["customerServiceRequestId"];

            FeatureSamplesApplication.features.Add(new CustomerServiceRequestsGetAll());
            FeatureSamplesApplication.features.Add(new CustomerServiceRequestGet());
            FeatureSamplesApplication.features.Add(new CustomerServiceRequestUpdate());
            FeatureSamplesApplication.features.Add(new CustomerServiceRequestsPagination());
            FeatureSamplesApplication.features.Add(new CustomerServiceRequestsSimpleQuerySearch());

            FeatureSamplesApplication.features.Add(new InvoicePaging());
            FeatureSamplesApplication.features.Add(new GetInvoiceSummary());
            FeatureSamplesApplication.features.Add(new GetInvoice());
            FeatureSamplesApplication.features.Add(new InvoiceLineItemsPaging());
            FeatureSamplesApplication.features.Add(new GetInvoiceSummaries());
            FeatureSamplesApplication.features.Add(new GetInvoiceStatement());
            FeatureSamplesApplication.features.Add(new GetInvoiceTaxReceiptStatement());
            FeatureSamplesApplication.features.Add(new GetEstimatesLinks());
            FeatureSamplesApplication.features.Add(new GetUsageLineItemsForOpenPeriodPaging());
            FeatureSamplesApplication.features.Add(new GetBillingLineItemsForOpenPeriodPaging());
            FeatureSamplesApplication.features.Add(new GetUsageLineItemsForClosePeriodPaging());
            FeatureSamplesApplication.features.Add(new GetInvoiceTaxReceiptStatement());

            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.CustomerForUsageDemo] = ConfigurationManager.AppSettings["customerForUsageDemo"];
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.SubscriptionForUsageDemo] = ConfigurationManager.AppSettings["subscriptionForUsageDemo"];
            FeatureSamplesApplication.features.Add(new PartnerUsageSummary());
            FeatureSamplesApplication.features.Add(new AllCustomerUsageRecords());
            FeatureSamplesApplication.features.Add(new CustomerSpendingBudget());
            FeatureSamplesApplication.features.Add(new CustomerUsageSummary());
            FeatureSamplesApplication.features.Add(new SubscriptionUsageRecords());
            FeatureSamplesApplication.features.Add(new SubscriptionUsageSummary());
            FeatureSamplesApplication.features.Add(new SubscriptionUsageRecordsByResource());
            FeatureSamplesApplication.features.Add(new SubscriptionUsageRecordsByMeter());

            FeatureSamplesApplication.features.Add(new CustomerUsersGet());
            FeatureSamplesApplication.features.Add(new CustomerUsersPaging());
            FeatureSamplesApplication.features.Add(new CustomerUserCreate());
            FeatureSamplesApplication.features.Add(new CustomerUserDelete());
            FeatureSamplesApplication.features.Add(new CustomerUserUpdate());
            FeatureSamplesApplication.features.Add(new GetCustomerUserDirectoryRoles());
            FeatureSamplesApplication.features.Add(new CustomerUserInformation());

            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.CustomerWithProducts] = ConfigurationManager.AppSettings["customerWithProducts"];
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.DirectResellerIntegrationTestAccountUser] = ConfigurationManager.AppSettings["directResellerIntegrationTestAccountUser"];
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.DirectResellerTestAccountCustomerTenantId] = ConfigurationManager.AppSettings["directResellerTestAccountCustomerTenantId"];
            FeatureSamplesApplication.applicationState[FeatureSamplesApplication.DirectResellerTestAccountUserId] = ConfigurationManager.AppSettings["directResellerTestAccountUserId"];

            FeatureSamplesApplication.features.Add(new GetCustomerGroup1SubscribedSkus());
            FeatureSamplesApplication.features.Add(new GetCustomerGroup2SubscribedSkus());
            FeatureSamplesApplication.features.Add(new GetCustomerGroup1AndGroup2SubscribedSkus());
            FeatureSamplesApplication.features.Add(new CustomerSubscribedSkus());
            FeatureSamplesApplication.features.Add(new GetCustomerDirectoryRoles());
            FeatureSamplesApplication.features.Add(new AddUserMemberToDirectoryRole());
            FeatureSamplesApplication.features.Add(new GetCustomerDirectoryRoleUserMembers());
            FeatureSamplesApplication.features.Add(new RemoveCustomerUserMemberFromDirectoryRole());
            FeatureSamplesApplication.features.Add(new CustomerUserAssignedGroup1Licenses());
            FeatureSamplesApplication.features.Add(new CustomerUserAssignedGroup2Licenses());
            FeatureSamplesApplication.features.Add(new CustomerUserAssignedGroup1AndGroup2Licenses());

            // Adding to the customerUserLicenseFeatures separately so that we can use our own test account
            FeatureSamplesApplication.customerUserLicenseFeatures.Add(new CustomerUserAssignMinecraftLicenses());
            FeatureSamplesApplication.customerUserLicenseFeatures.Add(new CustomerUserAssignGroup1Licenses());
            FeatureSamplesApplication.customerUserLicenseFeatures.Add(new CustomerUserAssignLicenses());
            FeatureSamplesApplication.customerUserLicenseFeatures.Add(new CustomerUserAssignedLicenses());

            FeatureSamplesApplication.features.Add(new CustomerUsersGetInactive());
            FeatureSamplesApplication.features.Add(new CustomerUserRestore());
            FeatureSamplesApplication.features.Add(new SortedCustomerUsers());
            FeatureSamplesApplication.applicationState[CustomerWithServiceCosts] = ConfigurationManager.AppSettings["customerWithServiceCosts"];
            FeatureSamplesApplication.features.Add(new CustomerServiceCostSummary());
            FeatureSamplesApplication.features.Add(new CustomerServiceCostLineItems());
            FeatureSamplesApplication.features.Add(new PartnerLicensesDeploymentInsights());
            FeatureSamplesApplication.features.Add(new PartnerLicensesUsageInsights());

            FeatureSamplesApplication.applicationState[CustomerWithTrialOffer] = ConfigurationManager.AppSettings["customerWithTrialOffer"];
            FeatureSamplesApplication.applicationState[TrialOfferId] = ConfigurationManager.AppSettings["trialOfferId"];
            FeatureSamplesApplication.features.Add(new ConvertSubscription());

            FeatureSamplesApplication.features.Add(new GetSubscriptionSupportContact());
            FeatureSamplesApplication.features.Add(new UpdateSubscriptionSupportContact());
            FeatureSamplesApplication.features.Add(new GetSubscriptionActivationLinks());

            // all cart scenarios
            if (testRICases)
            {
                FeatureSamplesApplication.features.Add(new CreateCart());
                FeatureSamplesApplication.features.Add(new UpdateCart());
                FeatureSamplesApplication.features.Add(new CheckoutCart());
            }

            // validations
            FeatureSamplesApplication.features.Add(new AddressValidation());
            FeatureSamplesApplication.features.Add(new ValidationStatus());
            FeatureSamplesApplication.features.Add(new CreateCart());
            FeatureSamplesApplication.features.Add(new UpdateCart());
            FeatureSamplesApplication.features.Add(new CheckoutCart());
            FeatureSamplesApplication.features.Add(new CreateCartWithAddons());
            FeatureSamplesApplication.features.Add(new CheckoutCartWithAddons());
            FeatureSamplesApplication.features.Add(new CreateCartAddonWithExistingSubscription());
            FeatureSamplesApplication.features.Add(new CheckoutCartWithAddons());
            FeatureSamplesApplication.features.Add(new SubscriptionProvisioningStatus());
            FeatureSamplesApplication.features.Add(new CreateCartWithDatabricks());
            FeatureSamplesApplication.features.Add(new CheckoutCart());

            // azureUtilizationRecords
            FeatureSamplesApplication.features.Add(new GetAzureSubscriptionUtilization());

            if (testModernAzureScenarios)
            {
                FeatureSamplesApplication.features.Add(new ProductUpgradeEligibility());
                FeatureSamplesApplication.features.Add(new ProductUpgrade());
                FeatureSamplesApplication.features.Add(new ProductUpgradeStatus());
                FeatureSamplesApplication.features.Add(new AzurePlanEntitlements());
                FeatureSamplesApplication.features.Add(new CancelAzurePlanEntitlement());
                FeatureSamplesApplication.features.Add(new GetAzurePlanEntitlement());

                FeatureSamplesApplication.features.Add(new SkuDetailsByReservationScope());
                FeatureSamplesApplication.features.Add(new CustomerSkuDetailsByReservationScope());
                FeatureSamplesApplication.features.Add(new SkusByReservationScope());
                FeatureSamplesApplication.features.Add(new SkusByTargetSegmentByReservationScope());
                FeatureSamplesApplication.features.Add(new CustomerSkusByReservationScope());
                FeatureSamplesApplication.features.Add(new CustomerSkusByTargetSegmentByReservationScope());

                FeatureSamplesApplication.features.Add(new CustomerProductDetailsByReservationScope());
                FeatureSamplesApplication.features.Add(new CustomerProductsByTargetSegmentByReservationScope());
                FeatureSamplesApplication.features.Add(new CustomerProductsByReservationScope());
                FeatureSamplesApplication.features.Add(new ProductDetailsByReservationScope());
                FeatureSamplesApplication.features.Add(new ProductsByReservationScope());
                FeatureSamplesApplication.features.Add(new ProductsByTargetSegmentByReservationScope());

                FeatureSamplesApplication.features.Add(new CustomerAvailabilitiesByReservationScope());
                FeatureSamplesApplication.features.Add(new CustomerAvailabilitiesByTargetSegmentByReservationScope());
                FeatureSamplesApplication.features.Add(new AvailabilitiesByTargetSegmentByReservationScope());
                FeatureSamplesApplication.features.Add(new AvailabilitiesByReservationScope());
            }
        }

        /// <summary>
        /// Logs into Azure active directory.
        /// </summary>
        /// <param name="accountClientId">The Account Client Id.</param>
        /// <param name="accountUserName">The Account User Name.</param>
        /// <param name="accountPassWord">The Account Password.</param>
        /// <returns>The authentication result.</returns>
        private static async Task<AuthenticationResult> LoginToAad(string accountClientId = null, string accountUserName = null, string accountPassWord = null)
        {
            // read AAD configuration
            string authority = ConfigurationManager.AppSettings["aad.authority"];
            string organizationsDomain = ConfigurationManager.AppSettings["aad.organizationsDomain"];
            string resourceUrl = ConfigurationManager.AppSettings["aad.resourceUrl"];
            string clientId = ConfigurationManager.AppSettings[accountClientId ?? "aad.clientId"];
            string userName = ConfigurationManager.AppSettings[accountUserName ?? "aad.userName"];
            string password = ConfigurationManager.AppSettings[accountPassWord ?? "aad.password"];

            var addAuthority = new UriBuilder(authority)
            {
                Path = organizationsDomain
            };

            var scopes = new string[] { $"{resourceUrl}/.default" };

            IPublicClientApplication publicClientApplication = PublicClientApplicationBuilder.Create(clientId)
                    .WithAuthority(addAuthority.Uri.AbsoluteUri)
                    .Build();

            var accounts = await publicClientApplication.GetAccountsAsync();

            if (accounts.Any())
            {
                return await publicClientApplication.AcquireTokenSilent(scopes, accounts.FirstOrDefault())
                                  .ExecuteAsync();
            }
            else
            {
                try
                {
                    var securePassword = new SecureString();
                    foreach (char c in password)
                    {
                        securePassword.AppendChar(c);
                    }

                    var res = await publicClientApplication.AcquireTokenByUsernamePassword(scopes, userName, securePassword)
                                       .ExecuteAsync();

                    return res;
                }
                catch (MsalException e)
                {
                    throw e;
                }
            }
        }
    }
}