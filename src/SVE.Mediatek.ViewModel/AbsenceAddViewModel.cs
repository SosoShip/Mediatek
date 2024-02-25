using SVE.Mediatek.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVE.Mediatek.ViewModel
{
    public class AbsenceAddViewModel
    {
        public string LblMediatek { get; set; }
        public string LblTitle { get; set; }
        public string LblAbsenceOf { get; set; }
        public string LblDateStart { get; set; }
        public string LblDateEnd { get; set; }
        public string TBDateStart { get; set; }
        public string TBDateEnd { get; set;}
        public string LblReason { get; set; }
        public List<Reason> ReasonList { get; set; }
        public Reason SelectedReason { get; set; }
        public string BtnValidate { get; set; }
        public string BtnCancel { get; set; }

        public AbsenceAddViewModel(Staff staff) 
        {
            LblMediatek = "MEDIATEK";
            LblTitle = "MODIFIER UN PERSONNEL";
            LblAbsenceOf = $"Absence de {staff.Name} {staff.FirsName}";

            LblDateStart = "Date de début";
            LblDateEnd = "Date de fin";
            LblReason = "Motif";
            ReasonList = GenerateReasonList();
            BtnValidate = "Valider";
            BtnCancel = "Annuler";
        }

        /// <summary>
        /// Generate a list of all departments.
        /// </summary>
        /// <returns>List all Department</returns>
        public List<Reason> GenerateReasonList()
        {
            return Enum.GetNames(typeof(Reason))
                .Select(name => (Reason)Enum
                .Parse(typeof(Reason), name))
                .ToList();
        }
    }
}
