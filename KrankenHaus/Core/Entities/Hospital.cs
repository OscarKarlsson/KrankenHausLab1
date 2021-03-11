using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Core.Entities
{
    public class Hospital
    {
        // För varje justering av sjukdomsnivån har patienter i kön 80 % risk att få en höjd 
        // sjukdomsnivå, 15% chans att sjukdomsnivån kvarstår, 5% chans till att naturlig 
        // tillfriskning sker. 
        private Queue<Patient> PatientsQueue { get; set; } = new Queue<Patient>();
        private Queue<Doctor> DoctorsQueue { get; set; } = new Queue<Doctor>();
        private List<int> TimeInQueue { get; set; } = new List<int>();
        private int RiskIncrease { get; set; }
        private int ChanceDecrease { get; set; }

        public List<Patient> TempPatients { get; set; } = new List<Patient>();
        //public event EventHandler<PatientDeadOrAliveEventArgs> DeadOrAliveHandler;
        //PatientDeadOrAliveEventArgs patientSend = new PatientDeadOrAliveEventArgs();
        public Hospital(int patients, int doctors, int risk, int chance)
        {
            for (int i = 0; i < patients; i++)
            {
                this.PatientsQueue?.Enqueue(new Patient());
            }
            for (int i = 0; i < doctors; i++)
            {
                this.DoctorsQueue?.Enqueue(new Doctor());
            }
            RiskIncrease = risk;
            ChanceDecrease = chance;
        }
        public int CalculateAvgTime()
        {
            int TotalTicks = 0;
            for (int i = 0; i < TimeInQueue.Count; i++)
            {
                TotalTicks += TimeInQueue[i];
            }
            return TotalTicks / TimeInQueue.Count;
        }
        public Doctor GetDoctor()
        {
            return DoctorsQueue.Dequeue();
        }
        public Patient GetPatient()
        {
            if (PatientsQueue.Count != 0)
            {
                TimeInQueue.Add(PatientsQueue.Peek().GetTimeInQueue());
                return PatientsQueue.Dequeue();
            }
            return null;
        }
        public int GetCountOfDoctor()
        {
            return DoctorsQueue.Count();
        }
        public int GetCountOfPatient()
        {
            return PatientsQueue.Count();
        }
        public void UpdateTickCount()
        {
            foreach (var patient in PatientsQueue)
            {
                patient.UpdateTickQueue();
            }
        }
        public List<int> UpdateSicknessQueue()
        {            
            Random rnd = new Random();
            List<int> sicknessLevels = new List<int>();
            int sickLevel;
            int randomNumber;
            
            foreach (var patients in PatientsQueue.ToArray())
            {
                
                randomNumber = rnd.Next(1, 101);

                if (randomNumber <= RiskIncrease)
                {
                    patients.UpdateSicknessLevel(1);
                }
                else if (randomNumber >= ChanceDecrease)
                {
                    patients.UpdateSicknessLevel(-1);
                }

                sickLevel = patients.CheckSicknessLevel();
                if (sickLevel != 0)
                {
                    TempPatients.Add(patients);                    
                    TimeInQueue.Add(patients.GetTimeInQueue());
                    sicknessLevels.Add(sickLevel);
                    RemoveSpecificPatient(patients);
                }
            }
            return sicknessLevels;
        }
        public void RemoveSpecificPatient(Patient patient)
        {
            PatientsQueue = new Queue<Patient>(PatientsQueue.Where(x => x != patient));
        } 
    }
}
