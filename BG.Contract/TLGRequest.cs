using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.Contract
{
    /// <summary>
    /// ID_APPL, ID_JOB, ID_USER, \n   HOST_NAME, VON, TLG_NO, \n   BODY
    /// </summary>
    public class TLGRequest
    {
        public int ID_APPL { get; set; }
        public int ID_JOB { get; set; }
        public int ID_USER { get; set; }
        public string HOST_NAME { get; set; }
        public DateTime VON { get; set; }
        /// <summary>
        /// 4 line replication
        /// </summary>
        public int TLG_NO { get; set; }
        public string BODY { get; set; }
    }
}
