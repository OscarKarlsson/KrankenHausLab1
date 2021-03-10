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

            StringBuilder write = new StringBuilder();
            write.AppendLine($"Day : {daycounter}\n");
            write.AppendLine($"Amount of patients dead: {args.AmountDead}");
            write.AppendLine($"\nAmount of patients recovered: {args.AmountRecovered}");
            write.AppendLine($"\nAmount of patients in IVA: {args.AmountIVA}");
            write.AppendLine($"\nAmount of patients in sanatorium: {args.AmountSanatorium}");
            write.AppendLine($"\nAmount of patients in queue: {args.AmountPatientsInQueue}");
            write.AppendLine($"\nDoctors waiting: {args.AmountDoctorsWaiting}\n");
            write.AppendLine($"\nNumber of ticks: {args.TickCount}\n");
            string textToFile = write.ToString();
            PrintToLog printToLog = new PrintToLog();
            printToLog.WriteToFile(textToFile);
            Console.WriteLine(textToFile);
            daycounter++;
        }
    }
}
