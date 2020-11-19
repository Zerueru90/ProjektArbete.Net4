using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic.Entities.Person_Entities;
using Logic.Entities.Vehicles_Entities;

namespace Logic
{
   public static class MechanicProgress
   {
        public static bool AddToMechanicProgressList(Mechanic objMechanic, CommonView objCommonView)
        {
            if (objMechanic != null)
            {
                if (objMechanic.MechanicProgressList.Count != 2) 
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
                        AddProgressList(objMechanic, item.ID);
                    }

                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public static void RemoveFromProgressList(Mechanic mechanic, Guid ErrandID)
        {
            AddDoneList(mechanic, ErrandID);
            mechanic.MechanicProgressList.Remove(ErrandID);
        }

        public static void AddProgressList(Mechanic mechanic, Guid ErrandID)
        {
            mechanic.MechanicProgressList.Add(ErrandID);
            mechanic.ListContainingInProgressAndDoneErrendIDs.Add(ErrandID);
        }

        public static void AddDoneList(Mechanic mechanic, Guid ErrandID)
        {
            mechanic.MechanicDoneList.Add(ErrandID);
        }


        public static bool UpdateMechanicStatus(CommonView objCommonView, Mechanic objMechanic, string newStatus)
        {
            if (objCommonView.MechanicID != Guid.Empty)
            {
                var objErrands = ErrandList.ErrandsList.Where(x => x.ID == objCommonView.ErrandID);
                foreach (var item in objErrands)
                {
                    RemoveFromProgressList(objMechanic, item.ID);
                    //sparar i datan.
                    item.Status = newStatus;
                }
                //ändrar i gui
                objCommonView.ChangeStatus = newStatus;
                return true;
            }
            else
                return false;
        }

        public static void UpdateMechanicSkill(Mechanic mec)
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
