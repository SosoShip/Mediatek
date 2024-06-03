using AutoMapper;
using SVE.Mediatek.DAL.Entities;
using SVE.Mediatek.DAL.Repository;
using SVE.Mediatek.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Xps;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SVE.Mediatek.ViewModel.ViewModels
{
    public class AbsenceAddViewModel : INotifyPropertyChanged
    {
        public string LblAbsenceOf { get; set; }
        public DateOnly? TBDateStartValue { get; set; }
        public DateOnly? TBDateEndValue { get; set; }
        public List<ReasonModel> ReasonList { get; set; }
        public ReasonModel? SelectedReason { get; set; }
        public ICommand ValidateCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        private StaffModel _theStaff;
        public StaffModel TheStaff
        {
            get { return _theStaff; }
            set
            {
                _theStaff = value;
                DisplayNameOfTheStaff();
            }
        }
        public Action ShowAbsenceAction { get; set; }
        public IAbsenceRepository AbsenceRepository { get; set; }
        IMapper Mapper { get; set; }
        // Event handler for page refresh
        public event PropertyChangedEventHandler? PropertyChanged;

        public AbsenceAddViewModel(IAbsenceRepository absenceRepository, IMapper mapper)
        {
            AbsenceRepository = absenceRepository;
            Mapper = mapper;
            ReasonList = GenerateReasonList();

            ValidateCommand = new CommandHandler() { CommandExecute = (arg) => ValidateAddAbsence() };
            CancelCommand = new CommandHandler() { CommandExecute = (arg) => ReturnToAbsenceHandler() };
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

        /// /// <summary>
        /// Displays the name staff member in the View 
        /// </summary>
        public void DisplayNameOfTheStaff()
        {
            LblAbsenceOf = $"Absence de {TheStaff.Name} {TheStaff.FirsName}";
        }

        /// <summary>
        /// Validate the addition of an absence
        /// </summary>
        public async void ValidateAddAbsence()
        { 
            // Field completion check 
            if (TBDateStartValue is null ||
                TBDateEndValue is null ||
                SelectedReason is null)
            {
                MessageBox.Show("Veuillez remplir tous les champs");
                ResetFields();
                return;
            }

            // Date consistency check
            if (TBDateStartValue.Value > TBDateEndValue.Value)
            {
                MessageBox.Show("La date de début est antérieure à la date de fin");
                ResetFields();
                return;
            }

            // Check if absence dates exist
            var isAbsenceRecorded = await CheckIfAbsenceRecorded(TBDateStartValue.Value, TBDateEndValue.Value);
            if (isAbsenceRecorded)
            {
                MessageBox.Show("L'absence est déjà enregistré");
                ResetFields();
                return;
            }

            if (MessageBox.Show("Voulez vous enregistrer l'absence ?",
                "",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question)
                == MessageBoxResult.Yes)
            {
                var newAbsence = new AbsenceModel
                {
                    BeginDate = TBDateStartValue.Value,
                    EndDate = TBDateEndValue.Value,
                    Reason = SelectedReason.Value
                };

                // Changing the AbsenceModel to AbsenceEntity and saving it in the database.
                var newAbsenceEntity = Mapper.Map<AbsenceEntity>(newAbsence);
                await AbsenceRepository.AddAbsence(TheStaff.Id, newAbsenceEntity);
                TheStaff.AbsenceList.Add(Mapper.Map<AbsenceModel>(newAbsenceEntity));

                // Open the absence manager window
                ResetFields();
                ReturnToAbsenceHandler();
            }
            else
            {
                ResetFields();
                return;
            }
            
        }

        /// <summary>
        /// Closes the absence add window and opens the absence manager window.
        /// </summary>
        public void ReturnToAbsenceHandler()
        {
            ShowAbsenceAction();
        }

        /// <summary>
        /// Checks if the absence dates exist in the database
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<bool> CheckIfAbsenceRecorded(DateOnly beginDate, DateOnly endDate)
        {
            bool isAbsenceExists = await AbsenceRepository.IsAbsenceDateExistsAsync(beginDate, endDate);

            if (isAbsenceExists) 
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Sets the field values to null and clears them
        /// </summary>
        public void ResetFields()
        {
            TBDateStartValue = null;
            TBDateEndValue = null;
            SelectedReason = null;
            RaisePropertyChanged(nameof(TBDateStartValue));
            RaisePropertyChanged(nameof(TBDateEndValue));
            RaisePropertyChanged(nameof(SelectedReason));
        }
    }
}
