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

        public StaffHandlerViewModel() 
        {
            //Displaying properties
            LblMediatek = "MEDIATEK";
            LblTitle = "GESTION DU PERSONNEL";
            BtnAdd = "Ajouter";
            BtnChange = "Modifier";
            BtnDel = "Supprimer";
            BtnAbsence = "Absence";
            //Button command
            BtnAddCommand = new CommandHandler() { CommandExecutte = (arg) => DisplayAddStaff() };
            BtnChangeCommand = new CommandHandler() { CommandExecutte = (arg) => DisplayChangeStaff() };
            BtnDelCommand = new CommandHandler() { CommandExecutte = (arg) => deleteStaff() };
            BtnAbsenceCommand = new CommandHandler() { CommandExecutte = (arg) => DisplayAbsenceHandler() };

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

        }

        /// <summary>
        /// Display the staff change window
        /// </summary>
        public void DisplayChangeStaff()
        {

        }

        /// <summary>
        /// Del a staff
        /// </summary>
        public void deleteStaff() 
        {
            // demander confirmation suppression
        }

        /// <summary>
        /// Display the absence handler window
        /// </summary>
        public void DisplayAbsenceHandler()
        {

        }
    }
}
