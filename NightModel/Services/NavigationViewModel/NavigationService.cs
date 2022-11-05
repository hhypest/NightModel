using NightModel.ViewModel;
using System;

namespace NightModel.Services.NavigationViewModel;

public abstract class NavigationService<T> where T : IRelayViewModel
{
	protected readonly NavigationContainer<T> _navigation;
	protected readonly Func<T> _createExecute;

	protected NavigationService(NavigationContainer<T> navigation, Func<T> createExecute)
	{
		_navigation = navigation;
		_createExecute = createExecute;
	}

	protected virtual void Navigate()
		=> _navigation.ViewModel = _createExecute();
}