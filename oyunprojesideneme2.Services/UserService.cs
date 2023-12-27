using ooyunsitesiprojedeneme2.domain.Collections.Interfaces;
using ooyunsitesiprojedeneme2.Domain;
using oyunsitesiprojedeneme2.Domain.Collections;
using oyunsitesiprojedeneme2.Domain.Models;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace oyunprojesideneme2.Services
{

    public class customerManager {

        private IRepository _manager;
        public customerManager(IRepository product)
        {
            _manager = product;
        }
        //-----------------------------------------------------
        public void registerSaving(UsersDataModel aga) {

            _manager.Add(aga);

            }
        public void usersRemove(UsersDataModel aga)
        {

            _manager.usersRemove(aga);

        }
        public bool Login(UsersDataModel aga)
        { 
        
            return _manager.Login(aga);

        }
        //-----------------------------------------------------
    }
}