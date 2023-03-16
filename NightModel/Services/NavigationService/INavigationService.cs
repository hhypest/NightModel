using System;

namespace NightModel.Services.NavigationService;

public interface INavigationService<T> where T : class
{
    public event EventHandler<EventArgs>? NavigationCompleted;

    public T NavigatedViewModel { get; }

    public void AddFactory(Type type, Func<T> factory);

    public void NavigateTo(Func<T> factory);

    public void NavigateTo(Type type);
}