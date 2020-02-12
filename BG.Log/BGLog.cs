using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.IO;

namespace BG.Log
{
    public class BGLog
    {
        private static ILog logger { get; set; }

        public static void InitLog()
        {
            String currentPath = AppDomain.CurrentDomain.BaseDirectory;
            String logConfig = Path.Combine(currentPath, "log4net.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(logConfig));
        }

        public static BGLog GetLogger(Type t)
        {
            logger = LogManager.GetLogger(t);

            return new BGLog();
        }

        public void Debug(String message, params object[] args)
        {
            logger.DebugFormat(message, args);
        }

        public void Info(String message, params object[] args)
        {
            logger.InfoFormat(message, args);
        }

        public void Error(String message, params object[] args)
        {
            logger.ErrorFormat(message, args);
        }

        public void Warn(String message, params object[] args)
        {
            logger.WarnFormat(message, args);
        }
    }
}
