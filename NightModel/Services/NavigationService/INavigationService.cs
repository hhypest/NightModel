using System;

namespace NightModel.Services.NavigationService;

public interface INavigationService<T> where T : class
{
    public T SelectedViewModel { get; }

    public event EventHandler<EventArgs> NavigationCompleted;

    public void NavigateTo(Func<T> func);
}