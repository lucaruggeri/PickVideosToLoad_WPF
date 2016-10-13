using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms; //must add reference to System.Windows.Forms

namespace Utilities
{
    public static class WpfClassicDialogs
    {

        public static string OpenFolderDialog(string defaultPath)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();

            dialog.Description = "My dialog";
            dialog.SelectedPath = defaultPath;

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return dialog.SelectedPath;
            }
            else
            {
                return null;
            }
        }
    }
}
