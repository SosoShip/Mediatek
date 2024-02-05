using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVE.Mediatek.ViewModel
{
    public class ConnectionViewModel
    {
        public string LblMediatek { get; set; }
        public string LblTitle { get; set; }
        public string lblInformation { get; set; }
        public string lblMail { get; set; }
        public string lblPassword { get; set; }
        public string btnValidate { get; set; }

        public ConnectionViewModel() 
        {
            LblMediatek = "MEDIATEK";
            LblTitle = "GESTION DU PERSONNEL";
            lblInformation = "Se connecter pour continer";
            lblMail = "EMAIL";
            lblPassword = "MOT DE PASSE";
            btnValidate = "Se connecter";
        }
    }
}
