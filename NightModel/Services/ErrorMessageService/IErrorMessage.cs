namespace NightModel.Services.ErrorMessageService;

public interface IErrorMessage
{
    public string GetError(string propertyName);
}