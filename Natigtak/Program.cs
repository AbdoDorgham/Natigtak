using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Natigtak.Models;
using Natigtak.Validations;

namespace Natigtak
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssembly(typeof(MinMaxDegreeValidator).Assembly);
            builder.Services.AddScoped<IValidator<StageNewSearch>, StageNewSearchValidator>();
            builder.Services.AddScoped<IValidator<MinMaxDegree>, MinMaxDegreeValidator>();


            //Adding DBContext 
            builder.Services.AddDbContext<Natiga2023Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Students}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
