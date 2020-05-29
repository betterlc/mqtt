using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.Contract
{
    public class MqttCalculateResultVO:BaseVO
    {
        public List<string> result = new List<string>();
        public string funcID;
        public string beginTime;
        public string endTime;
    }
}
