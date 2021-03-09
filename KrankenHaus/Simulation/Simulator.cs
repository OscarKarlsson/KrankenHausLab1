using System;
using Core.Entities;
using System.Linq;
using System.Threading;

namespace Simulation
{
    public class Simulator
    {
        Hospital hospital = new Hospital();
        static Timer TickSecond = null;
        static Timer TickFiveSecond = null;
        Testar testar = new Testar();
        Random rnd = new Random();
        public int MaxIVA = 5;
        public int MaxSanatorium = 10;
        
        public void Second(object state)
        {

            //Thread updateFatigue = new Thread(UpdateFatigue);

            //Thread updateSickness = new Thread(UpdateSickness);
            //Thread assignPatientsToDepartments = new Thread(AssignPatientsToDepartments);


            //updateFatigue.Priority = ThreadPriority.Highest;
            //updateFatigue.Start();


            //updateSickness.Start();
            //assignPatientsToDepartments.Start();
            AssignPatientsToDepartments();
            UpdateFatigue();
            UpdateSickness();


            //testar.EntityTest(hospital);
            Console.WriteLine("---------------------------------------------------------------------------------------");
            
        }
        public void StartSimulation()
        {

            TickSecond = new Timer(new TimerCallback(Second), null, 1000, 2000);
            Thread.Sleep(40000);
                
            Console.WriteLine("End of bi...!");

            //Kalla på AssignPatients...
            //Kalla på addDoctor...
        }
        public void AssignPatientsToDepartments()
        {

            //Counting number of patients in IVA.
            int patientsInIva = hospital.Patients.Where(p => p.Department == Departments.IVA).Count();

            //Counting number of patients in Sanitorium.
            int patientsInSanitorium = hospital.Patients.Where(p => p.Department == Departments.SANATORIUM).Count();

            //Checking if patientsInIva is less than MaxIVA.

            //Iterating through patients list to find patients in queue. 
            for (int i = 0; i < hospital.Patients.Count && patientsInIva < MaxIVA; i++)
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

            //Checking if patientsInSanitorium is less than MaxSanitorium.

            //Iterating through patients list to find patients in queue. 
            for (int i = 0; i < hospital.Patients.Count && patientsInSanitorium < MaxSanatorium; i++)
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
        //public void AddDoctor()
        //{
        //    AddDoctorToIVA();
        //    AddDoctorToSanatorium();
        //}
        public void AddDoctorToIVA()
        {
            if (hospital.CurrentDoctorIVA == null && hospital.DoctorsList.Count != 0)
            {
                if (hospital.DoctorsList.Count != 0)
                {
                    hospital.DoctorsList[0].Department = Departments.IVA;
                    hospital.CurrentDoctorIVA = hospital.DoctorsList[0];
                    hospital.DoctorsList.RemoveAt(0);
                }
            }
        }
        public void AddDoctorToSanatorium()
        {
            if (hospital.CurrentDoctorSanatorium == null && hospital.DoctorsList.Count != 0)
            {
                if (hospital.DoctorsList.Count != 0)
                {
                    hospital.DoctorsList[0].Department = Departments.SANATORIUM;
                    hospital.CurrentDoctorSanatorium = hospital.DoctorsList[0];

                    hospital.DoctorsList.RemoveAt(0);
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
                        SicknessSanatorium(randomNumber, patients);
                    }
                }
                if (patients.SicknessLevel >= 10)
                {
                    patients.Status = Status.Dead;
                    patients.Department = Departments.CHECKEDOUT;
                }
                if (patients.SicknessLevel <= 0)
                {
                    patients.Status = Status.Recovered;
                    patients.Department = Departments.CHECKEDOUT;
                }
            }
        }
        public void SicknessSanatorium(int randomNumber, Patient patients)
        {
            //a.För varje justering av sjukdomsnivån har patienterpåSanatoriet
            //50 % risk för att fåen höjd sjukdomsnivå, 
            //15 % chans att sjukdomsnivånkvarstår, 
            //35 % att behandlingenhjälper och att sjukdomsnivån minskar
            if (hospital.CurrentDoctorSanatorium != null)
            {
                randomNumber += hospital.CurrentDoctorSanatorium.SkillLevel;
            }
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

            UpdateFatigueIVA();
            UpdateFatigueSanatorium();
        }
        public void UpdateFatigueIVA()
        {
            AddDoctorToIVA();
            if (hospital.CurrentDoctorIVA != null)
            {
                hospital.CurrentDoctorIVA.FatigueLevel += rnd.Next(1, 10);
                Console.WriteLine($"{hospital.CurrentDoctorIVA.Name} fatigue: {hospital.CurrentDoctorIVA.FatigueLevel} IVA");
                if (hospital.CurrentDoctorIVA.FatigueLevel >= 20)
                {
                    Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                    Console.WriteLine(hospital.CurrentDoctorIVA.Name);
                    Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                    hospital.CurrentDoctorIVA = null;
                    AddDoctorToIVA();
                }
            }
        }
        public void UpdateFatigueSanatorium()
        {
            AddDoctorToSanatorium();

            if (hospital.CurrentDoctorSanatorium != null)
            {
                hospital.CurrentDoctorSanatorium.FatigueLevel += rnd.Next(1, 10);
                Console.WriteLine($"{hospital.CurrentDoctorSanatorium.Name} fatigue: {hospital.CurrentDoctorSanatorium.FatigueLevel} Sanatorium");
                if (hospital.CurrentDoctorSanatorium.FatigueLevel >= 20)
                {
                    Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                    Console.WriteLine(hospital.CurrentDoctorSanatorium.Name);
                    Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                    hospital.CurrentDoctorSanatorium = null;
                    AddDoctorToSanatorium();
                }
            }
        }
    }
}
