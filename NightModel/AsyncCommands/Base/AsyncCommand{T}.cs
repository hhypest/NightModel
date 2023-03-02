using NightModel.Extensions.TaskExtension;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NightModel.AsyncCommands.Base;

public abstract class AsyncCommand<T> : ICommand
{
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    protected virtual void OnCanExecuteChanged()
        => CommandManager.InvalidateRequerySuggested();

    protected abstract bool CanExecute(T? parameter);

    protected abstract Task Execute(T? parameter);

    bool ICommand.CanExecute(object? parameter)
        => CanExecute((T?)parameter);

    void ICommand.Execute(object? parameter)
        => Task.Run(() => Execute((T?)parameter).RunVoid());
}