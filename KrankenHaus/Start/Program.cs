using System;
using Simulation;

namespace Start
{
    class Program
    {
        static void Main(string[] args)
        {

            var simulator = new Simulator();
            simulator.StartSimulation();
            Console.ReadLine();



            //TestClass test = new TestClass();
            //test.EntityTest();
        }
    }
}