using Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities   
{
    public enum Departments { QUEUE, IVA, SANATORIUM }
    public class Patient : DataGenerator
    {
        public Patient()
        {
            Name = GenerateName();
            DateOfBirth = GenerateDOB();
            SicknessLevel = GenerateInt(0, 10);
            Department = Departments.QUEUE;
        }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int SicknessLevel { get; set; }
        public Departments Department { get; set; }
        public override string ToString()
        {
            return $"Name: {Name}\nDate of birth: {DateOfBirth}\nSickness level: {SicknessLevel}";
        }
    }
}
