using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.Contract
{
    public class LineInfo
    {
        public int Section { get; set; }
        public int Line { get; set; }
        /// <summary>
        /// 0 no lock
        /// 1 lock in
        /// 2 lock out
        /// 3 lock both
        /// </summary>
        public int LockType { get; set; }
        public String Role { get; set; }
        /// <summary>
        /// car color
        /// </summary>
        public String Color { get; set; }
        public int Codition { get; set; }
        /// <summary>
        /// car type
        /// </summary>
        public String Body { get; set; }
        public String Spic { get; set; }
    }
}
