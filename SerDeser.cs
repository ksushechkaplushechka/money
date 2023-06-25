
using Newtonsoft.Json;
using System.IO;

namespace учет_буджета
{
    internal class SerDeser
    {
        public static void JasonIn<T>(T LibIn)
        {
            string file = JsonConvert.SerializeObject(LibIn);
            File.WriteAllText("C:\\Users\\porfi\\OneDrive\\Рабочий стол\\file.json", file);
        }
        public static T JasonOut<T>()
        {
            string file = File.ReadAllText("C:\\Users\\porfi\\OneDrive\\Рабочий стол\\file.json");
            T write = JsonConvert.DeserializeObject<T>(file);
            return write;
        }
    }
}
