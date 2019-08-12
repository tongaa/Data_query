using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTypeHelper;
using HistorydataPolling.Model;

namespace HistorydataPolling.Server.ThreeLevelLinkageDataItem
{
    public class SatinfoAndParaGroup
    {
        public IMongoCollection<BsonDocument> col;  //集合 
        public IMongoCollection<BsonDocument> col2;  //集合 
        public IMongoCollection<BsonDocument> col3;  //集合 

        //卫星信息表和T_ParaMain表联系
        public List<BsonDocument> getSatinfoParaGroup()
        {
            string ParaCodeFirstHeader;
            col = MongoDBHelper<SatInfomation>.Init();
            col2 = MongoDBHelper<ParaMain>.Init();
            col3 = MongoDBHelper<Package>.Init();



            List<BsonDocument> DataForSelect = new List<BsonDocument>();
            //PkgId


            var result = from u in col.AsQueryable() join o in col2.AsQueryable() on u["_id"] equals o["SatId"] select new { SatName = u["SatName"], ParaSysId = o["ParaSysId"], ParaCode = o["ParaCode"], ParamDesc = o["ParamDesc"] };
            var result2 = from u in col.AsQueryable() join o in col3.AsQueryable() on u["_id"] equals o["SatId"] select new { SatName = u["SatName"], Sys_ID = o["Sys_ID"], PKG_DESC = o["PKG_DESC"] };



            foreach (var item in result)
            {

                // Console.WriteLine("SatName:" + item.SatName + "--ParaSysId:"+item.ParaSysId+ "===ParaCode:" + item.ParaCode + "===" + item.ParamDesc);
                foreach (var tepmt in result2)
                {
                    if (tepmt.SatName == item.SatName && tepmt.Sys_ID == item.ParaSysId)
                    {
                        if (item.ParaCode.ToString().Substring(0, 2) == "YY")
                        {
                            ParaCodeFirstHeader = item.ParaCode.ToString().Substring(0, 2);
                        }
                        else if (item.ParaCode.ToString().Substring(0, 2) == "NS")
                        {
                            ParaCodeFirstHeader = item.ParaCode.ToString().Substring(0, 2);
                        }
                        else
                        {
                            ParaCodeFirstHeader = item.ParaCode.ToString().Substring(0, 3);
                        }


                        DataForSelect.Add(item.ToBsonDocument().Add(new BsonElement("PKG_DESC", tepmt.PKG_DESC)).Add(new BsonElement("ParaCodeFirstHeader", ParaCodeFirstHeader))); //ParaCodeFirstHeader 包标志

                    }

                }


            }

            return DataForSelect;

        }
    }
}



