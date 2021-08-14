using FluentValidation;
using PacientApp.ViewModels;

namespace PacientApp.Validation
{
    public class RegisterUCVMValidator : AbstractValidator<RegisterUCVM>
    {
        public RegisterUCVMValidator()
        {
            RuleFor(p => p.Name).NotEmpty().Length(3, 36).Matches("^[A-Za-z]*$");

            RuleFor(p => p.Surname).NotEmpty().Length(3, 36).Matches("^[A-Za-z]*$"); ;

            RuleFor(p => p.Mail).NotEmpty().Length(3, 80);

            //RuleFor(p => p.BirthDay).NotEmpty().Matches("^[0-9]*$");

            RuleFor(p => p.Number).NotEmpty().Length(10).Matches("^[0-9]*$");

            RuleFor(p => p.Username).NotEmpty().Length(8, 128);

            RuleFor(p => p.Password).NotEmpty().Length(8, 128);

        }
    }
}
