using System.Collections.Generic;

namespace NightModel.Services.ErrorMessageService;

public sealed class ErrorMessage : IErrorMessage
{
    private readonly IDictionary<string, string> _errors;

    public ErrorMessage(IDictionary<string, string> errors)
    {
        _errors = errors;
    }

    public string GetError(string propertyName)
    {
        if (_errors.TryGetValue(propertyName, out var error) && error is not null)
            return error;

        _errors.Add(propertyName, $"В свойстве <{propertyName}> возникла неизвестная ошибка!");
        return _errors[propertyName];
    }
}