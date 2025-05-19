using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Unach.Gastosdiarios.Conexion; // Asegúrate de que este namespace sea correcto

namespace Unach.Gastosdiarios.Logica
{
	public static class RutaDAL
	{
		// Método para obtener rutas por ID de operadora
		public static List<RutaEntidad> ObtenerTodas(int idOperadora)
		{
			var lista = new List<RutaEntidad>();

			using (var conexion = Unach.Gastosdiarios.Conexion.Conexion.ObtenerConexion())
			{
				conexion.Open();
				string query = "SELECT id_ruta, nombre_ruta, origen, destino, id_operadora FROM rutas WHERE id_operadora = @idOperadora";
				using (MySqlCommand cmd = new MySqlCommand(query, conexion))
				{
					cmd.Parameters.AddWithValue("@idOperadora", idOperadora);
					using (MySqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							lista.Add(new RutaEntidad
							{
								IdRuta = reader.GetInt32("id_ruta"),
								NombreRuta = reader.GetString("nombre_ruta"),
								Origen = reader.GetString("origen"),
								Destino = reader.GetString("destino"),
								IdOperadora = reader.GetInt32("id_operadora")
							});
						}
					}
				}
			}

			return lista;
		}

		// Método para agregar una nueva ruta
		public static bool AgregarRuta(RutaEntidad ruta)
		{
			try
			{
				using (var conn = Unach.Gastosdiarios.Conexion.Conexion.ObtenerConexion())
				{
					conn.Open();
					using (MySqlCommand cmd = new MySqlCommand("INSERT INTO rutas (nombre_ruta, origen, destino, id_operadora) VALUES (@nombre, @origen, @destino, @idOperadora)", conn))
					{
						cmd.Parameters.AddWithValue("@nombre", ruta.NombreRuta);
						cmd.Parameters.AddWithValue("@origen", ruta.Origen);
						cmd.Parameters.AddWithValue("@destino", ruta.Destino);
						cmd.Parameters.AddWithValue("@idOperadora", ruta.IdOperadora);

						int filasAfectadas = cmd.ExecuteNonQuery();
						return filasAfectadas > 0;
					}

				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error al agregar la ruta: " + ex.Message);
			}
		}

		// Método para eliminar una ruta por su ID
		public static bool EliminarRuta(int idRuta)
		{
			try
			{
				using (var conn = Unach.Gastosdiarios.Conexion.Conexion.ObtenerConexion())
				{
					conn.Open();
					using (MySqlCommand cmd = new MySqlCommand("DELETE FROM rutas WHERE id_ruta = @idRuta", conn))
					{
						cmd.Parameters.AddWithValue("@idRuta", idRuta);
						int filasAfectadas = cmd.ExecuteNonQuery();
						return filasAfectadas > 0;
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error al eliminar la ruta: " + ex.Message);
			}
		}

		// Método para actualizar una ruta
		public static bool EditarRuta(RutaEntidad ruta)
		{
			try
			{
				using (var conn = Unach.Gastosdiarios.Conexion.Conexion.ObtenerConexion())
				{
					conn.Open();
					using (MySqlCommand cmd = new MySqlCommand(
						"UPDATE rutas SET nombre_ruta = @nombreRuta, origen = @origen, destino = @destino, id_operadora = @idOperadora WHERE id_ruta = @idRuta", conn))
					{
						cmd.Parameters.AddWithValue("@nombreRuta", ruta.NombreRuta);
						cmd.Parameters.AddWithValue("@origen", ruta.Origen);
						cmd.Parameters.AddWithValue("@destino", ruta.Destino);
						cmd.Parameters.AddWithValue("@idOperadora", ruta.IdOperadora);
						cmd.Parameters.AddWithValue("@idRuta", ruta.IdRuta);

						int filasAfectadas = cmd.ExecuteNonQuery();
						return filasAfectadas > 0;
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error al editar la ruta: " + ex.Message);
			}
		}



	}
}
