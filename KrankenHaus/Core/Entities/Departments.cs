using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Department
    {
        private List<Patient> patients { get; set; } = new List<Patient>();
        private Doctor currentDoctor { get; set; }
        public bool checkDoktorExist()
        {
            if (currentDoctor == null)
            {
                return false;
            }
            return true;
        }
        public int GetCountOfPatient()
        {
            return patients.Count;
        }
        public void AddPatient(Hospital hospital)
        {
            patients.Add(hospital.GetPatient());
        }
        public void SetCurrentDoctor(Doctor doctor)
        {
            this.currentDoctor = doctor;
        }
        public void RemoveCurrentDoctor()
        {
            this.currentDoctor = null;
        }
    }
}
