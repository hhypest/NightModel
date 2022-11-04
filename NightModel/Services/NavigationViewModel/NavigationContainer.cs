using NightModel.ViewModel;
using System;

namespace NightModel.Services.NavigationViewModel;

public abstract class NavigationContainer<T> where T : IRelayViewModel
{
    private T? _viewModel;

    public virtual T? ViewModel { get => _viewModel; set => SetViewModel(ref _viewModel, value, OnViewModelChanged); }

    protected event Action? ViewModelChanged;

    protected virtual void OnViewModelChanged()
        => ViewModelChanged?.Invoke();

    protected virtual void SetViewModel(ref T? field, T? value, Action? onChanged)
    {
        field = value;
        onChanged?.Invoke();
    }
}