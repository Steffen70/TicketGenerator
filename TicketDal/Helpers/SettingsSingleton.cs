using System.Text.Json;

namespace TicketDal.Helpers
{
    public abstract class SettingsSingleton<TSettings> : Singleton<TSettings> where TSettings: class, new()
    {
        public new static TSettings I => Instance ??= GetSettings();

        public static TSettings GetSettings()
        {
            string fileName = System.Environment.GetEnvironmentVariable("APPSETTINGS") ?? throw new Exception("Set 'APPSETTINGS' environment variable!");

            var settings = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<TSettings>(settings) ?? throw new FileNotFoundException();
        }
    }
}
