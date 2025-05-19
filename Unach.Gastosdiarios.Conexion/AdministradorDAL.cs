using MySql.Data.MySqlClient;
using System;

namespace Unach.Gastosdiarios.Conexion
{
    public class AdministradorDAL
    {
		public static AdminEntidad VerificarLogin(string usuario, string pass)
		{
			AdminEntidad admin = null;

			try
			{
				using (MySqlConnection conn = Conexion.ObtenerConexion())
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

									// Esta es la línea crucial
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


		public static void RegistrarAdministrador(AdminEntidad admin)
        {
            try
            {
                using (MySqlConnection conn = Conexion.ObtenerConexion())
                {
                    string query = "INSERT INTO administrador (nombre, usuario,contrasena,id_operadora) VALUES (@nombre, @usuario, @pass)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", admin.Nombre);
                    cmd.Parameters.AddWithValue("@usuario", admin.Usuario);
                    cmd.Parameters.AddWithValue("@pass", admin.Password);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en RegistrarAdministrador: " + ex.Message);
            }
        }
    }
}
