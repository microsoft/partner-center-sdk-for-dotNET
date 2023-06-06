// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests.ValidationStatus
{
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models.ValidationStatus;
    using Microsoft.Store.PartnerCenter.Models.ValidationStatus.Enums;
    using Microsoft.Store.PartnerCenter.RequestContext;
    using Microsoft.Store.PartnerCenter.ValidationStatus;
    using Moq;
    using Network;
    using Network.Fakes;
    using QualityTools.Testing.Fakes;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit tests for ValidationStatusOperations
    /// </summary>
    [TestClass]
    public class ValidationStatusOperationsTests
    {
        /// <summary>
        /// Ensures that validation status pass in the right values to the proxy and actually calls the proxy for a validation type.
        /// </summary>
        /// <returns> A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task ValidationStatusOperationsTests_GetValidationStatus()
        {
            var validationType = ValidationType.Account;

            int proxyPostAsyncCalls = 0;
            var testCustomerId = System.Guid.NewGuid().ToString();

            Mock<IRequestContext> mockRequestContext = new Mock<IRequestContext>(MockBehavior.Strict);
            mockRequestContext.Setup(context => context.RequestId).Returns(System.Guid.NewGuid());

            Mock<IPartner> mockPartnerOperations = new Mock<IPartner>(MockBehavior.Strict);
            mockPartnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

            var validationStatusOperations = new ValidationStatusOperations(mockPartnerOperations.Object, testCustomerId);

            using (ShimsContext.Create())
            {
                // divert calls to PostAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<ValidationStatus, ValidationStatus>.AllInstances.GetAsync = (PartnerServiceProxy<ValidationStatus, ValidationStatus> jsonProxy) =>
                {
                    Assert.AreEqual(PartnerService.Instance.Configuration.Apis.GetValidationStatus.Path.Replace("{0}", testCustomerId), jsonProxy.ResourcePath);

                    // increment the number of the calls
                    proxyPostAsyncCalls++;
                    return Task.FromResult<ValidationStatus>(new ValidationStatus { Type = validationType, Status = "Approved" });
                };

                // call both sync and async versions of the validation status API
                var result = await validationStatusOperations.GetValidationStatusAsync(validationType);
                Assert.AreEqual("Approved", result.Status);
                Assert.AreEqual(validationType, result.Type);

                result = validationStatusOperations.GetValidationStatus(validationType);
                Assert.AreEqual("Approved", result.Status);
                Assert.AreEqual(validationType, result.Type);

                // ensure the proxy PostAsync() was called twice
                Assert.AreEqual(2, proxyPostAsyncCalls);
            }
        }
    }
}