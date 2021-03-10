using System;
using System.Collections.Generic;
using System.Text;

namespace Simulation.Eventargs
{
    public class ReportEventArgs : EventArgs
    {
        public int AmountDead { get; set; }
        public int AmountRecovered { get; set; }
        public int AmountPatientsInQueue { get; set; }
        public int AmountDoctorsWaiting { get; set; }
        public int AmountIVA { get; set; }
        public int AmountSanatorium { get; set; }
        public DateTime StartTime { get; set; }
        public int TickCount { get; set; }

        public ReportEventArgs()
        {
            this.StartTime = DateTime.Now;
        }

    }
}
