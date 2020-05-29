using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BG.Mqtt;
using BG.Database;

namespace BG.Mqtt.Tests
{
    [TestClass()]
    public class DownloadParamClientTests
    {
        [TestMethod()]
        public void client_funTest()
        {
            string msg = "ok";
            DownloadParamClient client = new DownloadParamClient();
            Assert.AreEqual(client.client_fun(msg), 1);
        }
    }
}