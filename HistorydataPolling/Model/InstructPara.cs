using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistorydataPolling.Model
{
    /// <summary>
    /// 指令参数实体
    /// </summary>
    [BsonIgnoreExtraElements]
    public class CMDMain
    {

        public ObjectId _id { get; set; }
        public ObjectId SatId { get; set; } //连表依据
        public string CMDCode { get; set; }
        public string CMDDesc { get; set; }  //参数的具体文字描述
        public string CMDData { get; set; }
        public Int32 createTime { get; set; }
    }
}
