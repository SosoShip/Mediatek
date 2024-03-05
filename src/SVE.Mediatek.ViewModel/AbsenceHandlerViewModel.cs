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
    public class AbsenceHandlerViewModel : INotifyPropertyChanged
    {
        public string LblMediatek { get; set; }
        public string LblTitle { get; set; }
        public string LblAbsenceOf {  get; set; }
        public string BtnAdd { get; set; }
        public string BtnChange { get; set; }
        public string BtnDel { get; set; }
        public string BtnReturn { get; set; }
        public ObservableCollection<Absence> AbsenceList { get; set; }
        public ICommand? AddAbsenceCommand { get; set; }
        public ICommand? ChangeAbsenceCommand { get; set; }
        public ICommand? DelAbsenceCommand { get; set; }
        public ICommand? ReturnAbsenceCommand { get; set; }
        public Staff SelectedSaff { get; set; }

        public AbsenceHandlerViewModel(Staff staff)
        {
            //Displaying properties
            SelectedSaff = staff;
            LblMediatek = "MEDIATEK";
            LblTitle = "GESTION DES ABSENCES";
            LblAbsenceOf = $"Absence de {staff.Name} {staff.FirsName}";
            BtnAdd = "Ajouter";
            BtnChange = "Modifier";
            BtnDel = "Supprimer";
            BtnReturn = "Retour";
            AbsenceList = GetAbsenceList();
            // Draw button command
            AddAbsenceCommand = new CommandHandler() { CommandExecutte = (arg) => DisplayAddAbsence() };
            ChangeAbsenceCommand = new CommandHandler() { CommandExecutte = (arg) => DisplayChangeAbsence() };
            DelAbsenceCommand = new CommandHandler() { CommandExecutte = (arg) => DelAnAbsence() };
            ReturnAbsenceCommand = new CommandHandler() { CommandExecutte = (Arg) => DisplayReturnAbsence() };
            // View update
            RaisePropertyChanged(nameof(AbsenceList));
        }

        // Event handler for page refresh
        public event PropertyChangedEventHandler? PropertyChanged;
        /// <summary>
        /// Refresh the page
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Retrieves a list of absences from the database and returns it as an ObservableCollection.
        /// </summary>
        /// <returns></returns>ObservableCollection<Absence></returns>
        public ObservableCollection<Absence> GetAbsenceList()
        {
            // TODO récupere la liste des absences de la DB
            return new ObservableCollection<Absence>([new Absence(new DateOnly(2024, 02, 25), new DateOnly(2024, 02, 26), Reason.RRT)]);           
        }

        /// <summary>
        /// Display the absence addition window
        /// </summary>
        public void DisplayAddAbsence()
        {

        }

        /// <summary>
        /// Display the absence change window
        /// </summary>
        public void DisplayChangeAbsence() 
        { 
        }

        /// <summary>
        /// Del an Absence
        /// </summary>
        public void DelAnAbsence()
        {
            // Afficher confirmation suppression
        }

        /// <summary>
        /// Display the staff handler window
        /// </summary>
        public void DisplayReturnAbsence()
        {

        }
    }
}
