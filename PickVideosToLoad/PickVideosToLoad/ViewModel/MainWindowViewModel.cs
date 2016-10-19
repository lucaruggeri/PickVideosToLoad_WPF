using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PickVideosToLoad.Model;
using System.ComponentModel;

namespace PickVideosToLoad.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public FilesTransferConfiguration myConfig { get; set; }

        public MainWindowViewModel()
        {
            myConfig = new FilesTransferConfiguration();
            myConfig.destinationFolder = @"C:\Users\Luca\Desktop\TEST COPY";
            myConfig.sourceFolder = @"G:\VIDEOS\extras\Extras Season 1\1x01";
            myConfig.foldersToIgnore = new List<string>(new string[] { "_DA SMISTARE" });
            myConfig.extensionsToTransfer = new List<string>(new string[] { "mp4", "avi", "mkv", "m4v" });
            myConfig.extensionsToIgnore = new List<string>(new string[] { "pdf", "txt", "mp3" });
            myConfig.moveAndDeleteFromSource = false;
            myConfig.startFromTheFirstFile = true;
            myConfig.gigas = 1;
            myConfig.megas = 500;
        }

        #region PropertyChanged event and handler
        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        public void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
