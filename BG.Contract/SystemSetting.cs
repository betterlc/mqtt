using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.Contract
{
    public class SystemSetting
    {
        public int TDMax { get; set; }
        public int TDMin { get; set; }
        public int TDCurrent { get; set; }
        public int SectionMax { get; set; }

        public Boolean EnableNoFollow { get; set; }
        public Boolean EnableMaxPerLine { get; set; }
        public Boolean EnableManual { get; set; }
        public Boolean EnableTimeOut { get; set; }

        public int ManualOrder { get; set; }
        public int TimeOutOrder { get; set; }

        public double TimeOutInterval { get; set; }
    }
}
