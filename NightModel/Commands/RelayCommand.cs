using NightModel.Commands.Base;
using System;

namespace NightModel.Commands;

public sealed class RelayCommand : Command
{
    private readonly Action _execute;

    private readonly Func<bool>? _canExecute;

    public RelayCommand(Action execute, Func<bool>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public override bool CanExecute()
        => _canExecute?.Invoke() ?? true;

    public override void Execute()
        => _execute();
}