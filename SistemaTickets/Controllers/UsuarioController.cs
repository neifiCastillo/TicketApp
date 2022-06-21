using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SistemaTickets.Data;
using SistemaTickets.DTO;
using SistemaTickets.Models;

namespace SistemaTickets.Controllers
{
    public class UsuarioController : Controller
    {
		private TicketContext _context; 
		private DbSet<Usuario> _usuarioRepo;
		private RespuestaServer _response;
		public UserDetails _userDetails;
		public UsuarioController()
		{
			_context = new TicketContext();
			_usuarioRepo = _context.Set<Usuario>();
			_response = new RespuestaServer();
			_userDetails = new UserDetails();
		}

		// GET: Usuario
		public ActionResult Index()
        {
            return View();
        }

		public bool ValidarPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
			{
				var computedHash = hmac.ComputeHash(System.Text.ASCIIEncoding.UTF8.GetBytes(password));
				for (int i = 0; i < computedHash.Length; i++)
				{
					if (computedHash[i] != passwordHash[i]) return false;
				}
			}

			return true;
		}

		public void GenerarPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new System.Security.Cryptography.HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.ASCIIEncoding.UTF8.GetBytes(password));
			}
		}

		private async Task<bool> ConfirmarUsuarioExiste(UsuarioModel model)
		{
			try
			{
				return await _usuarioRepo.AnyAsync(u => u.NombreUsuario == model.Usuario);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpPost]
        public async Task<ActionResult> RegistrarUsuario(UsuarioModel model)
		{
			try
			{
				// Si el usuario ya existe (esta en uso)
				if (await ConfirmarUsuarioExiste(model)) return Json(new RespuestaServer { Titulo = "Error", Mensaje = "Este usuario no esta disponible", Estatus = false }, JsonRequestBehavior.AllowGet);

				GenerarPasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt); // encriptamos la clave

				var nuevoUsuario = new Usuario
				{
					NombreUsuario = model.Usuario,
					PasswordHash = passwordHash,
					PasswordSalt = passwordSalt,
					Estatus = true,
					Rol = Roles.Cliente,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now
				};

				_usuarioRepo.Add(nuevoUsuario);

				await _context.SaveChangesAsync();

				return Json(new RespuestaServer { Titulo = "Ok", Mensaje = "Usuario registrado exitosamente", Estatus = true }, JsonRequestBehavior.AllowGet);

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpPost]
		public async Task<ActionResult> AutenticarUsuario(UsuarioModel model)
		{
			try
			{
				// Si el usuario no existe 
				if (!(await ConfirmarUsuarioExiste(model))) return Json(new RespuestaServer { Titulo = "Error", Mensaje = "No se puedo validar el usuario!!", Estatus = false });

				var usuarioObj = await _usuarioRepo.SingleAsync(x => x.NombreUsuario == model.Usuario); // Buscamos usuario por (Usuario)

				if (usuarioObj != null)
				{
					_userDetails.Id = usuarioObj.Id;
					_userDetails.Rol = usuarioObj.Rol;
					_userDetails.Usuario = usuarioObj.NombreUsuario;

					Session["user"] = _userDetails;
				}

				return	ValidarPasswordHash(model.Password, usuarioObj.PasswordHash, usuarioObj.PasswordSalt) // validamos la contrasena encryptada anteriormente
						? Json(new RespuestaServer { Titulo = "Ok", Mensaje = "Usuario autenticado exitosamente!!", Estatus = true }, JsonRequestBehavior.AllowGet)
						: Json(new RespuestaServer { Titulo = "Error", Mensaje = "Fallo al autenticar usuario", Estatus = false }, JsonRequestBehavior.AllowGet);

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}	

		[HttpGet]

		public ActionResult IrAHome()
		{
			return Json(new { url = "~/Home/Home" });
		}


	}

	public class UserDetails
	{
		public int Id { get; set; }
		public string Usuario { get; set; }
		public Roles Rol { get; set; }
	}
}