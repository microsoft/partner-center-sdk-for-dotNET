// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests.Domains
{
    using System;
    using System.Threading.Tasks;
    using ErrorHandling;
    using Models;
    using Models.Partners;
    using Moq;
    using Network;
    using Network.Fakes;
    using Newtonsoft.Json;
    using Profiles;
    using QualityTools.Testing.Fakes;
    using RequestContext;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit tests for DomainOperations
    /// </summary>
    [TestClass]
    public class LegalBusinessProfileOperationsTests
    {
        /// <summary>
        /// Ensures that get legal business profile pass in the right values to the proxy and actually calls the proxy
        /// network methods.
        /// </summary>
        /// <returns> A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task LegalBusinessProfileOperations_VerifyGetLegalBusinessProfile()
        {
            int proxyGetAsyncCalls = 0;

            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            var legalBusinessProfileOperations = new LegalBusinessProfileOperations(mockPartnerOperations.Object);

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below
                ShimPartnerServiceProxy<string, string>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                    PartnerServiceProxy<string, string> jsonProxy,
                    IPartner partnerOperations,
                    string resourcePath,
                    IFailedPartnerServiceResponseHandler errorHandler,
                    JsonConverter jsonConverter) =>
                {
                    Assert.AreEqual(PartnerService.Instance.Configuration.Apis.GetLegalBusinessProfile.Path, resourcePath);
                };

                var legalBusinessProfile = new LegalBusinessProfile
                {
                    CompanyName = "Company",
                    Address = new Models.Address
                    {
                        AddressLine1 = "Microsoft Street",
                        AddressLine2 = "Unit 4",
                        City = "Calgary",
                        State = "Alberta",
                        PostalCode = "T5T0N1"
                    },
                    PrimaryContact = new Contact
                    {
                        Email = "john@smith.com",
                        FirstName = "John",
                        LastName = "Smith",
                        PhoneNumber = "4444444444"
                    },
                    CompanyApproverAddress = new Models.Address
                    {
                        AddressLine1 = "Fake Street",
                        AddressLine2 = "Unit 3",
                        City = "Vancouver",
                        State = "BC",
                        PostalCode = "V5T0N1"
                    },
                    CompanyApproverEmail = "test@approver.com"
                };

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<LegalBusinessProfile, LegalBusinessProfile>.AllInstances.GetAsync = (PartnerServiceProxy<LegalBusinessProfile, LegalBusinessProfile> jsonProxy) =>
                {
                    // increment the number of the calls
                    proxyGetAsyncCalls++;
                    return Task.FromResult<LegalBusinessProfile>(legalBusinessProfile);
                };

                // call both sync and async versions of the get legal business profile API
                var result = await legalBusinessProfileOperations.GetAsync();
                Assert.IsNotNull(result);
                result = legalBusinessProfileOperations.Get();
                Assert.IsNotNull(result);

                // ensure the proxy GetAsync() was called twice
                Assert.AreEqual(2, proxyGetAsyncCalls);
            }

            mockPartnerOperations.VerifyAll();
            mockRequestContext.VerifyAll();
        }

        /// <summary>
        /// Ensures that get legal business profile pass in the right values to the proxy and actually calls the proxy
        /// network methods with the vetting version passed.
        /// </summary>
        /// <returns> A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task LegalBusinessProfileOperations_VerifyGetLegalBusinessProfileWithVettingVersionPassed()
        {
            int proxyGetAsyncCalls = 0;

            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            var legalBusinessProfileOperations = new LegalBusinessProfileOperations(mockPartnerOperations.Object);

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below
                ShimPartnerServiceProxy<string, string>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                    PartnerServiceProxy<string, string> jsonProxy,
                    IPartner partnerOperations,
                    string resourcePath,
                    IFailedPartnerServiceResponseHandler errorHandler,
                    JsonConverter jsonConverter) =>
                {
                    Assert.AreEqual(PartnerService.Instance.Configuration.Apis.GetLegalBusinessProfile.Path, resourcePath);

                    // verify that the get operation was added to the uri parameters
                    Assert.AreEqual(jsonProxy.UriParameters.Count, 1);

                    var enumerator = jsonProxy.UriParameters.GetEnumerator();
                    Assert.IsTrue(enumerator.MoveNext());
                    Assert.AreEqual(enumerator.Current.Key, PartnerService.Instance.Configuration.Apis.GetLegalBusinessProfile.Parameters.VettingVersion);
                    Assert.AreEqual(enumerator.Current.Value, VettingVersion.LastFinalized.ToString());
                };

                var legalBusinessProfile = new LegalBusinessProfile
                {
                    CompanyName = "Company",
                    Address = new Models.Address
                    {
                        AddressLine1 = "Microsoft Street",
                        AddressLine2 = "Unit 4",
                        City = "Calgary",
                        State = "Alberta",
                        PostalCode = "T5T0N1"
                    },
                    PrimaryContact = new Contact
                    {
                        Email = "john@smith.com",
                        FirstName = "John",
                        LastName = "Smith",
                        PhoneNumber = "4444444444"
                    },
                    CompanyApproverAddress = new Models.Address
                    {
                        AddressLine1 = "Fake Street",
                        AddressLine2 = "Unit 3",
                        City = "Vancouver",
                        State = "BC",
                        PostalCode = "V5T0N1"
                    },
                    CompanyApproverEmail = "test@approver.com"
                };

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<LegalBusinessProfile, LegalBusinessProfile>.AllInstances.GetAsync = (PartnerServiceProxy<LegalBusinessProfile, LegalBusinessProfile> jsonProxy) =>
                {
                    // increment the number of the calls
                    proxyGetAsyncCalls++;
                    return Task.FromResult<LegalBusinessProfile>(legalBusinessProfile);
                };

                // call both sync and async versions of the get legal business profile API
                var result = await legalBusinessProfileOperations.GetAsync(VettingVersion.LastFinalized);
                Assert.IsNotNull(result);
                result = legalBusinessProfileOperations.Get(VettingVersion.LastFinalized);
                Assert.IsNotNull(result);

                // ensure the proxy GetAsync() was called twice
                Assert.AreEqual(2, proxyGetAsyncCalls);
            }

            mockPartnerOperations.VerifyAll();
            mockRequestContext.VerifyAll();
        }

        /// <summary>
        /// Ensures that update legal business profile passes in the right values to the proxy and actually calls the proxy.
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task LegalBusinessProfileOperations_VerifyUpdateLegalBusinessProfile()
        {
            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            var legalBusinessProfileOperations = new LegalBusinessProfileOperations(mockPartnerOperations.Object);
            int proxyUpdateAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below.
                ShimPartnerServiceProxy<LegalBusinessProfile, LegalBusinessProfile>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                PartnerServiceProxy<LegalBusinessProfile, LegalBusinessProfile> jsonProxy,
                IPartner partnerOperations,
                string resourcePath,
                IFailedPartnerServiceResponseHandler errorHandler,
                JsonConverter jsonConverter) =>
                {
                    Assert.IsNotNull(mockRequestContext.Object.RequestId);
                    Assert.AreEqual(mockRequestContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(PartnerService.Instance.Configuration.Apis.GetLegalBusinessProfile.Path, resourcePath);
                };

                var legalBusinessProfileToUpdate = new LegalBusinessProfile
                {
                    CompanyName = "Company",
                    Address = new Models.Address
                    {
                        AddressLine1 = "Microsoft Street",
                        AddressLine2 = "Unit 4",
                        City = "Calgary",
                        State = "Alberta",
                        PostalCode = "T5T0N1"
                    },
                    PrimaryContact = new Contact
                    {
                        Email = "john@smith.com",
                        FirstName = "John",
                        LastName = "Smith",
                        PhoneNumber = "4444444444"
                    },
                    CompanyApproverAddress = new Models.Address
                    {
                        AddressLine1 = "Fake Street",
                        AddressLine2 = "Unit 3",
                        City = "Vancouver",
                        State = "BC",
                        PostalCode = "V5T0N1"
                    },
                    CompanyApproverEmail = "test@approver.com"
                };

                // divert calls to UpdateAsync on the JsonPartnerServiceProxy to our handler.
                ShimPartnerServiceProxy<LegalBusinessProfile, LegalBusinessProfile>.AllInstances.PutAsyncT0 = (PartnerServiceProxy<LegalBusinessProfile, LegalBusinessProfile> jsonProxy, LegalBusinessProfile legalBusinessProfile) =>
                {
                    // increment the number of the calls.
                    proxyUpdateAsyncCalls++;

                    // return the user object.
                    return Task.FromResult<LegalBusinessProfile>(legalBusinessProfile);
                };

                // call both sync and async versions of the update legal business profile API.
                var updatedLegalBusinessProfile = await legalBusinessProfileOperations.UpdateAsync(legalBusinessProfileToUpdate);
                Assert.AreEqual(legalBusinessProfileToUpdate, updatedLegalBusinessProfile);
                updatedLegalBusinessProfile = legalBusinessProfileOperations.Update(legalBusinessProfileToUpdate);
                Assert.AreEqual(legalBusinessProfileToUpdate, updatedLegalBusinessProfile);

                // ensure the proxy UpdateAsync() was called twice.
                Assert.AreEqual(2, proxyUpdateAsyncCalls);
                mockPartnerOperations.VerifyAll();
                mockRequestContext.VerifyAll();
            }
        }
    }
}