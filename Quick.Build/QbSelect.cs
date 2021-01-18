using System;
using System.Collections.Generic;
using System.Text;

namespace Quick.Build
{
    /// <summary>
    /// Quick.Build选择辅助类
    /// </summary>
    public static class QbSelect
    {
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
            string selectedPrefix = "[*] ",
            string notSelectedPrefix = "[ ] ",
            ConsoleColor? selectedForegroundColor = null,
            ConsoleColor? selectedBackgroundColor = null,
            ConsoleColor? notSelectedForegroundColor = null,
            ConsoleColor? notSelectedBackgroundColor = null)
        {
            var selectedIndex = 0;
            while (true)
            {
                for (var i = 0; i < items.Length; i++)
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
                    Console.WriteLine();
                }
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
                }
                if (key.Key == ConsoleKey.Enter)
                    break;
                Console.CursorLeft = 0;
                Console.CursorTop -= items.Length;
            }
            return items[selectedIndex].Key;
        }
    }
}
