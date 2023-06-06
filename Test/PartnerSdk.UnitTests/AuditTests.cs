// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using AuditRecords;
    using Models;
    using Models.Auditing;
    using Moq;
    using Network;
    using Network.Fakes;
    using QualityTools.Testing.Fakes;
    using RequestContext;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Audit Tests
    /// </summary>
    [TestClass]
    public class AuditTests
    {
        /// <summary>
        /// Test Audit Record
        /// </summary>
        private static readonly AuditRecord ExpectedAuditRecord = new AuditRecord
        {
            Id = "RecordId",
            PartnerId = "PartnerId",
            CustomerId = "CustomerId",
            CustomerName = "CustomerName",
            ResourceType = ResourceType.Order,
            ResourceNewValue = "New Value"
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
        /// Reference to the AuditRecordsCollection
        /// </summary>
        private static AuditRecordsCollection auditRecordsCollectionOperations;
        
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

            auditRecordsCollectionOperations = new AuditRecordsCollection(partnerOperations.Object);
        }

        /// <summary>
        /// Tests to ensure we are converting date correctly from a user locale to an invariant for SDK queries
        /// </summary>
        [TestMethod]
        public void AuditTests_PartnerCultureHandled()
        {
            SeekBasedResourceCollection<AuditRecord> testRecords = GenerateAuditRecords(5);

            // Run the test under a non-invariant to mimic a global sdk user
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-AU");

            // Generate a start date
            var startDate = DateTime.Now;

            // convert the start date to string in the format of the users locale
            var unhandledStartDateConversion = startDate.ToString();

            // convert the Local type start date to invariant
            var expectedStartDateConversion = startDate.ToString(CultureInfo.InvariantCulture);

            // Assert that the Local type conversion does not match what is expected
            Assert.AreNotEqual(unhandledStartDateConversion, expectedStartDateConversion);

            using (ShimsContext.Create())
            {
                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<AuditRecord, SeekBasedResourceCollection<AuditRecord>>.AllInstances.GetAsync
                    = (PartnerServiceProxy<AuditRecord, SeekBasedResourceCollection<AuditRecord>> jsonProxy) =>
                    {
                        // verify request properties
                        Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                        Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                        Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                        Assert.AreEqual(PartnerService.Instance.Configuration.Apis.GetAuditRecordsRequest.Path, jsonProxy.ResourcePath);
                        
                        // verify that the startdate was added to the uri parameters
                        Assert.AreEqual(jsonProxy.UriParameters.Count, 1);
                        var convertedStartDate = jsonProxy.UriParameters.First().Value;

                        // verify that the start date was converted correctly regardless of user locale
                        Assert.AreEqual(convertedStartDate, expectedStartDateConversion);
                        
                        return Task.FromResult<SeekBasedResourceCollection<AuditRecord>>(testRecords);
                    };

                var auditCollection = auditRecordsCollectionOperations.QueryAsync(startDate);

                Assert.IsNotNull(auditCollection, "Audit records collection should not be empty or null");
                Assert.AreEqual(auditCollection.Result.TotalCount, 5);
            }
        }

        /// <summary>
        /// Helper method for generating test audit data
        /// </summary>
        /// <param name="size">The number of test records to generate.  A null value defaults to 1</param>
        /// <returns>Paged resource collection of AuditRecord</returns>
        private static SeekBasedResourceCollection<AuditRecord> GenerateAuditRecords(int size = 1)
        {
            var auditRecords = new List<AuditRecord>(size);

            for (int i = 0; i < size; i++)
            {
                var auditRecord = new AuditRecord
                {
                    Id = ExpectedAuditRecord.Id,
                    PartnerId = ExpectedAuditRecord.PartnerId,
                    CustomerId = ExpectedAuditRecord.CustomerId,
                    CustomerName = ExpectedAuditRecord.CustomerName,
                    ResourceType = ExpectedAuditRecord.ResourceType,
                    ResourceNewValue = ExpectedAuditRecord.ResourceNewValue
                };

                auditRecords.Add(auditRecord);
            }

            return new SeekBasedResourceCollection<AuditRecord>(auditRecords);
        }
    }
}
