using System;
using Core.Entities;
using System.Linq;
using System.Threading;
using Simulation.Eventargs;
using System.Collections.Generic;

namespace Simulation
{
    public class Simulator
    {
        Hospital hospital = new Hospital();
        IVA IVADept = new IVA();
        Sanatorium sanatoriumDept = new Sanatorium();
        static Timer TickSecond = null;
        static Timer TickFiveSecond = null;
        Testar testar = new Testar();
        Random rnd = new Random();
        public int MaxIVA = 5;
        public int MaxSanatorium = 10;
        
        public event EventHandler<ReportEventArgs> ReportEventHandler;
        ReportEventArgs allinfo = new ReportEventArgs();

        public void OnceADay(object state)
        {
            Thread updateSicknessQueue = new Thread(UpdateSicknessQueue);
            updateSicknessQueue.Start();
           
            allinfo.AmountDoctorsWaiting = hospital.DoctorsQueue.Count();
            allinfo.AmountIVA = IVADept.PatientsInIVA.Count();
            allinfo.AmountSanatorium = sanatoriumDept.PatientsSanatorium.Count();
            allinfo.AmountPatientsInQueue = hospital.PatientsQueue.Count();
            Console.ForegroundColor = ConsoleColor.Cyan;
            ReportEventHandler?.Invoke(this, allinfo);
            Console.ResetColor();
        }
        public void Second(object state)
        {
            Thread updateFatigue = new Thread(UpdateFatigue);
            Thread updateSickness = new Thread(UpdateSicknessDept);
            Thread addPatient = new Thread(AssignPatientsToDepartments);

            updateFatigue.Start();
            updateFatigue.Join();
            addPatient.Start();
            updateSickness.Start();
            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString());

            //Console.WriteLine("---------------------------------------------------------------------------------------");   
        }
        public void StartSimulation()
        {
            
            TickSecond = new Timer(new TimerCallback(Second), null, 1000, 1000);
            TickFiveSecond = new Timer(new TimerCallback(OnceADay), null, 3000, 3000);

            while (hospital.PatientsQueue.Count != 0 || IVADept.PatientsInIVA.Count != 0 || sanatoriumDept.PatientsSanatorium.Count != 0)
            {
                Thread.Sleep(1000);
            }
            
            TickSecond.Dispose();
            TickFiveSecond.Dispose();
            Console.WriteLine("End of bi...!");

        }

        public void AssignPatientsToDepartments()
        {
            int patientsIVA = IVADept.GetCountOfPatient();
            int patientsSanatorium = sanatoriumDept.GetCountOfPatient();
            while (patientsIVA < MaxIVA)
            {
                IVADept.AddPatient(hospital);
                patientsIVA++;
            }
            while (patientsSanatorium < MaxSanatorium)
            {
                sanatoriumDept.AddPatient(hospital);
                patientsSanatorium++;
            }
        }
        public void AddDoctorToIVA()
        {
            if (!IVADept.checkDoktorExist() && hospital.GetCountOfDoctor() != 0)
            {
                IVADept.SetCurrentDoctor(hospital.GetDoctor());
            }
        }
        public void AddDoctorToSanatorium()
        {
            if (!sanatoriumDept.checkDoktorExist() && hospital.GetCountOfDoctor() != 0)
            {
                sanatoriumDept.SetCurrentDoctor(hospital.GetDoctor());
            }
        }
        public void UpdateSicknessDept()
        {
            SicknessIva();
            SicknessSanatorium();
        }
        public void UpdateSicknessQueue()
        {
            foreach (var patients in hospital.PatientsQueue)
            {
                int randomNumber = rnd.Next(1, 101);
                if (randomNumber <= 80)
                {
                    patients.SicknessLevel += 1;
                }
                else if (randomNumber >= 81 && randomNumber <= 95)
                {
                }
                else if (randomNumber >= 96)
                {
                    patients.SicknessLevel -= 1;
                }
                CheckSicknessLevel(patients);
            }
        }

        public void CheckSicknessLevel(Patient patients)
        {
            if (patients.SicknessLevel >= 10)
            {
                hospital.RemoveSpecificPatient(patients);
                allinfo.AmountDead++;
            }
            else if (patients.SicknessLevel <= 0)
            {
                allinfo.AmountRecovered++;
                hospital.RemoveSpecificPatient(patients);
            }
        }
        public void CheckSicknessLevelIVA(Patient patients)
        {
            if (patients.SicknessLevel >= 10)
            {
                allinfo.AmountDead++;
                IVADept.PatientsInIVA.Remove(patients);
            }
            else if (patients.SicknessLevel <= 0)
            {
                allinfo.AmountRecovered++;
                IVADept.PatientsInIVA.Remove(patients);
            }
        }
        public void CheckSicknessLevelSanatorium(Patient patients)
        {
            if (patients.SicknessLevel >= 10)
            {
                allinfo.AmountDead++;
                sanatoriumDept.PatientsSanatorium.Remove(patients);
            }
            else if (patients.SicknessLevel <= 0)
            {
                allinfo.AmountRecovered++;
                sanatoriumDept.PatientsSanatorium.Remove(patients);
            }
        }
        public void SicknessSanatorium()
        {
            // För varje justering av sjukdomsnivån har patienterpåSanatoriet
            // 50 % risk för att fåen höjd sjukdomsnivå, 
            // 15 % chans att sjukdomsnivånkvarstår, 
            // 35 % att behandlingenhjälper och att sjukdomsnivån minskar
            int randomNumber = rnd.Next(1, 101);
            foreach (var patients in sanatoriumDept.PatientsSanatorium.ToList())
            {
                if (sanatoriumDept.CurrentDoctorSanatorium != null)
                {
                    randomNumber += sanatoriumDept.CurrentDoctorSanatorium.SkillLevel;
                }
                if (randomNumber <= 50)
                {
                    patients.SicknessLevel = patients.SicknessLevel + 1;
                }
                else if (randomNumber >= 51 && randomNumber <= 65)
                {
                    patients.SicknessLevel = patients.SicknessLevel;
                }
                else if (randomNumber >= 66)
                {
                    patients.SicknessLevel = patients.SicknessLevel - 1;
                }
                CheckSicknessLevel(patients);
            }

        }
        public void SicknessIva()
        {
            //b.på IVA så är chansen för tillfrisknande ett steg 70%, 
            //20% att sjukdomsnivån äroförändrad och 
            //10% att patienten blir sämre.
            int randomNumber = rnd.Next(1, 101);
            foreach (var patients in IVADept.PatientsInIVA.ToList())
            {
                if (IVADept.CurrentDoctorIVA != null)
                {
                    randomNumber += IVADept.CurrentDoctorIVA.SkillLevel;
                }
                if (randomNumber <= 10)
                {
                    patients.SicknessLevel = patients.SicknessLevel + 1;
                }
                else if (randomNumber >= 11 && randomNumber <= 30)
                {
                    patients.SicknessLevel = patients.SicknessLevel;
                }
                else if (randomNumber >= 31)
                {
                    patients.SicknessLevel = patients.SicknessLevel - 1;
                }
                CheckSicknessLevel(patients);
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
            if (IVADept.CurrentDoctorIVA != null)
            {
                IVADept.CurrentDoctorIVA.FatigueLevel += rnd.Next(1, 4);
                
                if (IVADept.CurrentDoctorIVA.FatigueLevel >= 20)
                {
                    IVADept.RemoveCurrentDoctor();
                    AddDoctorToIVA();
                }
            }
        }
        public void UpdateFatigueSanatorium()
        {
            AddDoctorToSanatorium();

            if (sanatoriumDept.CurrentDoctorSanatorium != null)
            {
                sanatoriumDept.CurrentDoctorSanatorium.FatigueLevel += rnd.Next(1, 4);
                //Console.WriteLine($"{hospital.CurrentDoctorSanatorium.Name} fatigue: {hospital.CurrentDoctorSanatorium.FatigueLevel} Sanatorium");
                if (sanatoriumDept.CurrentDoctorSanatorium.FatigueLevel >= 20)
                {
                    sanatoriumDept.RemoveCurrentDoctor();
                    AddDoctorToSanatorium();
                }
            }
        }
    }
}
