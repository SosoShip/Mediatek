using AutoMapper;
using SVE.Mediatek.DAL.Entities;
using SVE.Mediatek.DAL.Repository;
using SVE.Mediatek.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SVE.Mediatek.ViewModel.ViewModels
{
    public class StaffChangeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private StaffModel _theStaff;
        public StaffModel TheStaff
        {
            get { return _theStaff; }
            set
            {
                _theStaff = value;
                DisplayTheStaff();
            }
        }
        private bool _isDepartmentEnabled;
        public bool IsDepartmentEnabled
        {
            get { return _isDepartmentEnabled; }
            set
            {
                _isDepartmentEnabled = value;
                RaisePropertyChanged("IsComboBoxEnabled");
            }
        }
        public string TbNameValue { get; set; }
        public string TbFirstNameValue { get; set; }
        public string TbPhoneValue { get; set; }
        public string TbMailValue { get; set; }
        public List<DepartmentModel>? DepartmentList { get; set; }
        public DepartmentModel SelectedDepartment { get; set; }
        public ICommand? ValidateCommand { get; set; }
        public ICommand? CancelCommand { get; set; }
        public Action ShowStaffAction { get; set; }
        public IRepository<StaffEntity> StaffRepository { get; set; }
        IMapper Mapper { get; set; }

        public StaffChangeViewModel(IRepository<StaffEntity> staffRepository, IMapper mapper)
        {
            StaffRepository = staffRepository;
            Mapper = mapper;
            DepartmentList = GenerateDepartmentList();

            ValidateCommand = new CommandHandler() { CommandExecute = async (arg) => await ModifyStaff() };
            CancelCommand = new CommandHandler() { CommandExecute = (arg) => ReturnToStaffHandler() };
        }

        /// <summary>
        /// Page refresh
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Generate a list of all departments except manager.
        /// </summary>
        /// <returns>List all Department</returns>
        public List<DepartmentModel> GenerateDepartmentList()
        {
            return Enum.GetNames(typeof(DepartmentModel))
                .Where(name => name != DepartmentModel.Manager.ToString())
                .Select(name => (DepartmentModel)Enum
                .Parse(typeof(DepartmentModel), name))
                .ToList();
        }

        /// <summary>
        /// Displays the properties of a staff member in the fields
        /// </summary>
        public void DisplayTheStaff()
        {
            TbNameValue = TheStaff.Name;
            TbFirstNameValue = TheStaff.FirsName;
            TbPhoneValue = TheStaff.Phone;
            TbMailValue = TheStaff.Email;
            SelectedDepartment = TheStaff.Department;

            // If department is Manager, disable the ComboBox
            if (TheStaff.Department == DepartmentModel.Manager)
            {
                IsDepartmentEnabled = false;
            }
            else
            {
                IsDepartmentEnabled = true;
            }
        }

        /// <summary>
        /// Recording staff changes and returning to the staff management window.
        /// </summary>
        public async Task ModifyStaff()
        {
            //Field completion check           
            if (string.IsNullOrEmpty(TbNameValue) ||
                string.IsNullOrEmpty(TbFirstNameValue) ||
                string.IsNullOrEmpty(TbPhoneValue) ||
                string.IsNullOrEmpty(TbMailValue))
            {
                MessageBox.Show("Veuillez remplir tous les champs.");
                return;
            }
            if (MessageBox.Show("Voulez vous enregistrer les modifications?",
                 "",
                 MessageBoxButton.OK,
                 MessageBoxImage.Question)
                 == MessageBoxResult.OK)
            {
                //Recording staff changes
                TheStaff.Name = TbNameValue;
                TheStaff.FirsName = TbFirstNameValue;
                TheStaff.Email = TbMailValue;
                TheStaff.Phone = TbPhoneValue;
                TheStaff.Department = SelectedDepartment;

                // Changing the staffModel to staffEntity and saving it in the database.
                var newStaffEntity = Mapper.Map<StaffEntity>(TheStaff);
                await StaffRepository.Update(newStaffEntity);

                // Open the staff manager window
                ShowStaffAction();
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
