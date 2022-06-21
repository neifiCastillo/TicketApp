using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SistemaTickets.Models
{
	public class Empleado : ModelMetadata
	{
		[Required]
		[MinLength(11)]
		[MaxLength(11)]
		public string Cedula { get; set; }
		public string NombreCompleto { get; set; }
	}
}