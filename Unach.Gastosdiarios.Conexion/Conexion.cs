using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unach.Gastosdiarios.Conexion
{
	public class Conexion
	{
		public static MySqlConnection ObtenerConexion()
		{
			string connectionString = "Server=localhost;Database=turismo;Uid=root;Pwd=root;";
			return new MySqlConnection(connectionString);
		}
	}
}
