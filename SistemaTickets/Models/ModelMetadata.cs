using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaTickets.Models
{
	public class ModelMetadata
	{
		[Key]
		public int Id { get; set; }
		[DataType(DataType.Date)]
		public DateTime FechaCreacion { get; set; }
		[DataType(DataType.Date)]
		public DateTime FechaModificacion { get; set; }
		public bool Estatus { get; set; }
	}
}