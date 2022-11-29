using System;
using System.Threading.Tasks;

namespace NightModel.Extensions.TaskExtension;

internal static class TaskExtension
{
    internal static async void RunVoid(this Task task)
    {
        try
        {
            await task;
        }
        catch (Exception)
        {
            throw;
        }
    }
}