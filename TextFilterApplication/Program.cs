using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TextFilterApplication.Repositories;
using TextFilterApplication.Settings;
using Microsoft.Extensions.Options;
using System.Text;

namespace TextFilterApp;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json");
            })
            .ConfigureServices((context, services) =>
            {
                var configuration = context.Configuration;
               
                services.Configure<FileSettings>(configuration.GetSection("FileSettings"));
                services.AddSingleton<IFileSettings>(sp => sp.GetRequiredService<IOptions<FileSettings>>().Value);

                services.AddSingleton<ITextFilterRepository, VowelMiddleFilterRepository>();
                services.AddSingleton<ITextFilterRepository, LengthFilterRepository>();
                services.AddSingleton<ITextFilterRepository, LetterTFilterRepository>();

                services.AddSingleton<TextFilterProcessor>();
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            })
            .Build();

        await RunApplicationAsync(host.Services);
    }

    private static async Task RunApplicationAsync(IServiceProvider services)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        var fileSetting = services.GetService<IFileSettings>();

        try
        {
            string filePath = GetFilePath(fileSetting);

            string inputText = await ReadInputFileAsync(filePath);

            if (string.IsNullOrEmpty(inputText))
            {
                logger.LogError("Input file is empty or not found.");
                return;
            }

            var processor = services.GetService<TextFilterProcessor>();
            var filteredWords = processor.ApplyFilters(inputText);

            Console.WriteLine("Filtered Text:");
            Console.WriteLine(string.Join(" ", filteredWords));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while processing the text filters.");
        }
        finally
        {
            logger.LogInformation("TextFilter application has completed.");
        }
    }

    private static string GetFilePath(IFileSettings fileSetting)
    {
        var filePath = Path.Combine(AppContext.BaseDirectory, fileSetting.InputFilePath);
        return Path.GetFullPath(filePath);
    }

    private static async Task<string> ReadInputFileAsync(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return null;
        }

        var stringBuilder = new StringBuilder();

        using (var reader = new StreamReader(filePath))
        {
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                stringBuilder.AppendLine(line);
            }
        }

        return stringBuilder.ToString();
    }
}
