using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Core.Entities;
using Simulation.Eventargs;



namespace PrintManager.Print
{
    public class ConsolePrint
    {
        int daycounter = 1;
        public void PrintFiveSeconds(object sender, ReportEventArgs args)
        {
            
            Console.WriteLine($"Day : {daycounter}\n" +
                $"Amount of patients dead: {args.AmountDead}" +
                $"\nAmount of patients recovered: {args.AmountRecovered}" +
                $"\nAmount of patients in IVA: {args.AmountIVA}" +
                $"\nAmount of patients in sanatorium: {args.AmountSanatorium}" +
                $"\nAmount of patients in queue: {args.AmountPatientsInQueue}" +
                $"\nDoctors waiting: {args.AmountDoctorsWaiting}\n\n");
            daycounter++;
        }
    }
}
