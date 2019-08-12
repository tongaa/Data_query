using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistorydataPolling
{
    /// <summary>
    /// 勿动
    /// 数据模板 for 时间 工程值 源码值
    /// </summary>
    public class ParaListory
    {
        public DateTime CreateTime { get; set; }
        public string ProjectValue { get; set; }
        public string SourceValue { get; set; }

        public ParaListory(DateTime createTime, string projectValue, string sourceValue)//构造函数
        {
            CreateTime = createTime;
            ProjectValue = projectValue;
            SourceValue = sourceValue;
        }
    }
   
}
