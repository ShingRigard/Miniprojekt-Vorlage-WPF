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
using System.Collections.Generic;
using System.Linq;

namespace ch.hsr.wpf.gadgeothek.GUI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private LibraryAdminService libraryAdminService;

        private DispatcherTimer DispatcherTimer;

        #region ICommands of Gadget
        private ICommand _deleteGadgetCommand;
        public ICommand DeleteGadgetCommand => _deleteGadgetCommand ?? (_deleteGadgetCommand = new RelayCommand(DeleteGadget));

        private ICommand _saveGadgetCommand;
        public ICommand SaveGadgetCommand => _saveGadgetCommand ?? (_saveGadgetCommand = new RelayCommand(SaveGadget));

        private ICommand _newGadgetCommand;
        public ICommand NewGadgetCommand => _newGadgetCommand ?? (_newGadgetCommand = new RelayCommand(CreateGadget));

        private ICommand _loadGadgetCommand;
        public ICommand LoadGadgetCommand => _loadGadgetCommand ?? (_loadGadgetCommand = new RelayCommand(UpdateGadget));
        #endregion
        
        #region ICommands of Customer
        private ICommand _deleteCustomerCommand;
        public ICommand DeleteCustomerCommand => _deleteCustomerCommand ?? (_deleteCustomerCommand = new RelayCommand(DeleteCustomer));

        private ICommand _saveCustomerCommand;
        public ICommand SaveCustomerCommand => _saveCustomerCommand ?? (_saveCustomerCommand = new RelayCommand(SaveCustomer));

        private ICommand _newCustomerCommand;
        public ICommand NewCustomerCommand => _newCustomerCommand ?? (_newCustomerCommand = new RelayCommand(CreateCustomer));

        private ICommand _loadCustomerCommand;
        public ICommand LoadCustomerCommand => _loadCustomerCommand ?? (_loadCustomerCommand = new RelayCommand(UpdateCustomer));
        #endregion

        #region ICommands of Loan
        private ICommand _deleteLoanCommand;
        public ICommand DeleteLoanCommand => _deleteLoanCommand ?? (_deleteLoanCommand = new RelayCommand(DeleteLoan));

        private ICommand _saveLoanCommand;
        public ICommand SaveLoanCommand => _saveLoanCommand ?? (_saveLoanCommand = new RelayCommand(SaveLoan));

        private ICommand _newLoanCommand;
        public ICommand NewLoanCommand => _newLoanCommand ?? (_newLoanCommand = new RelayCommand(CreateLoan));

        private ICommand _loadLoanCommand;
        public ICommand LoadLoanCommand => _loadLoanCommand ?? (_loadLoanCommand = new RelayCommand(UpdateLoan));
        #endregion

        #region Gadget Elements for Binding
        private ObservableCollection<Gadget> _gadgets;

        public ObservableCollection<Gadget> Gadgets
        {
            get => _gadgets;
            set => SetProperty(ref _gadgets, value, nameof(_gadgets));
        }

        public Gadget SelectedGadget { get; set; }

        private string _gadgetName;

        public string GadgetName
        {
            get => _gadgetName;
            set => SetProperty(ref _gadgetName, value, nameof(_gadgetName));
        }

        private double _gadgetPrice;

        public double GadgetPrice
        {
            get => _gadgetPrice;
            set => SetProperty(ref _gadgetPrice, value, nameof(_gadgetPrice));
        }

        private string _gadgetManufacturer;

        public string GadgetManufacturer
        {
            get => _gadgetManufacturer;
            set => SetProperty(ref _gadgetManufacturer, value, nameof(_gadgetManufacturer));
        }

        public IList<domain.Condition> Conditions => Enum.GetValues(typeof(domain.Condition)).Cast<domain.Condition>()
            .ToList<domain.Condition>();

        private domain.Condition _gadgetCondition;

        public domain.Condition GadgetCondition
        {
            get => _gadgetCondition;
            set => SetProperty(ref _gadgetCondition, value, nameof(_gadgetCondition));
        }
        #endregion

        #region Customer Elements for Binding
        private ObservableCollection<Customer> _customers;

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value, nameof(_customers));
        }

        public Customer SelectedCustomer { get; set; }

        private string _customerId;

        public string CustomerId
        {
            get => _customerId;
            set => SetProperty(ref _customerId, value, nameof(_customerId));
        }

        private string _customerName;

        public string CustomerName
        {
            get => _customerName;
            set => SetProperty(ref _customerName, value, nameof(_customerName));
        }

        private string _customerEmail;

        public string CustomerEmail
        {
            get => _customerEmail;
            set => SetProperty(ref _customerEmail, value, nameof(_customerEmail));
        }
        
        private string _customerPassword;

        public string CustomerPassword
        {
            get => _customerPassword;
            set => SetProperty(ref _customerPassword, value, nameof(_customerPassword));
        }
        #endregion

        #region Loan Elements for Binding
        private ObservableCollection<Loan> _loans;

        public ObservableCollection<Loan> Loans
        {
            get => _loans;
            set => SetProperty(ref _loans, value, nameof(Loans));
        }

        public Loan SelectedLoan { get; set; }

        private DateTime? _loanFrom;

        public DateTime? LoanFrom
        {
            get => _loanFrom;
            set => SetProperty(ref _loanFrom, value, nameof(_loanFrom));
        }

        private DateTime? _loanUntil;

        public DateTime? LoanUntil
        {
            get => _loanUntil;
            set => SetProperty(ref _loanUntil, value, nameof(_loanUntil));
        }

        private string _loanId;

        public string LoanId
        {
            get => _loanId;
            set => SetProperty(ref _loanId, value, nameof(_loanId));
        }

        private Customer _customer;

        public Customer Customer
        {
            get => _customer;
            set => SetProperty(ref _customer, value, nameof(_customer));
        }

        private string _selectedCustomerId;

        public string SelectedCustomerId
        {
            get => _selectedCustomerId;
            set => SetProperty(ref _selectedCustomerId, value, nameof(_selectedCustomerId));
        }

        private string _selectedGadgetId;

        public string SelectedGadgetId
        {
            get => _selectedGadgetId;
            set => SetProperty(ref _selectedGadgetId, value, nameof(_selectedGadgetId));
        }
        #endregion

        public MainWindowViewModel()
        {
            var serverString = ConfigurationManager.AppSettings.Get("server")?.ToString();
            libraryAdminService = new LibraryAdminService(serverString);
            var gadgets = libraryAdminService.GetAllGadgets();
            var loans = libraryAdminService.GetAllLoans();
            var customers = libraryAdminService.GetAllCustomers();

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
                if (customers.Count == 0)
                    MessageBox.Show("Loans list empty!");
                else
                    Customers = new ObservableCollection<Customer>(customers);
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
            var updatedCustomers = libraryAdminService.GetAllCustomers();

            if (updatedGadgets != null && updatedLoans != null && updatedCustomers != null)
            {
                if (SelectedLoan == null)
                {
                    Loans.Clear();
                }
                else if (SelectedLoan != null)
                {
                    List<Loan> LoanList = Loans.ToList();
                    foreach(Loan loanItem in LoanList)
                    {
                        if (!loanItem.Equals(SelectedLoan))
                        {
                            Loans.Remove(loanItem);
                        }
                    }
                }
                foreach(Loan loanItem in updatedLoans)
                {
                    if(!loanItem.Equals(SelectedLoan))
                    Loans.Add(loanItem);
                }
                if (SelectedGadget == null)
                {
                    Gadgets.Clear();
                }
                else if (SelectedGadget != null)
                {
                    List<Gadget> GadgetList = Gadgets.ToList();
                    foreach (Gadget GadgetItem in GadgetList)
                    {
                        if (!GadgetItem.Equals(SelectedGadget))
                        {
                            Gadgets.Remove(GadgetItem);
                        }
                    }
                }
                foreach (Gadget GadgetItem in updatedGadgets)
                {
                    if (!GadgetItem.Equals(SelectedGadget))
                        Gadgets.Add(GadgetItem);
                }
                if (SelectedCustomer == null)
                {
                    Customers.Clear();
                }
                else if (SelectedCustomer != null)
                {
                    List<Customer> CustomerList = Customers.ToList();
                    foreach (Customer CustomerItem in CustomerList)
                    {
                        if (!CustomerItem.Equals(SelectedCustomer))
                        {
                            Customers.Remove(CustomerItem);
                        }
                    }
                }
                foreach (Customer CustomerItem in updatedCustomers)
                {
                    if (!CustomerItem.Equals(SelectedCustomer))
                        Customers.Add(CustomerItem);
                }
            }
        }

        #region Gadget Functions
        private void DeleteGadget()
        {
            if (SelectedGadget != null)
            {
                string sMessageBoxText =
                    $"Are you sure you want to delete Gadget {SelectedGadget.Name} with ID  {SelectedGadget.InventoryNumber}?";
                string sCaption = "Delete";

                MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult rsltMessageBox =
                        MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                if (rsltMessageBox == MessageBoxResult.Yes)
                {
                    if (SelectedGadget != null && libraryAdminService.DeleteGadget(SelectedGadget))
                    {
                        Gadgets.Remove(SelectedGadget);
                    }
                    else
                    {
                        MessageBox.Show("Removing Gadget failed!");
                    }
                }
            }
        }

        private void SaveGadget()
        {
            if(SelectedGadget != null)
            {
                if(!libraryAdminService.UpdateGadget(SelectedGadget))
                {
                    libraryAdminService.AddGadget(SelectedGadget);
                }
            }
        }

        private void CreateGadget()
        {
            Gadgets.Add(new Gadget("new"));
        }

        private void UpdateGadget()
        {
            Gadgets.Clear();
            libraryAdminService.GetAllGadgets().ForEach(Gadgets.Add);
            SelectedGadget = Gadgets.FirstOrDefault();
        }
        #endregion

        #region Customer Functions
        private void DeleteCustomer()
        {
            if (SelectedCustomer != null)
            {
                string sMessageBoxText =
                    $"Are you sure you want to delete Customer {SelectedCustomer.Name} with ID  {SelectedCustomer.Studentnumber}?";
                string sCaption = "Delete";

                MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult rsltMessageBox =
                        MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                if (rsltMessageBox == MessageBoxResult.Yes)
                {
                    if (SelectedCustomer != null && libraryAdminService.DeleteCustomer(SelectedCustomer))
                    {
                        Customers.Remove(SelectedCustomer);
                    }
                    else
                    {
                        MessageBox.Show("Removing Customer failed!");
                    }
                }
            }
        }

        private void SaveCustomer()
        {
            if (SelectedCustomer != null)
            {
                if (!libraryAdminService.UpdateCustomer(SelectedCustomer))
                {
                    libraryAdminService.AddCustomer(SelectedCustomer);
                }
            }
        }

        private void CreateCustomer()
        {
            Customers.Add(new Customer("new", "password", "new@mail.com", "1565"));
        }

        private void UpdateCustomer()
        {
            Customers.Clear();
            libraryAdminService.GetAllCustomers().ForEach(Customers.Add);
            SelectedCustomer = Customers.FirstOrDefault();
        }
        #endregion

        #region Loan Functions
        private void DeleteLoan()
        {
            if (SelectedLoan != null)
            {
                string sMessageBoxText =
                    $"Are you sure you want to delete Loan of {SelectedLoan.Gadget} rented by {SelectedLoan.Customer} with ID {SelectedLoan.Id}?";
                string sCaption = "Delete";

                MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult rsltMessageBox =
                        MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                if (rsltMessageBox == MessageBoxResult.Yes)
                {
                    if (SelectedCustomer != null && libraryAdminService.DeleteLoan(SelectedLoan))
                    {
                        Loans.Remove(SelectedLoan);
                    }
                    else
                    {
                        MessageBox.Show("Removing Loan failed!");
                    }
                }
            }
        }

        private void SaveLoan()
        {
            if (SelectedLoan != null)
            {
                if (!libraryAdminService.UpdateLoan(SelectedLoan))
                {
                    libraryAdminService.AddLoan(SelectedLoan);
                }
            }
        }

        private void CreateLoan()
        {
            Loans.Add(new Loan("568", new Gadget(), new Customer(), new DateTime(), new DateTime()));
        }

        private void UpdateLoan()
        {
            Loans.Clear();
            libraryAdminService.GetAllLoans().ForEach(Loans.Add);
            SelectedLoan = Loans.FirstOrDefault();
        }
        #endregion
    }
}