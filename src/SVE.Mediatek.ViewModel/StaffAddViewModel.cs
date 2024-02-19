using SVE.Mediatek.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVE.Mediatek.ViewModel
{
    public class StaffAddViewModel
    {
        public string LblMediatek { get; set; }
        public string LblTitle { get; set; }
        public string LblName { get; set; }
        public string LblFirstName { get; set; }
        public string LblPhone { get; set; }
        public string LblMail { get; set; }
        public string LblDepartment { get; set; }
        public string TbMailValue { get; set; }
        public string BtnValidate { get; set; }
        public string BtnCancel { get; set; }
        public Department? TbDepartmentValue { get; set; }

        public StaffAddViewModel() 
        {
            LblMediatek = "MEDIATEK";
            LblTitle = "AJOUTER UN PERSONNEL";
            LblName = "Nom";
            LblFirstName = "Prénom";
            LblPhone = "Téléphone";
            LblMail = "Mail";
            LblDepartment = "Service";
            BtnValidate = "Valider";
            BtnCancel = "Annuler";
        }
    }
}
