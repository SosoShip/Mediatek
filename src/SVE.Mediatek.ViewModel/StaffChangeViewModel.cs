using SVE.Mediatek.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SVE.Mediatek.ViewModel
{
    public class StaffChangeViewModel
    {
        public Staff SelectedStaff { get; set; }
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
        public List<Department>? DepartmentList { get; set; }
        public Department SelectedDepartment { get; set; }
        public string BtnValidate { get; set; }
        public string BtnCancel { get; set; }
        public ICommand? ValidateCommand { get; set; }
        public ICommand? CancelCommand { get; set; }

        public StaffChangeViewModel(Staff staff)
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

            ValidateCommand = new CommandHandler() { CommandExecutte = (arg) => ModifyStaff() };
        }

        /// <summary>
        /// Generate a list of all departments.
        /// </summary>
        /// <returns>List all Department</returns>
        public List<Department> GenerateDepartmentList()
        {
            return Enum.GetNames(typeof(Department))
                .Select(name => (Department)Enum
                .Parse(typeof(Department), name))
                .ToList();
        }

        public void ModifyStaff()
        {
            //Field completion check           
            if (string.IsNullOrEmpty(TbNameValue) ||
                string.IsNullOrEmpty(TbFirstNameValue) ||
                string.IsNullOrEmpty(TbPhoneValue) ||
                string.IsNullOrEmpty(TbMailValue))
            {
                // TODO -> appel View errorFiel (ou messageBox?) mais this reste ouverte
            }
            else
            {
                if (MessageBox.Show("Voulez vous enregistrer les modifications?",
                        "",
                        MessageBoxButton.OK,
                        MessageBoxImage.Question)
                        == MessageBoxResult.OK)
                {
                    var modifiedStaff = new Staff(TbNameValue, TbFirstNameValue, TbMailValue, TbPhoneValue, SelectedDepartment);
                    SelectedStaff = modifiedStaff;
                    // TODO endregistrer dans la DB staff
                    // Todo puis Affichage AbscenceHandler -> verif maj modif staff

                }
            }
        }
    }
}
