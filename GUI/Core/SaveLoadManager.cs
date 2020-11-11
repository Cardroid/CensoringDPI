using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace GBDPIGUI.Core
{
    public interface ISaveLoadable
    {
        string Save();
        bool Load(string json);
    }

    public static class SaveLoadManager
    {
        public static bool Save(string path, object value)
        {
            string json;
            if (value is ISaveLoadable saveLoadble)
            {
                json = saveLoadble.Save();
            }
            else
            {
                try
                {
                    json = JsonConvert.SerializeObject(value);
                }
                catch
                {
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(json))
                return false;

            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            File.WriteAllText(path, json, Encoding.UTF8);
            return true;
        }

        public static bool Load<T>(string path, T loadObject) where T : class
        {
            if (!File.Exists(path))
                return false;

            var json = File.ReadAllText(path, Encoding.UTF8);

            if (string.IsNullOrWhiteSpace(json))
                return false;

            if (loadObject is ISaveLoadable saveLoadble)
            {
                return saveLoadble.Load(json);
            }
            else
            {
                try
                {
                    var newLoadObject = JsonConvert.DeserializeObject<T>(json);

                    foreach (var propertyInfo in typeof(T).GetProperties())
                    {
                        if (Attribute.GetCustomAttribute(propertyInfo, typeof(JsonPropertyAttribute)) != null)
                            propertyInfo.SetMethod.Invoke(loadObject, new object[] { propertyInfo.GetMethod.Invoke(newLoadObject, null) });
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
