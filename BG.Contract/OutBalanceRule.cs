using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.Contract
{
    /// <summary>
    /// TOPCOAT, BODYCODE, CNT
    /// </summary>
    public class OutBalanceRule
    {
        public String Topcoat { get; set; }
        public String BodyCode { get; set; }
        public int LimitCount { get; set; }
    }
}
