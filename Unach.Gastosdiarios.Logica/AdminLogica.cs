using MySql.Data.MySqlClient;
using System;
using Unach.Gastosdiarios.Conexion; // Ajusta el namespace si es necesario

namespace Unach.Gastosdiarios.Logica
{
	public static class AdminLogica
	{
		// Método para verificar el inicio de sesión
		public static AdminEntidad VerificarLogin(string usuario, string pass)
		{
			AdminEntidad admin = null;

			try
			{
				using (var conn = Unach.Gastosdiarios.Conexion.Conexion.ObtenerConexion())
				{
					conn.Open();

					string queryAdmin = "SELECT * FROM administrador WHERE usuario = @usuario AND contrasena = @pass";
					using (MySqlCommand cmdAdmin = new MySqlCommand(queryAdmin, conn))
					{
						cmdAdmin.Parameters.AddWithValue("@usuario", usuario);
						cmdAdmin.Parameters.AddWithValue("@pass", pass);

						using (MySqlDataReader readerAdmin = cmdAdmin.ExecuteReader())
						{
							if (readerAdmin.Read())
							{
								admin = new AdminEntidad()
								{
									Id = Convert.ToInt32(readerAdmin["id_admin"]),
									Nombre = readerAdmin["nombre"].ToString(),
									Usuario = readerAdmin["usuario"].ToString(),
									Password = readerAdmin["contrasena"].ToString(),
									IdOperadora = readerAdmin["id_operadora"] == DBNull.Value
										? (int?)null
										: Convert.ToInt32(readerAdmin["id_operadora"])
								};

								return admin;
							}
						}
					}
				}
			}
			catch (MySqlException ex)
			{
				throw new Exception("Error en la consulta de administrador: " + ex.Message);
			}
			catch (Exception ex)
			{
				throw new Exception("Error en VerificarLogin: " + ex.Message);
			}

			return admin;
		}
	}
}
