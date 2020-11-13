using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic.Entities.Person_Entities;
using Logic.Entities.Vehicles_Entities;

namespace Logic
{
   public static class MechanicSkill
   {
        public static bool AddMechanicErrandList(Mechanic objMechanic, CommonView objCommonView)
        {
            if (objMechanic.MechanicProgressList.Count != 2) //Så att man inte kan tilldela mer än 2 ärenden.
            {
                //Denna ser till att GUI håller sig uppdaterad
                objCommonView.ChangeMechanicID = objMechanic.ID;
                objCommonView.ChangeName = objMechanic.Name;
                objCommonView.ChangeStatus = "Pågående";

                //Denna ser till att Datan sparas.
                var objErrands = ErrandList.ErrandsList.Where(x => x.ID == objCommonView.ErrandID);
                foreach (var item in objErrands)
                {
                    item.MechanicID = objMechanic.ID;
                    item.Status = "Pågående";
                    objMechanic.ErrandID.Add(item.ID);

                    //Funkar
                    MechanicSkill.AddProgressList(objMechanic, item.ID.ToString());
                }

                return true;
            }
            else
                return false;

        }

        public static bool ChangeMechanicStatus(CommonView objCommonView, Mechanic objMechanic, string newStatus)
        {
            if (objCommonView.MechanicID != Guid.Empty)
            {
                var objErrands = ErrandList.ErrandsList.Where(x => x.ID == objCommonView.ErrandID);
                foreach (var item in objErrands)
                {
                    MechanicSkill.AddDoneList(objMechanic, item.ID.ToString());
                    item.Status = newStatus;
                }

                objCommonView.ChangeStatus = newStatus;
                return true;
            }
            else
                return false;
        }

        public static void AddAndRemoveMechanicSkill(Mechanic mec)
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

        public static void RemoveFromMechanicProgressList(Mechanic mechanic, string ErrandID)
        {
            mechanic.MechanicProgressList.Remove(ErrandID);
        }

        public static void AddProgressList(Mechanic mechanic, string ErrandID)
        {
            //Står att man ska kunna se den valda mekanikerns pågående och avslutade ärenden, så för att kunna koppla Errands så behöver vi ErrandsID så att vi sen kan hämta Ärendet för att visa på WPF.
            mechanic.MechanicProgressList.Add(ErrandID);
        }

        public static void AddDoneList(Mechanic mechanic, string ErrandID)
        {
            mechanic.MechanicDoneList.Add(ErrandID);
            RemoveFromMechanicProgressList(mechanic, ErrandID);
        }
    }
}
