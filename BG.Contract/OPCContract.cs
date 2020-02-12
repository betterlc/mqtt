using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BG.Contract
{
    public class OPCContract
    {
        public String FACH { get; set; }
        /// <summary>
        /// 4 get section failed.
        /// 3 sections are full
        /// </summary>
        public int Section { get; set; }
        public string Skid_Nr { get; set; }
        public string Body { get; set; }
        public string Body_ID { get; set; }
        public string Spec_ID { get; set; }

        public string Spec_ID_2 { get; set; }
        public String color { get; set; }
        public int L { get; set; }
        public int Y { get; set; }
        public String SpotFrom { get; set; }

        /// <summary>
        /// 2 blue
        /// 1 gray
        /// 15 超时
        /// 6 lock
        /// </summary>
        public int State { get; set; }
        public String E_Code { get; set; }

        public String E_Spot { get; set; }
        public String To_Src { get; set; }
        public String To_Des { get; set; }
        /// <summary>
        /// 用于出车和section比较，看看是否需要去42**
        /// </summary>
        public int Line { get; set; }

        public string Reserve { get; set; }
        public String Res1 { get; set; }
        public Nullable<DateTime> InTime { get; set; }//0101

        public Boolean HaveSPId { get; set; }
    }
}
