using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ch.hsr.wpf.gadgeothek.service;

namespace ch.hsr.wpf.gadgeothek.GUI.ViewModels
{
    public class MainWindowViewModel
    {
        public LibraryAdminService DataService;

        public MainWindowViewModel()
        {
            DataService = new LibraryAdminService(ConfigurationManager.AppSettings.Get("server")?.ToString());
        }

        #region Gadgets

        #endregion


        #region Customers

        private ICommand _addCustomerCommand;
        public ICommand AddCustomerCommand => _addCustomerCommand ??
            (_addCustomerCommand => new RelayCommand(() =>
            {
                var addNewGadgetWindow = new ChangeGadgetView(new AddGadgetViewModel(this));
                addNewGadgetWindow.Show();
            }

        #endregion


        #region Loan

        #endregion
    }
}