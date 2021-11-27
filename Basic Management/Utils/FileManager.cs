using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Basic_Management.Utils
{
    public class FileManager
    {
        public static object GetSerializable(string filePath)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
            object obj = formatter.Deserialize(stream);
            stream.Close();
            return obj;
        }
        
        public static void SaveSerializable<T>(string filePath, T serializable)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, serializable);
            stream.Close();
        }
    }
}