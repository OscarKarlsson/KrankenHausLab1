using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital
{
    public class TestClass
    {
        
        public void EntityTest()
        {
            List<Patient> patients = new List<Patient>();
            List<Doctor> doctors = new List<Doctor>();
            for (int i = 0; i < 10; i++)
            {
                patients.Add(new Patient());
                doctors.Add(new Doctor());
            }
            Console.WriteLine("Patients: ");
            for (int i = 0; i < patients.Count; i++)
            {
                Console.WriteLine(patients[i].ToString());
                Console.WriteLine(" --- ");
            }
            Console.WriteLine("Doctors: ");
            for (int i = 0; i < doctors.Count; i++)
            {
                Console.WriteLine(doctors[i].ToString());
                Console.WriteLine(" --- ");
            }
            Console.ReadLine();
        }
    }
}
