namespace Unach.Gastosdiarios.Conexion
{
	public class AdminEntidad
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Usuario { get; set; }
		public string Password { get; set; }
		public int? IdOperadora { get; set; } // Debe permitir nulosSELECT id_admin, nombre, usuario, contrasena, id_operadora FROM administrador WHERE usuario = 'SIRB';

	}


}