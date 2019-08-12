using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistorydataPolling.Model
{
  
    
        [BsonIgnoreExtraElements]
        public class Package //
        {

            public ObjectId _id { get; set; }
            public ObjectId SatId { get; set; }
            public Int32 Sys_ID { get; set; }
            public string PKG_DESC { get; set; }
     

        
        }
}
