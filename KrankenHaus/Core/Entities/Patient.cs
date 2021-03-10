using Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities   
{
    //public enum Departments { QUEUE, IVA, SANATORIUM, CHECKEDOUT }
    //public enum Status { Recovered, Dead, Sick }

    public class Patient : DataGenerator
    {
        public Patient()
        {
            Name = GenerateName();
            DateOfBirth = GenerateDOB();
            SicknessLevel = GenerateInt(1, 9);
            //Department = Departments.QUEUE;
            //Status = Status.Sick;
        }
        //public Status Status { get; set; }
        private string Name { get; set; }
        private DateTime DateOfBirth { get; set; }
        private int SicknessLevel { get; set; }
        //public Departments Department { get; set; }
        public void UpdateSicknessLevel(int change)
        {
            SicknessLevel += change;
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
                return $"Name: {Name}\nDate of birth: {DateOfBirth}\nSickness level: {SicknessLevel}\n";
        }
    }
}
