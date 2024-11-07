using AwayCCP.Services;
using Prism.Dialogs;
using System.Text.Json;
using System.Windows;
using System.Windows.Media;

namespace AwayCCP.ViewModels
{
    public class TrayIconViewModel : BindableBase
    {
        #region Constructors

        public TrayIconViewModel(IConfigRepo configRepo, IEventAggregator eventAggregator, ISentenceManager sentenceManager, IDialogService dialogService)
        {
            _configRepo = configRepo;
            _eventAggregator = eventAggregator;
            this.sentenceManager = sentenceManager;
            this.dialogService = dialogService;
            AssignCommands();
        }

        #endregion Constructors

        #region Fields

        private readonly IConfigRepo _configRepo;
        private readonly IEventAggregator _eventAggregator;
        private readonly ISentenceManager sentenceManager;
        private readonly IDialogService dialogService;
        private Color _backColor;
        private int _boxHeight;
        private int _boxWidth;
        private int _fontSize;
        private Color _foreColor;

        #endregion Fields

        #region Properties

        #region Commands

        public DelegateCommand BackColorCommand { get; private set; } = null!;
        public DelegateCommand ExitCommand { get; private set; } = null!;
        public DelegateCommand FontSizeCommand { get; private set; } = null!;
        public DelegateCommand ForeColorCommand { get; private set; } = null!;
        public AsyncDelegateCommand LoadedCommand { get; private set; } = null!;
        public DelegateCommand ShowTextBoxCommand { get; private set; } = null!;
        public DelegateCommand<string> LoadFileCommand { get; private set; } = null!;
        #endregion Commands

        public Color BackColor
        {
            get => _backColor;
            set
            {
                if (SetProperty(ref _backColor, value))
                {
                    _ = SaveAndNotifyAfterConfigModified();
                }
            }
        }

        public int BoxHeight
        {
            get => _boxHeight;
            set
            {
                if (SetProperty(ref _boxHeight, value))
                {
                    _ = SaveAndNotifyAfterConfigModified();
                }
            }
        }

        public int BoxWidth
        {
            get => _boxWidth;
            set
            {
                if (SetProperty(ref _boxWidth, value))
                {
                    _ = SaveAndNotifyAfterConfigModified();
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
                    _ = SaveAndNotifyAfterConfigModified();
                }
            }
        }

        public Color ForeColor
        {
            get => _foreColor;
            set
            {
                if (SetProperty(ref _foreColor, value))
                {
                    _ = SaveAndNotifyAfterConfigModified();
                }
            }
        }

        #endregion Properties

        #region Methods

        private void AssignCommands()
        {
            ExitCommand = new DelegateCommand(() =>
            {
                Application.Current.MainWindow!.Close();
            });

            ShowTextBoxCommand = new DelegateCommand(() =>
            {
                _eventAggregator.GetEvent<ShowTextBoxEvent>().Publish();
            });

            LoadedCommand = new AsyncDelegateCommand(async () =>
            {
                var config = await _configRepo.LoadAsync();
                _backColor = config.BackColor;
                _foreColor = config.ForeColor;
                _fontSize = config.FontSize;
                _boxHeight = config.BoxHeight;
                _boxWidth = config.BoxWidth;
                RaisePropertyChanged(nameof(BackColor));
                RaisePropertyChanged(nameof(ForeColor));
                RaisePropertyChanged(nameof(FontSize));
                RaisePropertyChanged(nameof(BoxHeight));
                RaisePropertyChanged(nameof(BoxWidth));
            });

            LoadFileCommand = new DelegateCommand<string>((path) =>
            {
                sentenceManager.Load(path);
            }).Catch<Exception>(e =>
            {
                var @params = new DialogParameters
                {
                    { "content", e.Message }
                };
                dialogService.Show("dialog", @params, null);
            });
        }

        private async Task SaveAndNotifyAfterConfigModified()
        {
            var config = new Config()
            {
                ForeColor = ForeColor,
                FontSize = FontSize,
                BackColor = BackColor,
                BoxWidth = BoxWidth,
                BoxHeight = BoxHeight,
            };
            var t = _configRepo.SaveAsync(config);
            string json = JsonSerializer.Serialize(config, new JsonSerializerOptions()
            {
                Converters = { new ColorJsonConverter() }
            });
            _eventAggregator.GetEvent<ConfigModifiedEvent>().Publish(json);
            await t;
        }

        #endregion Methods
    }
}