using SVE.Mediatek.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SVE.Mediatek.ViewModel
{
    public class StaffChangeViewModel
    {
        public StaffModel SelectedStaff { get; set; }
        public string LblMediatek { get; set; }
        public string LblTitle { get; set; }
        public string LblName { get; set; }
        public string TbNameValue { get; set; }
        public string LblFirstName { get; set; }
        public string TbFirstNameValue { get; set; }
        public string LblPhone { get; set; }
        public string TbPhoneValue { get; set; }
        public string LblMail { get; set; }
        public string TbMailValue { get; set; }     
        public String LblDepartment { get; set; }
        public List<DepartmentModel>? DepartmentList { get; set; }
        public DepartmentModel SelectedDepartment { get; set; }
        public string BtnValidate { get; set; }
        public string BtnCancel { get; set; }
        public ICommand? ValidateCommand { get; set; }
        public ICommand? CancelCommand { get; set; }
        public Action ShowStaffAction { get; set; }

        public StaffChangeViewModel(StaffModel staff)
        {
            SelectedStaff = staff;

            LblMediatek = "MEDIATEK";
            LblTitle = "MODIFIER UN PERSONNEL";

            LblName = "Nom";
            TbNameValue = staff.Name;
            LblFirstName = "Prénom";
            TbFirstNameValue = staff.FirsName;
            LblPhone = "Téléphone";
            TbPhoneValue = staff.Phone;
            LblMail = "Mail";
            TbMailValue = staff.Email;
            LblDepartment = "Service";
            DepartmentList = GenerateDepartmentList();
            SelectedDepartment = staff.Department;

            BtnValidate = "Valider";
            BtnCancel = "Annuler";

            ValidateCommand = new CommandHandler() { CommandExecute = (arg) => ModifyStaff() };
            CancelCommand = new CommandHandler() { CommandExecute = (arg)=> ReturnToStaffHandler() };
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
        /// Recording staff changes and returning to the staff management window.
        /// </summary>
        public void ModifyStaff()
        {
            //Field completion check           
            if (string.IsNullOrEmpty(TbNameValue) ||
                string.IsNullOrEmpty(TbFirstNameValue) ||
                string.IsNullOrEmpty(TbPhoneValue) ||
                string.IsNullOrEmpty(TbMailValue))
            {
                MessageBox.Show("Veuillez remplir tous les champs.");
            }
            else
            {
                if (MessageBox.Show("Voulez vous enregistrer les modifications?",
                        "",
                        MessageBoxButton.OK,
                        MessageBoxImage.Question)
                        == MessageBoxResult.OK)
                {
                    //Recording staff changes
                    SelectedStaff.Name = TbNameValue;
                    SelectedStaff.FirsName = TbFirstNameValue;
                    SelectedStaff.Email = TbMailValue;
                    SelectedStaff.Phone = TbPhoneValue;
                    SelectedStaff.Department = SelectedDepartment;
                    // TODO endregistrer dans la DB staff

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
