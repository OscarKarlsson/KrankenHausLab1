using System;
using Core.Entities;
using System.Linq;

namespace Simulation
{
    public class Simulator
    {
        Hospital hospital = new Hospital();
        public int MaxIVA = 5;
        public int MaxSanatorium = 10;
        public void StartSimulation()
        {
            //Kalla på AssignPatients...
            //Kalla på addDoctor...
        }
        public void AssignPatientsToDepartments()
        {
            
            //Counting number of patients in IVA.
            int patientsInIva = hospital.Patients.Where(p => p.Department == Departments.IVA).Count();

            //Counting number of patients in Sanitorium.
            int patientsInSanitorium = hospital.Patients.Where(p => p.Department == Departments.IVA).Count();            
            
            //Checking if patientsInIva is less than MaxIVA.
            while (patientsInIva < MaxIVA)
            {
                //Iterating through patients list to find patients in queue. 
                for (int i = 0; i < hospital.Patients.Count; i++)
                {
                    //Changing department to IVA and adding 1 to patientsInIva.
                    if (hospital.Patients[i].Department == Departments.QUEUE)
                    {
                        if (hospital.Patients[i].Status == Status.Sick)
                        {
                            hospital.Patients[i].Department = Departments.IVA;
                            patientsInIva++;
                        }
                        
                    }
                }
            }

            //Checking if patientsInSanitorium is less than MaxSanitorium.
            while (patientsInSanitorium < MaxSanatorium)
            {
                //Iterating through patients list to find patients in queue. 
                for (int i = 0; i < hospital.Patients.Count; i++)
                {
                    //Changing department to Sanitorium and adding 1 to patientsInSanitorium.
                    if (hospital.Patients[i].Department == Departments.QUEUE)
                    {
                        if (hospital.Patients[i].Status == Status.Sick)
                        {
                            hospital.Patients[i].Department = Departments.SANATORIUM;
                            patientsInSanitorium++;
                        }
                        
                    }
                }
            }
        }
        public void AddDoctorToDepartment()
        {
            //Add doctor to needed department
            //Ska prenumerera på ett event när doktor blir utbränd
        }
        public void UpdateSickness()
        {
            //Uppdatera för alla (kö, iva, sanatorium)
        }
        public void UpdateFatigue()
        {
            //1-3 ökar fatigue med varje gång metoden körs
        }
    }
}
