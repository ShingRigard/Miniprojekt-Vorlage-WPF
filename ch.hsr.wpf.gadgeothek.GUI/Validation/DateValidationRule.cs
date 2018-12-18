using System;
using System.Globalization;
using System.Windows.Controls;

namespace ch.hsr.wpf.gadgeothek.GUI.Validation
{
    class DateValidationRule : ValidationRule
    {
        public DateValidationRule()
        {
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime date = DateTime.Today;

            try
            {
                if (((string) value).Length > 0)
                    date = DateTime.Parse((String) value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

            if (date < StartDate || date > EndDate)
            {
                return new ValidationResult(false,
                    $"Date should be in range of: {StartDate.ToShortDateString()} and {EndDate.ToShortDateString()}");
            }

            return new ValidationResult(true, null);
        }
    }
}