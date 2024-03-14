using SVE.Mediatek.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace SVE.Mediatek.ViewModel
{
    public class AbsenceChangeViewModel 
    {      
        public Staff SelectedStaff { get; set; }//TODO besoin du staff?
        public Absence Absence { get; set; }
        public string LblMediatek { get; set; }
        public string LblTitle { get; set; }
        public string LblAbsenceOf { get; set; }
        public string LblBeginDate { get; set; }
        public string LblEndDate { get; set; }
        public string BtnValidate { get; set; }
        public string BtnCancel { get; set; }
        public string LblReason { get; set; }

        public DateOnly? TbBeginDate { get; set; }
        public DateOnly? TBEndDate { get; set; }     
        public List<Reason> ReasonList { get; set; }
        public Reason? SelectedReason { get; set; }
       
        public ICommand? ValidateCommand { get; set; }
        public ICommand? CancelCommand { get; set; }
        public Action ShowAbsenceAction { get; set; }

        public AbsenceChangeViewModel(Staff staff, Absence absence)
        {
            SelectedStaff = staff;
            Absence = absence;

            LblMediatek = "MEDIATEK";
            LblTitle = "MODIFIER UN PERSONNEL";
            LblAbsenceOf = $"Absence de {staff.Name} {staff.FirsName}";

            LblBeginDate = "Date de début";
            TbBeginDate = absence.BeginDate;
            LblEndDate = "Date de fin";
            TBEndDate = absence.EndDate;
            LblReason = "Motif";
            ReasonList = GenerateReasonList();
            SelectedReason = absence.Reason;
            BtnValidate = "Valider";
            BtnCancel = "Annuler";

            ValidateCommand = new CommandHandler() { CommandExecute = (arg) => ModifyAbsence() };
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
        /// Modifies an absence.
        /// </summary>
        public void ModifyAbsence()
        {
            //Field completion check           
            if (TbBeginDate is null ||
                TBEndDate is null ||
                SelectedReason is null)
            //TODO voir comment vérifier le format de date
            {
                // TODO -> appel View errorFiel (ou messageBox?) mais this reste ouverte
            }
            else
            {
                if (TbBeginDate > TBEndDate)
                {
                    // TODO -> appel View errorDate(ou messageBox?) mais this reste ouvert
                }
                //Saving modifications
                else
                {
                    if (MessageBox.Show("Voulez vous confirmer les modifications?",
                        "",
                        MessageBoxButton.OK,
                        MessageBoxImage.Question) 
                        == MessageBoxResult.OK)
                    {
                        var modifiedAbscence = new Absence(TbBeginDate.Value, TBEndDate.Value, SelectedReason.Value);
                        Absence = modifiedAbscence;
                        // TODO endregistrer dans la DB staff.Absence
                        ShowAbsenceAction();
                    }
                    
                }
            }

        }

        /// <summary>
        /// Closes the absence change window and opens the absence manager window.
        /// </summary>
        public void ReturnToAbsenceHandler()
        {
            ShowAbsenceAction();
        }
    }
}

