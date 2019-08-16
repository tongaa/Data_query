using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistorydataPolling.ViewModel
{
    public class InstructionParaListory
    {
        public InstructionParaListory(DateTime createTime ,string cMDCode, string cMDDesc, string cMDData )
        {
            CMDCode = cMDCode;
            CMDDesc = cMDDesc;
            CMDData = cMDData;
            this.CreateTime = createTime;
        }

        public string CMDCode { get; set; }
        public string CMDDesc { get; set; }  
        public string CMDData { get; set; }
        public DateTime CreateTime { get; set; }









    }
}
