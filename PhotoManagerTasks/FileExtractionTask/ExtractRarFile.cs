using Microsoft.Build.Framework;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;

namespace FileExtractionTask;

public class ExtractRarFile : ITask
{
    // The full path to the .rar file to extract
    [Required] public string? RarFilePath { get; set; } = string.Empty;

    // The path where the extracted contents will be placed
    [Required] public string? DestinationPath { get; set; } = string.Empty;

    public bool Execute()
    {
        try
        {
            if (!Directory.Exists(DestinationPath))
            {
                Directory.CreateDirectory(DestinationPath ?? string.Empty);
            }

            using (RarArchive archive = RarArchive.Open(RarFilePath ?? string.Empty))
            {
                foreach (RarArchiveEntry entry in archive.Entries.Where(entry => !entry.IsDirectory))
                {
                    entry.WriteToDirectory(DestinationPath ?? string.Empty, new SharpCompress.Common.ExtractionOptions
                    {
                        ExtractFullPath = true,
                        Overwrite = true
                    });
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
