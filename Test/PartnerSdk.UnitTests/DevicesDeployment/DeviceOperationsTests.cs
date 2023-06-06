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
    using Moq;
    using Network;
    using Network.Fakes;
    using Newtonsoft.Json;
    using QualityTools.Testing.Fakes;
    using RequestContext;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///  Tests the <see cref="DeviceCollectionOperations"/>, 
    ///  <see cref="DeviceOperations"/>, 
    ///  <see cref="CustomerDevicesCollectionOperations"/>,
    ///  <see cref="DevicesBatchOperations"/>,
    ///  <see cref="BatchJobStatusCollectionOperations"/>and 
    ///  <see cref="DevicesBatchCollectionOperations"/> class.
    /// </summary>
    [TestClass]
    public class DeviceOperationsTests
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
        /// The expected device batch Id.
        /// </summary>
        private static string expectedDeviceBatchId = "testDeviceBatchId";

        /// <summary>
        /// The expected device Id.
        /// </summary>
        private static string expectedDeviceId = Guid.NewGuid().ToString();

        /// <summary>
        /// The expected tracking Id.
        /// </summary>
        private static string expectedTrackingId = Guid.NewGuid().ToString();

        /// <summary>
        /// The partner device collection operations instance under test.
        /// </summary>
        private static DeviceCollectionOperations deviceCollectionOperations;

        /// <summary>
        /// The partner device operations instance under test.
        /// </summary>
        private static DeviceOperations deviceOperations;

        /// <summary>
        /// The customer devices collection operations instance under test.
        /// </summary>
        private static CustomerDevicesCollectionOperations customerDevicesCollectionOperations;

        /// <summary>
        /// The partner device batch collection operations instance under test.
        /// </summary>
        private static DevicesBatchCollectionOperations deviceBatchCollectionOperations;

        /// <summary>
        /// The partner device batch operations instance under test.
        /// </summary>
        private static DevicesBatchOperations deviceBatchOperations;

        /// <summary>
        /// The batch job status collection operations instance under test.
        /// </summary>
        private static BatchJobStatusCollectionOperations batchJobStatusCollectionOperations;

        /// <summary>
        /// The batch job status operations instance under test.
        /// </summary>
        private static BatchJobStatusOperations batchJobStatusOperations;

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

            deviceCollectionOperations = new DeviceCollectionOperations(partnerOperations.Object, expectedCustomerId, expectedDeviceBatchId);
            deviceOperations = new DeviceOperations(partnerOperations.Object, expectedCustomerId, expectedDeviceBatchId, expectedDeviceId);
            customerDevicesCollectionOperations = new CustomerDevicesCollectionOperations(partnerOperations.Object, expectedCustomerId);
            deviceBatchCollectionOperations = new DevicesBatchCollectionOperations(partnerOperations.Object, expectedCustomerId);
            deviceBatchOperations = new DevicesBatchOperations(partnerOperations.Object, expectedCustomerId, expectedDeviceBatchId);
            batchJobStatusCollectionOperations = new BatchJobStatusCollectionOperations(partnerOperations.Object, expectedCustomerId);
            batchJobStatusOperations = new BatchJobStatusOperations(partnerOperations.Object, expectedCustomerId, expectedTrackingId);
        }

        /// <summary>
        /// Ensures that get device batches pass in the right values to the proxy and actually calls the proxy
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task DeviceBatchOperationsTests_VerifyGetDeviceBatches()
        {
            int proxyGetAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below
                ShimPartnerServiceProxy<DeviceBatch, ResourceCollection<DeviceBatch>>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                    PartnerServiceProxy<DeviceBatch, ResourceCollection<DeviceBatch>> jsonProxy,
                    IPartner partnerOperations,
                    string resourcePath,
                    IFailedPartnerServiceResponseHandler errorHandler,
                    JsonConverter jsonConverter) =>
                {
                    Assert.AreEqual(expectedCredentials.Object, partnerOperations.Credentials);
                    Assert.AreEqual(expectedContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.GetDeviceBatches.Path, expectedCustomerId), resourcePath);
                };

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<DeviceBatch, ResourceCollection<DeviceBatch>>.AllInstances.GetAsync = (PartnerServiceProxy<DeviceBatch, ResourceCollection<DeviceBatch>> jsonProxy) =>
                {
                    // increment the number of the calls
                    proxyGetAsyncCalls++;
                    return Task.FromResult<ResourceCollection<DeviceBatch>>(null);
                };

                // call both sync and async versions of the Get device batches API
                await deviceBatchCollectionOperations.GetAsync();
                deviceBatchCollectionOperations.Get();

                // ensure the proxy GetAsync() was called twice
                Assert.AreEqual(proxyGetAsyncCalls, 2);
            }
        }

        /// <summary>
        /// Ensures that create device batch pass in the right values to the proxy and actually calls the proxy
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task DeviceBatchOperationsTests_VerifyCreateDeviceBatch()
        {
            using (ShimsContext.Create())
            {
                // divert calls to PostAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<DeviceBatchCreationRequest, System.Net.Http.HttpResponseMessage>.AllInstances.PostAsyncT0 =
                    (jsonProxy, conversion) =>
                    {
                        Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.CreateDeviceBatch.Path, expectedCustomerId), jsonProxy.ResourcePath);

                        return Task.FromResult(new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Created));
                    };

                Device newDevice = new Device
                {
                    HardwareHash = "12345"
                };

                DeviceBatchCreationRequest deviceBatch1 = new DeviceBatchCreationRequest
                {
                    BatchId = "testBatch1",
                    Devices = new List<Device> { newDevice }
                };

                DeviceBatchCreationRequest deviceBatch2 = new DeviceBatchCreationRequest
                {
                    BatchId = "testBatch2",
                    Devices = new List<Device> { newDevice }
                };

                bool isNullCheck = false;

                try
                {
                    await deviceBatchCollectionOperations.CreateAsync(null);
                }
                catch (ArgumentNullException)
                {
                    try
                    {
                        deviceBatchCollectionOperations.Create(null);
                    }
                    catch (ArgumentNullException)
                    {
                        isNullCheck = true;
                    }
                }

                // validate that empty object throws an exception.
                Assert.IsTrue(isNullCheck);

                string result1 = await deviceBatchCollectionOperations.CreateAsync(deviceBatch1);
                string result2 = deviceBatchCollectionOperations.Create(deviceBatch2);
                Assert.IsNotNull(result1, "Result should not be empty or null");
                Assert.IsNotNull(result2, "Result should not be empty or null");
            }
        }

        /// <summary>
        /// Ensures that getting a device batch by id works as expected.
        /// </summary>
        [TestMethod]
        public void DeviceBatchOperationsTests_VerifyByIdNavigation()
        {
            using (ShimsContext.Create())
            {
                // route all CustomerOperations constructors to our handler below
                ShimDevicesBatchCollectionOperations.ConstructorIPartnerString =
                    (DevicesBatchCollectionOperations operations, IPartner partnerOperations, string customerId) =>
                    {
                        // ensure the device batch collection operations pass in the right values to the operations
                        Assert.AreEqual(customerId, expectedCustomerId);
                        Assert.AreEqual(partnerOperations.Credentials, expectedCredentials.Object);
                        Assert.AreEqual(partnerOperations.RequestContext, expectedContext.Object);
                    };

                // invoke by id
                Assert.IsNotNull(deviceBatchCollectionOperations.ById(expectedCustomerId));
            }
        }

        /// <summary>
        /// Ensures that get devices pass in the right values to the proxy and actually calls the proxy
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task DeviceOperationTests_VerifyGetDevices()
        {
            int proxyGetAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below
                ShimPartnerServiceProxy<Device, ResourceCollection<Device>>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                    PartnerServiceProxy<Device, ResourceCollection<Device>> jsonProxy,
                    IPartner partnerOperations,
                    string resourcePath,
                    IFailedPartnerServiceResponseHandler errorHandler,
                    JsonConverter jsonConverter) =>
                {
                    Assert.AreEqual(expectedCredentials.Object, partnerOperations.Credentials);
                    Assert.AreEqual(expectedContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.GetDevices.Path, expectedCustomerId, expectedDeviceBatchId), resourcePath);
                };

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<Device, ResourceCollection<Device>>.AllInstances.GetAsync = (PartnerServiceProxy<Device, ResourceCollection<Device>> jsonProxy) =>
                {
                    // increment the number of the calls
                    proxyGetAsyncCalls++;
                    return Task.FromResult<ResourceCollection<Device>>(null);
                };

                // call both sync and async versions of the Get devices API
                await deviceCollectionOperations.GetAsync();
                deviceCollectionOperations.Get();

                // ensure the proxy GetAsync() was called twice
                Assert.AreEqual(proxyGetAsyncCalls, 2);
            }
        }

        /// <summary>
        /// Ensures that creating devices in existing batch pass in the right values to the proxy and actually calls the proxy
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task DeviceOperationsTests_VerifyCreateDevices()
        {
            int proxyPostAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below
                ShimPartnerServiceProxy<IEnumerable<Device>, System.Net.Http.HttpResponseMessage>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                    PartnerServiceProxy<IEnumerable<Device>, System.Net.Http.HttpResponseMessage> jsonProxy,
                    IPartner partnerOperations,
                    string resourcePath,
                    IFailedPartnerServiceResponseHandler errorHandler,
                    JsonConverter jsonConverter) =>
                {
                    Assert.AreEqual(expectedCredentials.Object, partnerOperations.Credentials);
                    Assert.AreEqual(expectedContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.AddDevicestoDeviceBatch.Path, expectedCustomerId, expectedDeviceBatchId), resourcePath);
                };

                // divert calls to PostAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<IEnumerable<Device>, System.Net.Http.HttpResponseMessage>.AllInstances.PostAsyncT0 = (PartnerServiceProxy<IEnumerable<Device>, System.Net.Http.HttpResponseMessage> jsonProxy, IEnumerable<Device> newDevices) =>
                {
                    // increment the number of the calls
                    proxyPostAsyncCalls++;

                    // return the same device object
                    return Task.FromResult(new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Created));
                };

                // ensure creating null device will fail
                bool nullCheckOk = false;

                try
                {
                    await deviceCollectionOperations.CreateAsync(null);
                }
                catch (ArgumentNullException)
                {
                    try
                    {
                        deviceCollectionOperations.Create(null);
                    }
                    catch (ArgumentNullException)
                    {
                        // good, both operations threw the expected exception!
                        nullCheckOk = true;
                    }
                }

                // ensure that the exceptions were thrown
                Assert.IsTrue(nullCheckOk);

                // configure a couple of devices
                var firstDevice = new Device { HardwareHash = "123" };
                var secondDevice = new Device { HardwareHash = "456" };

                // call both sync and async versions of the create devices API
                var firstResult = await deviceCollectionOperations.CreateAsync(new List<Device> { firstDevice });
                var secondResult = deviceCollectionOperations.Create(new List<Device> { secondDevice });

                // ensure the proxy PostAsync() was called twice
                Assert.AreEqual(proxyPostAsyncCalls, 2);

                // ensure that the expected values were returned
                Assert.IsNotNull(firstResult);
                Assert.IsNotNull(secondResult);
            }
        }

        /// <summary>
        /// Ensures that update device pass in the right values to the proxy and actually calls the proxy
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task DeviceOperationsTests_VerifyUpdateDevice()
        {
            var proxyPutAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below
                ShimPartnerServiceProxy<Device, Device>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (jsonProxy, partnerOperations, resourcePath, errorHandler, jsonConverter) =>
                {
                    Assert.AreEqual(expectedCredentials.Object, partnerOperations.Credentials);
                    Assert.AreEqual(expectedContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.UpdateDevice.Path, expectedCustomerId, expectedDeviceBatchId, expectedDeviceId), resourcePath);
                };

                // divert calls to UpdateAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<Device, Device>.AllInstances.PutAsyncT0 = (jsonProxy, updateDevice) =>
                {
                    // increment the number of the calls
                    proxyPutAsyncCalls++;
                    return Task.FromResult(updateDevice);
                };

                // configure a couple of devices
                var firstDevice = new Device { HardwareHash = "123" };
                var secondDevice = new Device { HardwareHash = "456" };

                // call both sync and async versions of the patch device API
                var firstReturnedDevice = await deviceOperations.PatchAsync(firstDevice);
                var secondReturnedDevice = deviceOperations.Patch(secondDevice);

                // ensure the proxy PutAsync() was called twice
                Assert.AreEqual(proxyPutAsyncCalls, 2);

                // ensure that the expected values were returned
                Assert.AreEqual(firstDevice, firstReturnedDevice);
                Assert.AreEqual(secondDevice, secondReturnedDevice);
            }
        }

        /// <summary>
        /// Ensures that delete device pass in the right values to the proxy and actually calls the proxy
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task DeviceOperationsTests_VerifyDeleteDevice()
        {
            int proxyDeleteAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below
                ShimPartnerServiceProxy<Device, Device>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                    PartnerServiceProxy<Device, Device> jsonProxy,
                    IPartner partnerOperations,
                    string resourcePath,
                    IFailedPartnerServiceResponseHandler errorHandler,
                    JsonConverter jsonConverter) =>
                {
                    Assert.AreEqual(expectedCredentials.Object, partnerOperations.Credentials);
                    Assert.AreEqual(expectedContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.DeleteDevice.Path, expectedCustomerId, expectedDeviceBatchId, expectedDeviceId), resourcePath);
                };

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<Device, Device>.AllInstances.DeleteAsync = (PartnerServiceProxy<Device, Device> jsonProxy) =>
                {
                    // increment the number of the calls
                    proxyDeleteAsyncCalls++;
                    return Task.FromResult<Device>(null);
                };

                // call both sync and async versions of the delete device API
                await deviceOperations.DeleteAsync();
                deviceOperations.Delete();

                // ensure the proxy GetAsync() was called twice
                Assert.AreEqual(proxyDeleteAsyncCalls, 2);
            }
        }

        /// <summary>
        /// Ensures that update devices pass in the right values to the proxy and actually calls the proxy
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task DeviceCollectionOperationsTests_VerifyUpdateDevices()
        {
            int proxyGetAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below
                ShimPartnerServiceProxy<DevicePolicyUpdateRequest, System.Net.Http.HttpResponseMessage>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                    PartnerServiceProxy<DevicePolicyUpdateRequest, System.Net.Http.HttpResponseMessage> jsonProxy,
                    IPartner partnerOperations,
                    string resourcePath,
                    IFailedPartnerServiceResponseHandler errorHandler,
                    JsonConverter jsonConverter) =>
                {
                    Assert.AreEqual(expectedCredentials.Object, partnerOperations.Credentials);
                    Assert.AreEqual(expectedContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.UpdateDevicesWithPolicies.Path, expectedCustomerId), resourcePath);
                };

                // divert calls to PatchAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<DevicePolicyUpdateRequest, System.Net.Http.HttpResponseMessage>.AllInstances.PatchAsyncT0 = (PartnerServiceProxy<DevicePolicyUpdateRequest, System.Net.Http.HttpResponseMessage> jsonProxy, DevicePolicyUpdateRequest device) =>
                {
                    // increment the number of the calls
                    proxyGetAsyncCalls++;
                    return Task.FromResult<System.Net.Http.HttpResponseMessage>(new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Created));
                };

                var list = new List<KeyValuePair<PolicyCategory, string>>();
                list.Add(new KeyValuePair<PolicyCategory, string>(PolicyCategory.OOBE, Guid.NewGuid().ToString()));

                var firstDevice = new Device { HardwareHash = "123", Id = Guid.NewGuid().ToString(), Policies = list };
                DevicePolicyUpdateRequest updateRequest = new DevicePolicyUpdateRequest { Devices = new List<Device> { firstDevice } };

                // call both sync and async versions of the Get update devices API
                await customerDevicesCollectionOperations.UpdateAsync(updateRequest);
                customerDevicesCollectionOperations.Update(updateRequest);
            }
        }

        /// <summary>
        /// Ensures that get batch job status pass in the right values to the proxy and actually calls the proxy
        /// network methods.
        /// </summary>
        /// <returns>A task which completed when the test is finished.</returns>
        [TestMethod]
        public async Task BatchJobStatusTests_VerifyGetBatchJobStatus()
        {
            int proxyGetAsyncCalls = 0;

            using (ShimsContext.Create())
            {
                // route all JsonPartnerServiceProxy constructors to our handler below
                ShimPartnerServiceProxy<string, BatchUploadDetails>.ConstructorIPartnerStringIFailedPartnerServiceResponseHandlerJsonConverter = (
                    PartnerServiceProxy<string, BatchUploadDetails> jsonProxy,
                    IPartner partnerOperations,
                    string resourcePath,
                    IFailedPartnerServiceResponseHandler errorHandler,
                    JsonConverter jsonConverter) =>
                {
                    Assert.AreEqual(expectedCredentials.Object, partnerOperations.Credentials);
                    Assert.AreEqual(expectedContext.Object, partnerOperations.RequestContext);
                    Assert.AreEqual(string.Format(PartnerService.Instance.Configuration.Apis.GetBatchUploadStatus.Path, expectedCustomerId, expectedTrackingId), resourcePath);
                };

                // divert calls to GetAsync on the JsonPartnerServiceProxy to our handler
                ShimPartnerServiceProxy<string, BatchUploadDetails>.AllInstances.GetAsync = (PartnerServiceProxy<string, BatchUploadDetails> jsonProxy) =>
                {
                    // increment the number of the calls
                    proxyGetAsyncCalls++;
                    return Task.FromResult<BatchUploadDetails>(new BatchUploadDetails
                    {
                        BatchTrackingId = expectedTrackingId,
                        Status = DeviceUploadStatusType.Finished
                    });
                };

                // call both sync and async versions of the Get batch job status API
                var returnedBatchJobStatus1 = await batchJobStatusOperations.GetAsync();
                var returnedBatchJobStatus2 = batchJobStatusOperations.Get();

                // ensure the proxy GetAsync() was called twice
                Assert.AreEqual(proxyGetAsyncCalls, 2);
                Assert.AreEqual(returnedBatchJobStatus1.BatchTrackingId, expectedTrackingId);
            }
        }
    }
}
