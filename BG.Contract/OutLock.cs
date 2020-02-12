using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.Contract
{
    public class OutLock
    {
        public String Id { get; set; }
        public String Topcoat { get; set; }
        public String BodyCode { get; set; }
        public int Interval { get; set; }
        public DateTime LastOutTime { get; set; }
        public DateTime LastOutTime2 { get; set; }
        public int ClearState { get; set; }
        public int ClearState2 { get; set; }
    }
}
