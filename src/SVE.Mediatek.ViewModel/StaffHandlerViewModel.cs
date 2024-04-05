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

namespace SVE.Mediatek.ViewModel
{
    public class StaffHandlerViewModel : INotifyPropertyChanged
    {
        public string LblMediatek { get; set; }
        public string LblTitle { get; set; }
        public string BtnAdd { get; set; }
        public string BtnChange { get; set; }
        public string BtnDel { get; set; }
        public string BtnAbsence { get; set; }
        public ICommand? BtnAddCommand { get; set; }
        public ICommand? BtnChangeCommand { get; set; }
        //private ICommand? _btnChangeCommand;
        //public ICommand? BtnChangeCommand
        //{
        //    get { return _btnChangeCommand; }
        //    set
        //    {
        //        RaisePropertyChanged(nameof(BtnChangeCommand));
        //    }
        //}
        public ICommand? BtnDelCommand {  get; set; }
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

        public StaffHandlerViewModel() 
        {
            //Displaying properties
            LblMediatek = "MEDIATEK";
            LblTitle = "GESTION DU PERSONNEL";
            BtnAdd = "Ajouter";
            BtnChange = "Modifier";
            BtnDel = "Supprimer";
            BtnAbsence = "Absence";
            //Buttons command
            BtnAddCommand = new CommandHandler() { CommandExecute = (arg) => DisplayAddStaff() }; 
            BtnChangeCommand = new CommandHandler() 
            {
                CommandExecute = (arg) => DisplayChangeStaff(),
                CommandCanExecute = (arg) => CanExecuteStaffCommand()
            }; 
            BtnDelCommand = new CommandHandler() 
            { 
                CommandExecute = (arg) => deleteStaff(),
                CommandCanExecute = (arg) => CanExecuteStaffCommand()
            };
            BtnAbsenceCommand = new CommandHandler() 
            { 
                CommandExecute = (arg) => DisplayAbsenceHandler(),
                CommandCanExecute = (arg) => CanExecuteStaffCommand()
            };
            // Array Staff
            StaffList = GenerateStaffList();
            RaisePropertyChanged(nameof(StaffList));
        }

        public event PropertyChangedEventHandler? PropertyChanged; // Page refresh event
        /// <summary>
        /// Page refresh
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Retrieves a list of staff from the database and returns it as an ObservableCollection.
        /// </summary>
        /// <returns></returns>ObservableCollection<Staff></returns>
        public ObservableCollection<StaffModel> GenerateStaffList() 
        {
            // TODO récuperer la liste du personnel dans la DB
           return  StaffList = [new StaffModel("Durand", "Cecile", "durantc@gmail.com", "0265847912", DepartmentModel.Reception),
           new StaffModel("lejoie", "Vincent", "Vincent@gmail.com", "0265847912", DepartmentModel.Reception)];
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
        public void deleteStaff() 
        {
            if (MessageBox.Show($"Voulez vous vraiment supprimer {SelectedStaff}?",
                "Supression d'un collaborateur",
                MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                // TODO suprimer le staff et rafraichir la page
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
    }
}
