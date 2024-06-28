using System;

namespace Quick.Build
{
    public static class QbGui
    {
        public static void OpenFolder(string folder)
        {
            if (OperatingSystem.IsWindows())
            {
                QbCommand.Run("Explorer", folder);
            }
            else if (OperatingSystem.IsMacOS())
            {
                QbCommand.Run("open", folder);
            }
            else
            {
                QbCommand.Run("xdg-open", folder);
            }
        }
    }
}
