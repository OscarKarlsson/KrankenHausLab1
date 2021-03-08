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
            //Kollat mot MaxIva och MaxSanatorium.
            //Add patients to IVA and Sanatorium.
            
            
        }
        public void AddDoctorToDepartment()
        {
            for (int i = 0; i < hospital.DoctorsList.Count; i++)
            {
                if (hospital.CurrentDoctor == null)
                {
                    hospital.CurrentDoctor = hospital.DoctorsList[i];
                    hospital.DoctorsList[i].Department = Departments.IVA;
                }
            }
        }
        public void UpdateSickness()
        {
            //Uppdatera för alla (kö, iva, sanatorium)
        }
        public void UpdateFatigue()
        {
            Random rnd = new Random();
            hospital.CurrentDoctor.FatigueLevel += rnd.Next(1, 3);
            if (hospital.CurrentDoctor.FatigueLevel >= 20)
            {
                hospital.CurrentDoctor.Burned = true;
                hospital.CurrentDoctor = null;
            }
        }
    }
}
