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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SVE.Mediatek.ViewModel
{
    public class AbsenceChangeViewModel 
    {      
        public StaffModel SelectedStaff { get; set; }//TODO besoin du staff?
        public AbsenceModel Absence { get; set; }
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
        public List<ReasonModel> ReasonList { get; set; }
        public ReasonModel? SelectedReason { get; set; }
       
        public ICommand? ValidateCommand { get; set; }
        public ICommand? CancelCommand { get; set; }
        public Action ShowAbsenceAction { get; set; }

        public AbsenceChangeViewModel(StaffModel staff, AbsenceModel absence)
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
        public List<ReasonModel> GenerateReasonList()
        {
            return Enum.GetNames(typeof(ReasonModel))
                .Select(name => (ReasonModel)Enum
                .Parse(typeof(ReasonModel), name))
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
                MessageBox.Show("Veuillez remplir tous les champs");
            }
            else
            {
                //Date consistency
                if (TbBeginDate > TBEndDate) 
                { 
                    MessageBox.Show("La date de début est antérieure à la date de fin"); 
                }
                
                else
                {
                    //TODO if (les champs existent déja dans la dDB) {MessageBox.Show("L'absence est déjà enregistrée");}

                    //Saving modifications
                    if (MessageBox.Show("Voulez vous confirmer les modifications?",
                        "",
                        MessageBoxButton.OK,
                        MessageBoxImage.Question) 
                        == MessageBoxResult.OK)
                    {
                        var modifiedAbscence = new AbsenceModel(TbBeginDate.Value, TBEndDate.Value, SelectedReason.Value);
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

