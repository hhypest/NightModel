using System.Threading.Tasks;

namespace NightModel.Extensions.TaskExtension;

internal static class TaskExtension
{
    internal static void RunVoid(this Task task)
    {
        try
        {
            task.RunSynchronously();
        }
        catch
        {
            throw;
        }
    }
}