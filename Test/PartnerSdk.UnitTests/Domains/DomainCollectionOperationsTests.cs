// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests.Domains
{
    using Moq;
    using PartnerCenter.Domains;
    using PartnerCenter.Domains.Fakes;
    using QualityTools.Testing.Fakes;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="DomainCollectionOperations"/> class.
    /// </summary>
    [TestClass]
    public class DomainCollectionOperationsTests
    {
        /// <summary>
        /// The expected domain.
        /// </summary>
        private static string expectedDomain = "domain";

        /// <summary>
        /// Ensures that checking if a domain exists or not as expected.
        /// </summary>
        [TestMethod]
        public void DomainCollectionOperationsTests_VerifyByDomainNavigation()
        {
            var domainsCollectionOperations = new DomainCollectionOperations(Mock.Of<IPartner>());

            using (ShimsContext.Create())
            {
                // route all DomainsOperations constructors to our handler below
                ShimDomainOperations.ConstructorIPartnerString =
                    (DomainOperations operations, IPartner partnerOperations, string domain) =>
                    {
                        Assert.AreEqual(expectedDomain, domain);
                    };

                // invoke by domain
                Assert.IsNotNull(domainsCollectionOperations.ByDomain(expectedDomain));
            }
        }
    }
}