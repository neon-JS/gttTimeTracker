namespace GttTimeTracker.Commands;

public class Start(IEntryStorage entryStorage) : ICommand
{
    public const string COMMAND = "start";
    public bool ContinueToGit => false;

    public async Task HandleAsync(IReadOnlyList<string> parameters)
    {
        var lastEntry = entryStorage.Entries.MaxBy(t => t.Start);

        if (lastEntry is { End: null })
        {
            await Console.Error.WriteLineAsync("fatal: There is an active task.");
            return;
        }

        var taskName = parameters.FirstOrDefault() ?? lastEntry?.Task;
        if (taskName is null)
        {
            await Console.Error.WriteLineAsync(
                """
                fatal: No task specified and no task to resume.
                usage: gtt start [<TASK>]
                       Task can only be omitted if there's an existing task to resume.
                """
            );
            return;
        }

        var entry = new TimeTrackingEntry(taskName, DateTime.Now);

        entryStorage.Add(entry);
        await entryStorage.StoreAsync();

        Console.WriteLine($"started: {entry.Task} at {entry.Start:u}");
    }
}