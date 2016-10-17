using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Utilities;
using PickVideosToLoad.ViewModel;
using PickVideosToLoad.Model;

namespace PickVideosToLoad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel viewModel { get; set; }

        public MainWindow()
        {
            viewModel = new MainWindowViewModel();
            this.DataContext = viewModel;
            InitializeComponent();
        }

        private void btnSetSourceFolder_Click(object sender, RoutedEventArgs e)
        {
            viewModel.myConfig.sourceFolder = Utilities.WpfClassicDialogs.OpenFolderDialog(viewModel.myConfig.sourceFolder);
            viewModel.NotifyPropertyChanged("myConfig");
        }

        private void btnSetDestinationFolder_Click(object sender, RoutedEventArgs e)
        {
            viewModel.myConfig.destinationFolder = Utilities.WpfClassicDialogs.OpenFolderDialog(viewModel.myConfig.destinationFolder);
            viewModel.NotifyPropertyChanged("myConfig");
        }

        private void txtGigas_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void btnTransfer_Click(object sender, RoutedEventArgs e)
        {
            Utilities.FilesAndFolders faf = new FilesAndFolders();
            faf.TransferRandomFiles(viewModel.myConfig);
        }

        private void chkMove_Checked(object sender, RoutedEventArgs e)
        {

        }

    }
}
