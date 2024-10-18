using System.Linq;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppMasters.Json
{
    public class JsonLoader
    {
        public string LoadJson(string path) 
        {
            var resource = ResourcesLoader.Instance.Load(path);
            return ((TextAsset)resource).text;
        }
        public JToken LoadJsonAsJToken(string path) => JsonConvert.DeserializeObject<JToken>(LoadJson(path));

        public Dictionary<string, JsonItem> StringToJsonItem(string path, string collectionName)
        {
            var jtoken = LoadJsonAsJToken(path);
            // Debug.Log($"jtoken {jtoken.ToString()} ");
            // Debug.Log($"jtoken {jtoken[collectionName].ToString()} ");
            // Debug.Log($"jtoken {jtoken[collectionName].ToObject<List<JsonItem>>().FirstOrDefault()} ");
            return jtoken[collectionName].ToObject<List<JsonItem>>().ToDictionary(x => x.id, x => x);
        }

        // optional
        // Dictionary<string, JToken> StringToJsonJToken(string path)
        // {
        //     // return JsonConvert.DeserializeObject<JToken>(LoadJson(path)).ToObject<List<JToken>>().ToDictionary(x => x["id"].ToString(), x => x);
        // }
    }
}