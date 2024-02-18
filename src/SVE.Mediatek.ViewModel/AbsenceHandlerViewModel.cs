using SVE.Mediatek.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Staff SelectedSaff { get; set; }

        public AbsenceHandlerViewModel(string name, string firstName)
        {
            //Displaying properties
            LblMediatek = "MEDIATEK";
            LblTitle = "GESTION DES ABSENCES";
            LblAbsenceOf = $"Absence de {name} {firstName}"; 
            BtnAdd = "Ajouter";
            BtnChange = "Modifier";
            BtnDel = "Supprimer";
            BtnReturn = "Retour";
            AbsenceList = GenerateAbsenceList();
            RaisePropertyChanged(nameof(AbsenceList));
            //TODO rajouterle SelectedSaff
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        /// <summary>
        /// Page refresh
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Absence> GenerateAbsenceList()
        {
            // TODO 
            return AbsenceList = [new Absence("01/02/2024", "05/02/2024", Reason.RRT)];
        }
    }
}
