//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Data Access Application Block
//===============================================================================
// Copyright � Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

#if UNIT_TESTS && USEEVENTLOG
using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Microsoft.Practices.EnterpriseLibrary.Data.Instrumentation.Tests
{
    [TestFixture]
    public class EventLogFixture
    {
        private int startCount = 0;
        private EventLog log = new EventLog("Application");

        [SetUp]
        public void Setup()
        {
            startCount = log.Entries.Count;
        }

        [Test]
        public void DataConnectionFailed()
        {
            DataConnectionFailedEvent.Fire("myConnection");

            string expected = "Data connection failed to open: myConnection";
            string actual = log.Entries[log.Entries.Count - 1].Message;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DataConfigFailure()
        {
            DataServiceConfigFailureEvent.Fire("test", new Exception("test exception"));
            Assert.AreEqual(1, log.Entries.Count - startCount);

            string expected = "test Exception: System.Exception: test exception";
            string actual = log.Entries[log.Entries.Count - 1].Message;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DataConfigFailureWithConfigFile()
        {
            DataServiceConfigFailureEvent.Fire("test", new Exception("test exception"), "ConnectionSettings.config");
            Assert.AreEqual(1, log.Entries.Count - startCount);

            string expected = "Unable to load configuration file \"ConnectionSettings.config\".  Exception: System.Exception: test exception";
            string actual = log.Entries[log.Entries.Count - 1].Message;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DataServiceFailure()
        {
            DataServiceFailureEvent.Fire("test", new Exception("test exception"));
            Assert.AreEqual(1, log.Entries.Count - startCount);

            string expected = "test Exception: System.Exception: test exception";
            string actual = log.Entries[log.Entries.Count - 1].Message;

            Assert.AreEqual(expected, actual);
        }
    }
}

#endif