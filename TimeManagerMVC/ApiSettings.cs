namespace TimeManagerMVC
{
    public class ApiSettings
    {
        public const string SectionName = "ShiftLogerApi";

        public string ConnectionType { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
