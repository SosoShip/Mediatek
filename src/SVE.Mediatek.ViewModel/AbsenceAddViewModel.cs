using SVE.Mediatek.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Xps;

namespace SVE.Mediatek.ViewModel
{
    public class AbsenceAddViewModel
    {
        public string LblMediatek { get; set; }
        public string LblTitle { get; set; }
        public string LblAbsenceOf { get; set; }
        public string LblDateStart { get; set; }
        public string LblDateEnd { get; set; }
        public DateOnly? TBDateStart { get; set; }
        public DateOnly? TBDateEnd { get; set;}
        public string LblReason { get; set; }
        public List<Reason> ReasonList { get; set; }
        public Reason? SelectedReason { get; set; }
        public string BtnValidate { get; set; }
        public ICommand ValidateCommand { get; set; }
        public string BtnCancel { get; set; }
        public ICommand CancelCommand { get; set; }
        public Staff SelectedStaff { get; set; }
        public Action ShowAbsenceAction { get; set; }

        public AbsenceAddViewModel(Staff staff) 
        {
            SelectedStaff = staff;

            LblMediatek = "MEDIATEK";
            LblTitle = "MODIFIER UN PERSONNEL";
            LblAbsenceOf = $"Absence de {staff.Name} {staff.FirsName}";

            LblDateStart = "Date de début";
            LblDateEnd = "Date de fin";
            LblReason = "Motif";
            ReasonList = GenerateReasonList(); 
            BtnValidate = "Valider";
            BtnCancel = "Annuler";

            ValidateCommand = new CommandHandler() { CommandExecute = (arg) => ValidateAddAbsence()};           
            CancelCommand = new CommandHandler() { CommandExecute = (arg) => ReturnToAbsenceHandler() };
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

        /// <summary>
        /// Validate the addition of an absence
        /// </summary>
        public void ValidateAddAbsence()
        {
            //TODO voir comment vérifier le format de date
            if (TBDateStart is null || 
                TBDateEnd is null || 
                SelectedReason is null) 
            {
                // TODO -> appel View errorFiel(ou messageBox?) mais this reste ouverte
            }
            else
            {
                if (TBDateStart > TBDateEnd) 
                {
                    // TODO -> appel View errorDaten (ou messageBox?) mais this reste ouvert
                }
                // TODO si date existe deja -> alerte 
                else
                {
                    SelectedStaff.AbsenceList.Add(new Absence(TBDateStart.Value, TBDateEnd.Value, SelectedReason.Value));
                    // TODO Ajouter l'absence à la DB
                    // puis maj classe absence
                    ShowAbsenceAction();
                }
            }// => => TODO remplacer par switch??
        }

        /// <summary>
        /// Closes the absence add window and opens the absence manager window.
        /// </summary>
        public void ReturnToAbsenceHandler()
        {
            ShowAbsenceAction();
        }
    }
}
