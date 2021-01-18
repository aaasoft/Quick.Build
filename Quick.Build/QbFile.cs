using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Quick.Build
{
    /// <summary>
    /// Quick.Build文件辅助类
    /// </summary>
    public static class QbFile
    {
        /// <summary>
        /// 改变文件头
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <param name="fileHeader">文件头</param>
        public static void ChangeHeader(string file, string fileHeader)
        {
            var fileHeaderByteArray = Encoding.ASCII.GetBytes(fileHeader);
            using (var fs = File.OpenWrite(file))
                fs.Write(fileHeaderByteArray, 0, fileHeaderByteArray.Length);
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="srcFile">源文件</param>
        /// <param name="destFile">目的文件</param>
        /// <param name="overwrite">是否覆盖</param>
        public static void Copy(string srcFile, string destFile, bool overwrite = false)
        {
            if (!File.Exists(srcFile))
                return;
            var dir = Path.GetDirectoryName(destFile);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            File.Copy(srcFile, destFile, overwrite);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="file">文件名</param>
        public static void Delete(string file)
        {
            if (!File.Exists(file))
                return;
            File.Delete(file);
        }

        /// <summary>
        /// 删除指定的文件
        /// </summary>
        /// <param name="folder">文件夹</param>
        /// <param name="searchPattern">搜索表达示</param>
        /// <param name="searchOption">搜索选项</param>
        public static void DeleteFiles(string folder, string searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var files = QbFolder.SearchFiles(folder, searchPattern, searchOption);
            if (files == null || files.Length == 0)
                return;
            foreach (var file in files)
                file.Delete();
        }

        /// <summary>
        /// 复制指定的文件
        /// </summary>
        /// <param name="srcFolder">源文件夹</param>
        /// <param name="destFolder">目的文件夹</param>
        /// <param name="searchPattern">搜索表达示</param>
        /// <param name="overwrite">是否覆盖</param>
        public static void CopyFiles(string srcFolder, string destFolder, string searchPattern, bool overwrite = false)
        {
            var files = QbFolder.SearchFiles(srcFolder, searchPattern);
            if (files == null || files.Length == 0)
                return;
            foreach (var file in files)
                file.CopyTo(Path.Combine(destFolder, file.Name), overwrite);
        }
    }
}
