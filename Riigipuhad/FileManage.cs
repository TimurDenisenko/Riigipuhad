using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Riigipuhad
{
    public static class FileManage
    {
        public static string GetSolutionDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }
        public static string[] GetFilesFromFolder(string filePath = null)
        {
            DirectoryInfo d = new DirectoryInfo(filePath ?? GetSolutionDirectory());
            FileInfo[] Files = d.GetFiles();
            foreach (FileInfo item in Files)
            {
                string a = item.Name;
            }
            return Files.Select(x => x.Name).ToArray();
        }
        public static void ClearFiles(string filePath = null)
        {
            foreach (string item in GetFilesFromFolder(filePath))
            {
                File.Delete(GetSolutionDirectory()+"/"+item);
            }
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
        public static ImageSource ConvertToImageSource(byte[] img) => 
            ImageSource.FromStream(() => new MemoryStream(img));
        public static ImageSource ConvertToImageSource(MediaFile img) => 
            ImageSource.FromStream(() => img.GetStream());
        public async static Task<byte[]> ConvertToByteArray(ImageSource imageSource)
        {
            Stream stream = await ((StreamImageSource)imageSource).Stream(CancellationToken.None);
            byte[] image = new byte[stream.Length];
            stream.Read(image, 0, image.Length);
            return image;
        }
    }
}
