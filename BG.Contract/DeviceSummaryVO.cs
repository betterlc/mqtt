using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.Contract
{
    public class DeviceSummaryVO
    {
        public int connectorCount = 0;
        public List<ConnectorSummaryVO> connectorList = new List<ConnectorSummaryVO>();
        public List<ChannelSummaryVO> channelList = new List<ChannelSummaryVO>();
        public string sno;
        public string name;
        public string model;
        public string sampleRateType;
        public string ip;
        public string runstatus;
    }
}
