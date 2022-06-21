using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaTickets.DTO
{
	public class RespuestaServer
	{
		public string Titulo { get; set; }
		public string Mensaje { get; set; }
		public bool Estatus { get; set; }
	}
}