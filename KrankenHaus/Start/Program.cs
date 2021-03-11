using PrintManager.Print;
using Simulation;
using System;

namespace Start
{
    class Program
    {
        static public int IVADecrease { get; set; }
        static public int IVAIncrease { get; set; }
        static public int SanatoriumDecrease { get; set; }
        static public int SanatoriumIncrease { get; set; }
        static private int AmountOfPatients { get; set; }
        static public int AmountOfDoctors { get; set; }
        static public int QueueIncrease { get; set; }
        static public int QueueDecrease { get; set; }
        static public int ChangeFastTick { get; set; }
        static public int ChangeSlowTick { get; set; }
        static void Main(string[] args)
        {
            ManualInputs();
            var simulator = new Simulator(IVADecrease, IVAIncrease, SanatoriumDecrease, SanatoriumIncrease, AmountOfPatients, AmountOfDoctors, QueueIncrease, QueueDecrease);
            var consolePrint = new ConsolePrint();
            simulator.FinishEventHandler += consolePrint.PrintWhenFinish;
            simulator.ReportEventHandler += consolePrint.PrintFiveSeconds;
            simulator.DeadOrAliveHandler += consolePrint.PrintPatient;
            
            simulator.StartSimulation();

            Console.ReadLine();
        }

        private static void ManualInputs()
        {
            Console.WriteLine("Welcome to KrankenHaus - 2000\n");
            Console.WriteLine("How many patients would you like to start with?");
            AmountOfPatients = int.Parse(Console.ReadLine());
            Console.WriteLine("How many doctors would you like to start with?");
            AmountOfDoctors = int.Parse(Console.ReadLine());
            Console.WriteLine("What difficulty level would you like to start with?\n" +
                "[EASY]---[MEDIUM]---[HARD]\n" +
                "--[1]-------[2]-------[3]--");
            switch (Console.ReadLine())
            {
                case "1":
                    IVAIncrease = 0;
                    IVADecrease = 10;
                    SanatoriumIncrease = 30;
                    SanatoriumDecrease = 50;
                    QueueIncrease = 50;
                    QueueDecrease = 51;
                    break;
                case "2":
                    IVAIncrease = 10;
                    IVADecrease = 30;
                    SanatoriumIncrease = 50;
                    SanatoriumDecrease = 65;
                    QueueIncrease = 80;
                    QueueDecrease = 95;
                    break;
                case "3":
                    IVAIncrease = 25;
                    IVADecrease = 50;
                    SanatoriumIncrease = 65;
                    SanatoriumDecrease = 75;
                    QueueIncrease = 95;
                    QueueDecrease = 96;
                    break;
                default:
                    break;
            }
        }
    }
}