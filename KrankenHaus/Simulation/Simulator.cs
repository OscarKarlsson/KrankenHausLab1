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
        public Simulator(int IVAD, int IVAI, int SanaD, int SanaI, int Patients, int Doctors, int QueI, int QueD)
        {
            hospital = new Hospital(Patients, Doctors, QueI, QueD);
            IVADept = new IVA(IVAI, IVAD);
            sanatoriumDept = new Sanatorium(SanaI, SanaD);
        }
        Hospital hospital;
        IVA IVADept;
        Sanatorium sanatoriumDept;
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

            allinfo.AmountDoctorsWaiting = hospital.GetCountOfDoctor();
            allinfo.AmountIVA = IVADept.GetCountOfPatient();
            allinfo.AmountSanatorium = sanatoriumDept.GetCountOfPatient();
            allinfo.AmountPatientsInQueue = hospital.GetCountOfPatient();
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            ReportEventHandler?.Invoke(this, allinfo);
            Console.ResetColor();
        }
        public void Second(object state)
        {
            Thread updateFatigue = new Thread(UpdateFatigue);
            //Thread updateSicknessQueue = new Thread(UpdateSicknessQueue);
            //Thread updateSickness = new Thread(UpdateSicknessDepartments);
            Thread updateSickness = new Thread(() => { UpdateSicknessIVA(); UpdateSicknessSanatorium(); });
            //hread updateSicknessIVA = new Thread(UpdateSicknessIVA);
            //Thread updateSicknessSanatorium = new Thread(UpdateSicknessSanatorium);             
            Thread addPatient = new Thread(AssignPatientsToDepartments);
            allinfo.TickCount++;

            updateFatigue.Start();
            updateFatigue.Join();
            addPatient.Start();
            addPatient.Join();
            //updateSicknessQueue.Start();
            //updateSicknessQueue.Join();
            updateSickness.Start();
           //updateSickness.Join();
            //updateSicknessIVA.Start();
            //updateSicknessSanatorium.Start();
            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString());

            //Console.WriteLine("---------------------------------------------------------------------------------------");   
        }
        public void StartSimulation()
        {
            
            TickSecond = new Timer(new TimerCallback(Second), null, 1000, 1000);
            TickFiveSecond = new Timer(new TimerCallback(OnceADay), null, 3000, 3000);

            while (hospital.GetCountOfPatient() != 0 || IVADept.GetCountOfPatient() != 0 || sanatoriumDept.GetCountOfPatient() != 0)
            {
                Thread.Sleep(1000);
            }
            
            TickSecond.Dispose();
            Thread.Sleep(4000);
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
        public void UpdateSicknessQueue()
        {
            
            List<int> deadOrAlive = hospital.UpdateSicknessQueue();
            UpdateReportEventArgs(deadOrAlive);
        }               
       
        public void UpdateSicknessSanatorium()
        {
            List<int> deadOrAlive = sanatoriumDept.UpdateSickenessLevel();
            UpdateReportEventArgs(deadOrAlive);
        }
        public void UpdateSicknessIVA()
        {
            List<int> deadOrAlive = IVADept.UpdateSickenessLevel();
            UpdateReportEventArgs(deadOrAlive);
        }
        public void UpdateFatigue()
        {
            UpdateFatigueIVA();
            UpdateFatigueSanatorium();
        }
        public void UpdateFatigueIVA()
        {            
            if (IVADept.checkDoktorExist())
            {
                IVADept.UpdateFatigueLevel(rnd.Next(1, 4));                
            }
            AddDoctorToIVA();
        }
        public void UpdateFatigueSanatorium()
        {
            if (sanatoriumDept.checkDoktorExist())
            {
                sanatoriumDept.UpdateFatigueLevel(rnd.Next(1, 4));
            }
            AddDoctorToSanatorium();
        }
        public void UpdateReportEventArgs(List<int> DeadOrAlive)
        {
            foreach (var item in DeadOrAlive)
            {
                if (item == 1)
                {
                    allinfo.AmountRecovered++;
                }
                else if (item == -1)
                {
                    allinfo.AmountDead++;
                }
            }
        }
    }
}
