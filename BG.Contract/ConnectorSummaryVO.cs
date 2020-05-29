using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.Contract
{
    public class ConnectorSummaryVO
    {
        public int channelCount = 0;
        public int connectorIndex = 0;
        public List<ChannelSummaryVO> channelList = new List<ChannelSummaryVO>();
    }
}
