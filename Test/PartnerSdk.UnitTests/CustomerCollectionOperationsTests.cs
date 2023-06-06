// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Customers;
    using Customers.Fakes;
    using ErrorHandling;
    using Models;
    using Models.Customers;
    using Models.JsonConverters;
    using Models.Query;
    using Moq;
    using Network;
    using Network.Fakes;
    using Newtonsoft.Json;
    using QualityTools.Testing.Fakes;
    using RequestContext;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="CustomerCollectionOperations"/> class.
    /// </summary>
    [TestClass]
    public class CustomerCollectionOperationsTests
    {
        /// <summary>
        /// The expected credentials.
        /// </summary>
        private static Mock<IPartnerCredentials> expectedCredentials;

        /// <summary>
        /// The expected context.
        /// </summary>
        private static Mock<IRequestContext> expectedContext;

        /// <summary>
        /// The expected customer id.
        /// </summary>
        private static string expectedCustomerId = "1";

        /// <summary>
        /// The customer collection operations instance under test.
        /// </summary>
        private static CustomerCollectionOperations customerCollectionOperations;

        /// <summary>
        /// Initializes the test suite.
        /// </summary>
        /// <param name="testContext">The test context.</param>
        [ClassInitialize]
        public static void InitializeTestSuite(TestContext testContext)
        {
            expectedCredentials = new Mock<IPartnerCredentials>();
            expectedCredentials.Setup(credentials => credentials.PartnerServiceToken).Returns("Fake Token");
            expectedCredentials.Setup(credentials => credentials.ExpiresAt).Returns(DateTimeOffset.MaxValue);
            
            expectedContext = new Mock<IRequestContext>();
            expectedContext.Setup(context => context.CorrelationId).Returns(Guid.NewGuid());
            expectedContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> partnerOperations = new Mock<IPartner>();
            partnerOperations.Setup(partner => partner.Credentials).Returns(expectedCredentials.Object);
            partnerOperations.Setup(partner => partner.RequestContext).Returns(expectedContext.Object);

            customerCollectionOperations = new CustomerCollectionOperations(partnerOperations.Object);
        }

        /// <summary>
        /// Ensures that create customers pass in the right values to the proxy and actually calls the proxy
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task CustomerCollectionOperationsTests_VerifyCreateCustomer()
        {
            int proxyPostAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below
                ShimPartnerServiceProxy<Customer, ResourceCollection<Customer>>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                    PartnerServiceProxy<Customer, ResourceCollection<Customer>> jsonProxy,
                    IPartner partnerOperations,
                    string resourcePath,
                    IFailedPartnerServiceResponseHandler errorHandler,
                    JsonConverter jsonConverter) =>
                {
                    Assert.AreEqual(expectedCredentials.Object, partnerOperations.Credentials);
                    Assert.AreEqual(expectedContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(PartnerService.Instance.Configuration.Apis.CreateCustomer.Path, resourcePath);
                    Assert.IsInstanceOfType(jsonConverter, typeof(ResourceCollectionConverter<Customer>));
                };

                // divert calls to PostAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<Customer, Customer>.AllInstances.PostAsyncT0 = (PartnerServiceProxy<Customer, Customer> jsonProxy, Customer newCustomer) =>
                {
                    // increment the number of the calls
                    proxyPostAsyncCalls++;

                    // return the same customer object
                    return Task.FromResult<Customer>(newCustomer);
                };

                // ensure creating null customer will fail
                bool nullCheckOk = false;

                try
                {
                    await customerCollectionOperations.CreateAsync(null);
                }
                catch (ArgumentNullException)
                {
                    try
                    {
                        customerCollectionOperations.Create(null);
                    }
                    catch (ArgumentNullException)
                    {
                        // good, both operations threw the expected exception!
                        nullCheckOk = true;
                    }
                }

                // ensure that the exceptions were thrown
                Assert.IsTrue(nullCheckOk);

                // configure a couple of customers
                var firstCustomer = new Customer() { Id = "1" };
                var secondCustomer = new Customer() { Id = "2" };

                // call both sync and async versions of the create customers API
                var firstReturnedCustomer = await customerCollectionOperations.CreateAsync(firstCustomer);
                var secondReturnedCustomer = customerCollectionOperations.Create(secondCustomer);

                // ensure the proxy PostAsync() was called twice
                Assert.AreEqual(proxyPostAsyncCalls, 2);

                // ensure that the expected values were returned
                Assert.AreEqual(firstReturnedCustomer, firstCustomer);
                Assert.AreEqual(secondReturnedCustomer, secondCustomer);
            }
        }

        /// <summary>
        /// Ensures that getting a customer by id works as expected.
        /// </summary>
        [TestMethod]
        public void CustomerCollectionOperationsTests_VerifyByIdNavigation()
        {
            using (ShimsContext.Create())
            {
                // route all CustomerOperations constructors to our handler below
                ShimCustomerOperations.ConstructorIPartnerString =
                    (CustomerOperations operations, IPartner partnerOperations, string customerId) =>
                {
                    // ensure the customer collection operations pass in the right values to the customer operations
                    Assert.AreEqual(customerId, expectedCustomerId);
                    Assert.AreEqual(partnerOperations.Credentials, expectedCredentials.Object);
                    Assert.AreEqual(partnerOperations.RequestContext, expectedContext.Object);
                };

                // invoke by id
                Assert.IsNotNull(customerCollectionOperations.ById(expectedCustomerId));
            }
        }

        /// <summary>
        /// Ensures that navigating to customers relationship request is done properly.
        /// </summary>
        [TestMethod]
        public void CustomerCollectionOperationsTests_VerifyCustomerRelationshipRequestNavigation()
        {
            var customerRelationshipRequestOperations = CustomerCollectionOperationsTests.customerCollectionOperations.RelationshipRequest;

            // ensure that the partner operations instance is passed
            Assert.IsNotNull(customerRelationshipRequestOperations);
            Assert.AreEqual(CustomerCollectionOperationsTests.customerCollectionOperations.Partner, customerRelationshipRequestOperations.Partner);
        }

        /// <summary>
        /// Ensures that get customers pass in the right values to the proxy and actually calls the proxy
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task CustomerCollectionOperationsTests_VerifyGetCustomers()
        {
            int proxyGetAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below
                ShimPartnerServiceProxy<Customer, SeekBasedResourceCollection<Customer>>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                    PartnerServiceProxy<Customer, SeekBasedResourceCollection<Customer>> jsonProxy,
                    IPartner partnerOperations,
                    string resourcePath,
                    IFailedPartnerServiceResponseHandler errorHandler,
                    JsonConverter jsonConverter) =>
                {
                    Assert.AreEqual(expectedCredentials.Object, partnerOperations.Credentials);
                    Assert.AreEqual(expectedContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(PartnerService.Instance.Configuration.Apis.GetCustomers.Path, resourcePath);
                    Assert.IsInstanceOfType(jsonConverter, typeof(ResourceCollectionConverter<Customer>));
                };

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<Customer, SeekBasedResourceCollection<Customer>>.AllInstances.GetAsync = (PartnerServiceProxy<Customer, SeekBasedResourceCollection<Customer>> jsonProxy) =>
                {
                    // increment the number of the calls
                    proxyGetAsyncCalls++;
                    return Task.FromResult<SeekBasedResourceCollection<Customer>>(null);
                };

                // call both sync and async versions of the Get customers API
                await customerCollectionOperations.GetAsync();
                customerCollectionOperations.Get();

                // ensure the proxy GetAsync() was called twice
                Assert.AreEqual(proxyGetAsyncCalls, 2);
            }
        }

        /// <summary>
        /// Ensures that query customers properly handles bad inputs.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task CustomerCollectionOperationsTests_VerifyQueryCustomersWithInvalidArguments()
        {
            bool validationOk = false;

            try
            {
                await customerCollectionOperations.QueryAsync(null);
            }
            catch (ArgumentNullException)
            {
                try
                {
                    customerCollectionOperations.Query(null);
                }
                catch (ArgumentNullException)
                {
                    validationOk = true;
                }
            }

            Assert.IsTrue(validationOk);
            validationOk = false;

            try
            {
                // you should not be able to query for customers count
                await customerCollectionOperations.QueryAsync(QueryFactory.Instance.BuildCountQuery());
            }
            catch (ArgumentException)
            {
                try
                {
                    customerCollectionOperations.Query(QueryFactory.Instance.BuildCountQuery());
                }
                catch (ArgumentException)
                {
                    validationOk = true;
                }
            }

            Assert.IsTrue(validationOk);
        }

        /// <summary>
        /// Ensures that customer paging works properly.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task CustomerCollectionOperationsTests_VerifyQueryCustomersWithSeekQuery()
        {
            using (ShimsContext.Create())
            {
                int numberOfCalls = 0;
                string expectedToken = "SomeToken";
                SeekOperation expectedSeekOperation = SeekOperation.Next;

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<Customer, SeekBasedResourceCollection<Customer>>.AllInstances.GetAsync = (PartnerServiceProxy<Customer, SeekBasedResourceCollection<Customer>> jsonProxy) =>
                {
                    // increment the number of the calls
                    numberOfCalls++;

                    // verify request properties
                    Assert.AreEqual(expectedCredentials.Object, jsonProxy.Partner.Credentials);
                    Assert.AreEqual(expectedContext.Object.RequestId, jsonProxy.RequestId);
                    Assert.AreEqual(expectedContext.Object.CorrelationId, jsonProxy.CorrelationId);
                    Assert.AreEqual(PartnerService.Instance.Configuration.Apis.GetCustomers.Path, jsonProxy.ResourcePath);
                    Assert.IsInstanceOfType(jsonProxy.JsonConverter, typeof(ResourceCollectionConverter<Customer>));

                    // verify that the token was added to the request headers
                    Assert.AreEqual(jsonProxy.AdditionalRequestHeaders.Count, 1);

                    IEnumerator<KeyValuePair<string, string>> enumerator = jsonProxy.AdditionalRequestHeaders.GetEnumerator();
                    Assert.IsTrue(enumerator.MoveNext());
                    Assert.AreEqual(enumerator.Current.Key, PartnerService.Instance.Configuration.Apis.GetCustomers.AdditionalHeaders.ContinuationToken);
                    Assert.AreEqual(enumerator.Current.Value, expectedToken);

                    // verify that the seek operation was added to the uri parameters
                    Assert.AreEqual(jsonProxy.UriParameters.Count, 1);

                    enumerator = jsonProxy.UriParameters.GetEnumerator();
                    Assert.IsTrue(enumerator.MoveNext());
                    Assert.AreEqual(enumerator.Current.Key, PartnerService.Instance.Configuration.Apis.GetCustomers.Parameters.SeekOperation);
                    Assert.AreEqual(enumerator.Current.Value, expectedSeekOperation.ToString());
 
                    return Task.FromResult<SeekBasedResourceCollection<Customer>>(null);
                };

                try
                {
                    // attempt to query without a token
                    customerCollectionOperations.Query(QueryFactory.Instance.BuildSeekQuery(SeekOperation.Next, token: null));
                    Assert.Fail();
                }
                catch (ArgumentNullException)
                {
                    // expected, token has to be set
                }

                // invoke query and query async
                customerCollectionOperations.Query(QueryFactory.Instance.BuildSeekQuery(expectedSeekOperation, token: expectedToken));
                await customerCollectionOperations.QueryAsync(QueryFactory.Instance.BuildSeekQuery(expectedSeekOperation, token: expectedToken));

                // ensure that both the Query calls invoked the proxy get async method
                Assert.AreEqual(numberOfCalls, 2);
            }
        }

        /// <summary>
        /// Ensures that customer querying works properly.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task CustomerCollectionOperationsTests_VerifyQueryCustomersWithQuery()
        {
            var filter = new SimpleFieldFilter("id", FieldFilterOperation.StartsWith, "x");
            var simpleQueryWithFilter = QueryFactory.Instance.BuildSimpleQuery(filter);
            var simpleQueryWithNoFilter = QueryFactory.Instance.BuildSimpleQuery();
            var indexedQueryWithFilter = QueryFactory.Instance.BuildIndexedQuery(100, filter: filter);
            var indexedQueryWithNoFilter = QueryFactory.Instance.BuildIndexedQuery(100);

            int numberOfCalls = 0;
            IQuery currentQuery = null;

            using (ShimsContext.Create())
            {
                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<Customer, SeekBasedResourceCollection<Customer>>.AllInstances.GetAsync = (PartnerServiceProxy<Customer, SeekBasedResourceCollection<Customer>> jsonProxy) =>
                {
                    // increment the number of the calls
                    numberOfCalls++;

                    // verify request properties
                    Assert.AreEqual(expectedCredentials.Object, jsonProxy.Partner.Credentials);
                    Assert.AreEqual(expectedContext.Object.RequestId, jsonProxy.RequestId);
                    Assert.AreEqual(expectedContext.Object.CorrelationId, jsonProxy.CorrelationId);
                    Assert.AreEqual(PartnerService.Instance.Configuration.Apis.GetCustomers.Path, jsonProxy.ResourcePath);
                    Assert.IsInstanceOfType(jsonProxy.JsonConverter, typeof(ResourceCollectionConverter<Customer>));

                    // verify that the filter (if specified) and page size were added to the URI parameters
                    Assert.AreEqual(jsonProxy.UriParameters.Count, currentQuery.Filter == null ? 1 : 2);

                    var enumerator = jsonProxy.UriParameters.GetEnumerator();
                    Assert.IsTrue(enumerator.MoveNext());
                    Assert.AreEqual(enumerator.Current.Key, PartnerService.Instance.Configuration.Apis.GetCustomers.Parameters.Size);
                    Assert.AreEqual(enumerator.Current.Value, currentQuery.Type == QueryType.Simple ? "0" : currentQuery.PageSize.ToString());

                    if (currentQuery.Filter != null)
                    {
                        Assert.IsTrue(enumerator.MoveNext());
                        Assert.AreEqual(enumerator.Current.Key, PartnerService.Instance.Configuration.Apis.GetCustomers.Parameters.Filter);
                        Assert.AreEqual(enumerator.Current.Value, WebUtility.UrlEncode(JsonConvert.SerializeObject(currentQuery.Filter)));
                    }

                    return Task.FromResult<SeekBasedResourceCollection<Customer>>(null);
                };

                // query using different combinations
                customerCollectionOperations.Query(currentQuery = simpleQueryWithFilter);
                await customerCollectionOperations.QueryAsync(currentQuery = simpleQueryWithNoFilter);
                await customerCollectionOperations.QueryAsync(currentQuery = indexedQueryWithFilter);
                customerCollectionOperations.Query(currentQuery = indexedQueryWithNoFilter);

                // ensure that both the Query calls invoked the proxy get async method
                Assert.AreEqual(numberOfCalls, 4);
            }
        }
    }
}