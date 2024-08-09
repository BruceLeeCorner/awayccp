using Prism.Mvvm;
using Color = System.Windows.Media.Color;

namespace AwayCCP
{
    internal class MainViewModel : BindableBase
    {
        private readonly Config _config;
        private Color _backColor;
        private Color _fontColor;
        private int _height;
        private int _width;
        private int _fontSize;

        public MainViewModel(Config config)
        {
            _config = config;
            FontSize = config.FontSize;
        }

        public Color BackColor
        {
            get => _backColor;
            set
            {
                if (SetProperty(ref _backColor, value))
                {
                    
                }
            }
        }

        public Color FontColor
        {
            get => _fontColor;
            set
            {
                if (SetProperty(ref _fontColor, value))
                {
                    
                }
            }
        }

        public int Height
        {
            get => _height;
            set
            {
                if (SetProperty(ref _height, value))
                {
                  
                }
            }
        }

        public int Width
        {
            get => _width;
            set
            {
                if (SetProperty(ref _width, value))
                {
                    
                }
            }
        }

        public int FontSize
        {
            get => _fontSize;
            set
            {
                if (SetProperty(ref _fontSize, value))
                {
                    _config.FontSize = value;
                }
            }
        }
    }
}