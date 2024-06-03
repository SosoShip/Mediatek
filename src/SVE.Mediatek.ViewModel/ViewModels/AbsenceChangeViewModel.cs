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
using System.Windows.Media.Animation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SVE.Mediatek.ViewModel.ViewModels
{
    public class AbsenceChangeViewModel
    {
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
        private AbsenceModel _theAbsence;
        public AbsenceModel TheAbsence
        {
            get { return _theAbsence; }
            set
            {
                _theAbsence = value;
                DisplayAbsence();
            }
        }
        public string LblAbsenceOf { get; set; }
        public DateOnly? TbBeginDate { get; set; }
        public DateOnly? TBEndDate { get; set; }
        public List<ReasonModel> ReasonList { get; set; }
        public ReasonModel? SelectedReason { get; set; }

        public ICommand? ValidateCommand { get; set; }
        public ICommand? CancelCommand { get; set; }
        public Action ShowAbsenceAction { get; set; }
        public IAbsenceRepository AbsenceRepository { get; set; }
        IMapper Mapper { get; set; }

        public AbsenceChangeViewModel(IAbsenceRepository absenceRepository, IMapper mapper)
        {
            AbsenceRepository = absenceRepository;
            Mapper = mapper;
            ReasonList = GenerateReasonList();

            ValidateCommand = new CommandHandler() { CommandExecute = (arg) => ModifyAbsence() };
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
        /// Displays the absences of a staff member in the fields of View
        /// </summary>
        public void DisplayAbsence()
        {
            TbBeginDate = TheAbsence.BeginDate;
            TBEndDate = TheAbsence.EndDate;
            SelectedReason = TheAbsence.Reason;
        }

        /// <summary>
        /// Displays the name staff member in the View
        /// </summary>
        public void DisplayTheStaff()
        {
            LblAbsenceOf = $"Absence de {TheStaff.Name} {TheStaff.FirsName}";
        }

        /// <summary>
        /// Modifies an absence.
        /// </summary>
        public async void ModifyAbsence()
        {
            //Field completion check           
            if (TbBeginDate is null ||
                TBEndDate is null ||
                SelectedReason is null)
            {
                MessageBox.Show("Veuillez remplir tous les champs");
                DisplayAbsence();
                return;
            }
            //Date consistency
            if (TbBeginDate.Value > TBEndDate.Value)
            {
                MessageBox.Show("La date de début est antérieure à la date de fin");
                DisplayAbsence();
                return;
            }

            //Saving modifications
            if (MessageBox.Show("Voulez vous confirmer les modifications?",
                "",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question)
                == MessageBoxResult.Yes)
            {
                //Recording staff changes changes
                TheAbsence.BeginDate = TbBeginDate.Value;
                TheAbsence.EndDate = TBEndDate.Value;
                TheAbsence.Reason = SelectedReason.Value;

                // Changing the absenceModel to absenceEntity and saving it in the database.
                var absenceRepository = Mapper.Map<AbsenceEntity>(TheAbsence);
                await AbsenceRepository.Update(absenceRepository);

                // Open the absence manager window
                ReturnToAbsenceHandler();
            }
            else
            {
                DisplayAbsence();
                return;
            }
        }

        /// <summary>
        /// Closes the absence change window and opens the absence manager window.
        /// </summary>
        public void ReturnToAbsenceHandler()
        {
            ShowAbsenceAction();
        }
    }
}

