using SVE.Mediatek.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SVE.Mediatek.ViewModel
{
    public class StaffChangeViewModel
    {
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
        public Department? SelectedDepartment { get; set; }
        public string BtnValidate { get; set; }
        public string BtnCancel { get; set; }

        public StaffChangeViewModel(Staff staff)
        {
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
    }
}
