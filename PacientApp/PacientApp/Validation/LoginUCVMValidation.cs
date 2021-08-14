using FluentValidation;
using PacientApp.ViewModels;

namespace PacientApp.Validation
{
    public class LoginUCVMValidation : AbstractValidator<LoginUCVM>
    {
        public LoginUCVMValidation()
        {
            RuleFor(p => p.Username).NotEmpty().Length(1, 128);

            RuleFor(p => p.Password).NotEmpty().Length(1, 128);
        }
    }
}
