// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DevicesDeployment;
    using DevicesDeployment.Fakes;
    using ErrorHandling;
    using Models;
    using Models.DevicesDeployment;
    using Models.JsonConverters;
    using Moq;
    using Network;
    using Network.Fakes;
    using Newtonsoft.Json;
    using QualityTools.Testing.Fakes;
    using RequestContext;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="ConfigurationPolicyOperations"/> and <see cref="ConfigurationPolicyCollectionOperations"/> class.
    /// </summary>
    [TestClass]
    public class ConfigurationPolicyOperationsTests
    {
        /// <summary>
        /// The expected credentials.
        /// </summary>
        private static Mock<IPartnerCredentials> expectedCredentials;

        /// <summary>
        /// The expected context.
        /// </summary>
        private static Mock<IRequestContext> expectedContext;

        /// <summary>
        /// The expected customer Id.
        /// </summary>
        private static string expectedCustomerId = Guid.NewGuid().ToString();

        /// <summary>
        /// The expected policy Id.
        /// </summary>
        private static string expectedPolicyId = Guid.NewGuid().ToString();

        /// <summary>
        /// The partner configuration policy collection operations instance under test.
        /// </summary>
        private static ConfigurationPolicyCollectionOperations configurationPolicyCollectionOperations;

        /// <summary>
        /// The partner configuration policy operations instance under test.
        /// </summary>
        private static ConfigurationPolicyOperations configurationPolicyOperations;

        /// <summary>
        /// Initializes the test suite.
        /// </summary>
        /// <param name="testContext">The test context.</param>
        [ClassInitialize]
        public static void InitializeTestSuite(TestContext testContext)
        {
            expectedCredentials = new Mock<IPartnerCredentials>();
            expectedCredentials.Setup(credentials => credentials.PartnerServiceToken).Returns("Fake Token");
            expectedCredentials.Setup(credentials => credentials.ExpiresAt).Returns(DateTimeOffset.MaxValue);

            expectedContext = new Mock<IRequestContext>();
            expectedContext.Setup(context => context.CorrelationId).Returns(Guid.NewGuid());
            expectedContext.Setup(context => context.RequestId).Returns(Guid.NewGuid());

            Mock<IPartner> partnerOperations = new Mock<IPartner>();
            partnerOperations.Setup(partner => partner.Credentials).Returns(expectedCredentials.Object);
            partnerOperations.Setup(partner => partner.RequestContext).Returns(expectedContext.Object);

            configurationPolicyCollectionOperations = new ConfigurationPolicyCollectionOperations(partnerOperations.Object, expectedCustomerId);
            configurationPolicyOperations = new ConfigurationPolicyOperations(partnerOperations.Object, expectedCustomerId, expectedPolicyId);
        }

        /// <summary>
        /// Ensures that create configuration policy pass in the right values to the proxy and actually calls the proxy
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task ConfigurationPolicyTests_VerifyCreateConfigurationPolicy()
        {
            int proxyPostAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below
                ShimPartnerServiceProxy<ConfigurationPolicy, ResourceCollection<ConfigurationPolicy>>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                    PartnerServiceProxy<ConfigurationPolicy, ResourceCollection<ConfigurationPolicy>> jsonProxy,
                    IPartner partnerOperations,
                    string resourcePath,
                    IFailedPartnerServiceResponseHandler errorHandler,
                    JsonConverter jsonConverter) =>
                {
                    Assert.AreEqual(expectedCredentials.Object, partnerOperations.Credentials);
                    Assert.AreEqual(expectedContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.CreateConfigurationPolicy.Path, expectedCustomerId), resourcePath);
                    Assert.IsInstanceOfType(jsonConverter, typeof(ResourceCollectionConverter<ConfigurationPolicy>));
                };

                // divert calls to PostAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<ConfigurationPolicy, ConfigurationPolicy>.AllInstances.PostAsyncT0 = (PartnerServiceProxy<ConfigurationPolicy, ConfigurationPolicy> jsonProxy, ConfigurationPolicy newConfigurationPolicy) =>
                {
                    // increment the number of the calls
                    proxyPostAsyncCalls++;

                    // return the same configuration policy object
                    return Task.FromResult<ConfigurationPolicy>(newConfigurationPolicy);
                };

                // configure a couple of configuration policies
                ConfigurationPolicy firstConfigPolicy = new ConfigurationPolicy()
                {
                    Name = "testProfile1",
                    Description = "test new profile from unit tests",
                };

                List<PolicySettingsType> policySettings = new List<PolicySettingsType>();
                policySettings.Add(PolicySettingsType.OobeUserNotLocalAdmin);
                policySettings.Add(PolicySettingsType.SkipExpressSettings);
                firstConfigPolicy.PolicySettings = policySettings;

                ConfigurationPolicy secondConfigPolicy = new ConfigurationPolicy()
                {
                    Name = "testProfile2",
                    Description = "test new profile from unit tests",
                };

                List<PolicySettingsType> policySettings2 = new List<PolicySettingsType>();
                policySettings.Add(PolicySettingsType.OobeUserNotLocalAdmin);
                secondConfigPolicy.PolicySettings = policySettings2;

                // ensure creating null configuration policy will fail
                bool nullCheckOk = false;

                try
                {
                    await configurationPolicyCollectionOperations.CreateAsync(null);
                }
                catch (ArgumentNullException)
                {
                    try
                    {
                        configurationPolicyCollectionOperations.Create(null);
                    }
                    catch (ArgumentNullException)
                    {
                        nullCheckOk = true;
                    }
                }

                // ensure that the exceptions were thrown
                Assert.IsTrue(nullCheckOk);

                // call both sync and async versions of the create configuration policy API
                var firstReturnedConfigPolicy = await configurationPolicyCollectionOperations.CreateAsync(firstConfigPolicy);
                var secondReturnedConfigPolicy = configurationPolicyCollectionOperations.Create(secondConfigPolicy);

                // ensure the proxy PostAsync() was called twice
                Assert.AreEqual(proxyPostAsyncCalls, 2);

                // ensure that the expected values were returned
                Assert.AreEqual(firstReturnedConfigPolicy, firstConfigPolicy);
                Assert.AreEqual(secondReturnedConfigPolicy, secondConfigPolicy);
            }
        }

        /// <summary>
        /// Ensures that get configuration policies pass in the right values to the proxy and actually calls the proxy
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task ConfigurationPolicyTests_VerifyGetConfigurationPolicies()
        {
            int proxyGetAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below
                ShimPartnerServiceProxy<ConfigurationPolicy, ResourceCollection<ConfigurationPolicy>>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                    PartnerServiceProxy<ConfigurationPolicy, ResourceCollection<ConfigurationPolicy>> jsonProxy,
                    IPartner partnerOperations,
                    string resourcePath,
                    IFailedPartnerServiceResponseHandler errorHandler,
                    JsonConverter jsonConverter) =>
                {
                    Assert.AreEqual(expectedCredentials.Object, partnerOperations.Credentials);
                    Assert.AreEqual(expectedContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.GetConfigurationPolicies.Path, expectedCustomerId), resourcePath);
                };

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<ConfigurationPolicy, ResourceCollection<ConfigurationPolicy>>.AllInstances.GetAsync = (PartnerServiceProxy<ConfigurationPolicy, ResourceCollection<ConfigurationPolicy>> jsonProxy) =>
                {
                    // increment the number of the calls
                    proxyGetAsyncCalls++;
                    return Task.FromResult<ResourceCollection<ConfigurationPolicy>>(null);
                };

                // call both sync and async versions of the Get configuration policies API
                await configurationPolicyCollectionOperations.GetAsync();
                configurationPolicyCollectionOperations.Get();

                // ensure the proxy GetAsync() was called twice
                Assert.AreEqual(proxyGetAsyncCalls, 2);
            }
        }

        /// <summary>
        /// Ensures that getting a configuration policy by id works as expected.
        /// </summary>
        [TestMethod]
        public void ConfigurationPolicyTests_VerifyByIdNavigation()
        {
            using (ShimsContext.Create())
            {
                // route all Configuration policy operation constructors to our handler below
                ShimConfigurationPolicyOperations.ConstructorIPartnerStringString =
                    (ConfigurationPolicyOperations operations, IPartner partnerOperations, string customerId, string policyId) =>
                    {
                        // ensure the configuration policy collection operations pass in the right values to the configuration policy operations
                        Assert.AreEqual(customerId, expectedCustomerId);
                        Assert.AreEqual(expectedPolicyId, expectedPolicyId);
                        Assert.AreEqual(partnerOperations.Credentials, expectedCredentials.Object);
                        Assert.AreEqual(partnerOperations.RequestContext, expectedContext.Object);
                    };

                // invoke by id
                Assert.IsNotNull(configurationPolicyCollectionOperations.ById(expectedCustomerId));
            }
        }

        /// <summary>
        /// Ensures that patching configuration policies pass in the right values to the proxy and actually calls the proxy
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task ConfigurationPolicyTests_VerifyPatchConfigurationPolicy()
        {
            int proxyPostAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below
                ShimPartnerServiceProxy<ConfigurationPolicy, ConfigurationPolicy>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                    PartnerServiceProxy<ConfigurationPolicy, ConfigurationPolicy> jsonProxy,
                    IPartner partnerOperations,
                    string resourcePath,
                    IFailedPartnerServiceResponseHandler errorHandler,
                    JsonConverter jsonConverter) =>
                {
                    Assert.AreEqual(expectedCredentials.Object, partnerOperations.Credentials);
                    Assert.AreEqual(expectedContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.UpdateConfigurationPolicy.Path, expectedCustomerId, expectedPolicyId), resourcePath);
                };

                // divert calls to PostAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<ConfigurationPolicy, ConfigurationPolicy>.AllInstances.PutAsyncT0 = (PartnerServiceProxy<ConfigurationPolicy, ConfigurationPolicy> jsonProxy, ConfigurationPolicy updateConfigurationPolicyRequest) =>
                {
                    // increment the number of the calls
                    proxyPostAsyncCalls++;

                    // return the same configuration policy object
                    return Task.FromResult<ConfigurationPolicy>(updateConfigurationPolicyRequest);
                };

                // configure a couple of configuration policies
                ConfigurationPolicy firstConfigPolicy = new ConfigurationPolicy()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "testProfile",
                    Description = "test update profile from unit tests",
                };

                List<PolicySettingsType> policySettings = new List<PolicySettingsType>();
                policySettings.Add(PolicySettingsType.OobeUserNotLocalAdmin);
                policySettings.Add(PolicySettingsType.SkipExpressSettings);
                firstConfigPolicy.PolicySettings = policySettings;

                ConfigurationPolicy secondConfigPolicy = new ConfigurationPolicy()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "testProfile2",
                    Description = "test update profile from unit tests",
                };

                List<PolicySettingsType> policySettings2 = new List<PolicySettingsType>();
                policySettings.Add(PolicySettingsType.OobeUserNotLocalAdmin);
                secondConfigPolicy.PolicySettings = policySettings2;

                // ensure creating null configuration policy will fail
                bool nullCheckOk = false;

                try
                {
                    await configurationPolicyOperations.PatchAsync(null);
                }
                catch (ArgumentNullException)
                {
                    try
                    {
                        configurationPolicyOperations.Patch(null);
                    }
                    catch (ArgumentNullException)
                    {
                        nullCheckOk = true;
                    }
                }

                // ensure that the exceptions were thrown
                Assert.IsTrue(nullCheckOk);

                // call both sync and async versions of the create configuration policy API
                var firstReturnedConfigPolicy = await configurationPolicyOperations.PatchAsync(firstConfigPolicy);
                var secondReturnedConfigPolicy = configurationPolicyOperations.Patch(secondConfigPolicy);

                // ensure the proxy PostAsync() was called twice
                Assert.AreEqual(proxyPostAsyncCalls, 2);

                // ensure that the expected values were returned
                Assert.AreEqual(firstReturnedConfigPolicy, firstConfigPolicy);
                Assert.AreEqual(secondReturnedConfigPolicy, secondConfigPolicy);
            }
        }

        /// <summary>
        /// Ensures that delete configuration policy pass in the right values to the proxy and actually calls the proxy
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task ConfigurationPolicyTests_VerifyDeleteConfigurationPolicy()
        {
            int proxyGetAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below
                ShimPartnerServiceProxy<ConfigurationPolicy, ConfigurationPolicy>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                    PartnerServiceProxy<ConfigurationPolicy, ConfigurationPolicy> jsonProxy,
                    IPartner partnerOperations,
                    string resourcePath,
                    IFailedPartnerServiceResponseHandler errorHandler,
                    JsonConverter jsonConverter) =>
                {
                    Assert.AreEqual(expectedCredentials.Object, partnerOperations.Credentials);
                    Assert.AreEqual(expectedContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.DeleteConfigurationPolicy.Path, expectedCustomerId, expectedPolicyId), resourcePath);
                };

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<ConfigurationPolicy, ConfigurationPolicy>.AllInstances.DeleteAsync = (PartnerServiceProxy<ConfigurationPolicy, ConfigurationPolicy> jsonProxy) =>
                {
                    // increment the number of the calls
                    proxyGetAsyncCalls++;
                    return Task.FromResult<ConfigurationPolicy>(null);
                };

                // call both sync and async versions of the delete configuration policies API
                await configurationPolicyOperations.DeleteAsync();
                configurationPolicyOperations.Delete();

                // ensure the proxy GetAsync() was called twice
                Assert.AreEqual(proxyGetAsyncCalls, 2);
            }
        }
    }
}
