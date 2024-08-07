namespace GttTimeTracker.Commands;

public class Cleanup(IEntryStorage entryStorage) : ICommand
{
    public const string COMMAND = "cleanup";
    public bool ContinueToGit => false;

    public async Task HandleAsync(IReadOnlyList<string> parameters)
    {
        if (
            parameters is not [var remainingDaysParameter]
            || string.IsNullOrWhiteSpace(remainingDaysParameter)
        )
        {
            await Console.Error.WriteLineAsync(
                """
                fatal: No number of remaining days given.
                usage: gtt cleanup <DAYS>
                """
            );
            return;
        }

        if (!byte.TryParse(remainingDaysParameter, out var remainingDays))
        {
            await Console.Error.WriteLineAsync(
                """
                fatal: Given remaining days are not a positive number.
                usage: gtt cleanup <DAYS>
                """
            );
            return;
        }

        var threshold = DateTime.Now.Date.AddDays(remainingDays * -1);

        var outdatedEntries = entryStorage.Entries
           .Where(e => e.End < threshold)
           .ToList();

        entryStorage.Remove(outdatedEntries);

        await entryStorage.StoreAsync();

        Console.WriteLine($"removed all tasks older than {remainingDays} days.");
    }
}