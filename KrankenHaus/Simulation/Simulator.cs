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
            Testar testar = new Testar();
            testar.EntityTest(hospital);
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
            if (hospital.CurrentDoctor == null)
            {
                if (hospital.DoctorsList.Count!= 0)
                {
                    hospital.DoctorsList[0].Department = Departments.IVA;
                    hospital.CurrentDoctor = hospital.DoctorsList[0];
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
                for (int i = 0; i < hospital.DoctorsList.Count; i++)
                {
                    if (hospital.CurrentDoctor.DoctorID == hospital.DoctorsList[i].DoctorID)
                    {
                        hospital.DoctorsList.RemoveAt(i);
                    }
                }
                hospital.CurrentDoctor = null;
            }
        }
    }
}
