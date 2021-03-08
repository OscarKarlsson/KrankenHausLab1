using Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Doctor : DataGenerator
    {
        public Doctor()
        {
            Name = GenerateName();
            FatigueLevel = 0;
            SkillLevel = GenerateInt(-10, 30);
            Burned = false;
            Department = Departments.QUEUE;
        }
        public string Name { get; set; }
        public int FatigueLevel { get; set; }
        public int SkillLevel { get; set; }
        public bool Burned { get; set; }
        public Departments Department { get; set; }
        public override string ToString()
        {
            return $"Name: {Name}\nFatigue: {FatigueLevel}\nSkill: {SkillLevel}";
        }
    }
}
