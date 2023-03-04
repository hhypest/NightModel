using System;

namespace NightModel.Services.FactoryViewModel;

public interface IRelayFactory<T>
{
    public Func<T>? CreateViewModel { get; set; }

    public T? SelectedViewModel { get; }

    public event Action? ChangedViewModel;

    public void Create();
}