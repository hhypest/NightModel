using System;
using System.Windows.Input;

namespace NightModel.Commands.Base;

public abstract class Command<T> : ICommand
{
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    protected abstract bool CanExecute(T? parameter);

    protected abstract void Execute(T? parameter);

    bool ICommand.CanExecute(object? parameter)
        => CanExecute((T?)parameter);

    void ICommand.Execute(object? parameter)
        => Execute((T?)parameter);
}