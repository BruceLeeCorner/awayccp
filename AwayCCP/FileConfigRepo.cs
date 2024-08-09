using Nito.AsyncEx;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Media;

namespace AwayCCP;

public class FileConfigRepo : IConfigRepo
{
    private readonly AsyncLock _fileLocker = new AsyncLock();
    private readonly string _path = Path.Combine(Path.GetDirectoryName(Environment.ProcessPath!)!, "Config.json");

    public async Task<IConfig> LoadAsync()
    {
        string? json = null;
        IConfig? config;
        // in case file doesn't exist.
        try
        {
            using (await _fileLocker.LockAsync())
            {
                json = await File.ReadAllTextAsync(_path, Encoding.UTF8);
            }
        }
        catch (Exception e)
        {
            // ignored
        }

        // in case of invalid json
        try
        {
#pragma warning disable CS8604
            config = JsonSerializer.Deserialize<Config>(json, new JsonSerializerOptions()
#pragma warning restore CS8604
            {
                Converters = { new ColorJsonConverter() }
            });
        }
        catch (Exception e)
        {
            config = new Config
            {
                BackColor = Colors.White,
                BoxHeight = 50,
                BoxWidth = 250,
                FontSize = 10,
                ForeColor = Colors.Black
            };
        }

        // in case json is null
        config ??= new Config
        {
            BackColor = Colors.White,
            BoxHeight = 50,
            BoxWidth = 250,
            FontSize = 10,
            ForeColor = Colors.Black
        };

        return config;
    }

    public async Task SaveAsync(IConfig config)
    {
        var json = JsonSerializer.Serialize(config, new JsonSerializerOptions()
        {
            Converters = { new ColorJsonConverter() },
            WriteIndented = true
        });

        using (await _fileLocker.LockAsync())
        {
            await File.WriteAllTextAsync(_path, json, Encoding.UTF8);
        }
    }
}