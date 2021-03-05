using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Data
{
    public class DataGenerator
    {
        static Random rnd = new Random();
        private string[] firstName = { "Oscar", "Karl", "Julius", "Bengt", "Sara", "Erika", "Sören", "Madelene",
            "Yumit", "Sigrid", "Herman", "Desiré", "Emanuel" };
        private string[] lastName = { "Karlsson", "Holstensson", "Bengtsson", "Friberg",
            "Svensson", "Göransson", "Backman", "Johansson" };

        internal string GenerateName()
        {
            return $"{firstName[rnd.Next(0, firstName.Length)]} {lastName[rnd.Next(0, lastName.Length)]}";
        }
        internal DateTime GenerateDOB()
        {
            return new DateTime(1940, 1, 1).AddDays(rnd.Next(1, 21900));
        }
        internal int GenerateInt(int low, int high)
        {
            return rnd.Next(low, high);
        }
    }
}
