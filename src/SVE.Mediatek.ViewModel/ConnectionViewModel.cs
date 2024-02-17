using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using SVE.Mediatek.Model;

namespace SVE.Mediatek.ViewModel
{
    public class ConnectionViewModel
    {
        public string LblMediatek { get; set; }
        public string LblTitle { get; set; }
        public string LblInformation { get; set; }
        public string LblMail { get; set; }
        public string LblPassword { get; set; }
        public string BtnValidate { get; set; }
        public string TbLoginValue { get; set; }
        public string TbPasswordValue { get; set; }
        public Manager? Manager { get; set; }
        public ICommand ConnectionCommand { get; set; }

        public ConnectionViewModel() 
        {
            //Displaying properties
            LblMediatek = "MEDIATEK";
            LblTitle = "GESTION DU PERSONNEL";
            LblInformation = "Se connecter pour continer";
            LblMail = "EMAIL";
            LblPassword = "MOT DE PASSE";
            BtnValidate = "Se connecter";
            TbLoginValue = string.Empty; // TODO new string ou ""?
            TbPasswordValue = string.Empty;
            //Click on the Validate Connection button
            ConnectionCommand = new CommandHandler() { CommandExecutte = (Arg) => DisplayViewStaffHandler () };
        }

        public void DisplayViewStaffHandler()
        {            
            var loginValue = TbLoginValue;
            var passwordValue = TbPasswordValue;

            if (string.IsNullOrEmpty(loginValue) && string.IsNullOrEmpty(passwordValue))
            {
                // TODO affichage de ErrorField
            }
            else
            {
                // TODO recuperer le login et PW du manager 
                //Manager = new Manager(); est ce que je créé un nouveau manager comme dans habilitation pour ensuite verifier si log pwd identique
                //ou je recup les données de la DB et je vérifie dierct dans le if?
                if (loginValue == Manager.Login &&  passwordValue == Manager.Password) 
                {
                    // TODO affichage StaffHandler
                    // comment afficher le staffHandler sans connaitre ni la vue ni le chapeau?
                }
                else
                {
                    // TODO affichage de ErrorConnection
                }
            }           
        }
    }
}
