using System.IO;

namespace NightModel.Services.DialogMessageService;

public interface IDialogMessage
{
    public FileInfo? OpenFileDialog(string title, string filter = "Все файлы|*.*");

    public FileInfo? SaveFileDialog(string title, string filter = "Все файлы|*.*", string? name = null);

    public void ShowErrorMessage(string title, string message);

    public void ShowInformationDialog(string title, string message);

    public bool ShowQuestionDialog(string title, string message);

    public void ShowWarningDialog(string title, string message);
}