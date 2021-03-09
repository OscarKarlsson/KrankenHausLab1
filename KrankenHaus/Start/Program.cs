using System;
using Simulation;
using PrintManager.Print;

namespace Start
{
    class Program
    {
        static void Main(string[] args)
        {

            var simulator = new Simulator();
            var consoleprint = new ConsolePrint();
            simulator.ReportEventHandler += consoleprint.PrintFiveSeconds;
            simulator.StartSimulation();
            Console.ReadLine();



            //TestClass test = new TestClass();
            //test.EntityTest();
        }
    }
}