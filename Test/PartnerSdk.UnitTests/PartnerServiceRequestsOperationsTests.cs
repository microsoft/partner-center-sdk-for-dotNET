// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ErrorHandling;
    using Models;
    using Models.JsonConverters;
    using Models.Query;
    using Models.ServiceRequests;
    using Moq;
    using Network;
    using Network.Fakes;
    using Newtonsoft.Json;
    using QualityTools.Testing.Fakes;
    using RequestContext;
    using ServiceRequests;
    using ServiceRequests.Fakes;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="PartnerServiceRequestOperations"/> class.
    /// </summary>
    [TestClass]
    public class PartnerServiceRequestsOperationsTests
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
        /// The expected service request id.
        /// </summary>
        private static string expectedServiceRequestId = "1";

        /// <summary>
        /// The partner service request collection operations instance under test.
        /// </summary>
        private static PartnerServiceRequestCollectionOperations partnerServiceRequestsCollectionOperations;

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

            partnerServiceRequestsCollectionOperations = new PartnerServiceRequestCollectionOperations(partnerOperations.Object);
        }

        /// <summary>
        /// Ensures that getting a service request by id works as expected.
        /// </summary>
        [TestMethod]
        public void PartnerServiceRequestsOperationsTests_VerifyByIdNavigation()
        {
            using (ShimsContext.Create())
            {
                // route all PartnerServiceRequestOperations constructors to our handler below
                ShimPartnerServiceRequestOperations.ConstructorIPartnerString =
                    (PartnerServiceRequestOperations operations, IPartner partnerOperations, string serviceRequestId) =>
                    {
                        // ensure the service request collection operations pass in the right values to the service request operations
                        Assert.AreEqual(serviceRequestId, expectedServiceRequestId);
                        Assert.AreEqual(partnerOperations.Credentials, expectedCredentials.Object);
                        Assert.AreEqual(partnerOperations.RequestContext, expectedContext.Object);
                    };

                // invoke by id
                Assert.IsNotNull(partnerServiceRequestsCollectionOperations.ById(expectedServiceRequestId));
            }
        }

        /// <summary>
        /// Ensures that get service requests pass in the right values to the proxy and actually calls the proxy
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task PartnerServiceRequestsOperationsTests_VerifyGetServiceRequests()
        {
            int proxyGetAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below
                ShimPartnerServiceProxy<ServiceRequest, ResourceCollection<ServiceRequest>>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                    PartnerServiceProxy<ServiceRequest, ResourceCollection<ServiceRequest>> jsonProxy,
                    IPartner partnerOperations,
                    string resourcePath,
                    IFailedPartnerServiceResponseHandler errorHandler,
                    JsonConverter jsonConverter) =>
                {
                    Assert.AreEqual(expectedCredentials.Object, partnerOperations.Credentials);
                    Assert.AreEqual(expectedContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(PartnerService.Instance.Configuration.Apis.GetAllServiceRequestsPartner.Path, resourcePath);
                };

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<ServiceRequest, ResourceCollection<ServiceRequest>>.AllInstances.GetAsync = (PartnerServiceProxy<ServiceRequest, ResourceCollection<ServiceRequest>> jsonProxy) =>
                {
                    // increment the number of the calls
                    proxyGetAsyncCalls++;
                    return Task.FromResult<ResourceCollection<ServiceRequest>>(null);
                };

                // call both sync and async versions of the Get service requests API
                await partnerServiceRequestsCollectionOperations.GetAsync();
                partnerServiceRequestsCollectionOperations.Get();

                // ensure the proxy GetAsync() was called twice
                Assert.AreEqual(proxyGetAsyncCalls, 2);
            }
        }

        /// <summary>
        /// Ensures that query service requests properly handles bad inputs.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task PartnerServiceRequestsOperationsTests_VerifyQueryServiceRequestsWithInvalidArguments()
        {
            bool validationOk = false;

            try
            {
                await partnerServiceRequestsCollectionOperations.QueryAsync(null);
            }
            catch (ArgumentNullException)
            {
                try
                {
                    partnerServiceRequestsCollectionOperations.Query(null);
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
                // you should not be able to query for service requests count
                await partnerServiceRequestsCollectionOperations.QueryAsync(QueryFactory.Instance.BuildCountQuery());
            }
            catch (ArgumentException)
            {
                try
                {
                    partnerServiceRequestsCollectionOperations.Query(QueryFactory.Instance.BuildCountQuery());
                }
                catch (ArgumentException)
                {
                    validationOk = true;
                }
            }

            Assert.IsTrue(validationOk);
            validationOk = false;

            try
            {
                // you should not be able to query for service requests seek
                await partnerServiceRequestsCollectionOperations.QueryAsync(QueryFactory.Instance.BuildSeekQuery(SeekOperation.Next));
            }
            catch (ArgumentException)
            {
                try
                {
                    partnerServiceRequestsCollectionOperations.Query(QueryFactory.Instance.BuildSeekQuery(SeekOperation.Next));
                }
                catch (ArgumentException)
                {
                    validationOk = true;
                }
            }

            Assert.IsTrue(validationOk);
        }

        /// <summary>
        /// Ensures that service requests paging works properly.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task PartnerServiceRequestsOperationsTests_VerifyQueryServiceRequestsWithIndexedQuery()
        {
            using (ShimsContext.Create())
            {
                int numberOfCalls = 0;

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<ServiceRequest, ResourceCollection<ServiceRequest>>.AllInstances.GetAsync = (PartnerServiceProxy<ServiceRequest, ResourceCollection<ServiceRequest>> jsonProxy) =>
                {
                    // increment the number of the calls
                    numberOfCalls++;

                    // verify request properties
                    Assert.AreEqual(expectedCredentials.Object, jsonProxy.Partner.Credentials);
                    Assert.AreEqual(expectedContext.Object.RequestId, jsonProxy.RequestId);
                    Assert.AreEqual(expectedContext.Object.CorrelationId, jsonProxy.CorrelationId);
                    Assert.AreEqual(PartnerService.Instance.Configuration.Apis.SearchServiceRequestPartner.Path, jsonProxy.ResourcePath);

                    // verify that the index and size was added to the uri parameters
                    Assert.AreEqual(jsonProxy.UriParameters.Count, 2);

                    var uriParameters = jsonProxy.UriParameters.ToDictionary(uriParameter => uriParameter.Key, uriParameter => uriParameter.Value);
                    Assert.IsTrue(uriParameters.ContainsKey(PartnerService.Instance.Configuration.Apis.SearchPartnerServiceRequests.Parameters.Size));
                    Assert.IsTrue(uriParameters.ContainsKey(PartnerService.Instance.Configuration.Apis.SearchPartnerServiceRequests.Parameters.Offset));

                    return Task.FromResult<ResourceCollection<ServiceRequest>>(null);
                };

                // invoke query and query async
                partnerServiceRequestsCollectionOperations.Query(QueryFactory.Instance.BuildIndexedQuery(50));
                await partnerServiceRequestsCollectionOperations.QueryAsync(QueryFactory.Instance.BuildIndexedQuery(50));

                // ensure that both the Query calls invoked the proxy get async method
                Assert.AreEqual(numberOfCalls, 2);
            }
        }

        /// <summary>
        /// Ensures that service requests querying works properly.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task PartnerServiceRequestsOperationsTests_VerifyQueryServiceRequestsWithFilter()
        {
            var filter = new SimpleFieldFilter("id", FieldFilterOperation.Equals, "x");
            var simpleQueryWithFilter = QueryFactory.Instance.BuildSimpleQuery(filter);
            var simpleQueryWithNoFilter = QueryFactory.Instance.BuildSimpleQuery();
            var indexedQueryWithFilter = QueryFactory.Instance.BuildIndexedQuery(100, filter: filter);
            var indexedQueryWithNoFilter = QueryFactory.Instance.BuildIndexedQuery(100);

            int numberOfCalls = 0;
            IQuery currentQuery = null;

            using (ShimsContext.Create())
            {
                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<ServiceRequest, ResourceCollection<ServiceRequest>>.AllInstances.GetAsync = (PartnerServiceProxy<ServiceRequest, ResourceCollection<ServiceRequest>> jsonProxy) =>
                {
                    // increment the number of the calls
                    numberOfCalls++;

                    // verify request properties
                    Assert.AreEqual(expectedCredentials.Object, jsonProxy.Partner.Credentials);
                    Assert.AreEqual(expectedContext.Object.RequestId, jsonProxy.RequestId);
                    Assert.AreEqual(expectedContext.Object.CorrelationId, jsonProxy.CorrelationId);
                    Assert.AreEqual(PartnerService.Instance.Configuration.Apis.SearchServiceRequestPartner.Path, jsonProxy.ResourcePath);

                    if (currentQuery.Type == QueryType.Simple)
                    {
                        // verify that the filter (if specified) were added to the URI parameters
                        Assert.AreEqual(jsonProxy.UriParameters.Count, currentQuery.Filter != null ? 1 : 0);
                    }
                    else if (currentQuery.Type == QueryType.Indexed)
                    {
                        // verify that the filter (if specified) and page size were added to the URI parameters
                        Assert.AreEqual(jsonProxy.UriParameters.Count, currentQuery.Filter != null ? 3 : 2);
                    }

                    var uriParameters = jsonProxy.UriParameters.ToDictionary(uriParameter => uriParameter.Key, uriParameter => uriParameter.Value);

                    if (currentQuery.Filter != null)
                    {
                        Assert.IsTrue(uriParameters.ContainsKey(PartnerService.Instance.Configuration.Apis.SearchPartnerServiceRequests.Parameters.Filter));
                        Assert.AreEqual(uriParameters[PartnerService.Instance.Configuration.Apis.SearchPartnerServiceRequests.Parameters.Filter], JsonConvert.SerializeObject(currentQuery.Filter));
                    }

                    if (currentQuery.Type == QueryType.Indexed)
                    {
                        Assert.IsTrue(uriParameters.ContainsKey(PartnerService.Instance.Configuration.Apis.SearchPartnerServiceRequests.Parameters.Size));
                        Assert.AreEqual(uriParameters[PartnerService.Instance.Configuration.Apis.SearchPartnerServiceRequests.Parameters.Size], currentQuery.PageSize.ToString());

                        Assert.IsTrue(uriParameters.ContainsKey(PartnerService.Instance.Configuration.Apis.SearchPartnerServiceRequests.Parameters.Offset));
                        Assert.AreEqual(uriParameters[PartnerService.Instance.Configuration.Apis.SearchPartnerServiceRequests.Parameters.Offset], currentQuery.Index.ToString());
                    }

                    return Task.FromResult<ResourceCollection<ServiceRequest>>(null);
                };

                // query using different combinations
                partnerServiceRequestsCollectionOperations.Query(currentQuery = simpleQueryWithFilter);
                await partnerServiceRequestsCollectionOperations.QueryAsync(currentQuery = simpleQueryWithNoFilter);
                await partnerServiceRequestsCollectionOperations.QueryAsync(currentQuery = indexedQueryWithFilter);
                partnerServiceRequestsCollectionOperations.Query(currentQuery = indexedQueryWithNoFilter);

                // ensure that both the Query calls invoked the proxy get async method
                Assert.AreEqual(numberOfCalls, 4);
            }
        }
    }
}
