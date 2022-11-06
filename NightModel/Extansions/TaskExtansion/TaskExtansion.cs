using System;
using System.Threading.Tasks;

namespace NightModel.Extansions.TaskExtansion;

internal static class TaskExtansion
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