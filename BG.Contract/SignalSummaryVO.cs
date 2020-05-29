using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.Contract
{
    public class SignalSummaryVO
    {
        public int connectorIndex = 0;
        public int channelIndex = 0;
        public int signalIndex = 0;
        public int sampleRate = 500;
        public Decimal behz = new Decimal(20);
        public double value = 1000000.0;
        public double zerovalue = 0.0;
        public string calMethod = "";
    }
}
