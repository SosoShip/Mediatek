using SVE.Mediatek.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVE.Mediatek.ViewModel
{
    public class AbsenceChangeViewModel
    {
        public string LblMediatek { get; set; }
        public string LblTitle { get; set; }
        public string LblAbsenceOf { get; set; }
        public string LblBeginDate { get; set; }
        public string LblEndDate { get; set; }
        public string TbBeginDate { get; set; }
        public string TBEndDate { get; set; }
        public string LblReason { get; set; }
        public List<Reason> ReasonList { get; set; }
        public Reason SelectedReason { get; set; }
        public string BtnValidate { get; set; }
        public string BtnCancel { get; set; }

        public AbsenceChangeViewModel(Staff staff, Absence absence)
        {
            LblMediatek = "MEDIATEK";
            LblTitle = "MODIFIER UN PERSONNEL";
            LblAbsenceOf = $"Absence de {staff.Name} {staff.FirsName}";

            LblBeginDate = "Date de début";
            TbBeginDate = absence.BeginDate.ToString();
            LblEndDate = "Date de fin";
            TBEndDate = absence.EndDate.ToString();
            LblReason = "Motif";
            ReasonList = GenerateReasonList();
            SelectedReason = absence.Reason;
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

