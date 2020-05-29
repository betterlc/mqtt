using Microsoft.VisualStudio.TestTools.UnitTesting;
using BG.Mqtt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.Mqtt.Tests
{
    [TestClass()]
    public class DownloadParamClientTests
    {
        [TestMethod()]
        public void client_funTest()
        {
            DownloadParamClient client = new DownloadParamClient();
            Assert.AreEqual(client.client_func("123"),1);
        }
    }
}