using System;
using System.Collections.Generic;
using System.Text;

namespace Simulation.Eventargs
{
    public class PatientDeadOrAliveEventArgs : EventArgs
    {
        public string Name { get; set; }
    }
}
