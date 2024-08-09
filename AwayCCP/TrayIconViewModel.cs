using Prism.Commands;
using System.Windows;
using System.Windows.Media;
using Prism.Mvvm;

namespace AwayCCP
{
    public class TrayIconViewModel : BindableBase
    {
        private Brush _backColor;
        private Brush _foreColor;
        private int _fontSize;
        private int _boxHeight;
        private int _boxWidth;

        public TrayIconViewModel()
        {
            AssignCommands();
        }

        private void AssignCommands()
        {
            ExitCommand = new DelegateCommand(() =>
            {
                Application.Current.MainWindow.Close();
            });


        }

        #region Define Commands

        public DelegateCommand ExitCommand { get; private set; }
        public DelegateCommand BackColorCommand { get; private set; }
        public DelegateCommand ForeColorCommand { get; private set; }
        public DelegateCommand FontSizeCommand { get; private set; }

        #endregion Define Commands

        #region Properties
        public Brush BackColor
        {
            get => _backColor;
            set => SetProperty(ref _backColor, value);
        }

        public Brush ForeColor
        {
            get => _foreColor;
            set => SetProperty(ref _foreColor, value);
        }

        public int FontSize
        {
            get => _fontSize;
            set => SetProperty(ref _fontSize, value);
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
        #endregion
    }
}