using Quick.Build;

//准备目录变量
var appFolder = QbFolder.GetAppFolder();
if (appFolder == Environment.CurrentDirectory)
    Environment.CurrentDirectory = Path.GetFullPath("../../../../");
var baseFolder = Environment.CurrentDirectory;

QbJson.WriteString("_build/test.json", "Name", "Quick.Build_" + Guid.NewGuid().ToString("N"));
Console.WriteLine("----------------------------------");
Console.WriteLine($"  欢迎使用{QbJson.ReadString("_build/test.json", "Name")}编译脚本");
Console.WriteLine("----------------------------------");
Console.WriteLine();
QbJson.WriteString("_build/test.json", "Name", "Quick.Build");
string selectedKey = string.Empty;

while (true)
{
    Console.WriteLine("------------------");
    Console.WriteLine("请选择：");
    Console.WriteLine("------------------");
    var dict = new Dictionary<string, string>()
    {
        ["1"] = "输入选择测试",
        ["2"] = "多项选择测试",
        ["3"] = "高亮选择测试",
        ["4"] = "反色选择测试",
        ["0"] = "退出"
    };
    selectedKey = QbSelect.ArrowSelect(dict.ToArray(), selectedForegroundColor: ConsoleColor.Green);

    if (selectedKey == "0")
        break;
    Console.WriteLine();
    Console.WriteLine("------------------");
    Console.WriteLine(dict[selectedKey]);
    Console.WriteLine("------------------");

    switch (selectedKey)
    {
        case "1":
            
            selectedKey = QbSelect.InputSelect(
                AppDomain.CurrentDomain.GetAssemblies()
                .ToDictionary(t => t.GetName().Name, t => t.GetName().Name)
                .ToArray());
            Console.WriteLine("选择了：" + selectedKey);
            break;
        case "2":
            var selectedKeys = QbSelect.MultiSelect(
                AppDomain.CurrentDomain.GetAssemblies()
                .ToDictionary(t => t.GetName().Name, t => t.GetName().Name)
                .ToArray());
            Console.WriteLine("选择了：" + string.Join(",", selectedKeys));
            break;
        case "3":
            selectedKey = QbSelect.ArrowSelect(
                AppDomain.CurrentDomain.GetAssemblies()
                .ToDictionary(t => t.GetName().Name, t => t.GetName().Name)
                .ToArray(), selectedForegroundColor: ConsoleColor.Green);
            Console.WriteLine("选择了：" + selectedKey);
            break;
        case "4":
            selectedKey = QbSelect.ArrowSelect(
                AppDomain.CurrentDomain.GetAssemblies()
                .ToDictionary(t => t.GetName().Name, t => t.GetName().Name)
                .ToArray(),
                "-> ", "   ",
                ConsoleColor.Black, ConsoleColor.White,
                ConsoleColor.White, ConsoleColor.Black);
            Console.WriteLine("选择了：" + selectedKey);
            break;
    }
}