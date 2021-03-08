using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital
{
    class Hospital
    {
        // För varje justering av sjukdomsnivån har patienter i kön 80 % risk att få en höjd 
        // sjukdomsnivå, 15% chans att sjukdomsnivån kvarstår, 5% chans till att naturlig 
        // tillfriskning sker. 
        public List<Patient> PatientsInQueue { get; set; }        
        public List<Doctor> DoctorsList { get; set; }
        public Doctor CurrentDoctor { get; set; }
        public int AmountPatientsAfterLife { get; set; }
        public int AmountPatientsRecovered { get; set; }

        public Hospital()
        {
            PatientsInQueue = new List<Patient>();

            for (int i = 0; i < 100; i++)
            {
                PatientsInQueue.Add(new Patient());
            }

            DoctorsList = new List<Doctor>();

            for (int i = 0; i < 10; i++)
            {
                DoctorsList.Add(new Doctor());
            }

            AmountPatientsAfterLife = 0;
            AmountPatientsRecovered = 0;
        }     
    }
}
