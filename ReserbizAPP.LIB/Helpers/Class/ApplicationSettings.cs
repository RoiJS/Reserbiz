using ReserbizAPP.LIB.Helpers.Class;

namespace ReserbizAPP.LIB.Interfaces
{
    public class ApplicationSettings
    {
        public GeneralSettings GeneralSettings { get; set; }
        public AccountStatementNotificationSettings AccountStatementNotificationSettings { get; set; }
        public ClientDatabaseNotificationSettings ClientDatabaseNotificationSettings { get; set; }
        public RemoveRefreshTokensNotificationSettings RemoveRefreshTokensNotificationSettings { get; set; }
        public SchedulerEmailNotificationSettings SchedulerEmailNotificationSettings { get; set; }
        public AppSettingsURL AppSettingsURL { get; set; }
    }
}