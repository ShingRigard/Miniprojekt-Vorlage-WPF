using System;
using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;


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
        get { return _customer.Email; }
        set
        {
            _customer.Email = value;
            _customer.UpdateCustomer(_customer);
        }
    }

    public string Name
    {
        get { return _customer.Name; }
        set
        {
            _customer.Name = value;
            _customer.UpdateCustomer(_customer);
        }
    }

    public string Password
    {
        get { return _customer.Password; }
        set {
            _customer.Password = value;
            _customer.UpdateCustomer(_customer);
        }
    }

    public string Studentnumber
    {
        get { return _customer.Studentnumber = value; }
        set
        {
            _customer.Studentnumber = value;
            _customer.UpdateCustomer(_customer);
        }
    }
}