using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BG.Contract
{
    public class SSCLager
    {
        /// <summary>
        /// SLY
        /// </summary>
        public int FACH { get; set; }

        public int S { get; set; }

        public int L { get; set; }

        public int Y { get; set; }

        public DateTime VON { get; set; }

        /// <summary>
        /// 4 manual
        /// 6 lcoked
        /// 3 outing
        /// 9 time out
        /// </summary>
        public int STAT { get; set; }

        public int KENN { get; set; }

        public int LINE { get; set; }

        public int SKID { get; set; }

        public String TOPCOAT { get; set; }

        public String SPECIAL { get; set; }

        public String BODYCODE { get; set; }

        public DateTime InTime { get; set; }
    }
}
