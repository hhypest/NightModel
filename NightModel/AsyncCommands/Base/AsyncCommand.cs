using NightModel.Extensions.TaskExtension;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NightModel.AsyncCommands.Base;

public abstract class AsyncCommand : ICommand
{
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    protected virtual void OnCanExecuteChanged()
        => CommandManager.InvalidateRequerySuggested();

    protected abstract bool CanExecute();

    protected abstract Task Execute();

    bool ICommand.CanExecute(object? parameter)
        => CanExecute();

    void ICommand.Execute(object? parameter)
        => Task.Run(() => Execute().RunVoid());
}