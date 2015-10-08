// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;

//using DHT.MainModule;

namespace DistributedSearchs
{
    /// <summary>
    ///   Interaction logic for MainWindow.xaml
    /// </summary>
    [Export]
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
        }

        [Import]
        public MainWindowViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                OnPropertyChanged("ViewModel");
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}