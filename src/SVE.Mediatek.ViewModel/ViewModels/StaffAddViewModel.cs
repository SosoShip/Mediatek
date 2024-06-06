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
using AutoMapper;
using SVE.Mediatek.DAL.Entities;
using System.ComponentModel;

namespace SVE.Mediatek.ViewModel.ViewModels
{
    public class StaffAddViewModel : INotifyPropertyChanged
    {
        public string? TbNameValue { get; set; }
        public string? TbFirstNameValue { get; set; }
        public string? TbMailValue { get; set; }
        public string? TbPhoneValue { get; set; }
        public List<DepartmentModel> DepartmentList { get; set; }
        public DepartmentModel? SelectedDepartment { get; set; }
        public ICommand ValidateCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public Action ShowStaffAction { get; set; }
        public IStaffRepository StaffRepository { get; set; }
        IMapper Mapper { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;

        public StaffAddViewModel(IStaffRepository staffRepository, IMapper mapper)
        {
            StaffRepository = staffRepository;
            Mapper = mapper;
            DepartmentList = GenerateDepartmentList();

            ValidateCommand = new CommandHandler() { CommandExecute = async (arg) => await ValidateAddStaff() };
            CancelCommand = new CommandHandler() { CommandExecute = (arg) => ReturnToStaffHandler() };
        }

        /// <summary>
        /// Refresh the page
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
        /// Validate the addition of a staff
        /// </summary>
        public async Task ValidateAddStaff()
        {
            //Field completion check           
            if (string.IsNullOrEmpty(TbNameValue) ||
                string.IsNullOrEmpty(TbFirstNameValue) ||
                string.IsNullOrEmpty(TbPhoneValue) ||
                string.IsNullOrEmpty(TbMailValue) ||
                SelectedDepartment is null)
            {
                MessageBox.Show("Veuillez remplir tous les champs");
                ResetFields();
                return;
            }
            if (MessageBox.Show("Voulez vous enregistrer le nouveau personnel?",
                "",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question)
                == MessageBoxResult.Yes)
            {
                var newStaff = new StaffModel
                {
                    Name = TbNameValue,
                    FirsName = TbFirstNameValue,
                    Email = TbMailValue,
                    Phone = TbPhoneValue,
                    Department = SelectedDepartment.Value
                };

                // Changing the staffModel to staffEntity and saving it in the database.
                var newStaffEntity = Mapper.Map<StaffEntity>(newStaff);
                await StaffRepository.Add(newStaffEntity);

                // Open the staff manager window
                ResetFields();
                ReturnToStaffHandler();
            }
            else
            {
                ResetFields();
                return;
            }
        }

        /// <summary>
        /// Closes the staff add window and opens the staff manager window.
        /// </summary>
        public void ReturnToStaffHandler()
        {
            ShowStaffAction();
        }

        /// <summary>
        /// Sets the field values to null and clears them
        /// </summary>
        public void ResetFields()
        {
            TbNameValue = null;
            TbFirstNameValue = null;
            TbMailValue = null;
            TbPhoneValue = null;
            SelectedDepartment = null;

            RaisePropertyChanged(nameof(TbNameValue));
            RaisePropertyChanged(nameof(TbFirstNameValue));
            RaisePropertyChanged(nameof(TbMailValue));
            RaisePropertyChanged(nameof(TbPhoneValue));
            RaisePropertyChanged(nameof(SelectedDepartment));
        }
    }
}
