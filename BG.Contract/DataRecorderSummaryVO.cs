using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.Contract
{
    public class DataRecorderSummaryVO
    {
        public string dno = "";
        public string configVersion = "";
        public string datatime = "";
        public int deviceCount = 0;
        public int dataRate = 20;
        public List<DeviceSummaryVO> deviceList = new List<DeviceSummaryVO>();
        public List<ChannelFunctionVO> functionLlist = new List<ChannelFunctionVO>();
        public List<CalculateTaskVO> taskList = new List<CalculateTaskVO>();
        public string gps_Latitude;
        public string gps_Longitude;
        public string gps_Altitude;
        public string gps_Speed;
        public string beginTime;
        public string curTime;
    }
}
