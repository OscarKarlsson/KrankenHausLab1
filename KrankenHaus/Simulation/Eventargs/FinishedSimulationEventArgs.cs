using System;
using System.Collections.Generic;
using System.Text;

namespace Simulation.Eventargs
{
    public class FinishedSimulationEventArgs : EventArgs
    {
        public int AvgTimeInQueue { get; set; }
        public int AvgTimeInIVA { get; set; }
        public int AvgTimeInSanatorium { get; set; }
        public int TotalTicks { get; set; }
        public DateTime SimulationStart { get; set; }
    }
}
