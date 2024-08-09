using Nito.AsyncEx;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace AwayCCP
{
    public class Config : IConfig
    {
        public Color BackColor { get; set; }
        public int BoxHeight { get; set; }
        public int BoxWidth { get; set; }
        public int FontSize { get; set; }
        public Color ForeColor { get; set; }

    }
}