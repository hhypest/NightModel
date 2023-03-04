using NightModel.Commands.Base;
using System;

namespace NightModel.Commands;

public sealed class RelayCommand<T> : Command<T>
{
    private readonly Action<T?> _execute;
    private readonly Predicate<T?>? _canExecute;

    public RelayCommand(Action<T?> execute, Predicate<T?>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    protected override bool CanExecute(T? parameter)
        => _canExecute?.Invoke(parameter) ?? true;

    protected override void Execute(T? parameter)
        => _execute(parameter);
}