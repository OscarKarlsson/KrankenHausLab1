
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Department
    {
        Random rnd = new Random();
        private List<Patient> patients { get; set; } = new List<Patient>();
        private List<int> TimeInDepartment { get; set; } = new List<int>();
        private Doctor currentDoctor { get; set; }
        private int RiskIncrease { get; set; }
        private int ChanceDecrease { get; set; }
      
        public Department(int increase, int decrease)
        {
            this.RiskIncrease = increase;
            this.ChanceDecrease = decrease;
        }
        public int CalculateAvgTime()
        {
            int TotalTicks = 0;
            for (int i = 0; i < TimeInDepartment.Count; i++)
            {
                TotalTicks += TimeInDepartment[i];
            }
            return TotalTicks / TimeInDepartment.Count;
        }
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
            var patient = hospital.GetPatient();
            if (patient != null)
            {
                patients.Add(patient);
            }
           
        }
        public List<int> UpdateSickenessLevel()
        {

            List<int> sicknessLevels = new List<int>();
            int sickLevel;
            int randomNumber = 0;
            foreach (var patient in patients.ToArray())
            {
                patient.UpdateTickDepartment();
                if (patient != null)
                {
                    randomNumber = rnd.Next(1, 101);

                    if (currentDoctor != null)
                    {
                        randomNumber += currentDoctor.SkillLevel;
                    }

                    if (randomNumber <= RiskIncrease)
                    {
                        patient.UpdateSicknessLevel(1);

                    }
                    else if (randomNumber >= ChanceDecrease)
                    {
                        patient.UpdateSicknessLevel(-1);
                    }

                    sickLevel = patient.CheckSicknessLevel();
                    if (sickLevel != 0)
                    {
                        TimeInDepartment.Add(patient.GetTimeAtDepartment());
                        sicknessLevels.Add(sickLevel);
                        patients.Remove(patient);
                    }
                }
            }
            return sicknessLevels;
        }
        public void SetCurrentDoctor(Doctor doctor)
        {
            this.currentDoctor = doctor;
        }
        public void RemoveCurrentDoctor()
        {
            this.currentDoctor = null;
        }
        public void UpdateFatigueLevel(int increaseFatigue)
        {
            currentDoctor.FatigueLevel += increaseFatigue;
            if (currentDoctor.FatigueLevel >= 20)
            {
                currentDoctor = null;
            }
        }

    }
}
