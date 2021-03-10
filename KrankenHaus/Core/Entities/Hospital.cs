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
        public Hospital()
        {
            for (int i = 0; i < 100; i++)
            {
                this.PatientsQueue?.Enqueue(new Patient());
            }
            for (int i = 0; i < 10; i++)
            {
                this.DoctorsQueue?.Enqueue(new Doctor());
            }
        }
        public Doctor GetDoctor()
        {
            return DoctorsQueue.Dequeue();
        }
        public Patient GetPatient()
        {
            if (PatientsQueue.Count != 0)
            {
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
        public List<int> UpdateSicknessQueue()
        {            
            Random rnd = new Random();
            List<int> sicknessLevels = new List<int>();
            int sickLevel;
            int randomNumber;
            
            foreach (var patients in PatientsQueue)
            {
                randomNumber = rnd.Next(1, 101);

                if (randomNumber <= 80)
                {
                    patients.UpdateSicknessLevel(1);
                }
                else if (randomNumber >= 81 && randomNumber <= 95)
                {
                }
                else if (randomNumber >= 96)
                {
                    patients.UpdateSicknessLevel(-1);
                }

                sickLevel = patients.CheckSicknessLevel();
                if (sickLevel != 0)
                {
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
