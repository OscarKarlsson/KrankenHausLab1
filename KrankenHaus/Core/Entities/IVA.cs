using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class IVA
    {
        public List<Patient> PatientsInIVA { get; private set; } = new List<Patient>();
        public Doctor CurrentDoctorIVA { get; private set; }
        public void SetCurrentDoctor(Doctor doctor)
        {
            this.CurrentDoctorIVA = doctor;
        }
        public void RemoveCurrentDoctor()
        {
            this.CurrentDoctorIVA = null;
        }
    }
}
