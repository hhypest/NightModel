using NightModel.Services.NavigationViewModel.NavigationContainer;
using NightModel.ViewModel;
using System;

namespace NightModel.Services.NavigationViewModel.NavigationService;

public class NavigationService<T> : INavigationService where T : RelayViewModel
{
	private readonly INavigationContainer _container;
	private readonly Func<T> _createViewModel;

	public NavigationService(INavigationContainer container, Func<T> createViewModel)
	{
		_container = container;
		_createViewModel = createViewModel;
	}

	public virtual void Navigate()
		=> _container.ViewModel = _createViewModel();
}