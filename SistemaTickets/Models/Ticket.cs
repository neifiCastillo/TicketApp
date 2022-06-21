using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaTickets.Models
{
	public class Ticket : ModelMetadata
	{
		public string Titulo { get; set; }
		public string Descripcion { get; set; }
		[DataType(DataType.Date)]
		public DateTime FechaVencimiento { get; set; }
		public Prioridad Prioridad { get; set; }
		public Estado Estado { get; set; }
		public Empleado Empleado { get; set; }
		public string NombreEmpleado { get; set; }

	}

	public enum Prioridad
	{
		Alta = 1,
		Media = 2,
		Baja = 3
	}

	public enum Estado
	{
		Pendiente = 1,
		EnProceso = 2,
		Completado = 3
	}
}