using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.Utilities
{
    public static class GeneralMethods
    {
        public static string InverseBit(string strbits)
        {
            string strInvers = "";
            int[] data = new int[10];
            for(int i=0;i<strbits.Length;i++)
            {
                data[i] = Convert.ToInt32(strbits.Substring(i, 1));
                if(data[i]==0)
                {
                    data[i] = 1;
                }
                else
                {
                    data[i] = 0;
                }
                strInvers = strInvers + Convert.ToString(data[i]);
            }
            return strInvers;
        }
    }
}
