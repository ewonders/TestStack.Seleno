﻿using Configurator = TestStack.BDDfy.Configuration.Configurator;
using TestStack.BDDfy.Processors.HtmlReporter;
using TestStack.Seleno.Configuration;
using TestStack.Seleno.Configuration.Contracts;
using TestStack.Seleno.Configuration.WebServers;

using NUnit.Framework;

namespace TestStack.Seleno.Samples.Movies.FunctionalTests
{
    [SetUpFixture]
    public class AssemblySetupFixture
    {
        private ISelenoApplication _selenoApplication;

        [SetUp]
        public void SetUp()
        {
            _selenoApplication = SelenoApplicationRunner
                .New(x =>
                         x.ProjectToTest(
                             WebApplication.Create(app =>
                             {
                                 app.Location = ProjectLocation.FromFolder("TestStack.Seleno.Samples.Movies");
                                 app.PortNumber = 19456;
                             })));
            _selenoApplication.Initialize();
            InitializeBDDfyReport();
        }

        [TearDown]
        public void TearDown()
        {
            _selenoApplication.ShutDown();
        }

        private void InitializeBDDfyReport()
        {
            Configurator.BatchProcessors.HtmlReport.Disable();
            Configurator.BatchProcessors.Add(new HtmlReporter(new FunctionalTestsHtmlReportConfig()));
        }
    }
}