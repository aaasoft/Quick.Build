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
        //     Runs a command without redirecting standard output (stdout) and standard error
        //     (stderr) and without writing to standard input (stdin). By default, the command
        //     line is echoed to standard output (stdout).
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
        //     to standard output (stdout).
        //
        //   echoPrefix:
        //     The prefix to use when echoing the command line and working directory (if specified)
        //     to standard output (stdout).
        //
        //   configureEnvironment:
        //     An action which configures environment variables for the command.
        //
        //   createNoWindow:
        //     Whether to run the command in a new window.
        //
        //   handleExitCode:
        //     A delegate which accepts an System.Int32 representing exit code of the command
        //     and returns true when it has handled the exit code and default exit code handling
        //     should be suppressed, and returns false otherwise.
        //
        //   cancellationIgnoresProcessTree:
        //     Whether to ignore the process tree when cancelling the command. If set to true,
        //     when the command is cancelled, any child processes created by the command are
        //     left running after the command is cancelled.
        //
        //   cancellationToken:
        //     A System.Threading.CancellationToken to observe while waiting for the command
        //     to exit.
        //
        // 异常:
        //   T:SimpleExec.ExitCodeException:
        //     The command exited with non-zero exit code.
        //
        // 言论：
        //     By default, the resulting command line and the working directory (if specified)
        //     are echoed to standard output (stdout). To suppress this behavior, provide the
        //     noEcho parameter with a value of true.
        public static void Run(string name, string args = "", string workingDirectory = "", bool noEcho = false, string echoPrefix = null, Action<IDictionary<string, string>> configureEnvironment = null, bool createNoWindow = false, Func<int, bool> handleExitCode = null, bool cancellationIgnoresProcessTree = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            SimpleExec.Command.Run(name, args, workingDirectory, noEcho, echoPrefix, configureEnvironment, createNoWindow, handleExitCode, cancellationIgnoresProcessTree, cancellationToken);
        }

        //
        // 摘要:
        //     Runs a command without redirecting standard output (stdout) and standard error
        //     (stderr) and without writing to standard input (stdin). By default, the command
        //     line is echoed to standard output (stdout).
        //
        // 参数:
        //   name:
        //     The name of the command. This can be a path to an executable file.
        //
        //   args:
        //     The arguments to pass to the command. As with System.Diagnostics.ProcessStartInfo.ArgumentList,
        //     the strings don't need to be escaped.
        //
        //   workingDirectory:
        //     The working directory in which to run the command.
        //
        //   noEcho:
        //     Whether or not to echo the resulting command name, arguments, and working directory
        //     (if specified) to standard output (stdout).
        //
        //   echoPrefix:
        //     The prefix to use when echoing the command name, arguments, and working directory
        //     (if specified) to standard output (stdout).
        //
        //   configureEnvironment:
        //     An action which configures environment variables for the command.
        //
        //   createNoWindow:
        //     Whether to run the command in a new window.
        //
        //   handleExitCode:
        //     A delegate which accepts an System.Int32 representing exit code of the command
        //     and returns true when it has handled the exit code and default exit code handling
        //     should be suppressed, and returns false otherwise.
        //
        //   cancellationIgnoresProcessTree:
        //     Whether to ignore the process tree when cancelling the command. If set to true,
        //     when the command is cancelled, any child processes created by the command are
        //     left running after the command is cancelled.
        //
        //   cancellationToken:
        //     A System.Threading.CancellationToken to observe while waiting for the command
        //     to exit.
        //
        // 异常:
        //   T:SimpleExec.ExitCodeException:
        //     The command exited with non-zero exit code.
        public static void Run(string name, IEnumerable<string> args, string workingDirectory = "", bool noEcho = false, string echoPrefix = null, Action<IDictionary<string, string>> configureEnvironment = null, bool createNoWindow = false, Func<int, bool> handleExitCode = null, bool cancellationIgnoresProcessTree = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            SimpleExec.Command.Run(name, args, workingDirectory, noEcho, echoPrefix, configureEnvironment, createNoWindow, handleExitCode, cancellationIgnoresProcessTree, cancellationToken);
        }


        //
        // 摘要:
        //     Runs a command asynchronously without redirecting standard output (stdout) and
        //     standard error (stderr) and without writing to standard input (stdin). By default,
        //     the command line is echoed to standard output (stdout).
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
        //     to standard output (stdout).
        //
        //   echoPrefix:
        //     The prefix to use when echoing the command line and working directory (if specified)
        //     to standard output (stdout).
        //
        //   configureEnvironment:
        //     An action which configures environment variables for the command.
        //
        //   createNoWindow:
        //     Whether to run the command in a new window.
        //
        //   handleExitCode:
        //     A delegate which accepts an System.Int32 representing exit code of the command
        //     and returns true when it has handled the exit code and default exit code handling
        //     should be suppressed, and returns false otherwise.
        //
        //   cancellationIgnoresProcessTree:
        //     Whether to ignore the process tree when cancelling the command. If set to true,
        //     when the command is cancelled, any child processes created by the command are
        //     left running after the command is cancelled.
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
        //   T:SimpleExec.ExitCodeReadException:
        //     The command exited with non-zero exit code.
        //
        // 言论：
        //     By default, the resulting command line and the working directory (if specified)
        //     are echoed to standard output (stdout). To suppress this behavior, provide the
        //     noEcho parameter with a value of true.
        public static Task RunAsync(string name, string args = "", string workingDirectory = "", bool noEcho = false, string echoPrefix = null, Action<IDictionary<string, string>> configureEnvironment = null, bool createNoWindow = false, Func<int, bool> handleExitCode = null, bool cancellationIgnoresProcessTree = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            return SimpleExec.Command.RunAsync(name, args, workingDirectory, noEcho, echoPrefix, configureEnvironment, createNoWindow, handleExitCode, cancellationIgnoresProcessTree, cancellationToken);
        }

        //
        // 摘要:
        //     Runs a command asynchronously without redirecting standard output (stdout) and
        //     standard error (stderr) and without writing to standard input (stdin). By default,
        //     the command line is echoed to standard output (stdout).
        //
        // 参数:
        //   name:
        //     The name of the command. This can be a path to an executable file.
        //
        //   args:
        //     The arguments to pass to the command. As with System.Diagnostics.ProcessStartInfo.ArgumentList,
        //     the strings don't need to be escaped.
        //
        //   workingDirectory:
        //     The working directory in which to run the command.
        //
        //   noEcho:
        //     Whether or not to echo the resulting command name, arguments, and working directory
        //     (if specified) to standard output (stdout).
        //
        //   echoPrefix:
        //     The prefix to use when echoing the command name, arguments, and working directory
        //     (if specified) to standard output (stdout).
        //
        //   configureEnvironment:
        //     An action which configures environment variables for the command.
        //
        //   createNoWindow:
        //     Whether to run the command in a new window.
        //
        //   handleExitCode:
        //     A delegate which accepts an System.Int32 representing exit code of the command
        //     and returns true when it has handled the exit code and default exit code handling
        //     should be suppressed, and returns false otherwise.
        //
        //   cancellationIgnoresProcessTree:
        //     Whether to ignore the process tree when cancelling the command. If set to true,
        //     when the command is cancelled, any child processes created by the command are
        //     left running after the command is cancelled.
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
        //   T:SimpleExec.ExitCodeReadException:
        //     The command exited with non-zero exit code.
        public static Task RunAsync(string name, IEnumerable<string> args, string workingDirectory = "", bool noEcho = false, string echoPrefix = null, Action<IDictionary<string, string>> configureEnvironment = null, bool createNoWindow = false, Func<int, bool> handleExitCode = null, bool cancellationIgnoresProcessTree = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            return SimpleExec.Command.RunAsync(name, args, workingDirectory, noEcho, echoPrefix, configureEnvironment, createNoWindow, handleExitCode, cancellationIgnoresProcessTree, cancellationToken);
        }


        //
        // 摘要:
        //     Runs a command and reads standard output (stdout) and standard error (stderr)
        //     and optionally writes to standard input (stdin).
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
        //   configureEnvironment:
        //     An action which configures environment variables for the command.
        //
        //   encoding:
        //     The preferred System.Text.Encoding for standard output (stdout) and standard
        //     output (stdout).
        //
        //   handleExitCode:
        //     A delegate which accepts an System.Int32 representing exit code of the command
        //     and returns true when it has handled the exit code and default exit code handling
        //     should be suppressed, and returns false otherwise.
        //
        //   standardInput:
        //     The contents of standard input (stdin).
        //
        //   cancellationIgnoresProcessTree:
        //     Whether to ignore the process tree when cancelling the command. If set to true,
        //     when the command is cancelled, any child processes created by the command are
        //     left running after the command is cancelled.
        //
        //   cancellationToken:
        //     A System.Threading.CancellationToken to observe while waiting for the command
        //     to exit.
        //
        // 返回结果:
        //     A System.Threading.Tasks.Task`1 representing the asynchronous running of the
        //     command and reading of standard output (stdout) and standard error (stderr).
        //     The task result is a System.ValueTuple`2 representing the contents of standard
        //     output (stdout) and standard error (stderr).
        //
        // 异常:
        //   T:SimpleExec.ExitCodeReadException:
        //     The command exited with non-zero exit code. The exception contains the contents
        //     of standard output (stdout) and standard error (stderr).
        public static Task<(string StandardOutput, string StandardError)> ReadAsync(string name, string args = "", string workingDirectory = "", Action<IDictionary<string, string>> configureEnvironment = null, Encoding encoding = null, Func<int, bool> handleExitCode = null, string standardInput = null, bool cancellationIgnoresProcessTree = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            return SimpleExec.Command.ReadAsync(name, args, workingDirectory, configureEnvironment, encoding, handleExitCode, standardInput, cancellationIgnoresProcessTree, cancellationToken);
        }

        //
        // 摘要:
        //     Runs a command and reads standard output (stdout) and standard error (stderr)
        //     and optionally writes to standard input (stdin).
        //
        // 参数:
        //   name:
        //     The name of the command. This can be a path to an executable file.
        //
        //   args:
        //     The arguments to pass to the command. As with System.Diagnostics.ProcessStartInfo.ArgumentList,
        //     the strings don't need to be escaped.
        //
        //   workingDirectory:
        //     The working directory in which to run the command.
        //
        //   configureEnvironment:
        //     An action which configures environment variables for the command.
        //
        //   encoding:
        //     The preferred System.Text.Encoding for standard output (stdout) and standard
        //     error (stderr).
        //
        //   handleExitCode:
        //     A delegate which accepts an System.Int32 representing exit code of the command
        //     and returns true when it has handled the exit code and default exit code handling
        //     should be suppressed, and returns false otherwise.
        //
        //   standardInput:
        //     The contents of standard input (stdin).
        //
        //   cancellationIgnoresProcessTree:
        //     Whether to ignore the process tree when cancelling the command. If set to true,
        //     when the command is cancelled, any child processes created by the command are
        //     left running after the command is cancelled.
        //
        //   cancellationToken:
        //     A System.Threading.CancellationToken to observe while waiting for the command
        //     to exit.
        //
        // 返回结果:
        //     A System.Threading.Tasks.Task`1 representing the asynchronous running of the
        //     command and reading of standard output (stdout) and standard error (stderr).
        //     The task result is a System.ValueTuple`2 representing the contents of standard
        //     output (stdout) and standard error (stderr).
        //
        // 异常:
        //   T:SimpleExec.ExitCodeReadException:
        //     The command exited with non-zero exit code. The exception contains the contents
        //     of standard output (stdout) and standard error (stderr).
        public static Task<(string StandardOutput, string StandardError)> ReadAsync(string name, IEnumerable<string> args, string workingDirectory = "", Action<IDictionary<string, string>> configureEnvironment = null, Encoding encoding = null, Func<int, bool> handleExitCode = null, string standardInput = null, bool cancellationIgnoresProcessTree = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            return SimpleExec.Command.ReadAsync(name, args, workingDirectory, configureEnvironment, encoding, handleExitCode, standardInput, cancellationIgnoresProcessTree, cancellationToken);
        }

    }
}
