using System.ComponentModel.DataAnnotations;

namespace ProvaRemotaCapta.Data.Validator
{
    public class ValidaCPF : ValidationAttribute
    {
        public ValidaCPF() : base("{0} não é um CPF válido")
        { }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                bool valido = Util.ValidaCPF(value.ToString());
                if (valido)
                    return null;
                return new ValidationResult(base.FormatErrorMessage(validationContext.MemberName)
                    , new string[] { validationContext.MemberName });
            }
            catch (Exception)
            {

                return new ValidationResult(base.FormatErrorMessage(validationContext.MemberName)
                , new string[] { validationContext.MemberName });
            }
            
        }
    }
}
