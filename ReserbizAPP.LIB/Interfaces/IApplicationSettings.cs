namespace ReserbizAPP.LIB.Interfaces
{
    public class IApplicationSettings
    {
        public string Token { get; set; }
        public bool ActivateEFMigration { get; set; }
        public bool ActivateDataSeed { get; set; }
    }
}