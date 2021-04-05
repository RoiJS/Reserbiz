using ReserbizAPP.Hangfire.Helpers.Class;

namespace ReserbizAPP.Hangfire.Interfaces
{
    public class ReserbizHangFireApplicationSettings
    {
        public string AutoGenerateAccountStatementsURL { get; set; }
        public string AutoGeneratePenaltiesURL { get; set; }

        public SchedulerEmailNotificationSettings SchedulerEmailNotificationSettings { get; set; }
    }
}