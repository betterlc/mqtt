using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.Contract
{
    public class SensorSummaryVO
    {
        public int connectorIndex = 0;
        public int channelIndex = 0;
        public Dictionary<string, string> propMap = new Dictionary<string, string>();
        public Dictionary<string, string> paramMap = new Dictionary<string, string>();
        public string name;
        public string uniqueName;
        public string unit;
        public string parentName;
        public string sensorTypeName;
        public string sensorScalingName;
    }
}
