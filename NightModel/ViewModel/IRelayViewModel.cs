using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NightModel.ViewModel;

public interface IRelayViewModel
{
    public bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null);

    public bool Set<T>(ref T field, T value, IEqualityComparer comparer, Action<T> action, Predicate<T> errorCheck, string errorMessage, string propertyName);

    public bool Set<T>(ref T field, T value, IEqualityComparer<T> comparer, [CallerMemberName] string? propertyName = null);

    public bool Set<T>(ref T field, T value, IEqualityComparer<T> comparer, Action<T> action, [CallerMemberName] string? propertyName = null);

    public bool Set<T>(ref T field, T value, IEqualityComparer<T> comparer, Predicate<T> errorCheck, string errorMessage, string propertyName);

    public bool Set<T>(ref T field, T value, Predicate<T> errorCheck, string errorMessage, string propertyName);
}