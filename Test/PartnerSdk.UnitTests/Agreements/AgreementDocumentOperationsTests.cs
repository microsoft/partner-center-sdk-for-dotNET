// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Models.Agreements;
    using Moq;
    using Network.Fakes;
    using Agreements;
    using QualityTools.Testing.Fakes;
    using RequestContext;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="AgreementDocumentOperations"/> class.
    /// </summary>
    [TestClass]
    public class AgreementDocumentOperationsTests
    {
        private static Mock<IPartnerCredentials> mockCredentials;
        private static Mock<IRequestContext> mockRequestContext;

        private const string AgreementTemplateId = "1a498d4a-1cc0-4035-a297-1532f4b49822";
        private const string DisplayUri = "https://foo";
        private const string DownloadUri = "https://bar";
        private const string DefaultLanguage = "en-US";
        private const string DefaultCountry = "US";

        /// <summary>
        /// Initializes the test suite.
        /// </summary>
        /// <param name="testContext">The test context.</param>
        [ClassInitialize]
        public static void InitializeTestSuite(TestContext testContext)
        {
            mockCredentials = new Mock<IPartnerCredentials>();
            mockCredentials.Setup(credentials => credentials.PartnerServiceToken).Returns("Fake Token");
            mockCredentials.Setup(credentials => credentials.ExpiresAt).Returns(DateTimeOffset.MaxValue);

            mockRequestContext = new Mock<IRequestContext>();
            mockRequestContext.Setup(context => context.CorrelationId).Returns(Guid.NewGuid());
            mockRequestContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());
        }

        /// <summary>
        /// Validates that parameter validation is done in the constructor.
        /// </summary>
        [TestMethod]
        public void AgreementDocumentOperationsConstructorFailsOnInvalidParameters()
        {
            VerifyAgreementDocumentOperationsConstructionFails(partnerOperations: null, templateId: AgreementTemplateId);
            VerifyAgreementDocumentOperationsConstructionFails(Mock.Of<IPartner>(), templateId: null);
            VerifyAgreementDocumentOperationsConstructionFails(Mock.Of<IPartner>(), templateId: string.Empty);
            VerifyAgreementDocumentOperationsConstructionFails(Mock.Of<IPartner>(), templateId: "    ");
        }

        /// <summary>
        /// Verifies that the constructor sets up the right members.
        /// </summary>
        [TestMethod]
        public void AgreementDocumentOperationsConstructorSucceeds()
        {
            VerifyAgreementDocumentOperationsConstructionSucceeds(language: null, country: null);
            VerifyAgreementDocumentOperationsConstructionSucceeds(language: null, country: DefaultCountry);
            VerifyAgreementDocumentOperationsConstructionSucceeds(language: string.Empty, country: DefaultCountry);
            VerifyAgreementDocumentOperationsConstructionSucceeds(language: "   ", country: DefaultCountry);
            VerifyAgreementDocumentOperationsConstructionSucceeds(DefaultLanguage, country: null);
            VerifyAgreementDocumentOperationsConstructionSucceeds(DefaultLanguage, country: string.Empty);
            VerifyAgreementDocumentOperationsConstructionSucceeds(DefaultLanguage, country: "    ");
        }

        /// <summary>
        /// Get Agreement Document success tests.
        /// </summary>
        [TestMethod]
        public void GetAgreementDocumentSucceeds()
        {
            using (ShimsContext.Create())
            {
                ShimPartnerServiceProxy<AgreementDocument, AgreementDocument>.AllInstances.GetAsync
                    = jsonProxy =>
                    {
                        Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                        Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                        Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);

                        if (string.Equals($"agreementtemplates/{AgreementTemplateId}/document", jsonProxy.ResourcePath, StringComparison.OrdinalIgnoreCase))
                        {
                            var languageParameter = jsonProxy.UriParameters.FirstOrDefault(param =>
                                string.Equals(param.Key, "language", StringComparison.OrdinalIgnoreCase));
                            var countryParameter = jsonProxy.UriParameters.FirstOrDefault(param =>
                                string.Equals(param.Key, "country", StringComparison.OrdinalIgnoreCase));

                            return Task.FromResult(new AgreementDocument
                            {
                                DisplayUri = DisplayUri,
                                DownloadUri = DownloadUri,
                                Language = languageParameter.Value ?? DefaultLanguage,
                                Country = countryParameter.Value ?? DefaultCountry
                            });
                        }

                        return null;
                    };

                var partnerOperations = new Mock<IPartner>();
                partnerOperations.Setup(partner => partner.Credentials).Returns(mockCredentials.Object);
                partnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

                VerifyGetAgreementDocumentSucceeds(partnerOperations.Object, null, null, DefaultLanguage, DefaultCountry);
                VerifyGetAgreementDocumentSucceeds(partnerOperations.Object, "de-DE", null, "de-DE", DefaultCountry);
                VerifyGetAgreementDocumentSucceeds(partnerOperations.Object, null, "CA", DefaultLanguage, "CA");
                VerifyGetAgreementDocumentSucceeds(partnerOperations.Object, DefaultLanguage, DefaultCountry, DefaultLanguage, DefaultCountry);
                VerifyGetAgreementDocumentSucceeds(partnerOperations.Object, "de-DE", "KO", "de-DE", "KO");
            }
        }

        /// <summary>
        /// Get Agreement Document by language fails when invalid values are passed in.
        /// </summary>
        [TestMethod]
        public void GetAgreementDocumentByLanguageFailsOnInvalidValues()
        {
            var agreementDocumentOperations = new AgreementDocumentOperations(Mock.Of<IPartner>(), AgreementTemplateId);

            VerifyGetAgreementDocumentByLanguageFails(agreementDocumentOperations, null);
            VerifyGetAgreementDocumentByLanguageFails(agreementDocumentOperations, string.Empty);
            VerifyGetAgreementDocumentByLanguageFails(agreementDocumentOperations, "   ");
        }

        /// <summary>
        /// Get Agreement Document by country fails when invalid values are passed in.
        /// </summary>
        [TestMethod]
        public void GetAgreementDocumentByCountryFailsOnInvalidValues()
        {
            var agreementDocumentOperations = new AgreementDocumentOperations(Mock.Of<IPartner>(), AgreementTemplateId);

            VerifyGetAgreementDocumentByCountryFails(agreementDocumentOperations, null);
            VerifyGetAgreementDocumentByCountryFails(agreementDocumentOperations, string.Empty);
            VerifyGetAgreementDocumentByCountryFails(agreementDocumentOperations, "   ");
        }

        /// <summary>
        /// Get Agreement Document by language and by country success tests.
        /// </summary>
        [TestMethod]
        public void GetAgreementDocumentByLanguageAndByCountrySucceeds()
        {
            using (ShimsContext.Create())
            {
                ShimPartnerServiceProxy<AgreementDocument, AgreementDocument>.AllInstances.GetAsync
                    = jsonProxy =>
                    {
                        Assert.AreEqual(mockCredentials.Object, jsonProxy.Partner.Credentials);
                        Assert.AreEqual(mockRequestContext.Object.RequestId, jsonProxy.RequestId);
                        Assert.AreEqual(mockRequestContext.Object.CorrelationId, jsonProxy.CorrelationId);

                        if (string.Equals($"agreementtemplates/{AgreementTemplateId}/document",
                            jsonProxy.ResourcePath, StringComparison.OrdinalIgnoreCase))
                        {
                            var languageParameter = jsonProxy.UriParameters.FirstOrDefault(param =>
                                string.Equals(param.Key, "language", StringComparison.OrdinalIgnoreCase));
                            var countryParameter = jsonProxy.UriParameters.FirstOrDefault(param =>
                                string.Equals(param.Key, "country", StringComparison.OrdinalIgnoreCase));

                            return Task.FromResult(new AgreementDocument
                            {
                                DisplayUri = DisplayUri,
                                DownloadUri = DownloadUri,
                                Language = languageParameter.Value ?? DefaultLanguage,
                                Country = countryParameter.Value ?? DefaultCountry
                            });
                        }

                        return null;
                    };

                var partnerOperations = new Mock<IPartner>();
                partnerOperations.Setup(partner => partner.Credentials).Returns(mockCredentials.Object);
                partnerOperations.Setup(partner => partner.RequestContext).Returns(mockRequestContext.Object);

                var agreementDocumentOperations = new AgreementDocumentOperations(partnerOperations.Object, AgreementTemplateId);

                VerifyGetAgreementDocumentByLanguageAndCountrySucceeds(agreementDocumentOperations.ByLanguage("de-DE"), "de-DE", DefaultCountry);
                VerifyGetAgreementDocumentByLanguageAndCountrySucceeds(agreementDocumentOperations.ByCountry("CA"), DefaultLanguage, "CA");
                VerifyGetAgreementDocumentByLanguageAndCountrySucceeds(agreementDocumentOperations.ByLanguage("de-DE").ByCountry("CA"), "de-DE", "CA");
                VerifyGetAgreementDocumentByLanguageAndCountrySucceeds(agreementDocumentOperations.ByCountry("CA").ByLanguage("de-DE"), "de-DE", "CA");
            }
        }

        private static void VerifyAgreementDocumentOperationsConstructionFails(IPartner partnerOperations, string templateId)
        {
            try
            {
                var _ = new AgreementDocumentOperations(partnerOperations, templateId);
                Assert.Fail("Constructor did not throw an exception for invalid parameter.");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);

                // We don't necessarily care about the specific error message returned, just that it's non-null.
                Assert.IsNotNull(ex.Message);
            }
        }

        private static void VerifyAgreementDocumentOperationsConstructionSucceeds(string language, string country)
        {
            var partnerOperations = new Mock<IPartner>();

            var agreementTemplateOperations = new AgreementDocumentOperations(partnerOperations.Object, AgreementTemplateId, language, country);

            Assert.AreEqual(partnerOperations.Object, agreementTemplateOperations.Partner);
            Assert.IsNotNull(agreementTemplateOperations.Context);
            Assert.AreEqual(AgreementTemplateId, agreementTemplateOperations.Context.TemplateId);
            if (string.IsNullOrWhiteSpace(language) && string.IsNullOrWhiteSpace(country))
            {
                Assert.IsNull(agreementTemplateOperations.Context.TransformOptions);
            }
            else
            {
                Assert.IsNotNull(agreementTemplateOperations.Context.TransformOptions);
                Assert.AreEqual(language, agreementTemplateOperations.Context.TransformOptions.Language);
                Assert.AreEqual(country, agreementTemplateOperations.Context.TransformOptions.Country);
            }
        }

        private static void VerifyGetAgreementDocumentSucceeds(IPartner partnerOperations, string language, string country, string expectedLanguage, string expectedCountry)
        {
            var agreementDocumentOperations = new AgreementDocumentOperations(partnerOperations, AgreementTemplateId, language, country);
            var agreementDocument = agreementDocumentOperations.Get();

            Assert.IsNotNull(agreementDocument);
            Assert.AreEqual(DisplayUri, agreementDocument.DisplayUri);
            Assert.AreEqual(DownloadUri, agreementDocument.DownloadUri);
            Assert.AreEqual(expectedLanguage, agreementDocument.Language);
            Assert.AreEqual(expectedCountry, agreementDocument.Country);
        }

        private static void VerifyGetAgreementDocumentByLanguageFails(IAgreementDocument agreementDocumentOperations, string language)
        {
            try
            {
                var _ = agreementDocumentOperations.ByLanguage(language);
                Assert.Fail("ByLanguage() did not throw an exception for invalid language.");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);

                // We don't necessarily care about the specific error message returned, just that it's non-null.
                Assert.IsNotNull(ex.Message);
            }
        }

        private static void VerifyGetAgreementDocumentByCountryFails(IAgreementDocument agreementDocumentOperations, string country)
        {
            try
            {
                var _ = agreementDocumentOperations.ByCountry(country);
                Assert.Fail("ByCountry() did not throw an exception for invalid country.");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);

                // We don't necessarily care about the specific error message returned, just that it's non-null.
                Assert.IsNotNull(ex.Message);
            }
        }

        private static void VerifyGetAgreementDocumentByLanguageAndCountrySucceeds(IAgreementDocument agreementDocOperations, string expectedLanguage, string expectedCountry)
        {
            var agreementDocument = agreementDocOperations.Get();

            Assert.IsNotNull(agreementDocument);
            Assert.AreEqual(DisplayUri, agreementDocument.DisplayUri);
            Assert.AreEqual(DownloadUri, agreementDocument.DownloadUri);
            Assert.AreEqual(expectedLanguage, agreementDocument.Language);
            Assert.AreEqual(expectedCountry, agreementDocument.Country);
        }
    }
}
