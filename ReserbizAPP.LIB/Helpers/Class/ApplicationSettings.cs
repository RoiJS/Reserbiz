using ReserbizAPP.LIB.Helpers.Class;

namespace ReserbizAPP.LIB.Interfaces
{
    public class ApplicationSettings
    {
        public string Token { get; set; }
        public bool ActivateEFMigration { get; set; }
        public bool ActivateDataSeed { get; set; }
        public AccountStatementNotificationSettings AccountStatementNotificationSettings { get; set; }
        public ClientDatabaseNotificationSettings ClientDatabaseNotificationSettings { get; set; }
    }
}