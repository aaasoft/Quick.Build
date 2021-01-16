using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Quick.Build
{
    /// <summary>
    /// Quick.Build文件夹辅助类
    /// </summary>
    public static class QbFolder
    {
        /// <summary>
        /// 获取当前程序目录
        /// </summary>
        /// <returns></returns>
        public static string GetAppFolder()
        {
            return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="folder">文件夹名称</param>
        public static void Delete(
            string folder)
        {
            var dir = new DirectoryInfo(folder);
            if (!dir.Exists)
                return;
            dir.Delete(true);
        }

        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="srcFolder">源文件夹</param>
        /// <param name="destFolder">目的文件夹</param>
        public static void Copy(
            string srcFolder,
            string destFolder)
        {
            var srcDir = new DirectoryInfo(srcFolder);
            if (!srcDir.Exists)
                return;
            //确保目的文件夹已创建
            var destDir = new DirectoryInfo(destFolder);
            if (!destDir.Exists)
                destDir.Create();
            //先复制文件
            foreach (var file in srcDir.GetFiles())
                file.CopyTo(Path.Combine(destDir.FullName, file.Name));

            //再复制目录            
            foreach (var dir in srcDir.GetDirectories())
                Copy(dir.FullName, Path.Combine(destDir.FullName, dir.Name));
        }

        /// <summary>
        /// 搜索文件
        /// </summary>
        /// <param name="folder">文件夹</param>
        /// <param name="searchPattern">搜索表达示</param>
        /// <param name="searchOption">搜索选项</param>
        /// <returns></returns>
        public static FileInfo[] SearchFiles(string folder, string searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var dir = new DirectoryInfo(folder);
            if (!dir.Exists)
                return new FileInfo[0];
            return dir.GetFiles(searchPattern, searchOption);
        }

        /// <summary>
        /// 搜索文件夹
        /// </summary>
        /// <param name="folder">文件夹</param>
        /// <param name="searchPattern">搜索表达示</param>
        /// <param name="searchOption">搜索选项</param>
        /// <returns></returns>
        public static DirectoryInfo[] SearchFolders(string folder, string searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var dir = new DirectoryInfo(folder);
            if (!dir.Exists)
                return new DirectoryInfo[0];
            return dir.GetDirectories(searchPattern, searchOption);
        }

        /// <summary>
        /// 删除指定的文件夹
        /// </summary>
        /// <param name="folder">文件夹</param>
        /// <param name="searchPattern">搜索表达示</param>
        /// <param name="searchOption">搜索选项</param>
        public static void DeleteFolders(string folder, string searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var items = SearchFolders(folder, searchPattern, searchOption);
            if (items == null || items.Length == 0)
                return;
            foreach (var item in items)
                item.Delete(true);
        }
    }
}
