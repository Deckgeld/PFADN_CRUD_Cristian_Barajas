using Microsoft.EntityFrameworkCore;
using Passengers.Core.Passengers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passengers.DataAccess
{
    public class PassengersDataContext : DbContext
    {
        public virtual DbSet<Passenger> Passengers { get; set; }

        public PassengersDataContext(DbContextOptions options) : base(options)
        {

        }

    }
}
