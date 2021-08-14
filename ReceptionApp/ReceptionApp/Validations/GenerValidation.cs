using FluentValidation;
using ReceptionApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReceptionApp.Validations
{
    public class GenerValidation : AbstractValidator<GenerUserPasswVM>
    {
        public GenerValidation()
        {
            RuleFor(x => x.PatientUsername).MinimumLength(5).WithMessage("Username must contain min. 5 character!");
            RuleFor(x => x.PatientPassword).MinimumLength(5).WithMessage("Password must contain min. 5 character!");
            RuleFor(x => x.DoctorUsername).MinimumLength(5).WithMessage("Username must contain min. 5 character!");
            RuleFor(x => x.DoctorPassword).MinimumLength(5).WithMessage("Password must contain min. 5 character!");
        }
    }
}
