using Logic.Entities.Person_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtensionMethods
{
    public static class MechanicSkill
    {
        public static void UpdateMechanicSkill(this Mechanic mec)
        {
            mec.SkillLista = new List<string>();
            if (mec.Breaks == true)
            {
                mec.SkillLista.Add("Bromsar");
            }
            if (mec.Engine == true)
            {
                mec.SkillLista.Add("Motor");
            }
            if (mec.Carbody == true)
            {
                mec.SkillLista.Add("Kaross");
            }
            if (mec.Windshield == true)
            {
                mec.SkillLista.Add("Vindruta");
            }
            if (mec.Tyre == true)
            {
                mec.SkillLista.Add("Däck");
            }
        }
    }
}
