using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.Contract
{
    public class Station
    {
        public int SPOT { get; set; }
        public DateTime NOT_TIME { get; set; }
        public int KENN { get; set; }
        public int LINE { get; set; }
        public int SKID { get; set; }
        public String TOPCOAT { get; set; }
        public String SPECIAL { get; set; }
        public String BODYCODE { get; set; }
        public int FA { get; set; }
        public String TEXT { get; set; }
    }
}
