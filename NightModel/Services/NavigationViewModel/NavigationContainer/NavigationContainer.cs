using NightModel.ViewModel;
using System;

namespace NightModel.Services.NavigationViewModel.NavigationContainer;

public class NavigationContainer : INavigationContainer
{
    private RelayViewModel? _viewModel;

    public virtual RelayViewModel? ViewModel { get => _viewModel; set => SetViewModel(ref _viewModel, value); }

    public event Action? ViewModelChanged;

    protected virtual void OnViewModelChanged()
        => ViewModelChanged?.Invoke();

    protected virtual bool SetViewModel<T>(ref T? field, T? value) where T : RelayViewModel
    {
        if (Equals(field, value))
            return false;

        field = value;
        OnViewModelChanged();
        return true;
    }
}