using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class FileSizesConverter
    {
        static double ConvertMegabytesToGigabytes(double megabytes) // SMALLER
        {
            // 1024 megabyte in a gigabyte
            return megabytes / 1024.0;
        }

        static double ConvertMegabytesToTerabytes(double megabytes) // SMALLER
        {
            // 1024 * 1024 megabytes in a terabyte
            return megabytes / (1024.0 * 1024.0);
        }

        static double ConvertGigabytesToMegabytes(double gigabytes) // BIGGER
        {
            // 1024 gigabytes in a terabyte
            return gigabytes * 1024.0;
        }

        static double ConvertGigabytesToTerabytes(double gigabytes) // SMALLER
        {
            // 1024 gigabytes in a terabyte
            return gigabytes / 1024.0;
        }

        static double ConvertTerabytesToMegabytes(double terabytes) // BIGGER
        {
            // 1024 * 1024 megabytes in a terabyte
            return terabytes * (1024.0 * 1024.0);
        }

        static double ConvertTerabytesToGigabytes(double terabytes) // BIGGER
        {
            // 1024 gigabytes in a terabyte
            return terabytes * 1024.0;
        }
    }
}
