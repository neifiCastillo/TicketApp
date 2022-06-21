using SistemaTickets.Data;
using SistemaTickets.DTO;
using SistemaTickets.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SistemaTickets.Controllers
{
    public class TicketsController : Controller
    {

		private TicketContext _context;
		private DbSet<Ticket> _repoTikets;
		private DbSet<Empleado> _repoEmpleados;

		public TicketsController()
        {
			_context = new TicketContext();
			_repoTikets = _context.Set<Ticket>();
			_repoEmpleados = _context.Set<Empleado>();
		}

		public ActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public async Task<ActionResult> GetAllTickets()
		{
			var result = await _repoTikets
						 .Select(x => new itemTicket
						 {
							 Titulo = x.Titulo,
							 Descripcion = x.Descripcion,
							 Estado = x.Estado,
							 EstadoNombre = "",
							 Prioridad = x.Prioridad,
							 PrioridadNombre = "",
							 //NombreEmpleado = x.Empleado.NombreCompleto,
							 NombreEmpleado = x.NombreEmpleado,
							 //IdEmpleado = x.Empleado.Id,
							 FechaVencimiento = x.FechaVencimiento,
							 FechaCreacion = x.FechaCreacion
						 }).ToListAsync();

			result.ForEach(x =>
			{
				x.PrioridadNombre = Enum.GetName(typeof(Prioridad), x.Prioridad);
				x.EstadoNombre = Enum.GetName(typeof(Estado), x.Estado);
			});

			return Json(result, JsonRequestBehavior.AllowGet);
		}
		[HttpPost]
		public JsonResult SaveTicket(CreateTicket ticket)
        {
            try
            {
				var NewTicket = new Ticket
				{
					NombreEmpleado = ticket.NombreEmpleado,
					Titulo = ticket.Titulo,
					Descripcion = ticket.Descripcion,
					FechaVencimiento = ticket.FechaVencimiento,
					Estado = ticket.Estado,
					Prioridad = ticket.Prioridad,
					Estatus = true,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now,
				};
				_repoTikets.Add(NewTicket);

				_context.SaveChanges();

				return Json(new RespuestaServer { Titulo = "Ok", Mensaje = "Guardado correctamente", Estatus = true }, JsonRequestBehavior.AllowGet);


			}
            catch (Exception)
            {

                throw;
            }
        }

		[HttpGet]
		public async Task<ActionResult> GetAllEmpleados()
		{
			try
			{
				var result = await _repoEmpleados.ToListAsync();
				return Json(result, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}

	public class CreateTicket
    {
		public string Titulo { get; set; }
		public string Descripcion { get; set; }
		[DataType(DataType.Date)]
		public DateTime FechaVencimiento { get; set; }
		public Prioridad Prioridad { get; set; }
		public Estado Estado { get; set; }
		public string NombreEmpleado { get; set; }

	}
	public class itemTicket
	{
		public string Titulo { get; set; }
		public string Descripcion { get; set; }
		[DataType(DataType.Date)]
		public DateTime FechaCreacion { get; set; }
		public DateTime FechaVencimiento { get; set; }
		public Prioridad Prioridad { get; set; }
        public string PrioridadNombre { get; set; }
        public Estado Estado { get; set; }
		public string EstadoNombre { get; set; }
		public string NombreEmpleado { get; set; }
		public int IdEmpleado { get; set; }
	}
}