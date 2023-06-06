// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests
{
    using System;
    using Moq;
    using Agreements;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="AgreementTemplateOperations"/> class.
    /// </summary>
    [TestClass]
    public class AgreementTemplateOperationsTests
    {
        private const string AgreementTemplateId = "1a498d4a-1cc0-4035-a297-1532f4b49822";

        /// <summary>
        /// Initializes the test suite.
        /// </summary>
        /// <param name="testContext">The test context.</param>
        [ClassInitialize]
        public static void InitializeTestSuite(TestContext testContext)
        {
        }

        /// <summary>
        /// Validates that parameter validation is done in the constructor.
        /// </summary>
        [TestMethod]
        public void AgreementTemplateOperationsConstructorFailsOnInvalidParameters()
        {
            VerifyAgreementTemplateOperationsConstructionFails(null, AgreementTemplateId);
            VerifyAgreementTemplateOperationsConstructionFails(Mock.Of<IPartner>(), null);
            VerifyAgreementTemplateOperationsConstructionFails(Mock.Of<IPartner>(), string.Empty);
            VerifyAgreementTemplateOperationsConstructionFails(Mock.Of<IPartner>(), "     ");
        }

        /// <summary>
        /// Ensures that the Document member in teh agreement template operations object is not null.
        /// </summary>
        [TestMethod]
        public void VerifyDocumentMemberIsNotNull()
        {
            var partnerOperations = new Mock<IPartner>();

            var agreementTemplateOperations = new AgreementTemplateOperations(partnerOperations.Object, AgreementTemplateId);
            Assert.IsNotNull(agreementTemplateOperations.Document);
            Assert.AreEqual(partnerOperations.Object, agreementTemplateOperations.Document.Partner);
            Assert.IsNotNull(agreementTemplateOperations.Document.Context);
            Assert.AreEqual(AgreementTemplateId, agreementTemplateOperations.Document.Context.TemplateId);
        }

        private static void VerifyAgreementTemplateOperationsConstructionFails(IPartner partnerOperations, string templateId)
        {
            try
            {
                var _ = new AgreementTemplateOperations(partnerOperations, templateId);
                Assert.Fail("Constructor did not throw an exception for invalid parameter.");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);
                // We don't necessarily care about the specific error message returned, just that it's non-null.
                Assert.IsNotNull(ex.Message);
            }
        }
    }
}
