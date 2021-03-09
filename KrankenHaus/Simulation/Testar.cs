using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Simulation
{
    class Testar
    {
        public void EntityTest(Hospital hp)
        {
            

            foreach (var patient in hp.Patients)
            {
                Console.WriteLine(patient.ToString());
            }

           
            Console.ReadLine();
        }

    }
}
