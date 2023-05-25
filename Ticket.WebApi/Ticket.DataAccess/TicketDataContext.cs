using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core;

namespace Ticket.DataAccess
{
    public class TicketDataContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public virtual DbSet<TicketC> Tikets { get; set; }

        public TicketDataContext(DbContextOptions<TicketDataContext> options) : base(options)
        {

        }
    
    }
}
