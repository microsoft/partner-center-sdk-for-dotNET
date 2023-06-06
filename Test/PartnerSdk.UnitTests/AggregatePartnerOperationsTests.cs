// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests
{
    using System;
    using Moq;
    using RequestContext;
    using VisualStudio.TestTools.UnitTesting;
    
    /// <summary>
    /// Tests the <see cref="AggregatePartnerOperations"/> class.
    /// </summary>
    [TestClass]
    public class AggregatePartnerOperationsTests
    {
        /// <summary>
        /// Ensures when creating a partner operations object with a context that the correct context
        /// is applied and the credentials are passed properly.
        /// </summary>
        [TestMethod]
        public void AggregatePartnerOperationsTests_VerifyScopedPartnerOperation()
        {
            // Arrange
            Mock<IPartnerCredentials> mockCredentials = new Mock<IPartnerCredentials>();
            IRequestContext expectedContext = RequestContextFactory.Instance.Create();
            
            // Act
            var parnterOperations = PartnerService.Instance.CreatePartnerOperations(mockCredentials.Object);
            var scopedPartnerOperations = parnterOperations.With(expectedContext);

            // Assert, ensure that the partner operations maintain the credentials
            Assert.AreEqual(parnterOperations.Credentials, mockCredentials.Object);

            // ensure that there is not specific request id set as this is the global partner operations
            Assert.AreEqual(parnterOperations.RequestContext.RequestId, Guid.Empty);

            // ensure that the scoped partner operations have the same credentials and the expected context
            Assert.AreEqual(scopedPartnerOperations.Credentials, mockCredentials.Object);
            Assert.AreEqual(scopedPartnerOperations.RequestContext, expectedContext);
        }
    }
}