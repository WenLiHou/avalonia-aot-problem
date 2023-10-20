using ReactiveUI;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System;

namespace AvaloniaApplication1.ViewModels;

public class ViewModelBase : ReactiveObject
{
}
public sealed class RelayCommand<T> : ICommand
{
    //
    // 摘要:
    //     The System.Action to invoke when CommunityToolkit.Mvvm.Input.RelayCommand`1.Execute(`0)
    //     is used.
    private readonly Action<T?> execute;

    //
    // 摘要:
    //     The optional action to invoke when CommunityToolkit.Mvvm.Input.RelayCommand`1.CanExecute(`0)
    //     is used.
    private readonly Predicate<T?>? canExecute;

    public event EventHandler? CanExecuteChanged;

    //
    // 摘要:
    //     Initializes a new instance of the CommunityToolkit.Mvvm.Input.RelayCommand`1
    //     class that can always execute.
    //
    // 参数:
    //   execute:
    //     The execution logic.
    //
    // 异常:
    //   T:System.ArgumentNullException:
    //     Thrown if execute is null.
    //
    // 言论：
    //     Due to the fact that the System.Windows.Input.ICommand interface exposes methods
    //     that accept a nullable System.Object parameter, it is recommended that if T is
    //     a reference type, you should always declare it as nullable, and to always perform
    //     checks within execute.
    public RelayCommand(Action<T?> execute)
    {
        ArgumentNullException.ThrowIfNull(execute, "execute");
        this.execute = execute;
    }

    //
    // 摘要:
    //     Initializes a new instance of the CommunityToolkit.Mvvm.Input.RelayCommand`1
    //     class.
    //
    // 参数:
    //   execute:
    //     The execution logic.
    //
    //   canExecute:
    //     The execution status logic.
    //
    // 异常:
    //   T:System.ArgumentNullException:
    //     Thrown if execute or canExecute are null.
    //
    // 言论：
    //     See notes in CommunityToolkit.Mvvm.Input.RelayCommand`1.#ctor(System.Action{`0}).
    public RelayCommand(Action<T?> execute, Predicate<T?> canExecute)
    {
        ArgumentNullException.ThrowIfNull(execute, "execute");
        ArgumentNullException.ThrowIfNull(canExecute, "canExecute");
        this.execute = execute;
        this.canExecute = canExecute;
    }

    public void NotifyCanExecuteChanged()
    {
        this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool CanExecute(T? parameter)
    {
        return canExecute?.Invoke(parameter) ?? true;
    }

    public bool CanExecute(object? parameter)
    {
        if (parameter == null && default(T) != null)
        {
            return false;
        }

        if (!TryGetCommandArgument(parameter, out var result))
        {
            ThrowArgumentExceptionForInvalidCommandArgument(parameter);
        }

        return CanExecute(result);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Execute(T? parameter)
    {
        execute(parameter);
    }

    public void Execute(object? parameter)
    {
        if (!TryGetCommandArgument(parameter, out var result))
        {
            ThrowArgumentExceptionForInvalidCommandArgument(parameter);
        }

        Execute(result);
    }

    //
    // 摘要:
    //     Tries to get a command argument of compatible type T from an input System.Object.
    //
    // 参数:
    //   parameter:
    //     The input parameter.
    //
    //   result:
    //     The resulting T value, if any.
    //
    // 返回结果:
    //     Whether or not a compatible command argument could be retrieved.
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool TryGetCommandArgument(object? parameter, out T? result)
    {
        if (parameter == null && default(T) == null)
        {
            result = default(T);
            return true;
        }

        if (parameter is T)
        {
            T val = (result = (T)parameter);
            return true;
        }

        result = default(T);
        return false;
    }

    //
    // 摘要:
    //     Throws an System.ArgumentException if an invalid command argument is used.
    //
    // 参数:
    //   parameter:
    //     The input parameter.
    //
    // 异常:
    //   T:System.ArgumentException:
    //     Thrown with an error message to give info on the invalid parameter.
    [DoesNotReturn]
    internal static void ThrowArgumentExceptionForInvalidCommandArgument(object? parameter)
    {
        throw GetException(parameter);
        [MethodImpl(MethodImplOptions.NoInlining)]
        static Exception GetException(object? parameter)
        {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
            if (parameter == null)
            {
                defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(90, 2);
                defaultInterpolatedStringHandler.AppendLiteral("Parameter \"");
                defaultInterpolatedStringHandler.AppendFormatted("parameter");
                defaultInterpolatedStringHandler.AppendLiteral("\" (object) must not be null, as the command type requires an argument of type ");
                defaultInterpolatedStringHandler.AppendFormatted(typeof(T));
                defaultInterpolatedStringHandler.AppendLiteral(".");
                return new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear(), "parameter");
            }

            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(92, 3);
            defaultInterpolatedStringHandler.AppendLiteral("Parameter \"");
            defaultInterpolatedStringHandler.AppendFormatted("parameter");
            defaultInterpolatedStringHandler.AppendLiteral("\" (object) cannot be of type ");
            defaultInterpolatedStringHandler.AppendFormatted(parameter!.GetType());
            defaultInterpolatedStringHandler.AppendLiteral(", as the command type requires an argument of type ");
            defaultInterpolatedStringHandler.AppendFormatted(typeof(T));
            defaultInterpolatedStringHandler.AppendLiteral(".");
            return new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear(), "parameter");
        }
    }
}
public sealed class RelayCommand : ICommand
{
    //
    // 摘要:
    //     The System.Action to invoke when CommunityToolkit.Mvvm.Input.RelayCommand.Execute(System.Object)
    //     is used.
    private readonly System.Action execute;

    //
    // 摘要:
    //     The optional action to invoke when CommunityToolkit.Mvvm.Input.RelayCommand.CanExecute(System.Object)
    //     is used.
    private readonly Func<bool>? canExecute;

    public event EventHandler? CanExecuteChanged;

    //
    // 摘要:
    //     Initializes a new instance of the CommunityToolkit.Mvvm.Input.RelayCommand class
    //     that can always execute.
    //
    // 参数:
    //   execute:
    //     The execution logic.
    //
    // 异常:
    //   T:System.ArgumentNullException:
    //     Thrown if execute is null.
    public RelayCommand(System.Action execute)
    {
        ArgumentNullException.ThrowIfNull(execute, "execute");
        this.execute = execute;
    }

    //
    // 摘要:
    //     Initializes a new instance of the CommunityToolkit.Mvvm.Input.RelayCommand class.
    //
    // 参数:
    //   execute:
    //     The execution logic.
    //
    //   canExecute:
    //     The execution status logic.
    //
    // 异常:
    //   T:System.ArgumentNullException:
    //     Thrown if execute or canExecute are null.
    public RelayCommand(System.Action execute, Func<bool> canExecute)
    {
        ArgumentNullException.ThrowIfNull(execute, "execute");
        ArgumentNullException.ThrowIfNull(canExecute, "canExecute");
        this.execute = execute;
        this.canExecute = canExecute;
    }

    public void NotifyCanExecuteChanged()
    {
        this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool CanExecute(object? parameter)
    {
        return canExecute?.Invoke() ?? true;
    }

    public void Execute(object? parameter)
    {
        execute();
    }
}
