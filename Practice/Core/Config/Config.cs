using Microsoft.Extensions.Configuration;

namespace Core.Config;
public static class Config
{
    private static readonly IConfigurationRoot _config;

    static Config()
    {
        _config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
    }

    public static AppSettings AppSettings
    {
        get
        {
            return _config.GetSection("AppSettings").Get<AppSettings>();
        }
    }

    public static DownloadSettings DownloadSettings
    {
        get
        {
            return _config.GetSection("DownloadSettings").Get<DownloadSettings>();
        }
    }
}
