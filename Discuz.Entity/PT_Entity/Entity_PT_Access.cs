using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Discuz.Entity
{

    class Entity_PT_Access
    {
    }

    public class PTLogMessage
    {
        public int Id = 0;
        public DateTime Logtime = new DateTime(1970, 1, 1);
        public string LogData1 = "";
        public string LogData2 = "";
        public string LogData3 = "";
        public string LogData4 = "";
        public string LogMessage = "";
    }
}
