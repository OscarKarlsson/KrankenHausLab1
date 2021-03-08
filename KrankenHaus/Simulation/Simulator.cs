using System;
using Core.Entities;

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
            //Kalla på AssignPatients...
            //Kalla på addDoctor...
        }
        public void AssignPatientsToDepartments()
        {
            //Add patients to IVA and Sanatorium
        }
        public void AddDoctorToDepartment()
        {
            //Add doctor to needed department
            //Ska prenumerera på ett event när doktor blir utbränd
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
            if (hospital.CurrentDoctor != null)
            {
                randomNumber += hospital.CurrentDoctor.SkillLevel;
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
            //1-3 ökar fatigue med varje gång metoden körs
        }
    }
}
