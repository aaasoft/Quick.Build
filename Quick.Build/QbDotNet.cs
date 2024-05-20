using System.Collections.Generic;
using System.IO;

namespace Quick.Build
{
    public static class QbDotNet
    {
        public const string RUNTIME_FOLDER_NAME = "runtimes";
        /// <summary>
        /// 保留发布目录的运行库
        /// </summary>
        /// <param name="publishFolder"></param>
        /// <param name="runtimes"></param>
        public static void KeepPublishRuntimes(string publishFolder, params string[] runtimes)
        {
            var runtimesHashSet = new HashSet<string>(runtimes);
            var runtimesFolder = Path.Combine(publishFolder, RUNTIME_FOLDER_NAME);
            if (Directory.Exists(runtimesFolder))
                foreach (var runtimeDi in new DirectoryInfo(runtimesFolder).GetDirectories())
                {
                    if (!runtimesHashSet.Contains(runtimeDi.Name))
                        runtimeDi.Delete(true);
                }
        }

        /// <summary>
        /// 移除发布目录的运行库
        /// </summary>
        /// <param name="publishFolder"></param>
        /// <param name="runtimes"></param>
        public static void RemovePublishRuntimes(string publishFolder, params string[] runtimes)
        {
            var runtimesHashSet = new HashSet<string>(runtimes);
            var runtimesFolder = Path.Combine(publishFolder, RUNTIME_FOLDER_NAME);
            if (Directory.Exists(runtimesFolder))
                foreach (var runtimeDi in new DirectoryInfo(runtimesFolder).GetDirectories())
                {
                    if (runtimesHashSet.Contains(runtimeDi.Name))
                        runtimeDi.Delete(true);
                }
        }
    }
}