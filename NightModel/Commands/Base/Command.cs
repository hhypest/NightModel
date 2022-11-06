using System;
using System.Windows.Input;

namespace NightModel.Commands.Base;

public abstract class Command : ICommand
{
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public abstract bool CanExecute();

    public abstract void Execute();

    bool ICommand.CanExecute(object? parameter)
        => CanExecute();

    void ICommand.Execute(object? parameter)
        => Execute();
}