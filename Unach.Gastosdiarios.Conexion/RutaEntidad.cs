using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unach.Gastosdiarios.Conexion
{
	public class RutaEntidad
	{
		public int IdRuta { get; set; }
		public string NombreRuta { get; set; }
		public string Origen { get; set; }       // <-- Añade esto
		public string Destino { get; set; }
		public int IdOperadora { get; set; }
	}
}
