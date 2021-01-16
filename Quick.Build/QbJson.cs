using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Quick.Build
{
    /// <summary>
    /// Quick.Build JSON辅助类
    /// </summary>
    public static class QbJson
    {
        /// <summary>
        /// 读取文件中属性的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static T Read<T>(string file, string propertyName)
        {
            var content = File.ReadAllText(file);
            var jobj = JObject.Parse(content);
            var property = jobj.Property(propertyName);
            return property.Value<T>();
        }
        /// <summary>
        /// 读取文件中属性的值(String)
        /// </summary>
        /// <param name="file"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string ReadString(string file, string propertyName)
            => Read<string>(file, propertyName);
        /// <summary>
        /// 读取文件中属性的值(Int32)
        /// </summary>
        /// <param name="file"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static int ReadInt32(string file, string propertyName)
            => Read<int>(file, propertyName);
        /// <summary>
        /// 读取文件中属性的值(Int64)
        /// </summary>
        /// <param name="file"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static long ReadInt64(string file, string propertyName)
            => Read<long>(file, propertyName);
        /// <summary>
        /// 读取文件中属性的值(Double)
        /// </summary>
        /// <param name="file"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static double ReadDouble(string file, string propertyName)
            => Read<double>(file, propertyName);
        /// <summary>
        /// 读取文件中属性的值(Boolean)
        /// </summary>
        /// <param name="file"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static bool ReadBoolean(string file, string propertyName)
            => Read<bool>(file, propertyName);

        /// <summary>
        /// 将属性的值写入到文件中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void Write<T>(string file, string propertyName, T value)
        {
            var content = File.ReadAllText(file);
            var jobj = JObject.Parse(content);
            var property = jobj.Property(propertyName);
            if (property == null)
            {
                jobj.Add(propertyName, null);
                property = jobj.Property(propertyName);
            }
            property.Value = JToken.FromObject(value);
            File.WriteAllText(file, jobj.ToString(Newtonsoft.Json.Formatting.Indented));
        }

        /// <summary>
        /// 将属性的值写入到文件中(String)
        /// </summary>
        /// <param name="file"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void WriteString(string file, string propertyName, string value)
            => Write(file, propertyName, value);
        /// <summary>
        /// 将属性的值写入到文件中(Int32)
        /// </summary>
        /// <param name="file"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void WriteInt32(string file, string propertyName, int value)
            => Write(file, propertyName, value);
        /// <summary>
        /// 将属性的值写入到文件中(Int64)
        /// </summary>
        /// <param name="file"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void WriteInt64(string file, string propertyName, long value)
            => Write(file, propertyName, value);
        /// <summary>
        /// 将属性的值写入到文件中(Double)
        /// </summary>
        /// <param name="file"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void WriteDouble(string file, string propertyName, double value)
            => Write(file, propertyName, value);
        /// <summary>
        /// 将属性的值写入到文件中(Boolean)
        /// </summary>
        /// <param name="file"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void WriteBoolean(string file, string propertyName, bool value)
            => Write(file, propertyName, value);
    }
}
