using System;

namespace Quick.Build
{
    public static class QbGui
    {
        public static void OpenFolder(string folder)
        {
            if (OperatingSystem.IsWindows())
            {
                QbCommand.Run("explorer", folder, handleExitCode: ret => true);
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
