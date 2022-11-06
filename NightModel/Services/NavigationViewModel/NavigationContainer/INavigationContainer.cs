using NightModel.ViewModel;
using System;

namespace NightModel.Services.NavigationViewModel.NavigationContainer;

public interface INavigationContainer
{
    public RelayViewModel? ViewModel { get; set; }

    public event Action? ViewModelChanged;
}