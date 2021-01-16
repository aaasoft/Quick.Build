using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quick.Build
{
    /// <summary>
    /// Quick.Build命令辅助类
    /// </summary>
    public static class QbCommand
    {
        //
        // 摘要:
        //     Runs a command and reads standard output (stdout). By default, the command line
        //     is echoed to standard error (stderr).
        //
        // 参数:
        //   name:
        //     The name of the command. This can be a path to an executable file.
        //
        //   args:
        //     The arguments to pass to the command.
        //
        //   workingDirectory:
        //     The working directory in which to run the command.
        //
        //   noEcho:
        //     Whether or not to echo the resulting command line and working directory (if specified)
        //     to standard error (stderr).
        //
        //   windowsName:
        //     The name of the command to use on Windows only.
        //
        //   windowsArgs:
        //     The arguments to pass to the command on Windows only.
        //
        //   echoPrefix:
        //     The prefix to use when echoing the command line and working directory (if specified)
        //     to standard error (stderr).
        //
        //   configureEnvironment:
        //     An action which configures environment variables for the command.
        //
        //   createNoWindow:
        //     Whether to run the command in a new window.
        //
        // 返回结果:
        //     A System.String representing the contents of standard output (stdout).
        //
        // 异常:
        //   T:SimpleExec.NonZeroExitCodeException:
        //     The command exited with non-zero exit code.
        //
        // 言论：
        //     By default, the resulting command line and the working directory (if specified)
        //     are echoed to standard error (stderr). To suppress this behavior, provide the
        //     noEcho parameter with a value of true. This method uses System.Threading.Tasks.Task.WaitAll(System.Threading.Tasks.Task[]).
        //     This should be fine in most contexts, such as console apps, but in some contexts,
        //     such as a UI or ASP.NET, it may deadlock. In those contexts, SimpleExec.Command.ReadAsync(System.String,System.String,System.String,System.Boolean,System.String,System.String,System.String,System.Action{System.Collections.Generic.IDictionary{System.String,System.String}},System.Boolean,System.Threading.CancellationToken)
        //     should be used instead.
        public static string Read(
            string name,
            string args = null,
            string workingDirectory = null,
            bool noEcho = false,
            string windowsName = null,
            string windowsArgs = null,
            string echoPrefix = null,
            Action<IDictionary<string, string>> configureEnvironment = null,
            bool createNoWindow = false)
        {
            return SimpleExec.Command.Read(name, args, workingDirectory, noEcho, windowsName, windowsArgs, echoPrefix, configureEnvironment, createNoWindow);
        }

        //
        // 摘要:
        //     Runs a command and reads standard output (stdout). By default, the command line
        //     is echoed to standard error (stderr).
        //
        // 参数:
        //   name:
        //     The name of the command. This can be a path to an executable file.
        //
        //   args:
        //     The arguments to pass to the command.
        //
        //   workingDirectory:
        //     The working directory in which to run the command.
        //
        //   noEcho:
        //     Whether or not to echo the resulting command line and working directory (if specified)
        //     to standard error (stderr).
        //
        //   windowsName:
        //     The name of the command to use on Windows only.
        //
        //   windowsArgs:
        //     The arguments to pass to the command on Windows only.
        //
        //   echoPrefix:
        //     The prefix to use when echoing the command line and working directory (if specified)
        //     to standard error (stderr).
        //
        //   configureEnvironment:
        //     An action which configures environment variables for the command.
        //
        //   createNoWindow:
        //     Whether to run the command in a new window.
        //
        //   cancellationToken:
        //     A System.Threading.CancellationToken to observe while waiting for the command
        //     to exit.
        //
        // 返回结果:
        //     A System.Threading.Tasks.Task`1 representing the asynchronous running of the
        //     command and reading of standard output (stdout). The task result is a System.String
        //     representing the contents of standard output (stdout).
        //
        // 异常:
        //   T:SimpleExec.NonZeroExitCodeException:
        //     The command exited with non-zero exit code.
        //
        // 言论：
        //     By default, the resulting command line and the working directory (if specified)
        //     are echoed to standard error (stderr). To suppress this behavior, provide the
        //     noEcho parameter with a value of true.
        public static Task<string> ReadAsync(
            string name,
            string args = null,
            string workingDirectory = null,
            bool noEcho = false,
            string windowsName = null,
            string windowsArgs = null,
            string echoPrefix = null,
            Action<IDictionary<string, string>> configureEnvironment = null,
            bool createNoWindow = false,
            CancellationToken cancellationToken = default)
        {
            return SimpleExec.Command.ReadAsync(name, args, workingDirectory, noEcho, windowsName, windowsArgs, echoPrefix, configureEnvironment, createNoWindow, cancellationToken);
        }
        //
        // 摘要:
        //     Runs a command. By default, the command line is echoed to standard error (stderr).
        //
        // 参数:
        //   name:
        //     The name of the command. This can be a path to an executable file.
        //
        //   args:
        //     The arguments to pass to the command.
        //
        //   workingDirectory:
        //     The working directory in which to run the command.
        //
        //   noEcho:
        //     Whether or not to echo the resulting command line and working directory (if specified)
        //     to standard error (stderr).
        //
        //   windowsName:
        //     The name of the command to use on Windows only.
        //
        //   windowsArgs:
        //     The arguments to pass to the command on Windows only.
        //
        //   echoPrefix:
        //     The prefix to use when echoing the command line and working directory (if specified)
        //     to standard error (stderr).
        //
        //   configureEnvironment:
        //     An action which configures environment variables for the command.
        //
        //   createNoWindow:
        //     Whether to run the command in a new window.
        //
        // 异常:
        //   T:SimpleExec.NonZeroExitCodeException:
        //     The command exited with non-zero exit code.
        //
        // 言论：
        //     By default, the resulting command line and the working directory (if specified)
        //     are echoed to standard error (stderr). To suppress this behavior, provide the
        //     noEcho parameter with a value of true.
        public static void Run(
            string name,
            string args = null,
            string workingDirectory = null,
            bool noEcho = false,
            string windowsName = null,
            string windowsArgs = null,
            string echoPrefix = null,
            Action<IDictionary<string, string>> configureEnvironment = null,
            bool createNoWindow = false)
        {
            SimpleExec.Command.Run(name, args, workingDirectory, noEcho, windowsName, windowsArgs, echoPrefix, configureEnvironment, createNoWindow);
        }
        //
        // 摘要:
        //     Runs a command asynchronously. By default, the command line is echoed to standard
        //     error (stderr).
        //
        // 参数:
        //   name:
        //     The name of the command. This can be a path to an executable file.
        //
        //   args:
        //     The arguments to pass to the command.
        //
        //   workingDirectory:
        //     The working directory in which to run the command.
        //
        //   noEcho:
        //     Whether or not to echo the resulting command line and working directory (if specified)
        //     to standard error (stderr).
        //
        //   windowsName:
        //     The name of the command to use on Windows only.
        //
        //   windowsArgs:
        //     The arguments to pass to the command on Windows only.
        //
        //   echoPrefix:
        //     The prefix to use when echoing the command line and working directory (if specified)
        //     to standard error (stderr).
        //
        //   configureEnvironment:
        //     An action which configures environment variables for the command.
        //
        //   createNoWindow:
        //     Whether to run the command in a new window.
        //
        //   cancellationToken:
        //     A System.Threading.CancellationToken to observe while waiting for the command
        //     to exit.
        //
        // 返回结果:
        //     A System.Threading.Tasks.Task that represents the asynchronous running of the
        //     command.
        //
        // 异常:
        //   T:SimpleExec.NonZeroExitCodeException:
        //     The command exited with non-zero exit code.
        //
        // 言论：
        //     By default, the resulting command line and the working directory (if specified)
        //     are echoed to standard error (stderr). To suppress this behavior, provide the
        //     noEcho parameter with a value of true.
        public static Task RunAsync(
            string name,
            string args = null,
            string workingDirectory = null,
            bool noEcho = false,
            string windowsName = null,
            string windowsArgs = null,
            string echoPrefix = null,
            Action<IDictionary<string, string>> configureEnvironment = null,
            bool createNoWindow = false,
            CancellationToken cancellationToken = default)
        {
            return SimpleExec.Command.RunAsync(name, args, workingDirectory, noEcho, windowsName, windowsArgs, echoPrefix, configureEnvironment, createNoWindow, cancellationToken);
        }
    }
}
