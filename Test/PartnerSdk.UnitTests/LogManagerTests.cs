// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Store.PartnerCenter.UnitTests
{
    using System.Globalization;
    using Logging;
    using Moq;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="LogManager"/> class.
    /// </summary>
    [TestClass]
    public class LogManagerTests
    {
        /// <summary>
        /// The expected information text to find in the logs.
        /// </summary>
        private const string InformationText = "Sample Info";

        /// <summary>
        /// The expected warning text to find in the logs.
        /// </summary>
        private const string WarningText = "Sample Warning";

        /// <summary>
        /// The expected error text to find in the logs.
        /// </summary>
        private const string ErrorText = "Sample Error";

        /// <summary>
        /// A mock logger.
        /// </summary>
        private readonly Mock<ILogger> mockLogger1 = new Mock<ILogger>();

        /// <summary>
        /// Another mock logger to test multiple registered loggers.
        /// </summary>
        private readonly Mock<ILogger> mockLogger2 = new Mock<ILogger>();

        /// <summary>
        /// Initializes the stage for the tests.
        /// </summary>
        public void PrepareTests()
        {
            this.mockLogger1.Setup(logger => logger.Information(It.IsAny<string>()));
            this.mockLogger1.Setup(logger => logger.Warning(It.IsAny<string>()));
            this.mockLogger1.Setup(logger => logger.Error(It.IsAny<string>()));

            this.mockLogger2.Setup(logger => logger.Information(It.IsAny<string>()));
            this.mockLogger2.Setup(logger => logger.Warning(It.IsAny<string>()));
            this.mockLogger2.Setup(logger => logger.Error(It.IsAny<string>()));

            LogManager.Instance.Loggers.Clear();
        }

        /// <summary>
        /// Cleans up after the tests.
        /// </summary>
        public void TeardownTests()
        {
            this.mockLogger1.ResetCalls();
            this.mockLogger2.ResetCalls();
        }

        /// <summary>
        /// Ensures logging empty strings do not get propagated to the loggers.
        /// </summary>
        [TestMethod]
        public void LogManagerTests_VerifyEmptyStringLogging()
        {
            // add the mock logger
            LogManager.Instance.Loggers.Add(this.mockLogger1.Object);

            // inject some normal log messages
            InjectMessages("A");

            // inject some invalid empty log messages
            InjectInvalidMessages();

            // verify that only the normal log messages went through
            VerifyTotalNumberOfLoggedMessages(this.mockLogger1, 1);
            VerifyNumberOfLoggedMessages(this.mockLogger1, "A", 1);
        }

        /// <summary>
        /// Tests logging behavior.
        /// </summary>
        [TestMethod]
        public void LogManagerTests_VerifyLogging()
        {
            // add the mock logger
            LogManager.Instance.Loggers.Add(this.mockLogger1.Object);

            // ensure no calls have been made
            VerifyTotalNumberOfLoggedMessages(this.mockLogger1, 0);

            // log some messages and verify they were propagated
            InjectMessages("A");
            VerifyTotalNumberOfLoggedMessages(this.mockLogger1, 1);
            VerifyNumberOfLoggedMessages(this.mockLogger1, "A", 1);
            
            // log some more messages and verify they were also propagated
            InjectMessages("B");
            VerifyTotalNumberOfLoggedMessages(this.mockLogger1, 2);
            VerifyNumberOfLoggedMessages(this.mockLogger1, "B", 1);

            // remove the logger
            LogManager.Instance.Loggers.Remove(this.mockLogger1.Object);
            this.mockLogger1.ResetCalls();

            // log some message
            InjectMessages("C");

            // ensure the mock logger did no longer receives messages
            VerifyTotalNumberOfLoggedMessages(this.mockLogger1, 0);

            // add 2 mock loggers
            LogManager.Instance.Loggers.Add(this.mockLogger1.Object);
            LogManager.Instance.Loggers.Add(this.mockLogger2.Object);

            // log some errors
            InjectMessages("A");
            InjectMessages("B");

            // verify that each logger has been called once for each error
            VerifyTotalNumberOfLoggedMessages(this.mockLogger1, 2);
            VerifyNumberOfLoggedMessages(this.mockLogger1, "A", 1);
            VerifyNumberOfLoggedMessages(this.mockLogger1, "B", 1);

            VerifyTotalNumberOfLoggedMessages(this.mockLogger2, 2);
            VerifyNumberOfLoggedMessages(this.mockLogger2, "A", 1);
            VerifyNumberOfLoggedMessages(this.mockLogger2, "B", 1);
        }

        /// <summary>
        /// Ensures the total number of logged messages in each category is as expected.
        /// </summary>
        /// <param name="mockLogger">The mock logger to verify.</param>
        /// <param name="totalInvocationCount">The expected number of invocations.</param>
        private static void VerifyTotalNumberOfLoggedMessages(Mock<ILogger> mockLogger, int totalInvocationCount)
        {
            mockLogger.Verify(logger => logger.Information(It.IsAny<string>()), Times.Exactly(totalInvocationCount));
            mockLogger.Verify(logger => logger.Warning(It.IsAny<string>()), Times.Exactly(totalInvocationCount));
            mockLogger.Verify(logger => logger.Error(It.IsAny<string>()), Times.Exactly(totalInvocationCount));
        }

        /// <summary>
        /// Ensures that a certain message has been logged for the expected number of times.
        /// </summary>
        /// <param name="mockLogger">The mock logger to verify.</param>
        /// <param name="suffix">The message suffix.</param>
        /// <param name="totalInvocationCount">The expected number of invocations.</param>
        private static void VerifyNumberOfLoggedMessages(Mock<ILogger> mockLogger, string suffix, int totalInvocationCount)
        {
            mockLogger.Verify(logger => logger.Information(It.Is<string>(info => info == BuildInfoLogMessage(suffix))), Times.Exactly(totalInvocationCount));
            mockLogger.Verify(logger => logger.Warning(It.Is<string>(warning => warning == BuildWarningLogMessage(suffix))), Times.Exactly(totalInvocationCount));
            mockLogger.Verify(logger => logger.Error(It.Is<string>(error => error == BuildErrorLogMessage(suffix))), Times.Exactly(totalInvocationCount));
        }

        /// <summary>
        /// Builds an information log message.
        /// </summary>
        /// <param name="suffix">A suffix to append to the information message.</param>
        /// <returns>The information log message.</returns>
        private static string BuildInfoLogMessage(string suffix)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}", InformationText, suffix);
        }

        /// <summary>
        /// Builds a warning log message.
        /// </summary>
        /// <param name="suffix">A suffix to append to the warning message.</param>
        /// <returns>The warning log message.</returns>
        private static string BuildWarningLogMessage(string suffix)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}", WarningText, suffix);
        }

        /// <summary>
        /// Builds an error log message.
        /// </summary>
        /// <param name="suffix">A suffix to append to the error message.</param>
        /// <returns>The error log message.</returns>
        private static string BuildErrorLogMessage(string suffix)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}", ErrorText, suffix);
        }

        /// <summary>
        /// Injects one of each information, warning and error messages.
        /// </summary>
        /// <param name="suffix">An optional suffix to identify the messages.</param>
        private static void InjectMessages(string suffix = "")
        {
            LogManager.Instance.Information(BuildInfoLogMessage(suffix));
            LogManager.Instance.Warning(BuildWarningLogMessage(suffix));
            LogManager.Instance.Error(BuildErrorLogMessage(suffix));
        }

        /// <summary>
        /// Logs empty and null strings.
        /// </summary>
        private static void InjectInvalidMessages()
        {
            LogManager.Instance.Information(null);
            LogManager.Instance.Information(string.Empty);
            LogManager.Instance.Warning(null);
            LogManager.Instance.Warning(string.Empty);
            LogManager.Instance.Error(null);
            LogManager.Instance.Error(string.Empty);
        }
    }
}