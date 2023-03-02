using TicketDal.Helpers;

namespace TicketDal.Settings
{
    public class AppSettings : SettingsSingleton<AppSettings>
    {
        public string? ConnectionString { get; set; }
        public string? FlyerImageName { get; set; }
        public string? TempFolder { get; set; }
        public EmailConfiguration? EmailConfiguration { get; set; }
    }
}
