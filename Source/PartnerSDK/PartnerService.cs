// -----------------------------------------------------------------------
// <copyright file="PartnerService.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Text;
    using System.Threading.Tasks;
    using Factory;
    using Logging;
    using Newtonsoft.Json;
    using Properties;
    using RequestContext;
    using Retries;

    /// <summary>
    /// This class contains the partner SDK properties and acts as the main entry point to create partners.
    /// </summary>
    public sealed class PartnerService
    {
        /// <summary>
        /// A singleton instance of the partner service.
        /// </summary>
        private static Lazy<PartnerService> instance = new Lazy<PartnerService>(() => new PartnerService());

        /// <summary>
        /// Prevents a default instance of the <see cref="PartnerService"/> class from being created.
        /// </summary>
        private PartnerService()
        {
            // read the configuration from the Json configuration resource
            this.Configuration = this.ReadPartnerSdkConfiguration();

            // set the global partner service properties
            this.ApiRootUrl = this.Configuration.PartnerServiceApiRoot;
            this.ApiVersion = this.Configuration.PartnerServiceApiVersion;

            // set the SDK version number
            this.AssemblyVersion = typeof(PartnerService).Assembly.GetName().Version.ToString();

            // initialize the partner factory
            this.Factory = new StandardPartnerFactory();

            // define the default retry policy as exponential with 3 retry attempts
            this.RetryPolicy = new ExponentialBackOffRetryPolicy(this.Configuration.DefaultMaxRetryAttempts);

            // log to the debugger window
            LogManager.Instance.Loggers.Add(new DebugWindowLogger());
        }

        /// <summary>
        /// Defines a delegate which is called to refresh stale partner credentials.
        /// </summary>
        /// <param name="credentials">The outdated partner credentials.</param>
        /// <param name="context">The partner context.</param>
        /// <returns>A task that completes when the refresh is complete.</returns>
        internal delegate Task RefreshCredentialsHandler(IPartnerCredentials credentials, IRequestContext context);

        /// <summary>
        /// Gets an instance of the partner service.
        /// </summary>
        public static PartnerService Instance
        {
            get
            {
                return PartnerService.instance.Value;
            }
        }

        /// <summary>
        /// Gets or sets the partner service API root URL.
        /// </summary>
        public string ApiRootUrl { get; set; }

        /// <summary>
        /// Gets the partner service API version.
        /// </summary>
        public string ApiVersion { get; private set; }

        /// <summary>
        /// Gets or sets an application name used to identify the calling application.
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets the partner service API subscription key.
        /// </summary>
        public string ApiSubscriptionKey { get; set; }

        /// <summary>
        /// Gets or sets additional headers to be included with the API requests.
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> AdditionalHeaders { get; set; }

        /// <summary>
        /// Gets the current Partner Center SDK version.
        /// </summary>
        internal string AssemblyVersion { get; private set; }

        /// <summary>
        /// Gets the SDK's configuration.
        /// </summary>
        internal dynamic Configuration { get; private set; }

        /// <summary>
        /// Gets the partner factory used to create partner objects.
        /// </summary>
        internal IPartnerFactory Factory { get; set; }

        /// <summary>
        /// Gets the default retry policy used by the partner SDK.
        /// </summary>
        internal IRetryPolicy RetryPolicy { get; set; }

        /// <summary>
        /// This callback is invoked when the partner SDK needs to refresh a partner credentials.
        /// </summary>
        internal RefreshCredentialsHandler RefreshCredentials { get; set; }

        /// <summary>
        /// Creates a <see cref="IAggregatePartner" /> instance and configures it using the provided partner credentials. The partner instance can be used to
        /// access all the Partner Center APIs.
        /// </summary>
        /// <param name="credentials">The partner credentials. Use the <see cref="IPartnerCredentials" /> class to obtain these.</param>
        /// <returns>
        /// A configured partner operations object.
        /// </returns>
        public IAggregatePartner CreatePartnerOperations(IPartnerCredentials credentials)
        {
            return this.Factory.Build(credentials);
        }

        /// <summary>
        /// Executes an asynchronous method synchronously in a way that prevents deadlocks in UI applications.
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="operation">The asynchronous operation to execute.</param>
        /// <returns>The operation's return value.</returns>
        internal T SynchronousExecute<T>(Func<Task<T>> operation)
        {
            try
            {
                // run the async task on a non-UI thread and block that thread
                return Task.Run(async () => await operation.Invoke()).Result;
            }
            catch (AggregateException aggregateException)
            {
                // the aggregate exception is a side effect of running the task, hide it
                throw aggregateException.InnerException;
            }
        }

        /// <summary>
        /// Executes an asynchronous method synchronously in a way that prevents deadlocks in UI applications.
        /// </summary>
        /// <param name="operation">The asynchronous operation to execute.</param>
        internal void SynchronousExecute(Func<Task> operation)
        {
            try
            {
                // run the async task on a non-UI thread and block that thread
                Task.Run(async () => await operation.Invoke()).Wait();
            }
            catch (AggregateException aggregateException)
            {
                // the aggregate exception is a side effect of running the task, hide it
                throw aggregateException.InnerException;
            }
        }

        /// <summary>
        /// Reads the partner SDK configuration from the embedded resource file and massages its fields to be easily accessible.
        /// </summary>
        /// <returns>The partner SDK configuration.</returns>
        private dynamic ReadPartnerSdkConfiguration()
        {
            dynamic configuration = JsonConvert.DeserializeObject<ExpandoObject>(Encoding.UTF8.GetString(Resources.Configuration).Substring(1));

            // convert all non string configuration values into their respective types
            configuration.DefaultMaxRetryAttempts = int.Parse(configuration.DefaultMaxRetryAttempts);
            configuration.DefaultAuthenticationTokenExpiryBufferInSeconds = int.Parse(configuration.DefaultAuthenticationTokenExpiryBufferInSeconds);

            return configuration;
        }
    }
}