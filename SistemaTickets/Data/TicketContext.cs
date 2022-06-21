using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

using SistemaTickets.Models;

namespace SistemaTickets.Data
{
	public class TicketContext : DbContext
	{
		public TicketContext() : base("name = TicketDbContext")
		{
		}

		public DbSet<Usuario> Usuarios { get; set; }
		public DbSet<Empleado> Empleados { get; set; }
		public DbSet<Ticket> Tickets { get; set; }
		public DbSet<Sistema> Sistemas { get; set; }


	}
}