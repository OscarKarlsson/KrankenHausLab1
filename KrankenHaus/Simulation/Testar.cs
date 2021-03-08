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
            

            foreach (var doctor in hp.DoctorsList)
            {
                Console.WriteLine(doctor.ToString());
            }

           
            Console.ReadLine();
        }

    }
}
