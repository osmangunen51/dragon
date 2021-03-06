using System;
using System.Linq;
using FluentAssertions;
using Nop.Services.Configuration;
using NUnit.Framework;

namespace Nop.Services.Tests.Configuration
{
    [TestFixture]
    public class ConfigFileSettingServiceTests : ServiceTest
    {
        // requires following settings to exist in app.config
        // Setting1 : "SomeValue" : string
        // Setting2 : 25 : int
        // Setting3 : 25/12/2010 : Date

        ISettingService config;

        [SetUp]
        public new void SetUp()
        {
            config = new ConfigFileSettingService(null,null,null);
        }

        [Test]
        public void Can_get_all_settings()
        {
            var settings = config.GetAllSettings();
            settings.Should().NotBeNull();
            (settings.Any()).Should().BeTrue();
        }

        [Test]
        public void Can_get_setting_by_key()
        {
            var setting = config.GetSettingByKey<string>("Setting1");
            setting.Should().Be("SomeValue");
        }

        [Test]
        public void Can_get_typed_setting_value_by_key()
        {
            var setting = config.GetSettingByKey<DateTime>("Setting3");
            setting.Should().Be(new DateTime(2010, 12, 25));
        }

        [Test]
        public void Default_value_returned_if_setting_does_not_exist()
        {
            var setting = config.GetSettingByKey("NonExistentKey", 100);
            setting.Should().Be(100);
        }
    }
}
