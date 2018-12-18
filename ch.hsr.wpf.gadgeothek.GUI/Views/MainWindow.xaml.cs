using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ch.hsr.wpf.gadgeothek.GUI.ViewModels;
using ch.hsr.wpf.gadgeothek.service;

namespace ch.hsr.wpf.gadgeothek.GUI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>  
    public partial class MainWindow : Window
    {
        //public IEnumerable<CustomerViewModel> CustomerList { get; set; }
        //public ObservableCollection<GadgetViewModel> GadgetList { get; set; }
        //public ObservableCollection<ReservationViewModel> ReservationList { get; set; }
        //public ObservableCollection<LoanViewModel> LoanList { get; set; }
        //readonly LibraryAdminService _service;
        //private string _customerId;
        public MainWindowViewModel MainWindowViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel = new MainWindowViewModel();
            DataContext = MainWindowViewModel;
        }
    }
}