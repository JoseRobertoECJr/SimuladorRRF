using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimuladorRRF.Classes
{
    public class Response
    {
        public Process[] ProcessList { get; set; }
        public string[] Log { get; set; }

        public Response(Process[] processList, string[] log)
        {
            ProcessList = processList;
            Log = log;
        }
    }
}
