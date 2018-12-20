using ch.hsr.wpf.gadgeothek.GUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ch.hsr.wpf.gadgeothek.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel AppVm { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            AppVm = new MainWindowViewModel();
            DataContext = AppVm;
        }

        private void New_Click_Gadget(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;
            if (viewModel.NewGadgetCommand.CanExecute(sender))
                viewModel.NewGadgetCommand.Execute(sender);
            int rowIndex = GadgetDataGrid.Items.Count - 1;
            object item = GadgetDataGrid.Items[rowIndex];
            GadgetDataGrid.SelectedItem = item;
            DataGridRow row = GadgetDataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
            if (row == null)
            {
                GadgetDataGrid.ScrollIntoView(item);
                row = GadgetDataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
            }
            DataGridCell cell = GetCell(GadgetDataGrid, row, 0);
            cell.Focus();
        }

        private void New_Click_Customer(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;
            if (viewModel.NewCustomerCommand.CanExecute(sender))
                viewModel.NewCustomerCommand.Execute(sender);
            int rowIndex = CustomerDataGrid.Items.Count - 1;
            object item = CustomerDataGrid.Items[rowIndex];
            CustomerDataGrid.SelectedItem = item;
            DataGridRow row = CustomerDataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
            if (row == null)
            {
                CustomerDataGrid.ScrollIntoView(item);
                row = CustomerDataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
            }
            DataGridCell cell = GetCell(CustomerDataGrid, row, 0);
            cell.Focus();
        }

        private void New_Click_Loan(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;
            if (viewModel.NewLoanCommand.CanExecute(sender))
                viewModel.NewLoanCommand.Execute(sender);
            int rowIndex = LoanDataGrid.Items.Count - 1;
            object item = LoanDataGrid.Items[rowIndex];
            LoanDataGrid.SelectedItem = item;
            DataGridRow row = LoanDataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
            if (row == null)
            {
                LoanDataGrid.ScrollIntoView(item);
                row = LoanDataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
            }
            DataGridCell cell = GetCell(LoanDataGrid, row, 0);
            cell.Focus();
        }

        private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj)
       where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj)
            where childItem : DependencyObject
        {
            foreach (childItem child in FindVisualChildren<childItem>(obj))
            {
                return child;
            }

            return null;
        }


        private DataGridCell GetCell(DataGrid dataGrid, DataGridRow rowContainer, int column)
        {
            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(rowContainer);
                if (presenter == null)
                {
                    /* if the row has been virtualized away, call its ApplyTemplate() method
                     * to build its visual tree in order for the DataGridCellsPresenter
                     * and the DataGridCells to be created */
                    rowContainer.ApplyTemplate();
                    presenter = FindVisualChild<DataGridCellsPresenter>(rowContainer);
                }
                if (presenter != null)
                {
                    DataGridCell cell = presenter.ItemContainerGenerator.ContainerFromIndex(column) as DataGridCell;
                    if (cell == null)
                    {
                        /* bring the column into view
                         * in case it has been virtualized away */
                        dataGrid.ScrollIntoView(rowContainer, dataGrid.Columns[column]);
                        cell = presenter.ItemContainerGenerator.ContainerFromIndex(column) as DataGridCell;
                    }
                    return cell;
                }
            }
            return null;
        }
    }
}
