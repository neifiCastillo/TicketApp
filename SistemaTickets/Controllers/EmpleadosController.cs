using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SistemaTickets.Data;
using SistemaTickets.Models;

namespace SistemaTickets.Controllers
{
    public class EmpleadosController : Controller
    {
        private TicketContext _context;
        private DbSet<Empleado> _repoEmpleados; 
		public EmpleadosController()
		{
            _context = new TicketContext();
            _repoEmpleados = _context.Set<Empleado>();
		}

		// GET: Empleados
		public ActionResult Index()
        {
            return View();
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

        [HttpPost]
        public async Task<ActionResult> CreateEmpleado(EmpleadoCreate empleado)
		{
			try
			{
				var newEmpleado = new Empleado
				{
					Cedula = empleado.Cedula,
                    NombreCompleto = empleado.NombreCompleto,
                    FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now,
					Estatus = true
				};

				_repoEmpleados.Add(newEmpleado);
				await _context.SaveChangesAsync();

				return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpPost]
		public async Task<ActionResult> UpdateEmpleado(EmpleadoCreate empleado)
		{
			try
			{
				var empleadoXtoUpdate = await _repoEmpleados.FirstOrDefaultAsync(c => c.Id == empleado.Id);
				if (empleadoXtoUpdate == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

				empleadoXtoUpdate.Cedula = empleado.Cedula;
				empleadoXtoUpdate.NombreCompleto = empleado.NombreCompleto;


				if (_context.Entry(empleadoXtoUpdate).State == EntityState.Detached)
				{
					_repoEmpleados.Attach(empleadoXtoUpdate);
				}
				_context.Entry(empleadoXtoUpdate).State = EntityState.Modified;
				await _context.SaveChangesAsync();

				return Json(empleadoXtoUpdate, JsonRequestBehavior.AllowGet);

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpPost]
		public async Task<ActionResult> DeleteEmpleado(Empleado empleado)
		{
			
			try
			{
				var empleadoToDelete = await _repoEmpleados.FirstOrDefaultAsync(x => x.Id == empleado.Id);
				_repoEmpleados.Remove(empleadoToDelete);
				await _context.SaveChangesAsync();
				return Json(empleado, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

	}

	public class EmpleadoCreate
	{
		public int Id { get; set; }
		public string Cedula { get; set; }
		public string NombreCompleto { get; set; }
	}
}