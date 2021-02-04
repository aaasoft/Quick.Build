using Quick.Build;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

//准备目录变量
var appFolder = QbFolder.GetAppFolder();
if (appFolder == Environment.CurrentDirectory)
    Environment.CurrentDirectory = Path.GetFullPath("../../../../");
var baseFolder = Environment.CurrentDirectory;

Console.WriteLine("----------------------------------");
Console.WriteLine($"  欢迎使用{QbJson.ReadString("_build/test.json", "Name")}编译脚本");
Console.WriteLine("----------------------------------");
Console.WriteLine();
string selectedKey = string.Empty;

Console.WriteLine("输入选择测试，请选择：");
selectedKey = QbSelect.InputSelect(
    AppDomain.CurrentDomain.GetAssemblies()
    .ToDictionary(t => t.GetName().Name, t => t.GetName().Name)
    .ToArray());
Console.WriteLine("选择了：" + selectedKey);
Console.WriteLine();

Console.WriteLine("高亮选择测试，请选择：");
selectedKey = QbSelect.ArrowSelect(
    AppDomain.CurrentDomain.GetAssemblies()
    .ToDictionary(t => t.GetName().Name, t => t.GetName().Name)
    .ToArray(), selectedForegroundColor: ConsoleColor.Green);
Console.WriteLine("选择了：" + selectedKey);
Console.WriteLine();

Console.WriteLine("反色选择测试，请选择：");
selectedKey = QbSelect.ArrowSelect(
    AppDomain.CurrentDomain.GetAssemblies()
    .ToDictionary(t => t.GetName().Name, t => t.GetName().Name)
    .ToArray(),
    "-> ", "   ",
    ConsoleColor.Black, ConsoleColor.White,
    ConsoleColor.White, ConsoleColor.Black);
Console.WriteLine("选择了：" + selectedKey);
Console.WriteLine();

Console.ReadLine();