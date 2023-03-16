using System.Threading.Tasks;

namespace NightModel.Extensions.TaskExtension;

internal static class TaskExtension
{
    internal static void RunVoid(this Task task)
    {
        if (!task.IsCompleted || task.IsFaulted)
        {
            _ = ForgetAwaiter(task);
        }
    }

    private static async Task ForgetAwaiter(Task task)
    {
        try
        {
            await task.ConfigureAwait(false);
        }
        catch
        {
        }
    }
}