using FluentValidation;
using PropertyChanged;
using ReceptionApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReceptionApp.Validations
{
    [AddINotifyPropertyChangedInterface]
    public class SubscValidation:AbstractValidator<SubscVM>
    {
        public SubscValidation()
        {
            //RuleFor(PName => PName.PatientName).MinimumLength(3).WithMessage("Name must contain min. 3 character !");
            //RuleFor(DName => DName.DoctorName).MinimumLength(3).WithMessage("Name must contain min. 3 character !");
        }
    }
}
