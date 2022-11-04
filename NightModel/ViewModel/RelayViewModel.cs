using NightModel.ViewModel.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NightModel.ViewModel;

public abstract class RelayViewModel : BaseViewModel
{
    protected override bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (Equals(field, value))
            return false;

        OnPropertyChanging(propertyName);
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected override bool Set<T>(ref T field, T value, IEqualityComparer<T> comparer, [CallerMemberName] string? propertyName = null)
    {
        if (comparer.Equals(field, value))
            return false;

        OnPropertyChanging(propertyName);
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected override bool Set<T>(ref T field, T value, IEqualityComparer<T> comparer, Action<T> action, [CallerMemberName] string? propertyName = null)
    {
        if (comparer.Equals(field, value))
            return false;

        action(value);
        OnPropertyChanging(propertyName);
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected override bool Set<T>(ref T field, T value, Predicate<T> errorCheck, string errorMessage, string propertyName)
    {
        if (Equals(field, value))
            return false;

        ClearErrorList(propertyName);
        if (errorCheck(value))
        {
            field = value;
            AddErrorToList(propertyName, errorMessage);
            return true;
        }

        OnPropertyChanging(propertyName);
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected override bool Set<T>(ref T field, T value, IEqualityComparer<T> comparer, Predicate<T> errorCheck, string errorMessage, string propertyName)
    {
        if (comparer.Equals(field, value))
            return false;

        ClearErrorList(propertyName);
        if (errorCheck(value))
        {
            field = value;
            AddErrorToList(propertyName, errorMessage);
            return true;
        }

        OnPropertyChanging(propertyName);
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected override bool Set<T>(ref T field, T value, IEqualityComparer comparer, Action<T> action, Predicate<T> errorCheck, string errorMessage, string propertyName)
    {
        if (comparer.Equals(field, value))
            return false;

        ClearErrorList(propertyName);
        if (errorCheck(value))
        {
            action(value);
            field = value;
            AddErrorToList(propertyName, errorMessage);
            return true;
        }

        action(value);
        OnPropertyChanging(propertyName);
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}