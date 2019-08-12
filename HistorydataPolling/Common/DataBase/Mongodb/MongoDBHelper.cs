using HY.Common;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TimeTypeHelper
{
    public class MongoDBHelper<T> where T :class 
    {
        #region 字段、属性
        private static MongoClient client; //连接
   
        private static IMongoDatabase db;  //数据库
        public static IMongoCollection<BsonDocument> col;  //集合 
   

        public static string _baseBDconection = "baseDBIp"; //实例连接地址
        public static string _baseBDName = "baseDBName";    //访问数据库名称 
        public static string _collectionName = "collectionName";    //数据表名称（临时，后续使用界面配置） 

        #endregion

        #region 构造函数
        public MongoDBHelper()
        {

        }

        #endregion

        #region 方法
        /// <summary>
        /// 初始化
        /// </summary>
        public static IMongoCollection<BsonDocument> Init()
        {
            //1、实例连接客户端
            client = new MongoClient(ConfigManager.GetConnectionString(_baseBDconection));
          
            if (client == null)  //.Cluster.Description.State.ToString().Equals( "Disconnected") )
            {
                //待处理
                throw new Exception("MongoDB客户端建立失败");
            }
            //2、数据库
            db = client.GetDatabase(ConfigManager.GetConnectionString(_baseBDName));
            if (db == null)
            {
                //待处理
                throw new Exception(string.Format("数据库{0}访问失败", ConfigManager.GetConnectionString(_baseBDName)));
            }
            //3、文档集合（数据表）
            col = db.GetCollection<BsonDocument>(string.Format("{0}{1}", "T_", typeof(T).Name.Replace("Model", "")));
            if (col == null)
            {
                //待处理
                throw new Exception(string.Format("文档集合{0}读取失败", ConfigManager.GetConnectionString(_collectionName)));
            }

            return col;
        }
 

        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="doc"></param>
        public static void InsertOne(BsonDocument doc)
        {
            col.InsertOne(doc);
        }

        /// <summary>
        /// 根据过滤条件查询指定值
        /// </summary>
        /// <returns></returns>
        public static List<BsonDocument> GetDesignatedValueByFilter(ProjectionDefinition<BsonDocument> projection, FilterDefinitionBuilder<BsonDocument> builderFilter, int limitVal=10000)
        {


            return  col.Find<BsonDocument>(builderFilter.Empty).Limit(limitVal).Project(projection).ToList();
        }


        //改造
        public static List<BsonDocument> GetModelByFilter(FilterDefinition<BsonDocument> filter, int limitVal = 10000)
        {
            if (filter == null)
            {
                return col.Find(new BsonDocument()).Limit(limitVal).ToList();
            }
            else
            {
                return col.Find(filter).Limit(limitVal).ToList();
            }
        }
        public static List<BsonDocument> GetAllDocuments(IMongoCollection<BsonDocument> mydb, int limitVal = 10000) //获取有限条文档
        {

            List<BsonDocument> temp = mydb.Find(new BsonDocument()).Limit(limitVal).ToList();
         
            return temp;

        }
        #endregion


    }
}
