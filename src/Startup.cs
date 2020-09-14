using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OwnedEntityTypes.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace OwnedEntityTypes
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddTransient<IPersonService, PersonService>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite($"Data Source=ownedentitytypes.db")
                .EnableSensitiveDataLogging()
                );
                //options.UseSqlServer(Configuration.GetConnectionString("ApplicationDbContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            ILogger<Startup> logger,
            ApplicationDbContext applicationDbContext)
        {
            if (env.IsDevelopment())
            {
                logger.LogInformation("In Development mode.");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                logger.LogInformation("In Production/Staging mode.");
                app.UseExceptionHandler("/Error");
            }

            SeedData(logger, applicationDbContext);

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private void SeedData(ILogger<Startup> logger, ApplicationDbContext context)
        {
            bool createdDb = context.Database.EnsureCreated();

            if (createdDb)
            {
                logger.LogInformation("I've created the DB.");
            }

            logger.LogInformation("Start seeding data.");

            Person person = context.People.FirstOrDefault();
            if (person == null)
            {
                logger.LogInformation("No person in DB, I create one.");
                person = new Person
                {
                    FirstName = "Nicola",
                    LastName = "Biada",
                    MainAddress = new StreetAddress
                    {
                        City = "Merano",
                        Country = "Italy",
                        Street = "Piazza Teatro, 1"
                    }
                };
                context.People.Add(person);

                if (context.ChangeTracker.HasChanges())
                    context.SaveChanges();

                logger.LogInformation("Person with name \"{First} {Last}\" has been created!", person.FirstName, person.LastName);
            }
            else
            {
                logger.LogInformation("Found person called \"{First} {Last}\"", person.FirstName, person.LastName);
            }
        }
    }
}
