// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests.CustomerServiceCosts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Customers.ServiceCosts;
    using Customers.ServiceCosts.Fakes;
    using Models;
    using Models.ServiceCosts;
    using Moq;
    using Network;
    using Network.Fakes;
    using QualityTools.Testing.Fakes;
    using RequestContext;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit tests for ServiceCostsCollectionOperations.
    /// </summary>
    [TestClass]
    public class ServiceCostsCollectionOperationsTests
    {
        /// <summary>
        /// The expected credentials.
        /// </summary>
        private static Mock<IPartnerCredentials> mockCredentials;

        /// <summary>
        /// The expected context.
        /// </summary>
        private static Mock<IRequestContext> mockRequestContext;

        /// <summary>
        /// The customer service costs collection operations.
        /// </summary>
        private static ICustomerServiceCostsCollection customerServiceCostsCollectionOperations;        

        /// <summary>
        /// The expected customer id.
        /// </summary>
        private static string expectedCustomerId = "1";

        /// <summary>
        /// The expected service costs billing period
        /// </summary>
        private static ServiceCostsBillingPeriod expecxtedServiceCostsBillingPeriod = ServiceCostsBillingPeriod.MostRecent;

        /// <summary>
        /// Initializes the test suite.
        /// </summary>
        /// <param name="testContext">The test context.</param>
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            mockCredentials = new Mock<IPartnerCredentials>();
            mockCredentials.Setup(credentials => credentials.PartnerServiceToken).Returns("Fake Token");
            mockCredentials.Setup(credentials => credentials.ExpiresAt).Returns(DateTimeOffset.MaxValue);

            mockRequestContext = new Mock<IRequestContext>();
            mockRequestContext.Setup(context => context.CorrelationId).Returns(Guid.NewGuid());
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> partnerOperations = new Mock<IPartner>();
            partnerOperations.Setup(partner => partner.Credentials).Returns(mockCredentials.Object);
            partnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            customerServiceCostsCollectionOperations = new CustomerServiceCostsCollectionOperations(partnerOperations.Object, expectedCustomerId);
        }

        /// <summary>
        /// Ensures that getting Service Cost Collection Operations object by billing period works as expected.
        /// </summary>
        [TestMethod]
        public void ServiceCostsCollectionOperationsTests_VerifyByBillingPeriodNavigation()
        {
            using (ShimsContext.Create())
            {
                // Route all CustomerServiceCostsCollectionOperations constructors to our handler below
                ShimCustomerServiceCostsCollectionOperations.ConstructorIPartnerString =
                    (CustomerServiceCostsCollectionOperations operations, IPartner partnerOperations, string customerId) =>
                    {
                        // ensure the customer collection operations pass in the right values to the customer operations
                        Assert.AreEqual(customerId, expectedCustomerId);
                        Assert.AreEqual(partnerOperations.Credentials, mockCredentials.Object);
                        Assert.AreEqual(partnerOperations.RequestContext, mockRequestContext.Object);
                    };

                // Invoke by billing period.
                Assert.IsNotNull(customerServiceCostsCollectionOperations.ByBillingPeriod(ServiceCostsBillingPeriod.MostRecent));
            }
        }
        
        /// <summary>
        /// Ensures that getting Service Cost Summary Operations object works as expected.
        /// </summary>
        [TestMethod]
        public void ServiceCostsCollectionOperationsTests_VerifySummaryNavigation()
        {
            using (ShimsContext.Create())
            {
                // Route all CustomerServiceCostsCollectionOperations constructors to our handler below
                ShimServiceCostSummaryOperations.ConstructorIPartnerTupleOfStringString =
                    (ServiceCostSummaryOperations operations, IPartner partnerOperations, Tuple<string, string> context) =>
                    {
                        // ensure the customer collection operations pass in the right values to the customer operations
                        Assert.AreEqual(context.Item1, expectedCustomerId);
                        Assert.AreEqual(context.Item2, expecxtedServiceCostsBillingPeriod.ToString());
                        Assert.AreEqual(partnerOperations.Credentials, mockCredentials.Object);
                        Assert.AreEqual(partnerOperations.RequestContext, mockRequestContext.Object);
                    };

                // Invoke by billing period.
                Assert.IsNotNull(customerServiceCostsCollectionOperations.ByBillingPeriod(ServiceCostsBillingPeriod.MostRecent).Summary);
            }
        }

        /// <summary>
        /// Ensures that getting Service Cost Line Items Operations object works as expected.
        /// </summary>
        [TestMethod]
        public void ServiceCostsCollectionOperationsTests_VerifyLineItemNavigation()
        {
            using (ShimsContext.Create())
            {
                // Route all CustomerServiceCostsCollectionOperations constructors to our handler below
                ShimServiceCostLineItemsOperations.ConstructorIPartnerTupleOfStringString =
                    (ServiceCostLineItemsOperations operations, IPartner partnerOperations, Tuple<string, string> context) =>
                    {
                        // ensure the customer collection operations pass in the right values to the customer operations
                        Assert.AreEqual(context.Item1, expectedCustomerId);
                        Assert.AreEqual(context.Item2, expecxtedServiceCostsBillingPeriod.ToString());
                        Assert.AreEqual(partnerOperations.Credentials, mockCredentials.Object);
                        Assert.AreEqual(partnerOperations.RequestContext, mockRequestContext.Object);
                    };

                // Invoke by billing period.
                Assert.IsNotNull(customerServiceCostsCollectionOperations.ByBillingPeriod(ServiceCostsBillingPeriod.MostRecent).LineItems);
            }
        }

        /// <summary>
        /// Ensures that get summary passes in the right values to the proxy and actually calls the proxy network methods.
        /// </summary>
        [TestMethod]
        public void ServiceCostsCollectionOperationsTests_VerifyGetSummary()
        {
            ServiceCostsSummary expectedServiceCostsSummary = new ServiceCostsSummary()
            {
                CustomerId = expectedCustomerId,
            };

            int numberOfCalls = 0;

            using (ShimsContext.Create())
            {
                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<ServiceCostsSummary, ServiceCostsSummary>.AllInstances.GetAsync
                    = (PartnerServiceProxy<ServiceCostsSummary, ServiceCostsSummary> jsonProxy) =>
                    {
                        numberOfCalls++;

                        // verify request properties
                        Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                        Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                        Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                        Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.GetCustomerServiceCostsSummary.Path, expectedCustomerId, ServiceCostsBillingPeriod.MostRecent.ToString()), jsonProxy.ResourcePath);

                        return Task.FromResult<ServiceCostsSummary>(expectedServiceCostsSummary);
                    };

                var summary = customerServiceCostsCollectionOperations.ByBillingPeriod(ServiceCostsBillingPeriod.MostRecent).Summary.Get();

                Assert.IsNotNull(summary, "Service costs summary should not be empty or null");
                Assert.AreEqual(summary.CustomerId, expectedServiceCostsSummary.CustomerId);
                Assert.AreEqual(numberOfCalls, 1);
            }
        }

        /// <summary>
        /// Ensures that get line items passes in the right values to the proxy and actually calls the proxy network methods.
        /// </summary>
        [TestMethod]
        public void ServiceCostsCollectionOperationsTests_VerifyGetLineItems()
        {
            ResourceCollection<ServiceCostLineItem> expectedServiceCostsLineItems = new ResourceCollection<ServiceCostLineItem>(new List<ServiceCostLineItem>() { new ServiceCostLineItem() });
            int numberOfCalls = 0;

            using (ShimsContext.Create())
            {
                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<ServiceCostLineItem, ResourceCollection<ServiceCostLineItem>>.AllInstances.GetAsync
                    = (PartnerServiceProxy<ServiceCostLineItem, ResourceCollection<ServiceCostLineItem>> jsonProxy) =>
                    {
                        numberOfCalls++;

                        // verify request properties
                        Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                        Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                        Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                        Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.GetCustomerServiceCostLineItems.Path, expectedCustomerId, ServiceCostsBillingPeriod.MostRecent.ToString()), jsonProxy.ResourcePath);
                        
                        return Task.FromResult<ResourceCollection<ServiceCostLineItem>>(expectedServiceCostsLineItems);
                    };

                var lineItems = customerServiceCostsCollectionOperations.ByBillingPeriod(ServiceCostsBillingPeriod.MostRecent).LineItems.Get();

                Assert.IsNotNull(lineItems, "Service costs summary should not be empty or null");
                Assert.AreEqual(lineItems.Items.Count(), 1);
                Assert.AreEqual(numberOfCalls, 1);
            }
        }
    }
}
