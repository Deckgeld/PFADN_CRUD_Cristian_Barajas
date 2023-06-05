using Journey.ApplicationServices.Journey;
using Journey.Core.Journey;
using Journey.DataAccess;
using Journey.DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Journey.UnitTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<JourneysDataContext>(options =>
                options.UseInMemoryDatabase("JourneysTest")
            );

            services.AddAutoMapper(typeof(Journey.ApplicationServices.MapperProfile));

            services.AddTransient<IRepository<int, JourneyC>, JourneyRepository>();
            services.AddTransient<IJourneyAppService, JourneyAppService>();
        }

        public void Configure()
        {
            
        }
    }
}
