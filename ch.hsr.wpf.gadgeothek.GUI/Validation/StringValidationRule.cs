using System.Windows.Controls;
using System.Globalization;

namespace ch.hsr.wpf.gadgeothek.GUI.Validation
{
    public class StringValidationRule : ValidationRule
    {
        public StringValidationRule()
        {
        }

        public int Min { get; set; }
        public int Max { get; set; }
        public bool CanBeEmpty { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string str = "";

            if (!(value is string))
            {
                return new ValidationResult(false, "Value is not a string");
            }
            str = (string)value;

            if (!CanBeEmpty && str.Length == 0)
            {
                return new ValidationResult(false, "Value can't be empty");
            }
            else if (str.Length < Min || Max > 0 && str.Length > Max)
            {
                return new ValidationResult(false, $"String length has to be in the range: {Min} - {Max}");
            }

            return new ValidationResult(true, null);
        }
    }
}
