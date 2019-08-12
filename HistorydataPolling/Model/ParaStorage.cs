using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistorydataPolling.Model
{


    //历史遥测数据存储实体类

    [BsonIgnoreExtraElements]
 public   class ParaStorage //类名就是集合的名字
    {

        public ObjectId _id { get; set; }
        public string ProjectValue { get; set; }
        public string SourceValue { get; set; }
        public string ParaCode { get; set; }
        public  Int32   createTime  { get; set; }
       
       
     

    }
}
