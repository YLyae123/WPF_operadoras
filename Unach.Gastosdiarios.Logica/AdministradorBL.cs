using Unach.Gastosdiarios.Logica;

public class AdministradorBL
{
	public static AdminEntidad Login(string usuario, string pass)
	{
		AdminEntidad entidad = AdminDAL.VerificarLogin(usuario, pass);
		if (entidad == null)
			return null;

		return new AdminEntidad
		{
			Id = entidad.Id,
			Nombre = entidad.Nombre,
			Usuario = entidad.Usuario,
			Password = entidad.Password
		};
	}

	public static void AgregarAdministrador(AdminEntidad admin)
	{
		AdminDAL.RegistrarAdministrador(admin);
	}
}
