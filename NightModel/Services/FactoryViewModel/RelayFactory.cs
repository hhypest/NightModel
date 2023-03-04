using System;

namespace NightModel.Services.FactoryViewModel;

public sealed class RelayFactory<T> : IRelayFactory<T>
{
    public Func<T>? CreateViewModel { get; set; }

    public T? SelectedViewModel { get; private set; }

    public event Action? ChangedViewModel;

    public void Create()
    {
        if (CreateViewModel is null)
            throw new ArgumentException(nameof(CreateViewModel));

        SelectedViewModel = CreateViewModel();
        ChangedViewModel?.Invoke();
    }
}