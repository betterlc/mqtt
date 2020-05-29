using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BG.Utilities
{
    public class Constants
    {
        public static int CarCountPerLine = int.Parse(Configurations.Get("CarCountPerLine"));
        public static String KeepAliveDuration = Configurations.Get("KeepAliveDuration");
        public static String OPCLOCKPATH = Configurations.Get("OPCLockPATH");
        public static String OPCINPATH = Configurations.Get("OPCINPATH");
        public static String OPCHEALTHPATH = Configurations.Get("OPCHEALTHPATH");
        public static String OPCREPLICATIONPATH = Configurations.Get("OPCREPLICATIONPATH");
        public static String OPCName = Configurations.Get("OPCName");
        public static String OPCKEEPALIVEPATH = Configurations.Get("OPCKEEPALIVEPATH");
        public static String OPCOUTPATH = Configurations.Get("OPCOUTPATH");
        public static String CHANNELPATH = Configurations.Get("CHANNELPATH");
        public static String CHANNELPATH2 = Configurations.Get("CHANNELPATH2");
        public static String WaingDuration = Configurations.Get("WarningDuration");
        public static int ServiceSection = int.Parse(Configurations.Get("Section"));
        public static String Semaphore = "";
        public static String ServerIP = Configurations.Get("MqttServerIP");
        public static int ServerPort = int.Parse(Configurations.Get("MqttServerPort"));
        public static String MqttTopicHeart = Configurations.Get("MqttTopicHeart");
        public static String MqttTopicCalculate = Configurations.Get("MqttTopicCalculate");
        public static String MqttTopicTask = Configurations.Get("MqttTopicTask");
        public static String MqttTopicUploadParam = Configurations.Get("MqttTopicUploadParam");



        public static List<String> KeepAliveKeys = new List<string>()
        {
            Semaphore,
        };


        public static int js = 0;


    }
}
