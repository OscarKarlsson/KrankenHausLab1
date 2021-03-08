using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Hospital
    {
        // För varje justering av sjukdomsnivån har patienter i kön 80 % risk att få en höjd 
        // sjukdomsnivå, 15% chans att sjukdomsnivån kvarstår, 5% chans till att naturlig 
        // tillfriskning sker. 
        public List<Patient> Patients { get; set; }
        public List<Patient> PatientsTEMP { get; set; }
        public List<Doctor> DoctorsList { get; set; }
        public int AmountPatientsAfterLife { get; set; }
        public int AmountPatientsRecovered { get; set; }

        public Hospital()
        {
            Patients = new List<Patient>();

            for (int i = 0; i < 100; i++)
            {
                Patients.Add(new Patient());
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
