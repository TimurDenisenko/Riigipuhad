using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace Riigipuhad
{
    public static class FileManage
    {
        public static string GetSolutionDirectory()
        {
            var a = Directory.GetCurrentDirectory();
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            return a;
        }
        public static void SerializeToFile<T>(T obj, string filePath)
        {
            string json = JsonConvert.SerializeObject(obj);
            File.WriteAllText(filePath, json);
        }

        public static T DeserializeFromFile<T>(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
