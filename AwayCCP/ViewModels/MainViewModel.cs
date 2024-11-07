using System.Text.Json;
using System.Windows.Forms;
using System.Windows.Input;
using AwayCCP.Services;
using Prism.Mvvm;
using Color = System.Windows.Media.Color;

namespace AwayCCP.ViewModels
{
    internal class MainViewModel : BindableBase
    {
        private readonly IConfigRepo _configRepo;
        private readonly IEventAggregator _eventAggregator;
        private readonly ISentenceManager sentenceManager;
        private Color _backColor;
        private Color _foreColor;
        private int _boxHeight;
        private int _boxWidth;
        private int _fontSize;



        private string typedString;
        public string TypedString
        {
            get { return typedString; }
            set { SetProperty(ref typedString, value); }
        }


        private string toTypeString;
        public string ToTypeString
        {
            get { return toTypeString; }
            set { SetProperty(ref toTypeString, value); }
        }


        public List<string> Words { get; set; }
        public int CurrWordIndex { get; set; }
        public int CurrCharIndex { get; set; }

        public MainViewModel(IConfigRepo configRepo, IEventAggregator eventAggregator,ISentenceManager sentenceManager)
        {
            _configRepo = configRepo;
            _eventAggregator = eventAggregator;
            this.sentenceManager = sentenceManager;
            AssignCommands();
            _eventAggregator.GetEvent<ConfigModifiedEvent>().Subscribe((json) =>
            {
                IConfig config = JsonSerializer.Deserialize<Config>(json, new JsonSerializerOptions() { Converters = { new ColorJsonConverter() } })!;
                BackColor = config.BackColor;
                ForeColor = config.ForeColor;
                BoxWidth = config.BoxWidth;
                BoxHeight = config.BoxHeight;
                FontSize = config.FontSize;
            });
            Words = new List<string>();
        }

        public AsyncDelegateCommand LoadedCommand { get; private set; } = null!;

        public void AssignCommands()
        {

        }

        public async void LoadConfigs() // this method doesn't throw any exceptions,so don't worry
        {
            IConfig config = await _configRepo.LoadAsync().ConfigureAwait(false);
            BackColor = config.BackColor;
            ForeColor = config.ForeColor;
            BoxHeight = config.BoxHeight;
            BoxWidth = config.BoxWidth;
            FontSize = config.FontSize;
        }


        private LineController _lineController;

        public void TextInput(object sender, TextCompositionEventArgs e)
        {
            if (_lineController.IsMatch(e.Text[0], out string done, out string doing))
            {
                if (this.sentenceManager.Next())
                {
                    _lineController = new LineController(this.sentenceManager.CurrentLine);
                }
            }
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