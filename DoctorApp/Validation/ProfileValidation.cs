using DoctorApp.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorApp.Validation
{
    class ProfileValidation : AbstractValidator<ProfileVM>
    {
        public ProfileValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3);
            RuleFor(x => x.Surname)
                .NotEmpty()
                .MinimumLength(3);
            RuleFor(x => x.Mail)
                 .NotEmpty()
                 .MinimumLength(3)
                 .EmailAddress();
            RuleFor(x => x.Number)
                 .NotEmpty();
        }
    }
}
