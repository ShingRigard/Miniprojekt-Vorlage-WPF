using System;
using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;

namespace ch.hsr.wpf.gadgeothek.GUI.ViewModels
{
    public class LoanViewModel
    {
        private readonly Loan _loan;
        private readonly LibraryAdminService _service;

        public LoanViewModel(Loan loan, LibraryAdminService service)
        {
            _loan = loan;
            _service = service;
        }

        public Loan GetLoan()
        {
            return _loan;
        }
        public string Id => _loan.Id;

        public string CustomerId
        {
            get => _loan.CustomerId;
            set
            {
                _loan.CustomerId = value;
                _service.UpdateLoan(_loan);
            }
        }

        public Gadget Gadget
        {
            get => _loan.Gadget;
            set
            {
                _loan.Gadget = value;
                _service.UpdateLoan(_loan);
            }
        }

        public string GadgetId
        {
            get => _loan.GadgetId;
            set
            {
                _loan.GadgetId = value;
                _service.UpdateLoan(_loan);
            }
        }


        public DateTime? PickUpDate
        {
            get => _loan.PickupDate;
            set
            {
                _loan.PickupDate = value;
                _service.UpdateLoan(_loan);
            }
        }

        public DateTime? ReturnDate
        {
            get => _loan.ReturnDate;

            set
            {
                _loan.ReturnDate = value;
                _service.UpdateLoan(_loan);
            }
        }

        public bool Remove()
        {
            return _service.DeleteLoan(_loan);
        }
    }
}