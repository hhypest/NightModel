using NightModel.Commands.Base;
using System;

namespace NightModel.Commands;

public class RelayCommand : Command
{
    private readonly Action<object?> _execute;

    private readonly Predicate<object?>? _canExecute;

    public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public override bool CanExecute(object? parameter)
        => _canExecute?.Invoke(parameter) ?? true;

    public override void Execute(object? parameter)
        => _execute(parameter);
}