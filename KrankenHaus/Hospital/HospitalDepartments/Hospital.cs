using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital
{
    class Hospital
    {
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
        }
    }
}
