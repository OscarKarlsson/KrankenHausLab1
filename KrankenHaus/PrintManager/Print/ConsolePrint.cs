
using Simulation.Eventargs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;


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
            string textToFile = write.ToString();
            PrintToLog printToLog = new PrintToLog();
            printToLog.WriteToFile(textToFile);
            Console.WriteLine(textToFile);
            daycounter++;
        }
        public void PrintWhenFinish(object sender, FinishedSimulationEventArgs args)
        {
            Console.WriteLine($"Average time in queue: {args.AvgTimeInQueue}\n" +
                $"Average time in IVA: {args.AvgTimeInIVA}\n" +
                $"Average time in Sanatorium: {args.AvgTimeInSanatorium}\n" +
                $"Total tick count: {args.TotalTicks}\n" +
                $"Simulation started: {args.SimulationStart.ToString()}");
        }
        public void PrintPatient(object sender, PatientDeadOrAliveEventArgs args)
        {
            if (args.patient.CheckSicknessLevel() == 1)//Överlever
            {
                Console.WriteLine($"{args.patient.ToString()} fully recovered!");
            }
            else if(args.patient.CheckSicknessLevel() == -1)//Död
            {
                Console.WriteLine($"{args.patient.ToString()} died!");
            }
        }
    }
}
