using NightModel.AsyncCommands.Base;
using System;
using System.Threading.Tasks;

namespace NightModel.AsyncCommands;

public sealed class RelayAsyncCommand : AsyncCommand
{
    private readonly Func<Task> _execute;
    private readonly Func<bool>? _canExecute;
    private bool _isExecute;

    public RelayAsyncCommand(Func<Task> execute, Func<bool>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    protected override bool CanExecute()
        => !_isExecute && (_canExecute?.Invoke() ?? true);

    protected override async Task Execute()
    {
        if (!CanExecute())
        {
            OnCanExecuteChanged();
            return;
        }

        try
        {
            _isExecute = true;
            OnCanExecuteChanged();
            await _execute();
        }
        finally
        {
            _isExecute = false;
        }

        OnCanExecuteChanged();
    }
}