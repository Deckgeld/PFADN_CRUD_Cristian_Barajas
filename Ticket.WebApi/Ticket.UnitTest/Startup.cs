using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Passengers.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.ApplicationServices.Tickets;
using Ticket.Core;
using Ticket.DataAccess;
using Ticket.DataAccess.Repositories;

namespace Passengers.UnitTest
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

            services.AddDbContext<TicketDataContext>(options =>
                options.UseInMemoryDatabase("PassagersTest")
            );

            services.AddAutoMapper(typeof(Ticket.ApplicationServices.MapperProfile));

            services.AddTransient<IRepository<int, TicketC>, TicketRepository>();
            services.AddTransient<ITiketsAppService, TiketsAppService>();
        }

        public void Configure()
        {
            
        }
    }
}
