using Microsoft.Build.Framework;

namespace FileDateTask;

public class SetFileDates : ITask
{
    // List of files to apply the date
    [Required] public ITaskItem[] Files { get; set; } = [];

    // The date to set on the files
    [Required] public string? Date { get; set; } = string.Empty;

    public bool Execute()
    {
        try
        {
            DateTime fixedDate = DateTime.Parse(Date ?? string.Empty);  // Parse the provided date

            foreach (ITaskItem file in Files)
            {
                string filePath = file.ItemSpec;
                if (File.Exists(filePath))
                {
                    // Set both the creation and last modified times
                    File.SetCreationTime(filePath, fixedDate);
                    File.SetLastWriteTime(filePath, fixedDate);
                }
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public IBuildEngine? BuildEngine { get; set; }
    public ITaskHost? HostObject { get; set; }
}
