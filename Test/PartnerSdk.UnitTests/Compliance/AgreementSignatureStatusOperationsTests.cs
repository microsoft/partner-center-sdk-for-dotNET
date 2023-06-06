// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.Store.PartnerCenter.Compliance;
using Microsoft.Store.PartnerCenter.ErrorHandling;
using Microsoft.Store.PartnerCenter.Models.Compliance;
using Microsoft.Store.PartnerCenter.Network;
using Microsoft.Store.PartnerCenter.Network.Fakes;
using Microsoft.Store.PartnerCenter.RequestContext;
using Moq;
using Newtonsoft.Json;

namespace Microsoft.Store.PartnerCenter.UnitTests.Compliance
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AgreementSignatureStatusOperationsTests
    {
        [TestMethod]
        public async Task AgreementSignatureStatusOperations_VerifyAgreementSignatureStatus()
        {
            int proxyGetAsyncCalls = 0;

            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            var agreementSignatureStatusOperations = new AgreementSignatureStatusOperations(mockPartnerOperations.Object);

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
                    Assert.AreEqual(PartnerService.Instance.Configuration.Apis.GetAgreementSignatureStatus.Path, resourcePath);
                };

                var agreementSignatureStatus = new AgreementSignatureStatus
                {
                    IsAgreementSigned = true
                };

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<AgreementSignatureStatus, AgreementSignatureStatus>.AllInstances.GetAsync = (PartnerServiceProxy<AgreementSignatureStatus, AgreementSignatureStatus> jsonProxy) =>
                {
                    // increment the number of the calls
                    proxyGetAsyncCalls++;
                    return Task.FromResult<AgreementSignatureStatus>(agreementSignatureStatus);
                };

                // call both sync and async versions of the get legal business profile API
                var result = await agreementSignatureStatusOperations.GetAsync(mpnId:"111111");
                Assert.IsNotNull(result);
                result = agreementSignatureStatusOperations.Get();
                Assert.IsNotNull(result);

                // ensure the proxy GetAsync() was called twice
                Assert.AreEqual(2, proxyGetAsyncCalls);
            }

            mockPartnerOperations.VerifyAll();
            mockRequestContext.VerifyAll();
        }
    }
}
