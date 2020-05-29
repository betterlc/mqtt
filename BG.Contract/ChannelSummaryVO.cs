using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.Contract
{
    public class ChannelSummaryVO
    {
        public int canChannel = 0;
        public int signalCount = 0;
        public int connectorIndex = 0;
        public int channelIndex = 0;
        public SensorSummaryVO sensorVO = (SensorSummaryVO)null;
        public List<SignalSummaryVO> signalList = new List<SignalSummaryVO>();
        public int sampleRate = -1;
        public Decimal behz = new Decimal(20);
        public string filter = "";
        public int precision = 4;
        public double maxvalue = 0.0;
        public double minvalue = 0.0;
        public double rms = 0.0;
        public double maxvaluespeed = 0.0;
        public double minvaluespeed = 0.0;
        public string maxvaluetime = "";
        public string minvaluetime = "";
        public string name;
        public double value;
        public string unit;
        public double zerovalue;
        public string runstatus;
        public string sensorName;
        public string th;
        public string ts;
    }
}
