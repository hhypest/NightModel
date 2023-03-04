using System;

namespace NightModel.Services.FactoryViewModel;

public interface IRelayFactory<T>
{
    Func<T>? CreateViewModel { get; set; }

    T? SelectedViewModel { get; }

    event Action? ChangedViewModel;

    void Create();
}