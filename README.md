# Quick.Build

[![NuGet Version](http://img.shields.io/nuget/v/Quick.Build.svg?style=flat)](https://www.nuget.org/packages/Quick.Build/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Quick.Build.svg)](https://www.nuget.org/packages/Quick.Build/)
[![License](https://img.shields.io/github/license/aaasoft/Quick.Build.svg)](https://github.com/aaasoft/Quick.Build/blob/master/LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4.svg)](https://dotnet.microsoft.com/)

一组面向**构建脚本**的 .NET 小工具。

写 `_build` 控制台工程时，总要重复实现「删目录 / 递归复制 / 改一行版本号 / 下载依赖 / 控制台里选个目标框架 / 清理 publish 里的 runtimes」这些琐事。这个库把它们收敛成 8 个静态类，直接用，不用再写一遍。

## 特性一览

| 类 | 命名空间 | 用途 |
| --- | --- | --- |
| `QbFile` | Quick.Build | 文件头改写、安全复制/删除、按行读写文本 |
| `QbFolder` | Quick.Build | 当前程序目录、递归复制/删除、按通配符搜索 |
| `QbJson` | Quick.Build | 直接读写 JSON 文件里的单个属性，无需定义模型 |
| `QbNet` | Quick.Build | 浏览器化 `HttpClient`、带进度回调的文件下载 |
| `QbConsole` | Quick.Build | 同行刷新输出（进度条效果） |
| `QbSelect` | Quick.Build | 控制台单选 / 多选 / 方向键选择，自动降级重定向场景 |
| `QbGui` | Quick.Build | 跨平台打开文件夹（资源管理器 / Finder / xdg-open） |
| `QbDotNet` | Quick.Build | 裁剪 `publish/runtimes` 下的运行时目录 |

## 安装

```bash
dotnet add package Quick.Build
```

唯一的外部依赖是 [SimpleExec](https://github.com/adamralph/simple-exec)（仅 `QbGui` 使用）。

## 快速开始

典型用法是新建一个 `_build` 控制台工程引用本库，用顶层语句写构建流程：

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <ProjectReference Include="..\Quick.Build\Quick.Build.csproj" />
  </ItemGroup>
</Project>
```

`Program.cs` 里通常先做一次目录定位，让脚本无论在 `dotnet run` 还是双击 exe 时都能站在仓库根目录：

```csharp
using Quick.Build;

var appFolder = QbFolder.GetAppFolder();
if (appFolder == Environment.CurrentDirectory)
    Environment.CurrentDirectory = Path.GetFullPath("../../../../");
var baseFolder = Environment.CurrentDirectory;

// 清理上一轮的产物
QbFolder.Delete(Path.Combine(baseFolder, "publish"));

// 版本号自增：直接改 JSON 里的一个属性
var versionFile = Path.Combine(baseFolder, "_build/version.json");
var patch = QbJson.ReadInt32(versionFile, "Patch");
QbJson.WriteInt32(versionFile, "Patch", patch + 1);

// 让用户挑一个目标 RID
var rid = QbSelect.ArrowSelect(new Dictionary<string, string>
{
    ["win-x64"] = "Windows x64",
    ["linux-x64"] = "Linux x64",
    ["0"] = "退出"
}.ToArray(), selectedForegroundColor: ConsoleColor.Green);

// 下载依赖，进度同行刷新
await QbNet.DownloadFile(url, targetFile, CancellationToken.None, p =>
    QbConsole.DisplaySameLineInConsole(
        $"{p.Current * 100d / p.Total:F1}%  {p.Speed / 1024d / 1024d:F1} MB/s  剩余 {p.RemainingTime:hh\\:mm\\:ss}"));

// 只保留需要的运行时目录，减小发布体积
QbDotNet.KeepPublishRuntimes(Path.Combine(baseFolder, "publish"), rid);

// 打开输出目录
QbGui.OpenFolder(Path.Combine(baseFolder, "publish"));
```

## API 参考

### QbFile —— 文件

| 方法 | 说明 |
| --- | --- |
| `ChangeHeader(file, fileHeader)` | 用 ASCII 覆盖写文件头 N 个字节（自解压头、PE 标记等场景） |
| `Copy(srcFile, destFile, overwrite)` | 自动创建目标目录；**源文件不存在时静默返回** |
| `Delete(file)` | 不存在则忽略，不抛异常 |
| `DeleteFiles(folder, pattern, searchOption)` | 按通配符批量删除 |
| `CopyFiles(srcFolder, destFolder, pattern, overwrite)` | 按通配符批量复制（`CopyTo`，目标目录需已存在） |
| `ReadLine(path, lineIndex[, encoding])` | 读取指定行，默认 UTF-8 |
| `WriteLine(path, lineIndex, content[, encoding])` | 替换指定行（整文件读入再写回，编码需显式传入以保留） |

### QbFolder —— 目录

| 方法 | 说明 |
| --- | --- |
| `GetAppFolder()` | 入口程序集所在目录（基于 `Assembly.GetEntryAssembly()`） |
| `Delete(folder)` | 递归删除，不存在则忽略 |
| `Copy(src, dest, overwrite)` | 递归复制，自动创建目标目录 |
| `SearchFiles(folder, pattern, searchOption)` | 返回 `FileInfo[]`，目录不存在时返回空数组 |
| `SearchFolders(folder, pattern, searchOption)` | 返回 `DirectoryInfo[]`，同上 |
| `DeleteFolders(folder, pattern, searchOption)` | 按通配符批量删除目录 |

### QbJson —— JSON 单属性读写

基于 `System.Text.Json.Nodes`，不定义模型也能改配置里的一个字段。

```csharp
var name = QbJson.ReadString("appsettings.json", "Name");
var port = QbJson.ReadInt32("appsettings.json", "Port");
var ok   = QbJson.ReadBoolean("appsettings.json", "Enabled");

QbJson.WriteString("appsettings.json", "Name", "Quick.Build");
QbJson.Write("appsettings.json", "Custom", new[] { 1, 2, 3 });
```

读取侧：`Read<T>` / `ReadString` / `ReadInt32` / `ReadInt64` / `ReadDouble` / `ReadBoolean`。
写入侧：`Write<T>` / `WriteString` / `WriteInt32` / `WriteInt64` / `WriteDouble` / `WriteBoolean`。
写入可选传 `JsonSerializerOptions` 控制格式。

### QbNet —— 下载

| 成员 | 说明 |
| --- | --- |
| `GetHttpClient()` | 预置浏览器 UA / Accept / Accept-Encoding 的 `HttpClient` |
| `DownloadFile(url, file, ct, onProgress)` | 32KB 缓冲流式下载，约 0.5s 回调一次进度 |
| `DownloadFile(client, url, file, ct, onProgress)` | 复用现有 `HttpClient` |
| `TransferProgress` | `Current` / `Total` / `Speed`(B/s) / `RemainingTime` |

### QbConsole —— 控制台输出

`DisplaySameLineInConsole(line)`：光标归零覆写当前行，做出进度条效果；当输出被重定向时自动降级为 `WriteLine`，不会在 CI 日志里刷满回车。

### QbSelect —— 交互式选择

`items` 统一为 `KeyValuePair<string, string>[]`，返回被选中项的 **Key**。

| 方法 | 交互方式 | 返回 |
| --- | --- | --- |
| `InputSelect(items, inputText, template)` | 输入序号 | 单个 Key |
| `ArrowSelect(items, selectedPrefix, ...colors)` | ↑/↓ + Enter | 单个 Key |
| `MultiSelect(items, ...prefixes, ...colors)` | ↑/↓ 移动、空格切换、Enter 确认 | Key 数组 |

三者都检测 `Console.IsOutputRedirected`：被重定向时改为打印列表并读取一行输入（`MultiSelect` 支持 `1 3,5` 这类写法），方便在 CI 里跑。条目数超过窗口高度时会自动滚动窗口。前缀、前景/背景色均可定制，用于高亮或反色主题。

### QbGui —— 打开目录

`OpenFolder(folder)` 按平台分派：Windows 走 `explorer`（忽略其非零退出码），macOS 走 `open`，其他走 `xdg-open`。

### QbDotNet —— 发布体积裁剪

```csharp
QbDotNet.KeepPublishRuntimes(publishFolder, "win-x64", "linux-x64"); // 白名单，删掉其他
QbDotNet.RemovePublishRuntimes(publishFolder, "win-x86");            // 黑名单，只删指定
```

只处理 `publish/runtimes/<rid>` 这一层子目录，`runtimes` 不存在时直接返回。

## 注意事项

- **`QbNet.GetHttpClient()` 不校验 TLS 证书**（`ServerCertificateCustomValidationCallback` 恒返回 `true`）。它面向内网/构建机下载场景，不要用它承载生产流量。
- `QbJson` 只支持**顶层对象属性**，且目标文件必须已存在（内部是 `File.ReadAllText`）。嵌套属性请自行 `JsonNode` 处理。
- `QbFile.WriteLine` 会整文件读入后重写，行尾风格随 `File.WriteAllLines` 归一；需要保留原始换行符时请谨慎使用。
- `QbFolder.GetAppFolder()` 依赖 `Assembly.GetEntryAssembly()`，在单元测试宿主或自托管场景下可能为 `null`。
- 编号基准不一致：`InputSelect` 从 **1** 开始显示，`MultiSelect` / `ArrowSelect` 从 **0** 开始。

## 项目结构

```
Quick.Build.slnx
├── Quick.Build/          # 类库本体（net8.0），发布到 NuGet
│   ├── QbFile.cs  QbFolder.cs  QbJson.cs  QbNet.cs
│   ├── QbConsole.cs  QbSelect.cs  QbGui.cs  QbDotNet.cs
│   └── Quick.Build.csproj
└── _build/               # 示例/自测工程（net10.0），以控制台菜单跑通各模块
    ├── Program.cs
    └── _build.csproj
```

`_build` 同时充当 smoke test：菜单里的「输入选择 / 多项选择 / 高亮选择 / 反色选择」四项覆盖了 `QbSelect` 的三条路径与颜色参数，顺带验证 `QbFolder.GetAppFolder()` 的目录定位与 `QbJson` 的键盘输入落盘。

## License

[MIT](https://github.com/aaasoft/Quick.Build/blob/master/LICENSE) © 2021 张鹏
