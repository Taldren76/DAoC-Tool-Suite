using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
