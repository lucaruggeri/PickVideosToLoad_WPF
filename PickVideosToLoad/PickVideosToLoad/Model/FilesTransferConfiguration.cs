using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickVideosToLoad.Model
{
    public class FilesTransferConfiguration
    {
        public string sourceFolder { get; set; }
        public string destinationFolder { get; set; }
        public bool moveAndDeleteFromSource { get; set; }
        public bool startFromTheFirstFile { get; set; }
        public List<string> foldersToIgnore { get; set; }
        public List<string> extensionsToTransfer { get; set; }
        public List<string> extensionsToIgnore { get; set; }
        public float gigas { get; set; }
        public float megas { get; set; }

        public float maxMBAllowed {
            get { return Convert.ToSingle(this.ConvertGigasToMegas()) + megas; }
        }

        public double ConvertGigasToMegas()
        {
            return this.gigas * 1024.0;
        }

        public float GetMaxMegasAllowed()
        {
            return Convert.ToSingle(ConvertGigasToMegas()) + this.megas;
        }
    }
}
