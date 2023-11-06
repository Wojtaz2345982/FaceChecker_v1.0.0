using FluentValidation;
using HackHeroes.Application.ApplicationUser;
using HackHeroes.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Commands.Classes.CreateClass
{
    public class CreateClassCommandValidator : AbstractValidator<CreateClassCommand>
    {

        public CreateClassCommandValidator(IHackHeroesRepository repository, IUserContext userContext )
        {
            var user = userContext.GetCurrentUser();

            RuleFor(c => c.Name)
               .NotEmpty().WithMessage("Nazwa nie może być pusta!")
               .MaximumLength(4).WithMessage("Nazwa może zawierać maksymalnie 4 znaki!")
               .MinimumLength(2).WithMessage("Nazwa musi zawierać co najmniej 2 znaki!")
                .Custom((value, context) =>
                {
                    if (value == null)
                    {
                        return;
                    }
                    var existingclass = repository.GetClassByName(value, user!.Id).Result;
                    if (existingclass != null)
                    {
                        context.AddFailure($"Klasa: {value} już istnieje!");
                    }
                });
            
        }
    }
}
