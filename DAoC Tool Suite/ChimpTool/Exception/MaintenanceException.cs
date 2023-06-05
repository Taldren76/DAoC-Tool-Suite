namespace DAoCToolSuite.ChimpTool.Exception
{
    [Serializable]
    public class MaintenanceException : System.Exception
    {
        public MaintenanceException()
            : base()
        { }

        public MaintenanceException(string message)
            : base(message)
        { }

        public MaintenanceException(string message, System.Exception innerException)
            : base(message, innerException)
        { }
    }
}
