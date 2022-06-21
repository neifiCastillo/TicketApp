using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaTickets.Models
{
	public class Usuario : ModelMetadata
	{
		public string NombreUsuario { get; set; }

		public byte[] PasswordSalt { get; set; }
		public byte[] PasswordHash { get; set; }
		public Roles Rol { get; set; }

		//public int SistemaId { get; set; }
		//public Sistema Sistema { get; set; }
	}

	public enum Roles
	{
		Admin = 1,
		Soporte = 2,
		Cliente = 3
	}
}