using System;
using System.Runtime.InteropServices;

namespace Quick.Build
{
    public static class QbGui
    {
        public static void OpenFolder(string folder)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                QbCommand.Run("Explorer", folder);
            }
            else
            {
                QbCommand.Run("xdg-open", folder);
            }
        }
    }
}
