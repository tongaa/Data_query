using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using HistorydataPolling.Model;
using TimeTypeHelper;
using System.Globalization;
using HY.Common;
using System.Windows.Controls;

namespace HistorydataPolling.Server
{
    public class TimeServer
    {
        private static MongoClient client2; //连接
        private static IMongoDatabase db2;  //查询数据 数据库
        public static IMongoCollection<BsonDocument> col2;  //集合 

        public static string _baseBDconection = "baseDBIp"; //实例连接地址
        public static string _baseBDName2 = "baseDBName2";    //访问数据库名称 
        public static string _collectionName2 = "collectionName2";    //数据表名称（临时，后续使用界面配置） 
        public static string _collectName = "collectName";
        public static string _collectName2 = "collectNameForInstruct";

        //返回时间范围内的所有BsonDocument集合
        public static List<BsonDocument> TimeRangeJudgment(int startTime, int stopTime, string tbParaCode, int limitVal)
        {

            List<BsonDocument> result = new List<BsonDocument>();

            client2 = new MongoClient(ConfigManager.GetConnectionString(_baseBDconection));
            if (client2 == null)
            {
                //待处理
                throw new Exception("MongoDB客户端建立失败");
            }
            db2 = client2.GetDatabase(ConfigManager.GetConnectionString(_baseBDName2));
            if (db2 == null)
            {
                //待处理
                throw new Exception(string.Format("数据库{0}访问失败", ConfigManager.GetConnectionString(_baseBDName2)));
            }
            _collectName = ConfigManager.GetConnectionString(_collectName);
            //3、文档集合（数据表）
            col2 = db2.GetCollection<BsonDocument>(_collectName);
            if (col2 == null)
            {
                //待处理
                throw new Exception(string.Format("文档集合{0}读取失败", ConfigManager.GetConnectionString(_collectionName2)));
            }


            var filterBuilders = Builders<BsonDocument>.Filter;
            var filter = filterBuilders.Gte("createTime", startTime) & filterBuilders.Lte("createTime", stopTime) & filterBuilders.Eq("ParaCode", tbParaCode.Split('-')[0]);

            //  var bsonLst = MongoDBHelper<ParaStorage>.GetModelByFilter(filter);
            var bsonLst = col2.Find<BsonDocument>(filter).ToList();
            foreach (var bson in bsonLst)
            {
                result.Add(bson);
            }

            return result;
        }


        /// <summary>
        /// 从指定数据库获取指定的数据集合，然后查询有关满足条件的记录
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="stopTime"></param>
        /// <param name="Para">输入的查询参数</param>
        /// <param name="whichPara"></param>
        /// <param name="limitVal"></param>
        /// <returns></returns>
        public static List<BsonDocument> GetBaseData(int startTime, int stopTime, string Para, string whichPara, int limitVal)
        {
            List<BsonDocument> result = new List<BsonDocument>();

            client2 = new MongoClient(ConfigManager.GetConnectionString(_baseBDconection));
            if (client2 == null)
            {
                //待处理
                throw new Exception("MongoDB客户端建立失败");
            }
            db2 = client2.GetDatabase(ConfigManager.GetConnectionString(_baseBDName2));
            if (db2 == null)
            {
                //待处理
                throw new Exception(string.Format("数据库{0}访问失败", ConfigManager.GetConnectionString(_baseBDName2)));
            }
            //指令历史数据的mongodb集合
            if (whichPara == "RadioButtonZL")
            {
                _collectName2 = ConfigManager.GetConnectionString(_collectName2);
                //3、文档集合（数据表）
                col2 = db2.GetCollection<BsonDocument>(_collectName2);
                if (col2 == null)
                {
                    //待处理
                    throw new Exception(string.Format("文档集合{0}读取失败", ConfigManager.GetConnectionString(_collectionName2)));
                }
            }
            else
            {
                _collectName = ConfigManager.GetConnectionString(_collectName);
                //3、文档集合（数据表）
                col2 = db2.GetCollection<BsonDocument>(_collectName);
                if (col2 == null)
                {
                    //待处理
                    throw new Exception(string.Format("文档集合{0}读取失败", ConfigManager.GetConnectionString(_collectName)));
                }
            }


            var filterBuilders = Builders<BsonDocument>.Filter;
            var filter = filterBuilders.Gte("createTime", startTime) & filterBuilders.Lte("createTime", stopTime) & filterBuilders.Eq("CMDCode", Para.Split('-')[0]);

            //  var bsonLst = MongoDBHelper<ParaStorage>.GetModelByFilter(filter);
            var bsonLst = col2.Find<BsonDocument>(filter).ToList();
            foreach (var bson in bsonLst)
            {
                result.Add(bson);
            }

            return result;
        }

    }
}
