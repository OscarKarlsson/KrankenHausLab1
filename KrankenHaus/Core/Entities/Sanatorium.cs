using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Sanatorium
    {
        public List<Patient> PatientsSanatorium { get; private set; } = new List<Patient>();
        public Doctor CurrentDoctorSanatorium { get; private set; }
        public void SetCurrentDoctor(Doctor doctor)
        {
            this.CurrentDoctorSanatorium = doctor;
        }
        public void RemoveCurrentDoctor()
        {
            this.CurrentDoctorSanatorium = null;
        }
    }
}
