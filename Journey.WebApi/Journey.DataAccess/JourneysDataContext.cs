using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.DataAccess
{
    public class JourneysDataContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public virtual DbSet<Core.Journey.JourneyC> Journeys { get; set; }

        public JourneysDataContext(DbContextOptions<JourneysDataContext> options) : base(options)
        {

        }
    }
}
