// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Exceptions;
    using Models;
    using Models.Partners;
    using Moq;
    using Network;
    using Network.Fakes;
    using Profiles;
    using QualityTools.Testing.Fakes;
    using RequestContext;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit tests for <see cref="MpnProfileOperations"/>.
    /// </summary>
    [TestClass]
    public class MpnProfileOperationsTests
    {
        /// <summary>
        /// Test MPN Id.
        /// </summary>
        private const string TestMpnId = "12345";

        /// <summary>
        /// Test Partner Name.
        /// </summary>
        private const string TestPartnerName = "Some-Partner-Name";

        /// <summary>
        /// The expected credentials.
        /// </summary>
        private static Mock<IPartnerCredentials> mockCredentials;

        /// <summary>
        /// The expected context.
        /// </summary>
        private static Mock<IRequestContext> mockRequestContext;

        /// <summary>
        /// The partner profile collection operations.
        /// </summary>
        private static IPartnerProfileCollection partnerProfileCollectionOperations;

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

            partnerProfileCollectionOperations = new PartnerProfileCollectionOperations(partnerOperations.Object);
        }

        /// <summary>
        /// Test Get MPN Profile method with an invalid MPN Id.
        /// </summary>
        [TestMethod]
        public void MpnProfileOperationsTests_GetMpnProfileWithInvalidMpnId()
        {
            try
            {
                partnerProfileCollectionOperations.MpnProfile.Get(null);
                Assert.Fail("Expecting a ArgumentNullException.");
            }
            catch (ArgumentNullException)
            {
            }

            try
            {
                partnerProfileCollectionOperations.MpnProfile.Get(string.Empty);
                Assert.Fail("Expecting a ArgumentException.");
            }
            catch (ArgumentException)
            {
            }
        }

        /// <summary>
        /// Test Get MPN Profile method with an MPN Id that doesn't exists.
        /// </summary>
        [TestMethod]
        public void MpnProfileOperationsTests_GetMpnProfileWithMpnIdNotFound()
        {
            using (ShimsContext.Create())
            {
                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<MpnProfile, MpnProfile>.AllInstances.GetAsync
                    = (PartnerServiceProxy<MpnProfile, MpnProfile> jsonProxy) =>
                    {
                        throw new PartnerException(
                            new ApiFault(), 
                            mockRequestContext.Object, 
                            PartnerErrorCategory.NotFound);
                    };

                try
                {
                    partnerProfileCollectionOperations.MpnProfile.Get("12345");
                }
                catch (PartnerException pe)
                {
                    Assert.AreEqual(pe.ErrorCategory, PartnerErrorCategory.NotFound);
                }
            }
        }

        /// <summary>
        /// Test Get MPN Profile method with a valid MPN Id.
        /// </summary>
        [TestMethod]
        public void MpnProfileOperationsTests_GetPartnerWithValidMpnId()
        {
            using (ShimsContext.Create())
            {
                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<MpnProfile, MpnProfile>.AllInstances.GetAsync
                    = (PartnerServiceProxy<MpnProfile, MpnProfile> jsonProxy) =>
                    {
                        // verify request properties
                        Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                        Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                        Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);
                        Assert.AreEqual(PartnerService.Instance.Configuration.Apis.GetMpnProfile.Path, jsonProxy.ResourcePath);

                        // verify that the seek operation was added to the uri parameters
                        Assert.AreEqual(jsonProxy.UriParameters.Count, 1);

                        IEnumerator<KeyValuePair<string, string>> enumerator = jsonProxy.UriParameters.GetEnumerator();
                        Assert.IsTrue(enumerator.MoveNext());
                        Assert.AreEqual(enumerator.Current.Key, PartnerService.Instance.Configuration.Apis.GetMpnProfile.Parameters.MpnId);
                        Assert.AreEqual(enumerator.Current.Value, TestMpnId);

                        return Task.FromResult<MpnProfile>(
                            new MpnProfile
                            {
                                PartnerName = TestPartnerName,
                                MpnId = TestMpnId
                            });
                    };

                var profile = partnerProfileCollectionOperations.MpnProfile.Get(TestMpnId);
                Assert.IsNotNull(profile);
                Assert.AreEqual(profile.MpnId, TestMpnId);
                Assert.AreEqual(profile.PartnerName, TestPartnerName);                
            }
        }
    }
}
