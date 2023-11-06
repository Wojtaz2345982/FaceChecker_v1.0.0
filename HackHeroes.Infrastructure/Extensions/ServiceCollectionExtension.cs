using HackHeroes.Domain.Services;
using HackHeroes.Infrastructure.DBContext;
using HackHeroes.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
            configuration.GetConnectionString("HackHeroes")));

            services.AddDefaultIdentity<IdentityUser>(options => {
                options.Stores.MaxLengthForKeys = 450;
            })
           .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IHackHeroesRepository, HackHeroesRepository>();

            services.AddScoped<IStudentsRepository, StudentsRepository>();

            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
        }
    }
}
