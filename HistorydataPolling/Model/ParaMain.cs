using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistorydataPolling.Model
{
//所有卫星的所有参数的实体类

    [BsonIgnoreExtraElements]
    public class ParaMain  //对应T_ParaMain 集合
    {

        public ObjectId _id { get; set; }
        public ObjectId SatId { get; set; } //连表依据
        public string  ParaCode { get; set; }
        public string ParamDesc { get; set; }  //参数的具体文字描述

    }
}
