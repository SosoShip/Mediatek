using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVE.Mediatek.ViewModel
{
    public class ErrorConnectionViewModel
    {
        public string LblMessage { get; set; }

        public ErrorConnectionViewModel()
        {
            LblMessage = "Login ou mot de passe incorrect";
        }
    }
}
