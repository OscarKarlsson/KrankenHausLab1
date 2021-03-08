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
                if (hospital.DoctorsList[i].Department == Departments.IVA)
                {
                    break;
                }
                else if()
                {

                }
            }

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
