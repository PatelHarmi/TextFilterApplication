namespace TextFilterApplication.Settings
{
    /// <summary>
    /// Input File Path Settings
    /// </summary>
    public class FileSettings : IFileSettings
    {
        public required string InputFilePath { get; set; }
    }
}
