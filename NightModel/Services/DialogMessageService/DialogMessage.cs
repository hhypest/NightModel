using Microsoft.Win32;
using System.IO;
using System.Linq;
using System.Windows;

namespace NightModel.Services.DialogMessageService;

public class DialogMessage : IDialogMessage
{
    protected static Window? ActiveWindow => Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);

    protected static Window? FocusedWindow => Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsFocused);

    protected static Window? CurrentWindow => FocusedWindow ?? ActiveWindow;

    public virtual FileInfo? OpenFileDialog(string title, string filter = "Все файлы|*.*")
    {
        var dialog = new OpenFileDialog()
        {
            Title = title,
            Filter = filter,
            RestoreDirectory = true,
            AddExtension = true,
            CheckFileExists = true,
            CheckPathExists = true,
            Multiselect = false
        };

        if (dialog.ShowDialog(CurrentWindow) == false)
            return null;

        return new FileInfo(dialog.FileName);
    }

    public virtual FileInfo? SaveFileDialog(string title, string filter = "Все файлы|*.*", string? name = null)
    {
        var dialog = new SaveFileDialog()
        {
            Title = title,
            Filter = filter,
            FileName = name ?? "Новый файл",
            RestoreDirectory = true,
            AddExtension = true
        };

        if (dialog.ShowDialog(CurrentWindow) == false)
            return null;

        return new FileInfo(dialog.FileName);
    }

    public virtual void ShowInformationDialog(string title, string message)
        => MessageBox.Show(CurrentWindow, message, title, MessageBoxButton.OK, MessageBoxImage.Information);

    public virtual void ShowWarningDialog(string title, string message)
        => MessageBox.Show(CurrentWindow, message, title, MessageBoxButton.OK, MessageBoxImage.Warning);

    public virtual void ShowErrorMessage(string title, string message)
        => MessageBox.Show(CurrentWindow, message, title, MessageBoxButton.OK, MessageBoxImage.Error);

    public virtual bool ShowQuestionDialog(string title, string message)
    {
        var result = MessageBox.Show(CurrentWindow, message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
        return result == MessageBoxResult.Yes;
    }
}