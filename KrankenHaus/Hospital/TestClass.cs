//using Core.Entities;
//using Hospital.HospitalDepartments;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Hospital
//{
//    public class TestClass
//    {
        
//        public void EntityTest()
//        {
//            Hospital testar = new Hospital();
            

//            foreach (var Doctor in testar.DoctorsList)
//            {
//                Console.WriteLine(Doctor);
//            }

//            foreach (var Patient in testar.PatientsInQueue)
//            {
//                Console.WriteLine(Patient);
//            }        

//            List<Patient> patients = new List<Patient>();
//            List<Doctor> doctors = new List<Doctor>();
//            for (int i = 0; i < 10; i++)
//            {
//                patients.Add(new Patient());
//                doctors.Add(new Doctor());
//            }
//            Console.WriteLine("Patients: ");
//            for (int i = 0; i < patients.Count; i++)
//            {
//                Console.WriteLine(patients[i].ToString());
//                Console.WriteLine(" --- ");
//            }
//            Console.WriteLine("Doctors: ");
//            for (int i = 0; i < doctors.Count; i++)
//            {
//                Console.WriteLine(doctors[i].ToString());
//                Console.WriteLine(" --- ");
//            }
//            Console.ReadLine();
//        }
//    }
//}
