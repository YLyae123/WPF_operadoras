using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unach.Gastosdiarios.Conexion;

namespace Unach.Gastosdiarios.Conexion
{
	public class OperadoraEntidad
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
	}

	public static class OperadoraDAL
	{
		public static List<OperadoraEntidad> ObtenerOperadoras()
		{
			var lista = new List<OperadoraEntidad>();

			using (var conn = Conexion.ObtenerConexion())
			{
				conn.Open();
				string query = "SELECT id_operadora, nombre_operadora FROM operadoras";
				MySqlCommand cmd = new MySqlCommand(query, conn);
				MySqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					lista.Add(new OperadoraEntidad
					{
						Id = Convert.ToInt32(reader["id_operadora"]),
						Nombre = reader["nombre_operadora"].ToString()
					});
				}
			}

			return lista;
		}


		public static string ObtenerNombrePorId(int idOperadora)
		{
			string nombre = string.Empty;

			using (var conn = Conexion.ObtenerConexion())
			{
				try
				{
					conn.Open();
					string query = "SELECT nombre_operadora FROM operadoras WHERE id_operadora = @IdOperadora LIMIT 1";
					MySqlCommand cmd = new MySqlCommand(query, conn);
					cmd.Parameters.AddWithValue("@IdOperadora", idOperadora);

					var result = cmd.ExecuteScalar();
					if (result != null)
					{
						nombre = result.ToString();
					}
				}
				catch (Exception ex)
				{
					throw new Exception("Error al obtener nombre de operadora: " + ex.Message);
				}
			}

			return nombre;
		}

		// Método alias para compatibilidad con otros nombres
		public static List<OperadoraEntidad> ObtenerTodas()
		{
			return ObtenerOperadoras();
		}
	}

}
