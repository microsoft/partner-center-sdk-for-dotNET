// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests
{
    using System;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="AuthenticationToken"/> class.
    /// </summary>
    [TestClass]
    public class AuthenticationTokenTests
    {
        /// <summary>
        /// Ensures the token expiry logic works as expected.
        /// </summary>
        [TestMethod]
        public void AuthenticationTokenTests_VerifyTokenExpiry()
        {
            var expiryTimeInTheFuture = new DateTimeOffset(DateTime.UtcNow + TimeSpan.FromHours(1));
            var expiryTimeInThePast = new DateTimeOffset(DateTime.UtcNow - TimeSpan.FromSeconds(1));
            
            var authenticationToken = new AuthenticationToken("SomeToken", expiryTimeInTheFuture) { ExpiryBuffer = TimeSpan.FromMinutes(30) };
            Assert.IsFalse(authenticationToken.IsExpired());

            authenticationToken.ExpiryBuffer = TimeSpan.FromMinutes(60);
            Assert.IsTrue(authenticationToken.IsExpired());

            authenticationToken = new AuthenticationToken("SomeToken", expiryTimeInThePast) { ExpiryBuffer = TimeSpan.FromMinutes(0) };
            Assert.IsTrue(authenticationToken.IsExpired());
        }
    }
}