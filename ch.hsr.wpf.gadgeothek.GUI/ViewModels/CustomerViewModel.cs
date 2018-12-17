using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;

namespace ch.hsr.wpf.gadgeothek.GUI.ViewModels
{
    public class CustomerViewModel
    {
        private readonly Customer _customer;
        private readonly LibraryAdminService _service;

        public CustomerViewModel(LibraryAdminService service, Customer customer)
        {
            _service = service;
            _customer = customer;
        }

        public Customer GetCustomer()
        {
            return _customer;
        }

        public string Email
        {
            get => _customer.Email;
            set
            {
                _customer.Email = value;
                _service.UpdateCustomer(_customer);
            }
        }

        public string Name
        {
            get => _customer.Name;
            set
            {
                _customer.Name = value;
                _service.UpdateCustomer(_customer);
            }
        }

        public string Password
        {
            get => _customer.Password;
            set
            {
                _customer.Password = value;
                _service.UpdateCustomer(_customer);
            }
        }

        public string Studentnumber
        {
            get => _customer.Studentnumber;
            set
            {
                _customer.Studentnumber = value;
                _service.UpdateCustomer(_customer);
            }
        }

        public bool Remove()
        {
            return _service.DeleteCustomer(_customer);
        }
    }
}