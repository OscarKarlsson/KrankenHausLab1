
using Simulation.Eventargs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;


namespace PrintManager.Print
{
    public class ConsolePrint
    {
        int number = 1;
        int daycounter = 1;
        PrintToLog printToLog = new PrintToLog();

        public void PrintFiveSeconds(object sender, ReportEventArgs args)
        {
            StringBuilder write = new StringBuilder();
            write.AppendLine($"\nDay : {daycounter}\n");
            write.AppendLine($"Amount of patients dead: {args.AmountDead}");
            write.AppendLine($"\nAmount of patients recovered: {args.AmountRecovered}");
            write.AppendLine($"\nAmount of patients in IVA: {args.AmountIVA}");
            write.AppendLine($"\nAmount of patients in sanatorium: {args.AmountSanatorium}");
            write.AppendLine($"\nAmount of patients in queue: {args.AmountPatientsInQueue}");
            write.AppendLine($"\nDoctors waiting: {args.AmountDoctorsWaiting}\n");
            string textToFile = write.ToString();            
            printToLog.WriteToFile(textToFile);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(textToFile);
            Console.ResetColor();
            daycounter++;
        }
        public void PrintWhenFinish(object sender, FinishedSimulationEventArgs args)
        {
            Console.WriteLine($"\nAverage time in queue: {args.AvgTimeInQueue}\n" +
                $"Average time in IVA: {args.AvgTimeInIVA}\n" +
                $"Average time in Sanatorium: {args.AvgTimeInSanatorium}\n" +
                $"Total tick count: {args.TotalTicks}\n" +
                $"Simulation started: {args.SimulationStart.ToString()}");
        }
        public void PrintPatient(object sender, PatientDeadOrAliveEventArgs args)
        {            
            Console.ResetColor();
            if (args.Patient.CheckSicknessLevel() == 1)//Överlever
            {
                string temp = $"{number} {args.Patient.ToString()} fully recovered!\n";                              
                Console.WriteLine(temp);
                printToLog.WriteToFile(temp);
            }
            else if(args.Patient.CheckSicknessLevel() == -1)//Död
            {
                string temp = $"{number} {args.Patient.ToString()} died!\n";
                Console.WriteLine(temp);
                printToLog.WriteToFile(temp);
            }
            number++;
            Console.ResetColor();
        }
    }
}
