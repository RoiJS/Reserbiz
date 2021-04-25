namespace ReserbizAPP.LIB.Helpers.Class
{
    public class SchedulerEmailNotificationSettings
    {
        public string EmailSenderDisplayName { get; set; }
        public string SenderEmailAddress { get; set; }
        public string ReceiverEmailAddress { get; set; }
        public string AutoGenerateAccountStatementEmailTemplate { get; set; }
        public string AutoGeneratePenaltiesEmailTemplate { get; set; }
    }
}