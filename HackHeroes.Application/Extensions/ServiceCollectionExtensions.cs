using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using HackHeroes.Application.ApplicationUser;
using HackHeroes.Application.HackHeroes.Commands.Classes.CreateClass;
using HackHeroes.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.Extensions
{
    //Konfiguracja serwisów wartwy apliakcji
    public static class ServiceCollectionExtensions
    {
        public static void AddIApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();

            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                var scope = provider.CreateScope();
                var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();
                cfg.AddProfile(new HackHeroesMappingProfile(userContext));
            }).CreateMapper()
            );

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateClassCommand).Assembly));


            services.AddValidatorsFromAssemblyContaining<CreateClassCommand>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

        }
    }
}
