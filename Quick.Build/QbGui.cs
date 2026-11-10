using System;
using static SimpleExec.Command;

namespace Quick.Build
{
    public static class QbGui
    {
        public static void OpenFolder(string folder)
        {
            if (OperatingSystem.IsWindows())
            {
                Run("explorer", folder, handleExitCode: ret => true);
            }
            else if (OperatingSystem.IsMacOS())
            {
                Run("open", folder);
            }
            else
            {
                Run("xdg-open", folder);
            }
        }
    }
}
