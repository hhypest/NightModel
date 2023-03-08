# NightModel
Простейший фреймворк для базовой реализации MVVM-паттерна.

Далее базовый пример реализации модели представления:

```csharp

public class UserViewModel : RelayViewModel
{
    #region Внедрение зависимостей

    private readonly IErrorMessage _errorMessage;

    #endregion Внедрение зависимостей

    #region Поля для свойств

    private string _userFirstName;
    private string _userLastName;
    private int _userAge;

    #endregion Поля для свойств

    #region Свойства модели

    public string UserFirstName { get => _userFirstName; set => Set(ref _userFirstName, value, OnNameCheck, _errorMessage.GetError(nameof(UserFirstName)), nameof(UserFirstName)); }
    public string UserLastName { get => _userLastName; set => Set(ref _userLastName, value, OnNameCheck, _errorMessage.GetError(nameof(UserLastName)), nameof(UserLastName)); }
    public int UserAge { get => _userAge; set => Set(ref _userAge, value, OnAgeCheck, _errorMessage.GetError(nameof(UserAge)), nameof(UserAge)); }

    #endregion Свойства модели

    #region Команды

    public ICommand SaveUserCommand { get; }
    public ICommand LoadUserCommand { get; }
    public ICommand AddNewUserCommand { get; }
    public ICommand EditUserCommand { get; }

    #endregion Команды

    #region Конструктор

    public UserViewModel(IErrorMessage errorMessage)
    {
        _errorMessage = errorMessage;

        _userFirstName = "Иван";
        _userLastName = "Петров";
        _userAge = 20;

        SaveUserCommand = new RelayAsyncCommand<string>(OnSaveUserExecute, (pathSave) => !HasErrors && !string.IsNullOrWhiteSpace(pathSave));
        LoadUserCommand = new RelayAsyncCommand(OnLoadUserExecute);
        AddNewUserCommand = new RelayCommand(OnAddNewUserExecute);
        EditUserCommand = new RelayCommand<UserViewModel>(OnEditUserExecute, (user) => user is not null);
    }

    #endregion Конструктор

    #region Предикаты валидации

    private static bool OnNameCheck(string name)
        => string.IsNullOrWhiteSpace(name) || name.Length < 5;

    private static bool OnAgeCheck(int age)
        => age < 1 || age > 100;

    #endregion Предикаты валидации

    #region Обработчики команд

    private Task OnSaveUserExecute(string? pathSave)
    {
        // Ваша реализация
        throw new NotImplementedException(nameof(OnSaveUserExecute));
    }

    private Task OnLoadUserExecute()
    {
        // Ваша реализация
        throw new NotImplementedException(nameof(OnLoadUserExecute));
    }

    private void OnAddNewUserExecute()
    {
        // Ваша реализация
        throw new NotImplementedException(nameof(OnAddNewUserExecute));
    }

    private void OnEditUserExecute(UserViewModel? user)
    {
        // Ваша реализация
        throw new NotImplementedException(nameof(OnEditUserExecute));
    }

    #endregion Обработчики команд
}

```
