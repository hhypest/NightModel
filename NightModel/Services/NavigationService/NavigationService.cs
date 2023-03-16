using System;
using System.Collections.Generic;

namespace NightModel.Services.NavigationService;

public sealed class NavigationService<T> : INavigationService<T> where T : class
{
    public event EventHandler<EventArgs>? NavigationCompleted;

    public T NavigatedViewModel { get; private set; } = null!;

    private IDictionary<Type, Func<T>> Factorys { get; }

    public NavigationService()
    {
        Factorys = new Dictionary<Type, Func<T>>();
    }

    public void AddFactory(Type type, Func<T> factory)
    {
        if (factory is null)
            throw new ArgumentNullException(nameof(factory));

        Factorys.TryAdd(type, factory);
    }

    public void NavigateTo(Type type)
    {
        if (!typeof(T).IsAssignableFrom(type))
            throw new ArgumentException($"Тип {type} не наследуется от {typeof(T)}");

        if (!Factorys.TryGetValue(type, out var factory))
            throw new InvalidOperationException($"Не удается получить фабрику для {typeof(T)}");

        NavigatedViewModel = factory();
        NavigationCompleted?.Invoke(this, EventArgs.Empty);
    }

    public void NavigateTo(Func<T> factory)
    {
        NavigatedViewModel = factory();
        NavigationCompleted?.Invoke(this, EventArgs.Empty);
    }
}