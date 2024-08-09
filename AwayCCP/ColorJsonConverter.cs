using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AwayCCP
{
    public class ColorJsonConverter : JsonConverter<System.Windows.Media.Color>
    {
        public override System.Windows.Media.Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return (System.Windows.Media.Color)System.Windows.Media.ColorConverter
                .ConvertFromString(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, System.Windows.Media.Color value, JsonSerializerOptions options)
        {
            writer.WriteRawValue($"\"#{value.A:X2}{value.R:X2}{value.G:X2}{value.B:X2}\"");
        }
    }
}