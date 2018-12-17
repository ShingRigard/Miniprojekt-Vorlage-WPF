using System;
using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;

namespace ch.hsr.wpf.gadgeothek.GUI.ViewModels
{
    public class ReservationViewModel
    {
        private readonly Reservation _reservation;
        private readonly LibraryAdminService _service;

        public ReservationViewModel(Reservation reservation, LibraryAdminService service)
        {
            _reservation = reservation;
            _service = service;
        }

        public string Id => _reservation.Id;

        public string CustomerId
        {
            get => _reservation.CustomerId;
            set
            {
                _reservation.CustomerId = value;
                _service.UpdateReservation(_reservation);
            }
        }

        public Customer Customer
        {
            get => _reservation.Customer;
            set
            {
                _reservation.Customer = value;
                _service.UpdateReservation(_reservation);
            }
        }

        public string GadgetId
        {
            get => _reservation.GadgetId;
            set
            {
                _reservation.GadgetId = value;
                _service.UpdateReservation(_reservation);
            }
        }

        public Gadget Gadget
        {
            get => _reservation.Gadget;
            set
            {
                _reservation.Gadget = value;
                _service.UpdateReservation(_reservation);
            }
        }

        public DateTime? ReservationDate
        {
            get => _reservation.ReservationDate;
            set
            {
                _reservation.ReservationDate = value;
                _service.UpdateReservation(_reservation);
            }
        }

        public bool Finished
        {
            get => _reservation.Finished;
            set
            {
                _reservation.Finished = value;
                _service.UpdateReservation(_reservation);
            }
        }

        public int WaitingPosition
        {
            get => _reservation.WaitingPosition;
            set
            {
                _reservation.WaitingPosition = value;
                _service.UpdateReservation(_reservation);
            }
        }

        public bool isReady
        {
            get => _reservation.IsReady;
            set
            {
                _reservation.IsReady = value;
                _service.UpdateReservation(_reservation);
            }
        }

        public bool Remove()
        {
            return _service.DeleteReservation(_reservation);
        }
    }
}