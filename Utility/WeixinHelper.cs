using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    /// <summary>
    /// 微信操作类
    /// </summary>
    public partial class WeixinHelper
    {
        private WeixinApiBaseInfo ApiInfo;

        public WeixinHelper(WeixinApiBaseInfo info) => ApiInfo = info;
    }
}
