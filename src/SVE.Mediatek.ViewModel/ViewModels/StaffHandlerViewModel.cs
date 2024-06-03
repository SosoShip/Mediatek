using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SVE.Mediatek.DAL.Entities;
using SVE.Mediatek.DAL.Repository;
using SVE.Mediatek.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SVE.Mediatek.ViewModel.ViewModels
{
    public class StaffHandlerViewModel : INotifyPropertyChanged
    {
        public ICommand? BtnAddCommand { get; set; }
        public ICommand? BtnChangeCommand { get; set; }
        public ICommand? BtnDelCommand { get; set; }
        public ICommand? BtnAbsenceCommand { get; set; }
        public ObservableCollection<StaffModel> StaffList { get; set; }
        private StaffModel? _selectedStaff;
        public StaffModel? SelectedStaff
        {
            get { return _selectedStaff; }
            set
            {
                _selectedStaff = value;
                RaisePropertyChanged(nameof(SelectedStaff));
            }
        }
        public Action ShowAddStaffAction { get; set; }
        public Action<StaffModel> ShowChangeStaffAction { get; set; }
        public Action<StaffModel> ShowAbsenceAction { get; set; }
        public IRepository<StaffEntity> StaffRepository { get; set; }
        public IMapper Mapper { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged; // Page refresh event

        public StaffHandlerViewModel(IRepository<StaffEntity> staffRepository, IMapper mapper)
        {
            StaffRepository = staffRepository;
            Mapper = mapper;
            //Buttons command
            BtnAddCommand = new CommandHandler() { CommandExecute = (arg) => DisplayAddStaff() };
            BtnChangeCommand = new CommandHandler()
            {
                CommandExecute = (arg) => DisplayChangeStaff(),
                CommandCanExecute = (arg) => CanExecuteStaffCommand()
            };
            BtnDelCommand = new CommandHandler()
            {
                CommandExecute = (arg) => deleteStaffAsync(),
                CommandCanExecute = (arg) => CanExecuteStaffDelCommand()
            };
            BtnAbsenceCommand = new CommandHandler()
            {
                CommandExecute = (arg) => DisplayAbsenceHandler(),
                CommandCanExecute = (arg) => CanExecuteStaffCommand()
            };
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
        /// Displaying the staff list
        /// </summary>
        public async Task DisplaytStaffList()
        {
            var staffList = await GenerateStaffList();
            StaffList = staffList;
            RaisePropertyChanged(nameof(StaffList));
        }

        /// <summary>
        /// Retrieves a list of StaffEntity from the database and returns it as an ObservableCollection of StaffModel.
        /// </summary>
        /// <returns></returns>ObservableCollection<Staff></returns>
        public async Task<ObservableCollection<StaffModel>> GenerateStaffList()
        {
            ObservableCollection<StaffModel>? listStaffModel = null;
            try
            {
                var listStaff = new ObservableCollection<StaffEntity>(await StaffRepository.GetAll()
                    .OrderBy(e => e.Name).ToListAsync());
                listStaffModel = Mapper.Map<ObservableCollection<StaffModel>>(listStaff);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return listStaffModel;
        }

        /// <summary>
        /// Display the staff addition window
        /// </summary>
        public void DisplayAddStaff()
        {
            ShowAddStaffAction();
        }

        /// <summary>
        /// Display the staff change window
        /// </summary>
        public void DisplayChangeStaff()
        {
            ShowChangeStaffAction(SelectedStaff);
        }

        /// <summary>
        /// Del a staff
        /// </summary>
        public async void deleteStaffAsync()
        {
            if (MessageBox.Show($"Voulez vous vraiment supprimer {SelectedStaff.Name} {SelectedStaff.FirsName}?",
                "Supression d'un collaborateur",
                MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                await StaffRepository.Delete(SelectedStaff.Id);

                // Update
                StaffList.Remove(SelectedStaff); 
                RaisePropertyChanged(nameof(StaffList));
            }

        }

        /// <summary>
        /// Display the absence handler window
        /// </summary>
        public void DisplayAbsenceHandler()
        {
            ShowAbsenceAction(SelectedStaff);
        }

        /// <summary>
        /// Determines whether the staff command can be executed.
        /// </summary>
        /// <returns>True if the staff command can be executed; otherwise, false.</returns>        
        public bool CanExecuteStaffCommand()
        {
            if (SelectedStaff == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Determines whether the del staff command can be executed.
        /// </summary>
        /// <returns>True if the staff command can be executed; otherwise, false.</returns>
        public bool CanExecuteStaffDelCommand()
        {
            if (SelectedStaff == null)
            {
                return false;
            }
            if (SelectedStaff.Department == DepartmentModel.Manager)
            {
                return false;
            }
            return true;
        }
    }
}
