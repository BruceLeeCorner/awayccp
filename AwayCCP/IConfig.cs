using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AwayCCP
{
    public interface IConfig
    {
        Color BackColor { get; set; }
        Color ForeColor { get; set; }
        int FontSize { get; set; }
        int BoxWidth { get; set; }
        int BoxHeight { get; set; }

        void Load();
        void Save();
    }
}
