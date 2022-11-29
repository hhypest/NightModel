# NightModel
Простейший фреймворк для базовой реализации MVVM-паттерна.

```csharp
    protected override bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (Equals(field, value))
            return false;

        OnPropertyChanging(propertyName);
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
```
