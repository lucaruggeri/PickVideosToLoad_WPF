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

namespace PickVideosToLoad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSetSourceFolder_Click(object sender, RoutedEventArgs e)
        {
            lblSourceFolder.Content = Utilities.WpfClassicDialogs.OpenFolderDialog(@"G:\VIDEOS\Shark Tank Season 1-5\Shark.Tank.Season.5");
        }

        private void btnSetDestinationFolder_Click(object sender, RoutedEventArgs e)
        {
            lblDestinationFolder.Content = Utilities.WpfClassicDialogs.OpenFolderDialog(@"C:\_TEMP");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ResetGraphics();
        }

        private void ResetGraphics()
        {
            lblSourceFolder.Content = string.Empty;
            lblDestinationFolder.Content = string.Empty;
        }

        private void btnTransfer_Click(object sender, RoutedEventArgs e)
        {
            Utilities.FilesAndFolders faf = new FilesAndFolders();

            if (chkMove.IsChecked == true)
            {
                //faf.MoveFiles(lblSourceFolder.Content.ToString(), lblDestinationFolder.Content.ToString());
            }
            else
            {
                List<string> foldersIgnore = new List<string>(new string[] { "_DA SMISTARE" });
                List<string> extensionsToTransfer = new List<string>(new string[] { "mp4", "avi", "mkv", "m4v" });
                List<string> extensionsToIgnore = new List<string>(new string[] { "pdf", "txt", "mp3" });

                double megasFromGigas = double.Parse(txtGigas.Text, CultureInfo.InvariantCulture.NumberFormat) * 1024.0;
                float megas = float.Parse(txtMegabits.Text, CultureInfo.InvariantCulture.NumberFormat);

                float maxMegabytesAllowed = Convert.ToSingle(megasFromGigas) + megas;

                bool moveAndDeleteFromSource;
                if (chkMove.IsChecked == true)
                {
                    moveAndDeleteFromSource = true;
                }
                else
                {
                    moveAndDeleteFromSource = false;
                }

                faf.CopyRandomFiles(lblSourceFolder.Content.ToString(), lblDestinationFolder.Content.ToString(), moveAndDeleteFromSource, foldersIgnore, extensionsToTransfer, extensionsToIgnore, maxMegabytesAllowed);
            }
        }
    }
}
