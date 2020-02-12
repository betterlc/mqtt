using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Threading;
using BG.Log;
using BG.Service;

namespace CommunicateService
{
    public partial class MainService : ServiceBase
    {
        private BGLog logger { get; set; }
        private ServiceHolder hollder { get; set; }

        public MainService()
        {
            InitializeComponent();
        }

        public void StartTest()
        {
            this.OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            BGLog.InitLog();
            this.logger = BGLog.GetLogger(typeof(MainService));

            logger.Info("Service start.");

            ThreadPool.QueueUserWorkItem(new WaitCallback(Run));
            //Thread.Sleep(900000);
        }

        private void Run(object obj)
        {
            try
            {
                logger.Info("Thread runing");
                hollder = new ServiceHolder();
                hollder.Run();
            }
            catch (Exception e)
            {
                logger.Error(e.ToString());
            }
        }

        protected override void OnStop()
        {
            try
            {
                if (hollder != null)
                {
                    hollder.Stop();
                }
            }
            catch (Exception)
            {
                logger.Info("Service stop");
            }
        }
    }
}
