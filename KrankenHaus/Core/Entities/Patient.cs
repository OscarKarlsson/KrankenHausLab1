using Core.Data;
using System;


namespace Core.Entities
{
    
    public class Patient : DataGenerator
    {       
        public Patient()
        {
            Name = GenerateName();
            DateOfBirth = GenerateDOB();
            SicknessLevel = GenerateInt(1, 9);
         
        }
              
        private string Name { get; set; }
        private DateTime DateOfBirth { get; set; }
        private int TimeInQueue { get; set; }
        private int TimeAtDepartment { get; set; }
        private int SicknessLevel { get; set; }

        public void UpdateTickQueue()
        {
            TimeInQueue++;
        }
        public void UpdateTickDepartment()
        {
            TimeAtDepartment++;
        }
        public void UpdateSicknessLevel(int change)
        {
            SicknessLevel += change;
        }
        public int GetTimeInQueue()
        {
            return TimeInQueue;
        }
        public int GetTimeAtDepartment()
        {
            return TimeAtDepartment;
        }
        public int CheckSicknessLevel()
        {
            if (SicknessLevel == 0)
            {
                return 1;
            }
            else if (SicknessLevel == 10)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
        public override string ToString()
        {
                return $"{Name}";
        }
    }
}
