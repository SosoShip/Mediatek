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
    public class StaffHandlerViewModel : INotifyPropertyChanged
    {
        public string LblMediatek { get; set; }
        public string LblTitle { get; set; }
        public string BtnAdd { get; set; }
        public string BtnChange { get; set; }
        public string BtnDel { get; set; }
        public string BtnAbsence { get; set; }
        public ObservableCollection<Staff> StaffList { get; set; }
        public Staff SelectedSaff { get; set; }

        public StaffHandlerViewModel() 
        {
            //Displaying properties
            LblMediatek = "MEDIATEK";
            LblTitle = "GESTION DU PERSONNEL";
            BtnAdd = "Ajouter";
            BtnChange = "Modifier";
            BtnDel = "Supprimer";
            BtnAbsence = "Absence";
            StaffList = GenerateStaffList();
            RaisePropertyChanged(nameof(StaffList));
            //TODO rajouterle SelectedSaff
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

        public ObservableCollection<Staff> GenerateStaffList() 
        {
            // TODO 
           return  StaffList = [new Staff("Durand", "Cecile", "durantc@gmail.com", "0265847912", Department.Reception)];           
        }
    }
}
