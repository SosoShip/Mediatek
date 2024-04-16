using SVE.Mediatek.Model;
using SVE.Mediatek.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SVE.Mediatek.DAL.Repository;

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
        public string? TbNameValue { get; set; }
        public string? TbFirstNameValue { get; set; }
        public string? TbMailValue { get; set; }
        public string? TbPhoneValue { get; set; }
        public string BtnValidate { get; set; }
        public string BtnCancel { get; set; }
        public List<DepartmentModel> DepartmentList { get; set; }
        public DepartmentModel SelectedDepartment { get; set; }
        public ICommand ValidateCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public Action ShowStaffAction { get; set; }

        public StaffAddViewModel() 
        {
            LblMediatek = "MEDIATEK";
            LblTitle = "AJOUTER UN PERSONNEL";
            LblName = "Nom";
            LblFirstName = "Prénom";
            LblPhone = "Téléphone";
            LblMail = "Mail";
            LblDepartment = "Service";
            DepartmentList = GenerateDepartmentList();
            BtnValidate = "Valider";
            BtnCancel = "Annuler";

            ValidateCommand = new CommandHandler() { CommandExecute = (arg) => ValidateAddStaff() };
            CancelCommand = new CommandHandler() { CommandExecute = (arg) => ReturnToStaffHandler() };
        }

        /// <summary>
        /// Generate a list of all departments.
        /// </summary>
        /// <returns>List all Department</returns>
        public List<DepartmentModel> GenerateDepartmentList()
        {
            return Enum.GetNames(typeof(DepartmentModel))
                .Select(name => (DepartmentModel)Enum
                .Parse(typeof(DepartmentModel), name))
                .ToList();
        }

        /// <summary>
        /// Validate the addition of a staff
        /// </summary>
        public void ValidateAddStaff()
        {
            //Field completion check           
            if (string.IsNullOrEmpty(TbNameValue) ||
                string.IsNullOrEmpty(TbFirstNameValue) ||
                string.IsNullOrEmpty(TbPhoneValue) ||
                string.IsNullOrEmpty(TbMailValue))
            {
                MessageBox.Show("Veuillez remplir tous les champs");
            }
            else
            {
                if (MessageBox.Show("Voulez vous enregistrer le nouveau personnel?",
                        "",
                        MessageBoxButton.OK,
                        MessageBoxImage.Question)
                        == MessageBoxResult.OK)
                {
                    var newStaff = new StaffModel(TbNameValue, TbFirstNameValue, TbMailValue, TbPhoneValue, SelectedDepartment);
                    
                    // TODO endregistrer dans la DB staff
                    // Todo puis mise a jour tableau gestion perso -> verif maj modif staff, normalement ok
                    ShowStaffAction();
                }
            }
        }

        /// <summary>
        /// Closes the staff add window and opens the staff manager window.
        /// </summary>
        public void ReturnToStaffHandler()
        {
            ShowStaffAction();
        }
    }
}
