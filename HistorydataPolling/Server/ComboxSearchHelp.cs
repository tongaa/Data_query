using HistorydataPolling.Model;
using HistorydataPolling.Server.ThreeLevelLinkageDataItem;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTypeHelper;

namespace HistorydataPolling.Server
{
    /// <summary>
    /// for  输入查询的参数代号  设置为下拉菜单的方式 方便用户查找
    /// </summary>
    class ComboxSearchHelp
    {
        public static IMongoCollection<BsonDocument> col4;  //集合 
        public static List<BsonDocument> ToComboxDisplay(string whichSat,string whichPara)
        {
            List<BsonDocument> temp = new List<BsonDocument>();
            List<BsonDocument> targetData = new List<BsonDocument>();

            SatinfoAndParaGroup satinfoAndParaGroup = new SatinfoAndParaGroup();
            temp = satinfoAndParaGroup.getSatinfoParaGroup();
            foreach (var item in temp)
            {

                if (item[0] == whichSat)
                {
                    switch (whichPara)
                    {
                        case "RadioButtonYC":
                            targetData.Add(new BsonDocument { { "参数代号", item[2] }, { "参数描述", item[3] }, { "包描述", item[4] }, { "包头", item[5] } });
                            break;
                        case "RadioButtonZL":
                            targetData.Add(new BsonDocument { { "参数代号", item[2] }, { "参数描述", item[3] }, { "包描述", item[4] }, { "包头", item[5] } });
                            break;

                        default:
                            break;
                    }


                    
                }
            }
    
          

            return targetData;
        }
        public static List<BsonDocument> GetAllInstruct(string whichSat, string whichPara)
        {


            SatinfoAndParaGroup satinfoAndParaGroup = new SatinfoAndParaGroup(); //导入 col
            List<BsonDocument> cmdmMains = new List<BsonDocument>();
            col4 = MongoDBHelper<CMDMain>.Init();

            var result = from u in satinfoAndParaGroup.col.AsQueryable() join o in col4.AsQueryable() on u["_id"] equals o["SatId"] select new { SatName = u["SatName"], CMDCode = o["CMDCode"], CMDDesc = o["CMDDesc"] };

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }

                return cmdmMains;
            }

        
    }
}
