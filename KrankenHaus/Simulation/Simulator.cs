using System;

using System.Linq;
using System.Threading;
using System.Collections.Generic;
using Core.Entities;
using Simulation.Eventargs;

namespace Simulation
{
    public class Simulator
    {
        public int MyProperty { get; set; }
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
        public DateTime SimulationStart { get; set; }
        Random rnd = new Random();
        public int MaxIVA = 5;
        public int MaxSanatorium = 10;
        public event EventHandler<FinishedSimulationEventArgs> FinishEventHandler;
        public event EventHandler<ReportEventArgs> ReportEventHandler;
        ReportEventArgs allinfo = new ReportEventArgs();
        FinishedSimulationEventArgs finishedInfo = new FinishedSimulationEventArgs();

        public event EventHandler<PatientDeadOrAliveEventArgs> DeadOrAliveHandler;
        PatientDeadOrAliveEventArgs patientSend = new PatientDeadOrAliveEventArgs();

        public void OnceADay(object state)
        {
            Thread updateSicknessQueue = new Thread(UpdateSicknessQueue);
            updateSicknessQueue.Start();

            allinfo.AmountDoctorsWaiting = hospital.GetCountOfDoctor();
            allinfo.AmountIVA = IVADept.GetCountOfPatient();
            allinfo.AmountSanatorium = sanatoriumDept.GetCountOfPatient();
            allinfo.AmountPatientsInQueue = hospital.GetCountOfPatient();          
           
            ReportEventHandler?.Invoke(this, allinfo);
            
        }
        public void Second(object state)
        {
            Thread updateFatigue = new Thread(UpdateFatigue);

            Thread updateSickness = new Thread(() => { UpdateSicknessIVA(); UpdateSicknessSanatorium(); hospital.UpdateTickCount(); });
           
            Thread addPatient = new Thread(AssignPatientsToDepartments);

            finishedInfo.TotalTicks++;
            updateSickness.Priority = ThreadPriority.Highest;

            updateFatigue.Start();
            updateFatigue.Join();
            addPatient.Start();
            addPatient.Join();
            
            updateSickness.Start();
        }
        public void StartSimulation()
        {            
            SimulationStart = DateTime.Now;
            TickSecond = new Timer(new TimerCallback(Second), null, 1000, 2000);
            TickFiveSecond = new Timer(new TimerCallback(OnceADay), null, 6000, 6000);

            while (hospital.GetCountOfPatient() != 0 || IVADept.GetCountOfPatient() != 0 || sanatoriumDept.GetCountOfPatient() != 0)
            {
                Thread.Sleep(1000);
            }
            
            TickSecond.Dispose();
            Thread.Sleep(4000);
            TickFiveSecond.Dispose();
            AssignValueToFinished();
            FinishEventHandler?.Invoke(this, finishedInfo);
        }
        public void AssignValueToFinished()
        {
            finishedInfo.AvgTimeInIVA = IVADept.CalculateAvgTime();
            finishedInfo.AvgTimeInSanatorium = sanatoriumDept.CalculateAvgTime();
            finishedInfo.AvgTimeInQueue = hospital.CalculateAvgTime();
            finishedInfo.SimulationStart = SimulationStart;
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
            InvokeEvent(hospital.TempPatients);
            hospital.TempPatients.Clear();
        }       
        public void UpdateSicknessSanatorium()
        {
            List<int> deadOrAlive = sanatoriumDept.UpdateSickenessLevel();
            UpdateReportEventArgs(deadOrAlive);
            InvokeEvent(sanatoriumDept.TempPatients);
            sanatoriumDept.TempPatients.Clear();
        }
        public void UpdateSicknessIVA()
        {  
            
            List<int> deadOrAlive = IVADept.UpdateSickenessLevel();
            UpdateReportEventArgs(deadOrAlive);
            InvokeEvent(IVADept.TempPatients);
            IVADept.TempPatients.Clear();
        }
        public void InvokeEvent(List<Patient> tempList)
        {
            foreach (var patients in tempList)
            {
                patientSend.Patient = patients;
                DeadOrAliveHandler?.Invoke(this, patientSend);
            }             
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
