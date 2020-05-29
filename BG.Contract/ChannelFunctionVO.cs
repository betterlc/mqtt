using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.Contract
{
    public class ChannelFunctionVO
    {
        public List<ChannelFunctionSelectVO> selectList = new List<ChannelFunctionSelectVO>();
        public string id;
        public string name;
        public string type;//1 or 2
        public string calMethod;
        public string calLength;
    }
}
