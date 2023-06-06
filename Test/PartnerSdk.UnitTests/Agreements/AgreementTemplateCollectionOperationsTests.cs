// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests
{
    using System;
    using Moq;
    using Agreements;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="AgreementTemplateCollectionOperations"/> class.
    /// </summary>
    [TestClass]
    public class AgreementTemplateCollectionOperationsTests
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
        /// GetAgreementTemplateOperationsById fails on invalid template ID.
        /// </summary>
        [TestMethod]
        public void GetAgreementTemplateOperationsByIdFailsForInvalidTemplateId()
        {
            var partnerOperations = new Mock<IPartner>();

            var agreementTemplateCollectionOperations = new AgreementTemplateCollectionOperations(partnerOperations.Object);

            VerifyGetAgreementTemplateOperationsByIdFails(agreementTemplateCollectionOperations, null);
            VerifyGetAgreementTemplateOperationsByIdFails(agreementTemplateCollectionOperations, string.Empty);
            VerifyGetAgreementTemplateOperationsByIdFails(agreementTemplateCollectionOperations, "    ");
            VerifyGetAgreementTemplateOperationsByIdFails(agreementTemplateCollectionOperations, null, useById: true);
            VerifyGetAgreementTemplateOperationsByIdFails(agreementTemplateCollectionOperations, string.Empty, useById: true);
            VerifyGetAgreementTemplateOperationsByIdFails(agreementTemplateCollectionOperations, "     ", useById: true);
        }

        /// <summary>
        /// GetAgreementTemplateOperationsById success tests.
        /// </summary>
        [TestMethod]
        public void GetAgreementTemplateOperationsByIdSucceeds()
        {
            var partnerOperations = new Mock<IPartner>();
            var agreementTemplateOperations = new AgreementTemplateCollectionOperations(partnerOperations.Object);

            VerifyGetAgreementTemplateOperationsByIdSucceeds(partnerOperations.Object, agreementTemplateOperations[AgreementTemplateId]);
            VerifyGetAgreementTemplateOperationsByIdSucceeds(partnerOperations.Object, agreementTemplateOperations.ById(AgreementTemplateId));
        }

        private static void VerifyGetAgreementTemplateOperationsByIdFails(IAgreementTemplateCollection agreementTemplateCollectionOperations, string templateId, bool useById = false)
        {
            try
            {
                var _ = !useById ? agreementTemplateCollectionOperations[templateId] : agreementTemplateCollectionOperations.ById(templateId);
                Assert.Fail("Invalid templateId did not result in an exception.");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);
                // We don't necessarily care about the specific error message returned, just that it's non-null.
                Assert.IsNotNull(ex.Message);
            }
        }

        private static void VerifyGetAgreementTemplateOperationsByIdSucceeds(IPartner partnerOperations, IAgreementTemplate agreementTemplateOperationsById)
        {
            Assert.IsNotNull(agreementTemplateOperationsById);
            Assert.AreEqual(partnerOperations, agreementTemplateOperationsById.Partner);
            Assert.IsNotNull(agreementTemplateOperationsById.Context);
            Assert.AreEqual(AgreementTemplateId, agreementTemplateOperationsById.Context.TemplateId);
        }
    }
}
