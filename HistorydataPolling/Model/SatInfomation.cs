using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistorydataPolling.Model
{

//卫星信息实体类
    [BsonIgnoreExtraElements]
    public class SatInfomation
    {
        public ObjectId _id { get; set; }

        public string SatName { get; set; }
        public string version { get; set; }
        public Int32 StageId { get; set; }

    }

}
