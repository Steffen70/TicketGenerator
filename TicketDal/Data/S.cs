using TicketDal.Helpers;

namespace TicketDal.Data
{
    public class S : SettingsSingleton<S>
    {
        public string? ConnectionString { get; set; }
        public string? FlyerImageName { get; set; }
        public string? TempFolder { get; set; }
    }
}
