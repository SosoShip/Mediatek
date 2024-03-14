using SVE.Mediatek.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ICommand? BtnDelCommand {  get; set; }
        public ICommand? BtnAbsenceCommand { get; set; }
        public ObservableCollection<Staff> StaffList { get; set; }
        public Staff? SelectedSaff { get; set; }
        public Action ShowAddStaffAction { get; set; }
        public Action<Staff> ShowChangeStaffAction { get; set; }
        public Action<Staff> ShowAbsenceAction { get; set; }

        public StaffHandlerViewModel() 
        {
            SelectedSaff = new Staff("Dupond", "Alicia", "adupond@mediatek.fr", "0286545231", Department.Comptabilité); //TODO A remplacer par la ligne select

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
                CommandCanExecute = (arg) => false
            }; 
            BtnDelCommand = new CommandHandler() { CommandExecute = (arg) => deleteStaff() };
            BtnAbsenceCommand = new CommandHandler() { CommandExecute = (arg) => DisplayAbsenceHandler() };

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
        public ObservableCollection<Staff> GenerateStaffList() 
        {
            // TODO récuperer la liste du personnel dans la DB
           return  StaffList = [new Staff("Durand", "Cecile", "durantc@gmail.com", "0265847912", Department.Reception)];
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
            ShowChangeStaffAction(SelectedSaff);
        }

        /// <summary>
        /// Del a staff
        /// </summary>
        public void deleteStaff() 
        {
            // demander confirmation suppression puis supprimer
        }

        /// <summary>
        /// Display the absence handler window
        /// </summary>
        public void DisplayAbsenceHandler()
        {
            ShowAbsenceAction(SelectedSaff);
        }
    }
}
