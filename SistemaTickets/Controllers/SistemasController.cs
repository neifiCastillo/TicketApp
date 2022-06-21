using SistemaTickets.Data;
using SistemaTickets.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SistemaTickets.Controllers
{
    public class SistemasController : Controller
    {
		private TicketContext _context;
		private DbSet<Sistema> _repoSistemas; 
		public SistemasController()
		{
			_context = new TicketContext();
			_repoSistemas = _context.Set<Sistema>();
		}

		// GET: Sistemas
		public ActionResult Index()
        {
            return View();
        }

		[HttpGet]
		public async Task<ActionResult> GetAllSistemas()
		{
			try
			{
				var results = await _repoSistemas.ToListAsync();
				return Json(results, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

        [HttpPost]
        public async Task<ActionResult> CreateSistema(SistemaCreate sistema)
		{
			try
			{

				var newSistema = new Sistema
				{
					Nombre = sistema.Nombre,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now,
					Estatus = true
				};

				_repoSistemas.Add(newSistema);
				await _context.SaveChangesAsync();

				return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpPost]
		public async Task<ActionResult> UpdateSistema(SistemaCreate sistema)
		{
			try
			{
				var sistemaXtoUpdate = await _repoSistemas.FirstOrDefaultAsync(c => c.Id == sistema.Id);
				if (sistemaXtoUpdate == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

				sistemaXtoUpdate.Nombre = sistema.Nombre;


				if (_context.Entry(sistemaXtoUpdate).State == EntityState.Detached)
				{
					_repoSistemas.Attach(sistemaXtoUpdate);
				}
				_context.Entry(sistemaXtoUpdate).State = EntityState.Modified;
				await _context.SaveChangesAsync();

				return Json(sistemaXtoUpdate, JsonRequestBehavior.AllowGet);

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpPost]
		public async Task<ActionResult> DeleteSistema(Sistema sistema)
		{

			try
			{
				var sistemaToDelete = await _repoSistemas.FirstOrDefaultAsync(x => x.Id == sistema.Id);
				_repoSistemas.Remove(sistemaToDelete);
				await _context.SaveChangesAsync();
				return Json(sistema, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


	}

	public class SistemaCreate
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
	}

}