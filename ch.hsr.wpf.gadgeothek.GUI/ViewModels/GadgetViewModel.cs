using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;

namespace ch.hsr.wpf.gadgeothek.GUI.ViewModels
{
    public class GadgetViewModel
    {
        private readonly Gadget _gadget;
        private readonly LibraryAdminService _service;

        public GadgetViewModel(Gadget gadget, LibraryAdminService service)
        {
            _service = service;
            _gadget = gadget;
        }

        public Gadget GetGadget()
        {
            return _gadget;
        }

        public string InventoryNumber => _gadget.InventoryNumber;

        public string Manufacturer
        {
            get => _gadget.Manufacturer;

            set
            {
                _gadget.Manufacturer = value;
                _service.UpdateGadget(_gadget);
            }
        }

        public string Name
        {
            get => _gadget.Name;
            set
            {
                _gadget.Name = value;
                _service.UpdateGadget(_gadget);
            }
        }

        public double Price
        {
            get => _gadget.Price;
            set
            {
                _gadget.Price = value;
                _service.UpdateGadget(_gadget);
            }
        }

        public Condition Condition
        {
            get => _gadget.Condition;
            set
            {
                _gadget.Condition = value;
                _service.UpdateGadget(_gadget);
            }
        }

        public bool Remove()
        {
            return _service.DeleteGadget(_gadget);
        }
    }
}