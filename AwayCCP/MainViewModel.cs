using System.Text.Json;
using Prism.Mvvm;
using Color = System.Windows.Media.Color;

namespace AwayCCP
{
    internal class MainViewModel : BindableBase
    {
        private readonly IConfigRepo _configRepo;
        private readonly IEventAggregator _eventAggregator;
        private Color _backColor;
        private Color _foreColor;
        private int _boxHeight;
        private int _boxWidth;
        private int _fontSize;

        public MainViewModel(IConfigRepo configRepo,IEventAggregator eventAggregator)
        {
            _configRepo = configRepo;
            _eventAggregator = eventAggregator;
            AssignCommands();
            _eventAggregator.GetEvent<ConfigModifiedEvent>().Subscribe((json) =>
            {
                IConfig config = JsonSerializer.Deserialize<Config>(json,new JsonSerializerOptions(){Converters = { new ColorJsonConverter() }})!;
                BackColor = config.BackColor;
                ForeColor = config.ForeColor;
                BoxWidth = config.BoxWidth;
                BoxHeight = config.BoxHeight;
                FontSize = config.FontSize;
            });
        }

        public AsyncDelegateCommand LoadedCommand { get; private set; } = null!;

        public void AssignCommands()
        {
            LoadedCommand = new AsyncDelegateCommand(async () =>
            {
                IConfig config = await _configRepo.LoadAsync();
                BackColor = config.BackColor;
                ForeColor = config.ForeColor;
                BoxHeight = config.BoxHeight;
                BoxWidth = config.BoxWidth;
                FontSize = config.FontSize;
                //_eventAggregator.GetEvent<ConfigModifiedEvent>().Subscribe(() =>
                //{
                // ViewModel如何调用ViewModel的方法？
                //});
            });
        }

        public Color BackColor
        {
            get => _backColor;
            set => SetProperty(ref _backColor, value);
        }

        public Color ForeColor
        {
            get => _foreColor;
            set => SetProperty(ref _foreColor, value);
        }

        public int BoxHeight
        {
            get => _boxHeight;
            set => SetProperty(ref _boxHeight, value);
        }

        public int BoxWidth
        {
            get => _boxWidth;
            set => SetProperty(ref _boxWidth, value);
        }

        public int FontSize
        {
            get => _fontSize;
            set => SetProperty(ref _fontSize, value);
        }
    }
}