using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SVE.Mediatek.Dal;
using SVE.Mediatek.DAL.Entities;
using SVE.Mediatek.DAL.Repository;
using SVE.Mediatek.Model;

namespace SVE.Mediatek.ViewModel.ViewModels
{
    public class ConnectionViewModel
    {
        public string TbLoginValue { get; set; }
        public string Password { get; set; }
        public ManagerModel? Manager { get; set; }
        public ICommand ConnectionCommand { get; set; }
        public Action ShowStaffAction { get; set; }
        public IManagerRepository<ManagerEntity> ManagerRepository { get; set; }
        public IMapper Mapper { get; set; }

        public ConnectionViewModel(IManagerRepository<ManagerEntity> managerRepository, IMapper mapper)
        {
            Mapper = mapper;
            ManagerRepository = managerRepository;
            ConnectionCommand = new CommandHandler() { CommandExecute = async(Arg) => await CheckManagerCredentials() };
        }

        /// <summary>
        /// Verifies the credentials of a manager
        /// </summary>
        public async Task CheckManagerCredentials()
        {
            //Field completion check 
            if (string.IsNullOrEmpty(TbLoginValue) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Veuillez remplir tous les champs");
                return;
            }

            //Verifies that the manager is correctly retrieved from the database and also retrieves it
            ManagerModel managerModel = null;
            try
            {
                var managerEntity = await ManagerRepository.GetManager(Department.Manager);
                managerModel = Mapper.Map<ManagerModel>(managerEntity);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }

            if (managerModel == null)
            {
                MessageBox.Show("il semblerait que l'application ait quelques difficultés pour se connecter, " +
                    "veuillez réessayer ulterieurement.");
                return;
            }

            var connexionPassword = new PasswordHasher().HashPassword(Password, managerModel.Salt);

            // Verifies that the password and login are the same as those the manager in database
            if (connexionPassword == managerModel.Password && TbLoginValue == managerModel.Email)
            {
                ShowStaffAction();
            }
            else
            {
                MessageBox.Show("Login ou mot de passe incorrect");
                TbLoginValue = string.Empty;
                Password = string.Empty;
                return;
            }
        }
    }
}
