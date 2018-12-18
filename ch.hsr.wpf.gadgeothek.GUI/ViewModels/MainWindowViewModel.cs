using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using ch.hsr.wpf.gadgeothek.GUI.Commands;
using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;

namespace ch.hsr.wpf.gadgeothek.GUI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        //-----------
        private LibraryAdminService libraryAdminService;

        private ObservableCollection<Gadget> _gadgets;

        private DispatcherTimer DispatcherTimer;

        private ICommand _deleteCommand;
        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand(DeleteGadget));

        public ObservableCollection<Gadget> Gadgets
        {
            get => _gadgets;
            set => SetProperty(ref _gadgets, value, nameof(Gadgets));
        }

        private ObservableCollection<Loan> _loans;

        public ObservableCollection<Loan> Loans
        {
            get => _loans;
            set => SetProperty(ref _loans, value, nameof(Loans));
        }

        public Gadget CurrentGadget { get; set; }

        public MainWindowViewModel()
        {
            var serverString = ConfigurationManager.AppSettings.Get("server")?.ToString();
            libraryAdminService = new LibraryAdminService(serverString);
            var gadgets = libraryAdminService.GetAllGadgets();
            var loans = libraryAdminService.GetAllLoans();

            if (gadgets == null)
            {
                MessageBox.Show("Couldn't load data from server!");
            }
            else
            {
                if (gadgets.Count == 0)
                    MessageBox.Show("Gadget list empty!");
                else
                    Gadgets = new ObservableCollection<Gadget>(gadgets);

                if (loans.Count == 0)
                    MessageBox.Show("Loans list empty!");
                else
                    Loans = new ObservableCollection<Loan>(loans);
            }

            DispatcherTimer = new DispatcherTimer();
            DispatcherTimer.Tick += UpdateData;
            DispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            DispatcherTimer.Start();
        }

        private void UpdateData(object sender, EventArgs eventArgs)
        {
            var updatedLoans = libraryAdminService.GetAllLoans();
            var updatedGadgets = libraryAdminService.GetAllGadgets();

            if (updatedGadgets != null && updatedLoans != null)
            {
                Loans.Clear();
                updatedLoans.ForEach(Loans.Add);
                Gadgets.Clear();
                updatedGadgets.ForEach(Gadgets.Add);
            }
        }

        private void DeleteGadget()
        {
            if (CurrentGadget != null)
            {
                string sMessageBoxText =
                    $"Are you sure you want to delete Gadget {CurrentGadget.Name} with ID  {CurrentGadget.InventoryNumber}?";
                string sCaption = "Delete";

                MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult rsltMessageBox =
                        MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                if (rsltMessageBox == MessageBoxResult.Yes)
                {
                    if (CurrentGadget != null && libraryAdminService.DeleteGadget(CurrentGadget))
                    {
                        Gadgets.Remove(CurrentGadget);
                    }
                    else
                    {
                        MessageBox.Show("Removing Gadget failed!");
                    }
                }
            }
        }
    }
}