﻿using SVE.Mediatek.Model;
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
        public Absence? SelectedAbsence { get; set; }
        public ICommand? AddAbsenceCommand { get; set; }
        public ICommand? ChangeAbsenceCommand { get; set; }
        public ICommand? DelAbsenceCommand { get; set; }
        public ICommand? ReturnAbsenceCommand { get; set; }
        public Staff SelectedSaff { get; set; }
        public Action ShowStaffAction { get; set; }
        public Action ShowAddAbsenceAction { get; set; }
        public Action<Absence> ShowChangeAbsenceAction { get; set; }

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
            SelectedAbsence = new Absence(new DateOnly(2024, 02, 25), new DateOnly(2024, 02, 26), Reason.RRT); //TODO ligne select
            // Draw button command
            AddAbsenceCommand = new CommandHandler() { CommandExecute = (arg) => DisplayAddAbsence() };
            ChangeAbsenceCommand = new CommandHandler() { CommandExecute = (arg) => DisplayChangeAbsence() };
            DelAbsenceCommand = new CommandHandler() { CommandExecute = (arg) => DelAnAbsence() };
            ReturnAbsenceCommand = new CommandHandler() { CommandExecute = (Arg) => DisplayStaff() };
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
            ShowAddAbsenceAction();
        }

        /// <summary>
        /// Display the absence change window
        /// </summary>
        public void DisplayChangeAbsence() 
        {
            // TODO CanExecute absence select
            ShowChangeAbsenceAction(SelectedAbsence);
        }

        /// <summary>
        /// Del an Absence
        /// </summary>
        public void DelAnAbsence()
        {
            // TODO CanExecute absence select
            // Afficher confirmation suppression
            // enregistrer dans la db
            ShowStaffAction();
        }

        /// <summary>
        /// Display the staff handler window
        /// </summary>
        public void DisplayStaff()
        {
            ShowStaffAction();
        }
    }
}
