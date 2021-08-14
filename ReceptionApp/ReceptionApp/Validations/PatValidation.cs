using FluentValidation;
using FluentValidation.Validators;
using ReceptionApp.ViewModel;

namespace ReceptionApp.Validations
{
    public class PatValidation : AbstractValidator<AddPAtientVM>
    {
        public PatValidation()
        {
            RuleFor(Name => Name.Name).MinimumLength(3).WithMessage("Name must contain min. 3 character!");
            RuleFor(Surname => Surname.Surname).MinimumLength(3).WithMessage("Name must contain min. 3 character!");
            RuleFor(Number => Number.Number).MinimumLength(9).WithMessage("Number must contain min. 9 digit!");
            RuleFor(Date => Date.Birthdate).NotEmpty().WithMessage("Date must not empty!");
            RuleFor(Email => Email.Mail).EmailAddress(EmailValidationMode.Net4xRegex);
        }
    }
}
