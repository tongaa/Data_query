using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HY.Common
{
   public class ConfigManager
    {

        //配置信息
       private static Dictionary<string, string> _configInfo = new Dictionary<string, string>();

       public static Dictionary<string, string> configInfo {
           get {
               GetConfigs();
               return _configInfo;
           }   
       }

       public static string GetConnectionString(string conncectionName)
       {
           if (configInfo.ContainsKey(conncectionName))
           {
               return configInfo[conncectionName];
           }

           return conncectionName;
       }

       public static void InsertInfo(string key, string value)
       {
           if (_configInfo.ContainsKey(key))
           {
               _configInfo[key] = value;
           }
           else
           {
               _configInfo.Add(key, value);
           }

       }
       public static void SetConfigs()
       {
           if (File.Exists("config.ini"))
           {
               File.Delete("config.ini");
           }
          
           foreach (var value in configInfo)
           {
               File.AppendAllText("config.ini", value.Key + "#" + value.Value + "\r\n");
           }
       }
       public static void GetConfigs()
       {
           if (File.Exists("config.ini"))
           {
               string[] configs = File.ReadAllLines("config.ini");
               foreach (var config in configs)
               {
                   string[] value = config.Split('#');
                   InsertInfo(value[0], value[1]);
               }
           }
       }
       
    }
}
