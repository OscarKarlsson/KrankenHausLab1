using Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    class Doctor : DataGenerator
    {
        public Doctor()
        {
            Name = GenerateName();
            FatigueLevel = 0;
            SkillLevel = GenerateInt(-10, 30);
            Burned = false;
        }
        public string Name { get; set; }
        public int FatigueLevel { get; set; }
        public int SkillLevel { get; set; }
        public bool Burned { get; set; }
    }
}
