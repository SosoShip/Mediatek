using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVE.Mediatek.ViewModel
{
    public class DelConfirmationViewModel
    {
        public string LblMessage { get; set; }
        public string BtnValidate { get; set; }
        public string BtnCancel { get; set; }
        public DelConfirmationViewModel() 
        {
            LblMessage = "Confirmer la supression";
            BtnValidate = "Valider";
            BtnCancel = "Annuler";
        }
    }
}
