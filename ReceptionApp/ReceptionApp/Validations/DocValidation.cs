using FluentValidation;
using FluentValidation.Validators;
using ReceptionApp.ViewModel;

namespace ReceptionApp.Validations
{
    public class DocValidation : AbstractValidator<AddDoctorVM>
    {
        public DocValidation()
        {
            RuleFor(Name => Name.Name).MinimumLength(3).WithMessage("The name must contain min. 3 character!");
            RuleFor(Surname => Surname.Surname).MinimumLength(3).WithMessage("The surname must contain min. 3 character!");
            RuleFor(Email => Email.Email).EmailAddress(EmailValidationMode.Net4xRegex);
            RuleFor(Number => Number.Number).MinimumLength(9).WithMessage("Number must contain min. 9 digit!");
        }
    }
}
