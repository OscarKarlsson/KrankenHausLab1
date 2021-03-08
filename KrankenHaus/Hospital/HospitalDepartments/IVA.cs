using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.HospitalDepartments
{
    class IVA : Hospital
    {
        //IVA har fem platser
        // För varje justering av sjukdomsnivån så har patienter på IVA 10 % risk att få en höjd sjukdomsnivå
        // 20 % chans att sjukdomsnivån kvarstår
        // 70 % chans att sjukdomsnivån minskar
        public Patient[] PatientsIVA;
        public IVA()
        {

        }

        public Patient[] AddPatientsIVA()
        {            

            return PatientsIVA;
        }

        public void Developement()
        {


        }


    }
}
