using System;
using Core.Entities;
using System.Linq;

namespace Simulation
{
    public class Simulator
    {
        Hospital hospital = new Hospital();
        Random rnd = new Random();
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
        public void AddDoctorToIVA()
        {
            if (hospital.CurrentDoctorIVA == null)
            {
                if (hospital.DoctorsList.Count!= 0)
                {
                    hospital.DoctorsList[0].Department = Departments.IVA;
                    hospital.CurrentDoctorIVA = hospital.DoctorsList[0];
                }
            }
        }
        public void AddDoctorToSanatorium()
        {
            if (hospital.CurrentDoctorSanatorium == null)
            {
                if (hospital.DoctorsList.Count != 0)
                {
                    hospital.DoctorsList[0].Department = Departments.SANATORIUM;
                    hospital.CurrentDoctorSanatorium = hospital.DoctorsList[0];
                }
            }
        }
        public void UpdateSickness()
        {
            //Uppdatera för alla (kö, iva, sanatorium)
            foreach (var patients in hospital.Patients)
            {
                // If patients already sick otherways do not change sickness level
                if (patients.Status == Status.Sick)
                {
                    int randomNumber = rnd.Next(1, 101);
                    if (patients.Department == Departments.QUEUE)
                    {
                        SicknessQueue(randomNumber, patients);
                    }
                    if (patients.Department == Departments.IVA)
                    {
                        SicknessIva(randomNumber, patients);
                    }
                    if (patients.Department == Departments.SANATORIUM)
                    {
                        SicknessSanatorium(randomNumber,patients);
                    }
                }
                if (patients.SicknessLevel >= 10)
                {
                    patients.Status = Status.Dead;
                }
                if (patients.SicknessLevel <= 1)
                {
                    patients.Status = Status.Recovered;
                }
            }
        }
        public void SicknessSanatorium(int randomNumber, Patient patients)
        {
            //a.För varje justering av sjukdomsnivån har patienterpåSanatoriet
            //50 % risk för att fåen höjd sjukdomsnivå, 
            //15 % chans att sjukdomsnivånkvarstår, 
            //35 % att behandlingenhjälper och att sjukdomsnivån minskar
            if (randomNumber <= 50)
            {
                patients.SicknessLevel = patients.SicknessLevel + 1;
            }
            if (randomNumber >= 51 && randomNumber <= 65)
            {
                patients.SicknessLevel = patients.SicknessLevel;
            }
            if (randomNumber >= 66)
            {
                patients.SicknessLevel = patients.SicknessLevel - 1;
            }
        }

        public void SicknessIva(int randomNumber, Patient patients)
        {
            //b.på IVA så är chansen för tillfrisknande ett steg 70%, 
            //20% att sjukdomsnivån äroförändrad och 
            //10% att patienten blir sämre.
            if (hospital.CurrentDoctorIVA != null)
            {
                randomNumber += hospital.CurrentDoctorIVA.SkillLevel;
            }
            if (randomNumber <= 10)
            {
                patients.SicknessLevel = patients.SicknessLevel + 1;
            }
            if (randomNumber >= 11 && randomNumber <= 30)
            {
                patients.SicknessLevel = patients.SicknessLevel;
            }
            if (randomNumber >= 31)
            {
                patients.SicknessLevel = patients.SicknessLevel - 1;
            }
        }

        public void SicknessQueue(int randomNumber, Patient patients)
        {
            //Get rondom number for follow this rules=> 
            //80% risk för att få enhöjd sjukdomsnivå, 
            //15% chans att sjukdomsnivån kvarstår,
            //5% att naturlig tillfriskning sker
            if (randomNumber <= 80)
            {
                patients.SicknessLevel = patients.SicknessLevel + 1;
            }
            if (randomNumber >= 81 && randomNumber <= 95)
            {
                patients.SicknessLevel = patients.SicknessLevel;
            }
            if (randomNumber >= 96)
            {
                patients.SicknessLevel = patients.SicknessLevel - 1;
            }
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
