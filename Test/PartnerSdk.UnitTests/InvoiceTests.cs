// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Invoices;
    using Models;
    using Models.Invoices;
    using Models.JsonConverters;
    using Models.Query;
    using Moq;
    using Network;
    using Network.Fakes;
    using QualityTools.Testing.Fakes;
    using RequestContext;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit tests for InvoiceCollectionOperations and InvoiceOperations
    /// </summary>
    [TestClass]
    public class InvoiceTests
    {
        /// <summary>
        /// URL Format for pagination
        /// </summary>
        private const string PagedInvoicingUrlFormat = "/invoicing?size={0}&offset={1}";

        /// <summary>
        /// Test Invoice
        /// </summary>
        private static readonly Invoice ExpectedInvoice = new Invoice
        {
            CurrencyCode = "USD",
            Id = "INV1234",
            InvoiceDate = new DateTime(),
            InvoiceDetails = null,
            PaidAmount = 0.00M,
            TotalCharges = 1000.00M,
            PdfDownloadLink = new Uri("http://test.com")
        };

        /// <summary>
        /// Test Invoice Summary
        /// </summary>
        private static readonly InvoiceSummary ExpectedSummary = new InvoiceSummary
        {
            AccountingDate = new DateTime(),
            BalanceAmount = 2000.00M,
            CurrencyCode = "USD",
            FirstInvoiceCreationDate = new DateTime()
        };
        
        /// <summary>
        /// The expected credentials.
        /// </summary>
        private static Mock<IPartnerCredentials> mockCredentials;

        /// <summary>
        /// The expected context.
        /// </summary>
        private static Mock<IRequestContext> mockRequestContext;

        /// <summary>
        /// The invoice collection operations.
        /// </summary>
        private static IInvoiceCollection invoiceCollectionOperations;

        /// <summary>
        /// The invoice operations.
        /// </summary>
        private static IInvoice invoiceOperations;

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

            invoiceCollectionOperations = new InvoiceCollectionOperations(partnerOperations.Object);
            invoiceOperations = new InvoiceOperations(partnerOperations.Object, ExpectedInvoice.Id);
        }

        #region InvoiceCollectionOperations Tests

        /// <summary>
        /// Test Get Invoices success path tests.
        /// </summary>
        [TestMethod]
        public void InvoiceTests_GetInvoicesTestVerifySuccessPath()
        {
            ResourceCollection<Invoice> testInvoices = GenerateTestInvoices();

            using (ShimsContext.Create())
            {
                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<Invoice, ResourceCollection<Invoice>>.AllInstances.GetAsync
                    = (PartnerServiceProxy<Invoice, ResourceCollection<Invoice>> jsonProxy) =>
                    {
                        // verify request properties
                        Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                        Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                        Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                        Assert.AreEqual(PartnerService.Instance.Configuration.Apis.GetInvoices.Path, jsonProxy.ResourcePath);

                        // verify that the seek operation was added to the uri parameters
                        Assert.AreEqual(jsonProxy.UriParameters.Count, 0);

                        return Task.FromResult<ResourceCollection<Invoice>>(testInvoices);
                    };

                var invoiceCollection = invoiceCollectionOperations.Get();

                Assert.IsNotNull(invoiceCollection, "Invoices should not be empty or null");
                Assert.IsTrue(invoiceCollection.Items.Any(), "Invoices should not be empty or null");
                Assert.AreEqual(testInvoices.Items.Count(), invoiceCollection.Items.Count());

                var testInvoice = testInvoices.Items.First();
                var actualInvoice = invoiceCollection.Items.First(a => a.Id == testInvoice.Id);

                CompareInvoices(testInvoice, actualInvoice);
            }
        }

        /// <summary>
        /// Get Invoices Test - Pagination Requests
        /// </summary>
        /// <returns>async task.</returns>
        [TestMethod]
        public async Task InvoiceTests_GetInvoicesTestVerifyPaginationRequests()
        {
            int expectedSize = 2;
            int expectedOffset = 0;

            using (ShimsContext.Create())
            {
                int numberOfCalls = 0;

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<Invoice, ResourceCollection<Invoice>>.AllInstances.GetAsync = (PartnerServiceProxy<Invoice, ResourceCollection<Invoice>> jsonProxy) =>
                {
                    // increment the number of the calls
                    numberOfCalls++;

                    // verify request properties
                    Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                    Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                    Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                    Assert.AreEqual(PartnerService.Instance.Configuration.Apis.GetInvoices.Path, jsonProxy.ResourcePath);
                    Assert.IsInstanceOfType(jsonProxy.JsonConverter, typeof(ResourceCollectionConverter<Invoice>));

                    // verify that offset and page size are added to the uri parameters
                    Assert.AreEqual(jsonProxy.UriParameters.Count, 2);

                    IEnumerator<KeyValuePair<string, string>> enumerator = jsonProxy.UriParameters.GetEnumerator();
                    Assert.IsTrue(enumerator.MoveNext());
                    Assert.AreEqual(enumerator.Current.Key, PartnerService.Instance.Configuration.Apis.GetInvoices.Parameters.Size);
                    Assert.AreEqual(enumerator.Current.Value, expectedSize.ToString());
                    Assert.IsTrue(enumerator.MoveNext());
                    Assert.AreEqual(enumerator.Current.Key, PartnerService.Instance.Configuration.Apis.GetInvoices.Parameters.Offset);
                    Assert.AreEqual(enumerator.Current.Value, expectedOffset.ToString());

                    return Task.FromResult<ResourceCollection<Invoice>>(null);
                };

                // invoke query and query async
                invoiceCollectionOperations.Query(QueryFactory.Instance.BuildIndexedQuery(expectedSize, expectedOffset));
                await invoiceCollectionOperations.QueryAsync(QueryFactory.Instance.BuildIndexedQuery(expectedSize, expectedOffset));

                // ensure that both the Query calls invoked the proxy get async method
                Assert.AreEqual(numberOfCalls, 2);
            }            
        }

        /// <summary>
        /// Get Invoices Test - Verify invoice enumerator
        /// </summary>
        [TestMethod]
        public void InvoiceTests_GetInvoicesTestVerifyInvoiceEnumerator()
        {
            int testSize = 4;
            int sampleSize = 10;
            ResourceCollection<Invoice> testInvoices = GenerateTestInvoices(sampleSize);

            using (ShimsContext.Create())
            {
                int numberOfCalls = 0;
                int testOffset = 0;

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<Invoice, ResourceCollection<Invoice>>.AllInstances.GetAsync = (PartnerServiceProxy<Invoice, ResourceCollection<Invoice>> jsonProxy) =>
                {
                    // increment the number of the calls
                    numberOfCalls++;

                    // verify request properties
                    Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                    Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                    Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                    Assert.AreEqual(PartnerService.Instance.Configuration.Apis.GetInvoices.Path, jsonProxy.ResourcePath);
                    Assert.IsInstanceOfType(jsonProxy.JsonConverter, typeof(ResourceCollectionConverter<Invoice>));

                    // verify that offset and page size are added to the uri parameters
                    Assert.AreEqual(jsonProxy.UriParameters.Count, 2);

                    IEnumerator<KeyValuePair<string, string>> enumerator = jsonProxy.UriParameters.GetEnumerator();
                    Assert.IsTrue(enumerator.MoveNext());
                    int size = int.Parse(enumerator.Current.Value);
                    Assert.IsTrue(enumerator.MoveNext());
                    int offset = int.Parse(enumerator.Current.Value);

                    return Task.FromResult(GeneratePagedInvoices(testInvoices, size, offset));
                };

                // divert calls to GetAsync when calling for Next page
                ShimPartnerServiceProxy<ResourceCollection<Invoice>, ResourceCollection<Invoice>>.AllInstances.GetAsync = (PartnerServiceProxy<ResourceCollection<Invoice>, ResourceCollection<Invoice>> jsonProxy) =>
                {
                    // increment the number of the calls
                    numberOfCalls++;
                    testOffset += testSize;
                    
                    // verify request properties
                    Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                    Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                    Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                    Assert.AreEqual(string.Format(CultureInfo.InvariantCulture, PagedInvoicingUrlFormat, testSize, testOffset), jsonProxy.ResourcePath);
                    Assert.IsInstanceOfType(jsonProxy.JsonConverter, typeof(ResourceCollectionConverter<Invoice>));

                    return Task.FromResult(GeneratePagedInvoices(testInvoices, testSize, testOffset));
                };

                ResourceCollection<Invoice> invoiceCollection = invoiceCollectionOperations.Query(QueryFactory.Instance.BuildIndexedQuery(testSize, testOffset));
                Assert.IsNotNull(invoiceCollection);
                Assert.IsTrue(invoiceCollection.Items.Any());
                Assert.AreEqual(invoiceCollection.TotalCount, testSize);

                var actualInvoice = new List<Invoice>(invoiceCollection.Items);                
                
                IPartner scopedPartnerOperations = new PartnerOperations(mockCredentials.Object, mockRequestContext.Object);
                var invoiceEnumerator = scopedPartnerOperations.Enumerators.Invoices.Create(invoiceCollection);

                // Validate for the first page
                Assert.IsTrue(invoiceEnumerator.HasValue);
                Assert.IsTrue(invoiceEnumerator.IsFirstPage);
                Assert.IsFalse(invoiceEnumerator.IsLastPage);

                // Validate for the second page
                invoiceEnumerator.Next();
                invoiceCollection = invoiceEnumerator.Current;

                Assert.IsTrue(invoiceEnumerator.HasValue);
                Assert.IsFalse(invoiceEnumerator.IsFirstPage);
                Assert.IsFalse(invoiceEnumerator.IsLastPage);
                Assert.IsNotNull(invoiceCollection);

                actualInvoice.AddRange(invoiceCollection.Items);

                // Validate for the third (last) page
                invoiceEnumerator.Next();
                invoiceCollection = invoiceEnumerator.Current;

                Assert.IsTrue(invoiceEnumerator.HasValue);
                Assert.IsFalse(invoiceEnumerator.IsFirstPage);
                Assert.IsTrue(invoiceEnumerator.IsLastPage);
                Assert.IsNotNull(invoiceCollection);

                actualInvoice.AddRange(invoiceCollection.Items);

                // try to fetch next value
                invoiceEnumerator.Next();
                Assert.IsFalse(invoiceEnumerator.HasValue);

                // expecting 3 calls in total
                Assert.AreEqual(3, numberOfCalls);
                Assert.AreEqual(sampleSize, actualInvoice.Count);
            }
        }

        /// <summary>
        /// Test Get invoice summary success path tests.
        /// </summary>
        [TestMethod]
        public void InvoiceTests_GetInvoiceSummaryTestVerifySuccessPath()
        {
            using (ShimsContext.Create())
            {
                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<InvoiceSummary, InvoiceSummary>.AllInstances.GetAsync
                    = (PartnerServiceProxy<InvoiceSummary, InvoiceSummary> jsonProxy) =>
                    {
                        // verify request properties
                        Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                        Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                        Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                        Assert.AreEqual(PartnerService.Instance.Configuration.Apis.GetInvoiceSummary.Path, jsonProxy.ResourcePath);

                        return Task.FromResult<InvoiceSummary>(ExpectedSummary);
                    };

                var invoiceSummary = invoiceCollectionOperations.Summary.Get();
                Assert.IsNotNull(invoiceSummary, "Invoice summary should not be empty or null");
                Assert.AreEqual(ExpectedSummary.AccountingDate, invoiceSummary.AccountingDate);
                Assert.AreEqual(ExpectedSummary.BalanceAmount, invoiceSummary.BalanceAmount);
                Assert.AreEqual(ExpectedSummary.CurrencyCode, invoiceSummary.CurrencyCode);
                Assert.AreEqual(ExpectedSummary.FirstInvoiceCreationDate, invoiceSummary.FirstInvoiceCreationDate);
            }
        }

        /// <summary>
        /// Test Get Invoice By Id - Success Path
        /// </summary>
        [TestMethod]
        public void InvoiceTests_GetInvoiceByIdTestVerifySuccessPath()
        {
            var invoiceOperations = invoiceCollectionOperations.ById(ExpectedInvoice.Id);
            Assert.IsNotNull(invoiceOperations);
        }

        /// <summary>
        /// Test Get Invoice By Id - Invalid Invoice Id
        /// </summary>
        [TestMethod]
        public void InvoiceTests_GetInvoiceByIdTestInvalidInvoiceId()
        {
            try
            {
                invoiceCollectionOperations.ById(null);
                Assert.Fail("Expecting an exception.");
            }
            catch (ArgumentNullException)
            {
            }

            try
            {
                invoiceCollectionOperations.ById(string.Empty);
                Assert.Fail("Expecting an exception.");
            }
            catch (ArgumentException)
            {
            }
        }
        
        #endregion

        #region InvoiceOperations Tests

        /// <summary>
        /// Test Get Invoice success path tests.
        /// </summary>
        [TestMethod]
        public void InvoiceTests_GetInvoiceTestVerifySuccessPath()
        {
            using (ShimsContext.Create())
            {
                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<Invoice, Invoice>.AllInstances.GetAsync
                    = (PartnerServiceProxy<Invoice, Invoice> jsonProxy) =>
                    {
                        // verify request properties
                        Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                        Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                        Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                        Assert.AreEqual(string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetInvoice.Path, ExpectedInvoice.Id), jsonProxy.ResourcePath);

                        return Task.FromResult<Invoice>(ExpectedInvoice);
                    };

                var actualInvoice = invoiceOperations.Get();
                Assert.IsNotNull(actualInvoice, "Invoice should not be empty or null");

                CompareInvoices(ExpectedInvoice, actualInvoice);
            }
        }

        /// <summary>
        /// Test Get Invoice Line Items - Success Path
        /// </summary>
        [TestMethod]
        public void InvoiceTests_GetInvoiceLineItemsTestVerifySuccessPath()
        {
            ValidateGetInvoiceLineItems(BillingProvider.Office, InvoiceLineItemType.BillingLineItems, 6);

            ValidateGetInvoiceLineItems(BillingProvider.Azure, InvoiceLineItemType.BillingLineItems, 10);

            ValidateGetInvoiceLineItems(BillingProvider.Azure, InvoiceLineItemType.UsageLineItems, 7);
        }

        #endregion

        /// <summary>
        /// Helper method for generating test invoice data
        /// </summary>
        /// <param name="size">The number of test invoices to generate.</param>
        /// <returns>Paged resource collection of invoices</returns>
        private static ResourceCollection<Invoice> GenerateTestInvoices(int size = 5)
        {
            var invoices = new List<Invoice>(size);

            for (int i = 0; i < size; i++)
            {
                var invoice = new Invoice
                {
                    CurrencyCode = "AUD",
                    Id = "TestInvoice" + (i + 1),
                    InvoiceDate = new DateTime(),
                    PaidAmount = -0.00M,
                    TotalCharges = 10000.00M,
                    PdfDownloadLink = new Uri("http://some-link"),
                    InvoiceDetails = (i % 2 != 0) ? 
                        null :
                        new List<InvoiceDetail>
                        {
                            new InvoiceDetail { BillingProvider = BillingProvider.Office, InvoiceLineItemType = InvoiceLineItemType.BillingLineItems },
                            new InvoiceDetail { BillingProvider = BillingProvider.Azure, InvoiceLineItemType = InvoiceLineItemType.BillingLineItems },
                            new InvoiceDetail { BillingProvider = BillingProvider.Azure, InvoiceLineItemType = InvoiceLineItemType.UsageLineItems }
                        }
                };

                invoices.Add(invoice);
            }

            return new ResourceCollection<Invoice>(invoices);
        }

        /// <summary>
        /// Helper method for generating invoice line items.
        /// </summary>
        /// <param name="billingProvider">The billing provider.</param>
        /// <param name="invoiceLineItemType">The invoice line item type.</param>
        /// <param name="size">The number of test line items to generate.</param>
        /// <returns>Collection of invoice line items.</returns>
        private static ResourceCollection<InvoiceLineItem> GetTestInvoiceLineItems(            
            BillingProvider billingProvider, 
            InvoiceLineItemType invoiceLineItemType,
            int size = 5)
        {
            var invoiceLineItems = new List<InvoiceLineItem>(size);

            switch (billingProvider)
            {
                case BillingProvider.Office:
                    if (invoiceLineItemType == InvoiceLineItemType.BillingLineItems)
                    {
                        for (var i = 0; i < size; i++)
                        {
                            InvoiceLineItem invoicelineItem = new LicenseBasedLineItem
                            {
                                Amount = 1.00M,
                                ChargeEndDate = new DateTime(),
                                ChargeStartDate = new DateTime(),
                                ChargeType = "charge-type",
                                Currency = "USD",
                                CustomerId = Guid.NewGuid().ToString(),
                                CustomerName = "customer-name",
                                DurableOfferId = Guid.NewGuid().ToString(),
                                MpnId = 12345,
                                OfferId = Guid.NewGuid().ToString(),
                                OfferName = "offer-name",
                                OrderId = Guid.NewGuid().ToString(),
                                PartnerId = Guid.NewGuid().ToString(),
                                Quantity = 1,
                                SubscriptionEndDate = new DateTime(),
                                SubscriptionStartDate = new DateTime(),
                                SubscriptionId = Guid.NewGuid().ToString(),
                                Subtotal = 1.00M,
                                SyndicationPartnerSubscriptionNumber = Guid.NewGuid().ToString(),
                                Tax = 0.00M,
                                Tier2MpnId = 67890,
                                TotalForCustomer = 1.00M,
                                TotalOtherDiscount = 0.00M,
                                UnitPrice = 1.00M
                            };

                            invoiceLineItems.Add(invoicelineItem);
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Invalid invoice line item type for Office billing provider.");
                    }

                    break;
                case BillingProvider.Azure:
                    
                    if (invoiceLineItemType == InvoiceLineItemType.None)
                    {
                        throw new ArgumentException("Invalid invoice line item type for Azure billing provider.");
                    }
                       
                    for (var i = 0; i < size; i++)
                    {
                        InvoiceLineItem invoicelineItem;
                        if (invoiceLineItemType == InvoiceLineItemType.BillingLineItems)
                        {
                            invoicelineItem = new UsageBasedLineItem
                            {
                                ChargeEndDate = new DateTime(),
                                ChargeStartDate = new DateTime(),
                                ChargeType = "charge-type",
                                Currency = "USD",
                                MpnId = 12345,
                                OrderId = Guid.NewGuid().ToString(),
                                PartnerId = Guid.NewGuid().ToString(),
                                SubscriptionId = Guid.NewGuid().ToString(),
                                Tier2MpnId = 67890,
                                ConsumedQuantity = 2,
                                ConsumptionDiscount = 0.00M,
                                ConsumptionPrice = 10.00M,
                                CustomerCompanyName = "some-name",
                                DetailLineItemId = 1,
                                IncludedQuantity = 0,
                                InvoiceNumber = Guid.NewGuid().ToString(),
                                ListPrice = 5.00M,
                                OverageQuantity = 0,
                                PartnerBillableAccountId = Guid.NewGuid().ToString(),
                                PartnerName = "some-partner",
                                PostTaxEffectiveRate = 1.00M,
                                PostTaxTotal = 2.00M,
                                PretaxCharges = 10.00M,
                                PretaxEffectiveRate = 0.00M,
                                Region = "West",
                                ResourceGuid = Guid.NewGuid().ToString(),
                                ResourceName = "Storage",
                                ServiceName = "Table Storage",
                                ServiceType = "Data",
                                Sku = "some-sku",
                                SubscriptionDescription = "some description",
                                SubscriptionName = "some sub name",
                                TaxAmount = 10.00M,
                                Unit = "some-unit"
                            };
                        }
                        else
                        {
                            invoicelineItem = new DailyUsageLineItem
                            {
                                ChargeEndDate = new DateTime(),
                                ChargeStartDate = new DateTime(),
                                MpnId = 12345,
                                OrderId = Guid.NewGuid().ToString(),
                                PartnerId = Guid.NewGuid().ToString(),
                                SubscriptionId = Guid.NewGuid().ToString(),
                                Tier2MpnId = 67890,
                                ConsumedQuantity = 2,
                                CustomerCompanyName = "some-name",
                                InvoiceNumber = Guid.NewGuid().ToString(),
                                PartnerBillableAccountId = Guid.NewGuid().ToString(),
                                PartnerName = "some-partner",
                                Region = "West",
                                ResourceGuid = Guid.NewGuid().ToString(),
                                ResourceName = "Storage",
                                ServiceName = "Table Storage",
                                ServiceType = "Data",
                                SubscriptionDescription = "some description",
                                SubscriptionName = "some sub name",
                                CustomerBillableAccount = Guid.NewGuid().ToString(),
                                MeteredRegion = "West",
                                MeteredService = "some-metered-service",
                                MeteredServiceType = "some-metered-service-type",
                                Project = "some-project",
                                ServiceInfo = "some-service info",
                                UsageDate = new DateTime(),
                                Unit = "some-unit"
                            };
                        }

                        invoiceLineItems.Add(invoicelineItem);
                    }

                    break;
                default:
                    throw new ArgumentException("Invalid billing provider.");
            }
            
            return new ResourceCollection<InvoiceLineItem>(invoiceLineItems);
        }

        /// <summary>
        /// Helper method to mimic creation of paged invoice results.
        /// </summary>
        /// <param name="invoiceCollection">The invoice collection.</param>
        /// <param name="size">The page size.</param>
        /// <param name="offset">The page offset.</param>
        /// <returns>Paged collection.</returns>
        private static ResourceCollection<Invoice> GeneratePagedInvoices(
            ResourceCollection<Invoice> invoiceCollection, 
            int size, 
            int offset)
        {
            bool lastPage = (size + offset) >= invoiceCollection.TotalCount;
            bool firstPage = offset == 0;

            return new ResourceCollection<Invoice>(invoiceCollection.Items.Skip(offset).Take(size).ToList())
            {
                Links = new StandardResourceCollectionLinks()
                {
                    Self = new Link(new Uri(string.Format(CultureInfo.InvariantCulture, PagedInvoicingUrlFormat, size, offset), UriKind.RelativeOrAbsolute), HttpMethod.Get.ToString()),
                    Next = lastPage ? null : new Link(new Uri(string.Format(CultureInfo.InvariantCulture, PagedInvoicingUrlFormat, size, offset + size), UriKind.RelativeOrAbsolute), HttpMethod.Get.ToString()),
                    Previous = firstPage ? null : new Link(new Uri(string.Format(CultureInfo.InvariantCulture, PagedInvoicingUrlFormat, size, offset - size), UriKind.RelativeOrAbsolute), HttpMethod.Get.ToString())
                }
            };
        }

        /// <summary>
        /// Helper method to compare invoices
        /// </summary>
        /// <param name="expectedInvoice">The expected invoice.</param>
        /// <param name="actualInvoice">The actual invoice.</param>
        private static void CompareInvoices(Invoice expectedInvoice, Invoice actualInvoice)
        {
            Assert.AreEqual(expectedInvoice.Id, expectedInvoice.Id);
            Assert.AreEqual(expectedInvoice.CurrencyCode, expectedInvoice.CurrencyCode);
            Assert.AreEqual(expectedInvoice.InvoiceDate, expectedInvoice.InvoiceDate);
            Assert.AreEqual(expectedInvoice.PaidAmount, expectedInvoice.PaidAmount);
            Assert.AreEqual(expectedInvoice.TotalCharges, expectedInvoice.TotalCharges);
            Assert.AreEqual(expectedInvoice.PdfDownloadLink, expectedInvoice.PdfDownloadLink);
            Assert.AreEqual(expectedInvoice.InvoiceDetails, actualInvoice.InvoiceDetails);

            if (expectedInvoice.InvoiceDetails != null)
            {
                Assert.AreEqual(expectedInvoice.InvoiceDetails.Count(), expectedInvoice.InvoiceDetails.Count());

                foreach (var invoiceDetail in expectedInvoice.InvoiceDetails)
                {
                    Assert.IsTrue(
                        actualInvoice.InvoiceDetails.Any(
                            a =>
                                a.BillingProvider == invoiceDetail.BillingProvider &&
                                a.InvoiceLineItemType == invoiceDetail.InvoiceLineItemType));
                }
            }
        }

        /// <summary>
        /// Validates the get invoice line items
        /// </summary>
        /// <param name="billingProvider">The billing provider.</param>
        /// <param name="invoiceLineItemType">The invoice line item type.</param>
        /// <param name="testDataSize">The number of test line items to generate.</param>
        private static void ValidateGetInvoiceLineItems(
            BillingProvider billingProvider,
            InvoiceLineItemType invoiceLineItemType,
            int testDataSize)
        {
            ResourceCollection<InvoiceLineItem> testData = GetTestInvoiceLineItems(billingProvider, invoiceLineItemType, testDataSize);

            using (ShimsContext.Create())
            {
                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<InvoiceLineItem, ResourceCollection<InvoiceLineItem>>.AllInstances.GetAsync
                    = (PartnerServiceProxy<InvoiceLineItem, ResourceCollection<InvoiceLineItem>> jsonProxy) =>
                    {
                        // verify request properties
                        Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                        Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                        Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                        Assert.AreEqual(
                            string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetInvoiceLineItems.Path, ExpectedInvoice.Id, billingProvider, invoiceLineItemType),
                            jsonProxy.ResourcePath);

                        // verify that the seek operation was added to the uri parameters
                        Assert.AreEqual(jsonProxy.UriParameters.Count, 0);

                        return Task.FromResult<ResourceCollection<InvoiceLineItem>>(testData);
                    };

                var collection = invoiceOperations.By(billingProvider, invoiceLineItemType).Get();

                Assert.IsNotNull(collection, "Invoice line items should not be empty or null");
                Assert.IsTrue(collection.Items.Any(), "Invoices should not be empty or null");
                Assert.AreEqual(testData.Items.Count(), collection.Items.Count());

                InvoiceLineItem actualItem = collection.Items.First();

                if (billingProvider == BillingProvider.Office)
                {
                    Assert.AreEqual(typeof(LicenseBasedLineItem), actualItem.GetType());
                }
                else
                {
                    if (invoiceLineItemType == InvoiceLineItemType.BillingLineItems)
                    {
                        Assert.AreEqual(typeof(UsageBasedLineItem), actualItem.GetType());
                    }
                    else
                    {
                        Assert.AreEqual(typeof(DailyUsageLineItem), actualItem.GetType());
                    }
                }
            }
        }
    }
}
