using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NightModel.ViewModel.Base;

public abstract class BaseViewModel : INotifyPropertyChanged, INotifyPropertyChanging, INotifyDataErrorInfo, IDisposable
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public event PropertyChangingEventHandler? PropertyChanging;

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public bool HasErrors => ErrorList.Count > 0;

    public IEnumerable GetErrors(string? propertyName)
    {
        if (propertyName is null)
            return new List<string>();

        if (!ErrorList.ContainsKey(propertyName))
            return new List<string>();

        return ErrorList[propertyName];
    }

    protected virtual Dictionary<string, List<string>> ErrorList { get; } = new();

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected virtual void OnPropertyChanging([CallerMemberName] string? propertyName = null)
        => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    protected virtual void OnErrorsChanged([CallerMemberName] string? propertyName = null)
        => ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

    protected virtual void AddErrorToList(string propertyName, string errorMessage)
    {
        if (!ErrorList.ContainsKey(propertyName))
            ErrorList.Add(propertyName, new List<string>());

        ErrorList[propertyName].Add(errorMessage);
        OnPropertyChanging(propertyName);
        OnErrorsChanged(propertyName);
        OnPropertyChanged(propertyName);
    }

    protected virtual void ClearErrorList(string propertyName)
    {
        if (ErrorList.Remove(propertyName))
            OnErrorsChanged(propertyName);
    }

    public virtual void Dispose()
    {
    }

    protected abstract bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null);

    protected abstract bool Set<T>(ref T field, T value, IEqualityComparer<T> comparer, [CallerMemberName] string? propertyName = null);

    protected abstract bool Set<T>(ref T field, T value, IEqualityComparer<T> comparer, Action<T> action, [CallerMemberName] string? propertyName = null);

    protected abstract bool Set<T>(ref T field, T value, Predicate<T> errorCheck, string errorMessage, string propertyName);

    protected abstract bool Set<T>(ref T field, T value, IEqualityComparer<T> comparer, Predicate<T> errorCheck, string errorMessage, string propertyName);

    protected abstract bool Set<T>(ref T field, T value, IEqualityComparer comparer, Action<T> action, Predicate<T> errorCheck, string errorMessage, string propertyName);
}