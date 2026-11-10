namespace Quick.Build;

/// <summary>
/// Quick.Build命令辅助类
/// </summary>
public static class QbCommand
{
    public static void Run(
        string name,
        string args = "",
        string workingDirectory = "",
        Action<IDictionary<string, string>> configureEnvironment = null,
        IEnumerable<string> secrets = null,
        Func<int, bool> handleExitCode = null,
        string echoPrefix = null,
        bool noEcho = false,
        bool cancellationIgnoresProcessTree = false,
        bool createNoWindow = false,
        CancellationToken ct = default)
    {
        SimpleExec.Command.Run(
            name,
            args,
            workingDirectory,
            configureEnvironment,
            secrets,
            handleExitCode,
            echoPrefix,
            noEcho,
            cancellationIgnoresProcessTree,
            createNoWindow,
            ct);
    }
    public static void Run(
        string name,
        IEnumerable<string> args,
        string workingDirectory = "",        
        Action<IDictionary<string, string>> configureEnvironment = null,
        IEnumerable<string> secrets = null,
        Func<int, bool> handleExitCode = null,
        string echoPrefix = null,
        bool noEcho = false,
        bool cancellationIgnoresProcessTree = false,
        bool createNoWindow = false,
        CancellationToken ct = default)
    {
        SimpleExec.Command.Run(
            name,
            args,
            workingDirectory,
            configureEnvironment,
            secrets,
            handleExitCode,
            echoPrefix,
            noEcho,
            cancellationIgnoresProcessTree,
            createNoWindow,
            ct);
    }
}
