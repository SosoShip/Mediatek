using SVE.Mediatek.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        public DateOnly? TBDateStartValue { get; set; }
        public DateOnly? TBDateEndValue { get; set;}
        public string LblReason { get; set; }
        public List<ReasonModel> ReasonList { get; set; }
        public ReasonModel? SelectedReason { get; set; }
        public string BtnValidate { get; set; }
        public ICommand ValidateCommand { get; set; }
        public string BtnCancel { get; set; }
        public ICommand CancelCommand { get; set; }
        public StaffModel SelectedStaff { get; set; }
        public Action ShowAbsenceAction { get; set; }

        public AbsenceAddViewModel(StaffModel staff) 
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
        public List<ReasonModel> GenerateReasonList()
        {
            return Enum.GetNames(typeof(ReasonModel))
                .Select(name => (ReasonModel)Enum
                .Parse(typeof(ReasonModel), name))
                .ToList();
        }

        /// <summary>
        /// Validate the addition of an absence
        /// </summary>
        public void ValidateAddAbsence()
        {
            //TODO voir comment vérifier le format de date
            //Field completion check 
            if (TBDateStartValue is null || 
                TBDateEndValue is null || 
                SelectedReason is null) 
            {
                MessageBox.Show("Veuillez remplir tous les champs");
            }
            else
            {
                //Date consistency
                if (TBDateStartValue > TBDateEndValue) 
                {
                    MessageBox.Show("La date de début est antérieure à la date de fin");
                }
                else
                {
                    //TODO if (les champs existent déja dans la dDB) {MessageBox.Show("L'absence est déjà enregistrée");}

                    //Saving modifications
                    SelectedStaff.AbsenceList.Add(new AbsenceModel(TBDateStartValue.Value, TBDateEndValue.Value, SelectedReason.Value));
                    // TODO Ajouter l'absence à la DB
                    ShowAbsenceAction();
                }
            }
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
