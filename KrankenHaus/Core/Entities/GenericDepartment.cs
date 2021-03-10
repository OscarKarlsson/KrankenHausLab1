using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class GenericDepartment<T>
    {
        private T[] patients;
        private Doctor currentDoctor;
        private int rateOfReduction;
        private int rateOfStability;
        private int rateOfIncrease;
        public GenericDepartment(int size, int reduction, int stability, int increase)
        {
            patients = new T[size];
            this.rateOfReduction = reduction;
            this.rateOfStability = stability;
            this.rateOfIncrease = increase;
        }
        public void ChangeSicknessLevel(int randomNumber)
        {
            foreach (var patient in patients)
            {                
                (Patient)patient.
            }
        }
    }
}
