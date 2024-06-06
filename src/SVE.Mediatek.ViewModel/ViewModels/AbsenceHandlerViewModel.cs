using AutoMapper;
using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore;
using SVE.Mediatek.DAL.Entities;
using SVE.Mediatek.DAL.Repository;
using SVE.Mediatek.Model;
using System;
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
    public class AbsenceHandlerViewModel : INotifyPropertyChanged
    {
        public string LblAbsenceOf { get; set; }
        private AbsenceModel? _selectedAbsence;
        public AbsenceModel? SelectedAbsence
        {
            get { return _selectedAbsence; }
            set
            {
                _selectedAbsence = value;
                RaisePropertyChanged(nameof(SelectedAbsence));
            }
        }
        public ICommand? AddAbsenceCommand { get; set; }
        public ICommand? ChangeAbsenceCommand { get; set; }
        public ICommand? DelAbsenceCommand { get; set; }
        public ICommand? ReturnAbsenceCommand { get; set; }
        private StaffModel _theStaff;
        public StaffModel TheStaff
        {
            get { return _theStaff; }
            set
            {
                _theStaff = value;
                DisplayAbsenceOfTheStaff();
            }
        }
        public Action ShowStaffAction { get; set; }
        public Action ShowAddAbsenceAction { get; set; }
        public Action<AbsenceModel> ShowChangeAbsenceAction { get; set; }
        public ObservableCollection<AbsenceModel> AbsenceList { get; set; }
        public IAbsenceRepository AbsenceRepository { get; set; }
        public IStaffRepository StaffRepository { get; set; }
        IMapper Mapper { get; set; }
        // Event handler for page refresh
        public event PropertyChangedEventHandler? PropertyChanged;

        public AbsenceHandlerViewModel(IAbsenceRepository absenceRepository, IStaffRepository staffRepository, IMapper mapper)
        {
            AbsenceRepository = absenceRepository;
            StaffRepository = staffRepository;
            Mapper = mapper;

            // Draw button command
            AddAbsenceCommand = new CommandHandler() { CommandExecute = (arg) => DisplayAddAbsence() };
            ChangeAbsenceCommand = new CommandHandler()
            {
                CommandExecute = (arg) => DisplayChangeAbsence(),
                CommandCanExecute = (arg) => CanExecuteAbsenceCommand()
            };
            DelAbsenceCommand = new CommandHandler()
            {
                CommandExecute = (arg) => DelAnAbsence(),
                CommandCanExecute = (arg) => CanExecuteAbsenceCommand()
            };
            ReturnAbsenceCommand = new CommandHandler() { CommandExecute = (Arg) => DisplayStaff() };
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
        /// Displaying the list of absences
        /// </summary>
        public async Task DisplayAbsenceList()
        {
            AbsenceList = await GenerateAbsenceList();
            // View update
            RaisePropertyChanged(nameof(AbsenceList));
        }

        /// <summary>
        /// Displays the absences of a staff member in the fields of View
        /// </summary>
        public async Task DisplayAbsenceOfTheStaff()
        {
            LblAbsenceOf = $"Absence de {TheStaff.Name} {TheStaff.FirsName}";
            await DisplayAbsenceList();
        }

        /// <summary>
        /// Retrieves all AbsencesEntity from the database and returns it as an ObservableCollection of AbsenceModel.
        /// </summary>
        /// <returns></returns>ObservableCollection<TheAbsence></returns>
        public async Task<ObservableCollection<AbsenceModel>> GenerateAbsenceList()
        {
            ObservableCollection<AbsenceModel>? listAbscenceModel = null;
            try
            { 
                //var staffAbsencesIds = TheStaff.AbsenceList.Select(abs => abs.Id).ToList();
                var listAbsenceEntity = new ObservableCollection<AbsenceEntity>(
                    await AbsenceRepository.GetAll()
                    .Where(a => a.StaffEntityId == TheStaff.Id)
                    //.Where(s => staffAbsencesIds.Contains(s.Id))
                    .OrderByDescending(e => e.BeginDate).ToListAsync());
                listAbscenceModel = Mapper.Map<ObservableCollection<AbsenceModel>>(listAbsenceEntity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return listAbscenceModel;
        }

        /// <summary>
        /// Display the absence addition window
        /// </summary>
        public void DisplayAddAbsence()
        {
            ShowAddAbsenceAction();
        }

        /// <summary>
        /// Display the absence change window
        /// </summary>
        public void DisplayChangeAbsence()
        {
            ShowChangeAbsenceAction(SelectedAbsence);
        }

        /// <summary>
        /// Del an TheAbsence
        /// </summary>
        public async Task DelAnAbsence()
        {
            if (MessageBox.Show($"Voulez vous supprimer l'absence de {TheStaff}?",
                "Supression d'une abscence",
                MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                await AbsenceRepository.Delete(SelectedAbsence.Id);

                // update
                AbsenceList.Remove(SelectedAbsence);
                RaisePropertyChanged(nameof(AbsenceList));
            }
        }

        /// <summary>
        /// Display the staff handler window
        /// </summary>
        public void DisplayStaff()
        {
            ShowStaffAction();
        }

        /// <summary>
        /// Determines whether the staff command can be executed.
        /// </summary>
        /// <returns>True if the staff command can be executed; otherwise, false.</returns>        
        public bool CanExecuteAbsenceCommand()
        {
            if (SelectedAbsence == null)
            {
                return false;
            }
            return true;
        }
    }
}
