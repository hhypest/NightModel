using NightModel.ViewModel;
using System;

namespace NightModel.Services.NavigationViewModel.NavigationContainer;

public class NavigationContainer : INavigationContainer
{
    private RelayViewModel? _viewModel;
    private readonly bool _unsubscribe;

    public virtual RelayViewModel? ViewModel { get => _viewModel; set => SetViewModel(ref _viewModel, value); }

    public event Action? ViewModelChanged;

    public NavigationContainer(bool unsubscribe = false)
        => _unsubscribe = unsubscribe;

    protected virtual void OnViewModelChanged()
        => ViewModelChanged?.Invoke();

    protected virtual bool SetViewModel<T>(ref T? field, T? value) where T : RelayViewModel
    {
        if (Equals(field, value))
            return false;

        if (_unsubscribe)
            field?.UnsubscribeEvent();

        field = value;
        OnViewModelChanged();
        return true;
    }
}