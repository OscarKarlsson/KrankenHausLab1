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
        public Patient RemoveSpecificPatient(Patient patient)
        {
            PatientsQueue = new Queue<Patient>(PatientsQueue.Where(x => x != patient));
        }
    }
}
