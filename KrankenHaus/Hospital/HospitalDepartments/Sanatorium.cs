using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.HospitalDepartments
{
    class Sanatorium : Hospital
    {
        // Sanatoriet har 10 platser
        // För varje justering av sjukdomsnivån så har patienter på sanatoriet 50 % risk att få en höjd sjukdomsnivå
        // 15 % chans att sjukdomsnivån kvarstår
        // 35 % chans att sjukdomsnivån minskar
        public List<Patient> PatientsSanatorium { get; set; }
    }
}
