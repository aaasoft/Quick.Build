using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Quick.Build
{
    /// <summary>
    /// Quick.Build选择辅助类
    /// </summary>
    public static class QbSelect
    {
        /// <summary>
        /// 输入选择
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string InputSelect(KeyValuePair<string, string>[] items, string inputText = ">", string template = "[{0}] {1}")
        {
            for (var i = 0; i < items.Length; i++)
            {
                var item = items[i];
                Console.WriteLine(string.Format(template, i + 1, item.Value));
            }
            Console.Write(inputText);

            int selectedIndex = 0;
            while (true)
            {
                var key = Console.ReadLine();
                if (int.TryParse(key, out selectedIndex))
                {
                    selectedIndex--;
                    if (selectedIndex >= 0 && selectedIndex < items.Length)
                        break;
                }
                Console.Write(inputText);
            }
            return items[selectedIndex].Key;
        }

        public static string[] MultiSelect(KeyValuePair<string, string>[] items,
            string selectPrefix = "> ",
            string notSelectPrefix = "  ",
            string selectedPrefix = "[*] ",
            string notSelectedPrefix = "[ ] ",
            ConsoleColor? selectedForegroundColor = null,
            ConsoleColor? selectedBackgroundColor = null,
            ConsoleColor? notSelectedForegroundColor = null,
            ConsoleColor? notSelectedBackgroundColor = null)
        {
            var selectedIndex = 0;
            var displayWindowStartIndex = 0;

            List<int> selectedIndexList = new List<int>();
            while (true)
            {
                var itemCount = items.Length;
                var windowHeight = Console.WindowHeight;
                var startIndex = 0;
                var endIndex = itemCount - 1;
                //如果选择的条目数量大于了窗口行数
                if (itemCount > windowHeight)
                {
                    Console.Clear();
                    if (selectedIndex < displayWindowStartIndex)
                    {
                        displayWindowStartIndex--;
                    }
                    else if (selectedIndex > displayWindowStartIndex + windowHeight - 1)
                    {
                        displayWindowStartIndex++;
                    }
                    startIndex = displayWindowStartIndex;
                    endIndex = Math.Min(endIndex, displayWindowStartIndex + windowHeight - 1);
                }
                for (var i = startIndex; i <= endIndex; i++)
                {
                    //控制台之前的前景色
                    var preForegroundColor = Console.ForegroundColor;
                    //控制台之前的背景色
                    var preBackColor = Console.BackgroundColor;

                    if (i == selectedIndex)
                        Console.Write(selectPrefix);
                    else
                        Console.Write(notSelectPrefix);
                    var item = items[i];
                    if (selectedIndexList.Contains(i))
                    {
                        if (selectedForegroundColor != null)
                            Console.ForegroundColor = selectedForegroundColor.Value;
                        if (selectedBackgroundColor != null)
                            Console.BackgroundColor = selectedBackgroundColor.Value;
                        Console.Write(selectedPrefix);
                    }
                    else
                    {
                        if (notSelectedForegroundColor != null)
                            Console.ForegroundColor = notSelectedForegroundColor.Value;
                        if (notSelectedBackgroundColor != null)
                            Console.BackgroundColor = notSelectedBackgroundColor.Value;
                        Console.Write(notSelectedPrefix);
                    }
                    Console.Write(item.Value);
                    //改变回原来的颜色
                    if (Console.ForegroundColor != preForegroundColor)
                        Console.ForegroundColor = preForegroundColor;
                    if (Console.BackgroundColor != preBackColor)
                        Console.BackgroundColor = preBackColor;
                    if (i < endIndex)
                        Console.WriteLine();
                    else
                        Console.CursorLeft = Console.WindowWidth;
                }
                if (Console.IsInputRedirected)
                    throw new IOException("Console's input is redirected!");
                var key = Console.ReadKey();
                //清空输入
                Console.CursorLeft = 0;
                Console.Write(" ");
                Console.CursorLeft = 0;

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex--;
                        if (selectedIndex < 0)
                            selectedIndex = 0;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex++;
                        if (selectedIndex >= items.Length)
                            selectedIndex = items.Length - 1;
                        break;
                    case ConsoleKey.Spacebar:
                        if (selectedIndexList.Contains(selectedIndex))
                            selectedIndexList.Remove(selectedIndex);
                        else
                            selectedIndexList.Add(selectedIndex);
                        break;
                }
                if (key.Key == ConsoleKey.Enter)
                    break;
                Console.CursorLeft = 0;
                var currentCursorTop = Console.CursorTop - itemCount + 1;
                if (currentCursorTop < 0)
                    currentCursorTop = 0;
                Console.CursorTop = currentCursorTop;
            }
            Console.WriteLine();
            return selectedIndexList.Select(t => items[t].Key).ToArray();
        }

        /// <summary>
        /// 方向剪头键选择
        /// </summary>
        /// <param name="items"></param>
        /// <param name="selectedPrefix"></param>
        /// <param name="notSelectedPrefix"></param>
        /// <param name="selectedForegroundColor"></param>
        /// <param name="selectedBackgroundColor"></param>
        /// <param name="notSelectedForegroundColor"></param>
        /// <param name="notSelectedBackgroundColor"></param>
        /// <returns></returns>
        public static string ArrowSelect(
            KeyValuePair<string, string>[] items,
            string selectedPrefix = "> ",
            string notSelectedPrefix = "  ",
            ConsoleColor? selectedForegroundColor = null,
            ConsoleColor? selectedBackgroundColor = null,
            ConsoleColor? notSelectedForegroundColor = null,
            ConsoleColor? notSelectedBackgroundColor = null)
        {
            var selectedIndex = 0;
            var displayWindowStartIndex = 0;
            
            while (true)
            {
                var itemCount = items.Length;
                var windowHeight = Console.WindowHeight;
                var startIndex = 0;
                var endIndex = itemCount - 1;
                //如果选择的条目数量大于了窗口行数
                if (itemCount > windowHeight)
                {
                    Console.Clear();
                    if (selectedIndex < displayWindowStartIndex)
                    {
                        displayWindowStartIndex--;
                    }
                    else if (selectedIndex > displayWindowStartIndex + windowHeight - 1)
                    {
                        displayWindowStartIndex++;
                    }
                    startIndex = displayWindowStartIndex;
                    endIndex = Math.Min(endIndex, displayWindowStartIndex + windowHeight - 1);
                }
                for (var i = startIndex; i <= endIndex; i++)
                {
                    //控制台之前的前景色
                    var preForegroundColor = Console.ForegroundColor;
                    //控制台之前的背景色
                    var preBackColor = Console.BackgroundColor;

                    var item = items[i];
                    if (i == selectedIndex)
                    {
                        if (selectedForegroundColor != null)
                            Console.ForegroundColor = selectedForegroundColor.Value;
                        if (selectedBackgroundColor != null)
                            Console.BackgroundColor = selectedBackgroundColor.Value;
                        Console.Write(selectedPrefix);
                    }
                    else
                    {
                        if (notSelectedForegroundColor != null)
                            Console.ForegroundColor = notSelectedForegroundColor.Value;
                        if (notSelectedBackgroundColor != null)
                            Console.BackgroundColor = notSelectedBackgroundColor.Value;
                        Console.Write(notSelectedPrefix);
                    }
                    Console.Write(item.Value);
                    //改变回原来的颜色
                    if (Console.ForegroundColor != preForegroundColor)
                        Console.ForegroundColor = preForegroundColor;
                    if (Console.BackgroundColor != preBackColor)
                        Console.BackgroundColor = preBackColor;
                    if (i < endIndex)
                        Console.WriteLine();
                    else
                        Console.CursorLeft = Console.WindowWidth - 1;
                }
                if (Console.IsInputRedirected)
                    throw new IOException("Console's input is redirected!");
                var key = Console.ReadKey();
                //清空输入
                Console.CursorLeft = 0;
                Console.Write(" ");
                Console.CursorLeft = 0;

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex--;
                        if (selectedIndex < 0)
                            selectedIndex = 0;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex++;
                        if (selectedIndex >= itemCount)
                            selectedIndex = itemCount - 1;
                        break;
                }
                if (key.Key == ConsoleKey.Enter)
                    break;
                Console.CursorLeft = 0;
                var currentCursorTop = Console.CursorTop - itemCount + 1;
                if (currentCursorTop < 0)
                    currentCursorTop = 0;
                Console.CursorTop = currentCursorTop;
            }
            Console.WriteLine();
            return items[selectedIndex].Key;
        }
    }
}
