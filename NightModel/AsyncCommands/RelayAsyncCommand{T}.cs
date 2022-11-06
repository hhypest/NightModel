using NightModel.AsyncCommands.Base;
using System;
using System.Threading.Tasks;

namespace NightModel.AsyncCommands;

public class RelayAsyncCommand<T> : AsyncCommand<T>
{
    private readonly Func<T?, Task> _execute;
    private readonly Predicate<T?>? _canExecute;
    private bool _isExecute;

    public RelayAsyncCommand(Func<T?, Task> execute, Predicate<T?>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    protected override bool CanExecute(T? parameter)
        => !_isExecute && (_canExecute?.Invoke(parameter) ?? true);

    protected override async Task Execute(T? parameter)
    {
        if (!CanExecute(parameter))
        {
            OnCanExecuteChanged();
            return;
        }

        try
        {
            _isExecute = true;
            OnCanExecuteChanged();
            await _execute(parameter);
        }
        finally
        {
            _isExecute = false;
        }

        OnCanExecuteChanged();
    }
}