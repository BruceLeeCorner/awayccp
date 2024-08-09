using System.IO;
using System.Windows.Media;
using System.Windows.Media;
using System.Reflection;

namespace AwayCCP
{
    public class Config : IConfig
    {
        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }
        public int FontSize { get; set; }
        public int BoxWidth { get; set; }
        public int BoxHeight { get; set; }

        public void Load()
        {
            
        }

        public void Save()
        {
            
        }

        private readonly string _path;

        public Config()
        {
            string productName = Assembly.GetEntryAssembly()!.GetCustomAttribute<AssemblyProductAttribute>()!.Product;
            var configFileName = "Config.json";
            _path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), productName, configFileName);
        }

        public void Initialize()
        {
            try
            {
              
            }
            catch
            {
                // ignored
            }
        }

        public void Terminate()
        {
          
        }
    }


    //class ColorConverter : JsonConverter<System.Drawing.Color>
    //{
    //    public override void WriteJson(JsonWriter writer, System.Drawing.Color value, JsonSerializer serializer)
    //    {
    //        writer.WriteValue(ColorTranslator.ToHtml(value));
    //    }

    //    public override System.Drawing.Color ReadJson(JsonReader reader, Type objectType, System.Drawing.Color existingValue, bool hasExistingValue,
    //        JsonSerializer serializer)
    //    {
    //        DefaultContractResolver
    //    }
    //}


}