using System;

namespace NightModel.Services.NavigationService;

public sealed class NavigationService<T> where T : class
{
    public T SelectedViewModel { get; private set; } = null!;

    public event EventHandler<EventArgs> NavigationCompleted = null!;

    public void NavigateTo(Func<T> func)
    {
        SelectedViewModel = func();
        NavigationCompleted.Invoke(this, EventArgs.Empty);
    }
}