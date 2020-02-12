using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using Newtonsoft.Json.Converters;

namespace BG.Utilities
{
    public static class JsonHelper
    {
        /// <summary> 
        /// Json To Object
        /// </summary>
        /// <param name="json">Json</param> 
        /// <returns>Object</returns> 
        public static object ToObjct(this string json)
        {
            return json == null ? null : JsonConvert.DeserializeObject(json);
        }
        /// <summary>
        /// Object To Json
        /// </summary>
        /// <param name="obj">Object</param> 
        /// <returns>Json</returns> 
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        /// <summary>
        /// Json To Object T 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Json"></param>
        /// <returns></returns>
        public static T ToObject<T>(this string json)
        {
            return json == null ? default(T) : JsonConvert.DeserializeObject<T>(json);
        }
        /// <summary>
        /// Json To List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Json"></param>
        /// <returns></returns> 
        public static List<T> ToList<T>(this string json)
        {
            return json == null ? null : JsonConvert.DeserializeObject<List<T>>(json);
        }
        /// <summary>
        /// Json To Table
        /// </summary>
        /// <param name="Json"></param>
        /// <returns></returns>
        public static DataTable JsonToTable(this string json)
        {
            return json == null ? null : JsonConvert.DeserializeObject<DataTable>(json);
        }
        /// <summary> 
        /// Table To Json
        /// </summary> 
        /// <param name="dataTable"></param>
        /// <returns></returns> 
        public static string TableToJson(this DataTable dataTable)
        {
            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return dataTable == null ? "" : JsonConvert.SerializeObject(dataTable, new DataTableConverter(), timeFormat);
        }
    }
}
