using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NightModel.ViewModel.Base;

/// <summary>Представляет базовый класс для реализации ViewModel.</summary>
public abstract class BaseViewModel : INotifyPropertyChanged, INotifyPropertyChanging, INotifyDataErrorInfo
{
    /// <summary>Возникает при изменении значения свойства.</summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>Возникает при изменении значения свойства.</summary>
    public event PropertyChangingEventHandler? PropertyChanging;

    /// <summary>Возникает, когда ошибки проверки изменились для свойства или для всего объекта.</summary>
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    /// <summary>Возвращает значение, указывающее, есть ли у объекта ошибки проверки.</summary>
    /// <returns>
    ///   <see langword="true" /> если объект в данный момент имеет ошибки проверки; иначе, <see langword="false" />.</returns>
    public bool HasErrors => ErrorList.Count > 0;

    /// <summary>Возвращает ошибки проверки для указанного свойства или для всего объекта.</summary>
    /// <param name="propertyName">Имя свойства для извлечения ошибок проверки; или <see langword="null" /> или <see cref="F:System.String.Empty" />, для извлечения ошибок на уровне объекта.</param>
    /// <returns>Ошибки проверки для свойства или объекта.</returns>
    public IEnumerable GetErrors(string? propertyName)
    {
        if (propertyName is null)
            return new List<string>();

        if (!ErrorList.ContainsKey(propertyName))
            return new List<string>();

        return ErrorList[propertyName];
    }

    /// <summary>Словарь ошибок./></summary>
    protected virtual Dictionary<string, List<string>> ErrorList { get; } = new();

    /// <summary>Обработчик события <see cref="PropertyChanged"/>, уведомляющее об изменении свойства</summary>
    /// <param name="propertyName">Имя свойства, в котором меняется значения. Поумолчанию <see langword="null"/>. Имя устанавливается автоматически благодаря <see cref="CallerMemberNameAttribute"/>.</param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    /// <summary>Обработчик события <see cref="PropertyChanging"/>, уведомляющее о том, что свойство будет изменено.</summary>
    /// <param name="propertyName">Имя свойства, в котором меняется значения. Поумолчанию <see langword="null"/>. Имя устанавливается автоматически благодаря <see cref="CallerMemberNameAttribute"/>.</param>
    protected virtual void OnPropertyChanging([CallerMemberName] string? propertyName = null)
        => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    /// <summary>Обработчик события <see cref="ErrorsChanged"/>, уведомляющее о том, что список ошибок изменился.</summary>
    /// <param name="propertyName">Имя свойства. Поумолчанию <see langword="null"/>. Имя устанавливается автоматически благодаря <see cref="CallerMemberNameAttribute"/>.</param>
    protected virtual void OnErrorsChanged([CallerMemberName] string? propertyName = null)
        => ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

    /// <summary>Метод добавления сообщения об ошибке в список.</summary>
    /// <param name="propertyName">Имя свойства. Поумолчанию <see langword="null"/>. Имя устанавливается автоматически благодаря <see cref="CallerMemberNameAttribute"/>.</param>
    /// <param name="errorMessage">Сообщение об ошибке для свойства <see href="propertyName"/>.</param>
    protected virtual void AddErrorToList(string propertyName, string errorMessage)
    {
        if (!ErrorList.ContainsKey(propertyName))
            ErrorList.Add(propertyName, new List<string>());

        ErrorList[propertyName].Add(errorMessage);
        OnPropertyChanging(propertyName);
        OnErrorsChanged(propertyName);
    }

    /// <summary>Метод удаления ошибок из списка.</summary>
    /// <param name="propertyName">Имя свойства. Поумолчанию <see langword="null"/>. Имя устанавливается автоматически благодаря <see cref="CallerMemberNameAttribute"/>.</param>
    protected virtual void ClearErrorList(string propertyName)
    {
        if (ErrorList.Remove(propertyName))
            OnErrorsChanged(propertyName);
    }

    /// <summary>Метод установки нового значения в наблюдаемое свойство. Предотвращает циклическую смену значений в наблюдаемом свойстве.</summary>
    /// <typeparam name="T">Тип свойства</typeparam>
    /// <param name="field">Ссылка на поле, которое содержит старое значение. Параметр передается как <see langword="ref"/>.</param>
    /// <param name="value">Передаваемое значение.</param>
    /// <param name="propertyName">Имя свойства, в котором меняется значения. Поумолчанию <see langword="null"/>. Имя устанавливается автоматически благодаря <see cref="CallerMemberNameAttribute"/>.</param>
    /// <returns><see langword="true"/>: если <see langword="value"/> значение отличается от <see langword="field"/>. И <see langword="false"/> в противоположном случае.</returns>
    protected abstract bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null);

    /// <summary>Метод установки нового значения в наблюдаемое свойство. Предотвращает циклическую смену значений в наблюдаемом свойстве.</summary>
    /// <typeparam name="T">Тип свойства</typeparam>
    /// <param name="field">Ссылка на поле, которое содержит старое значение. Параметр передается как <see langword="ref"/>.</param>
    /// <param name="value">Передаваемое значение.</param>
    /// <param name="comparer">Пользовательский <see cref="IEqualityComparer{T}"/>, используемый для сравнения.</param>
    /// <param name="propertyName">Имя свойства, в котором меняется значения. Поумолчанию <see langword="null"/>. Имя устанавливается автоматически благодаря <see cref="CallerMemberNameAttribute"/>.</param>
    /// <returns><see langword="true"/>: если <see langword="value"/> значение отличается от <see langword="field"/>. И <see langword="false"/> в противоположном случае.</returns>
    protected abstract bool Set<T>(ref T field, T value, IEqualityComparer<T> comparer, [CallerMemberName] string? propertyName = null);

    /// <summary>Метод установки нового значения в наблюдаемое свойство. Предотвращает циклическую смену значений в наблюдаемом свойстве.</summary>
    /// <typeparam name="T">Тип свойства</typeparam>
    /// <param name="field">Ссылка на поле, которое содержит старое значение. Параметр передается как <see langword="ref"/>.</param>
    /// <param name="value">Передаваемое значение.</param>
    /// <param name="comparer">Пользовательский <see cref="IEqualityComparer{T}"/>, используемый для сравнения.</param>
    /// <param name="action">Делегат, который должен быть выполнен для установки свойства.</param>
    /// <param name="propertyName">Имя свойства, в котором меняется значения. Поумолчанию <see langword="null"/>. Имя устанавливается автоматически благодаря <see cref="CallerMemberNameAttribute"/>.</param>
    /// <returns><see langword="true"/>: если <see langword="value"/> значение отличается от <see langword="field"/>. И <see langword="false"/> в противоположном случае.</returns>
    protected abstract bool Set<T>(ref T field, T value, IEqualityComparer<T> comparer, Action<T> action, [CallerMemberName] string? propertyName = null);

    /// <summary>Метод установки нового значения в наблюдаемое свойство. Предотвращает циклическую смену значений в наблюдаемом свойстве.</summary>
    /// <typeparam name="T">Тип свойства</typeparam>
    /// <param name="field">Ссылка на поле, которое содержит старое значение. Параметр передается как <see langword="ref"/>.</param>
    /// <param name="value">Передаваемое значение.</param>
    /// <param name="errorCheck">Предикат, для проверки на ошибки.</param>
    /// <param name="errorMessage">Сообщение об ошибке.</param>
    /// <param name="propertyName">Имя свойства, в котором меняется значения. Поумолчанию <see langword="null"/>. Имя устанавливается автоматически благодаря <see cref="CallerMemberNameAttribute"/>.</param>
    /// <returns><see langword="true"/>: если <see langword="value"/> значение отличается от <see langword="field"/>. И <see langword="false"/> в противоположном случае.</returns>
    protected abstract bool Set<T>(ref T field, T value, Predicate<T> errorCheck, string errorMessage, string propertyName);

    /// <summary>Метод установки нового значения в наблюдаемое свойство. Предотвращает циклическую смену значений в наблюдаемом свойстве.</summary>
    /// <typeparam name="T">Тип свойства</typeparam>
    /// <param name="field">Ссылка на поле, которое содержит старое значение. Параметр передается как <see langword="ref"/>.</param>
    /// <param name="value">Передаваемое значение.</param>
    /// <param name="comparer">Пользовательский <see cref="IEqualityComparer{T}"/>, используемый для сравнения.</param>
    /// <param name="errorCheck">Предикат, для проверки на ошибки.</param>
    /// <param name="errorMessage">Сообщение об ошибке.</param>
    /// <param name="propertyName">Имя свойства, в котором меняется значения. Поумолчанию <see langword="null"/>. Имя устанавливается автоматически благодаря <see cref="CallerMemberNameAttribute"/>.</param>
    /// <returns><see langword="true"/>: если <see langword="value"/> значение отличается от <see langword="field"/>. И <see langword="false"/> в противоположном случае.</returns>
    protected abstract bool Set<T>(ref T field, T value, IEqualityComparer<T> comparer, Predicate<T> errorCheck, string errorMessage, string propertyName);

    /// <summary>Метод установки нового значения в наблюдаемое свойство. Предотвращает циклическую смену значений в наблюдаемом свойстве.</summary>
    /// <typeparam name="T">Тип свойства</typeparam>
    /// <param name="field">Ссылка на поле, которое содержит старое значение. Параметр передается как <see langword="ref"/>.</param>
    /// <param name="value">Передаваемое значение.</param>
    /// <param name="comparer">Пользовательский <see cref="IEqualityComparer{T}"/>, используемый для сравнения.</param>
    /// <param name="action">Делегат, который должен быть выполнен для установки свойства.</param>
    /// <param name="errorCheck">Предикат, для проверки на ошибки.</param>
    /// <param name="errorMessage">Сообщение об ошибке.</param>
    /// <param name="propertyName">Имя свойства, в котором меняется значения. Поумолчанию <see langword="null"/>. Имя устанавливается автоматически благодаря <see cref="CallerMemberNameAttribute"/>.</param>
    /// <returns><see langword="true"/>: если <see langword="value"/> значение отличается от <see langword="field"/>. И <see langword="false"/> в противоположном случае.</returns>
    protected abstract bool Set<T>(ref T field, T value, IEqualityComparer comparer, Action<T> action, Predicate<T> errorCheck, string errorMessage, string propertyName);
}