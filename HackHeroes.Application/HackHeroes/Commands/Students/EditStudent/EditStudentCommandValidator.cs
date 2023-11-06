using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Commands.Students.EditStudent
{
    public class EditStudentCommandValidator : AbstractValidator<EditStudentCommand>
    {
        public EditStudentCommandValidator()
        {

            Regex regex = new Regex("^[a-zA-ZĄĆĘŁŃÓŚŹŻąćęłńóśźż]+$");          
            RuleFor(s => s.FirstName)                       
                .NotEmpty().WithMessage("Imię nie może być puste!")
                .MinimumLength(2)
                .Custom((value, context) =>
                {
                    if (!regex.IsMatch(value) || value.Contains(" ")) 
                    {
                        context.AddFailure("Imię nie może zawierać cyfr, znaków białych oraz znaków specjalnych!");

                    }
                })
                .MaximumLength(20);

            RuleFor(s => s.LastName)
              .MinimumLength(2)
              .MaximumLength(20)
              .Custom((value, context) =>
              {
                  if (!regex.IsMatch(value) || value.Contains(" "))  
                  {
                      context.AddFailure("Nazwisko nie może zawierać cyfr, znaków białych oraz znaków specjalnych!");

                  }
              })
              .NotEmpty().WithMessage("Nazwisko nie może być puste!");
        }
    }
}
