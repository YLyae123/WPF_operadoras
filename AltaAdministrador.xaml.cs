using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Unach.Gastosdiarios.Conexion;
using Unach.Gastosdiarios.Logica;
using AdminConexion = Unach.Gastosdiarios.Conexion.AdminEntidad;
using AdminLogica = Unach.Gastosdiarios.Logica.AdminEntidad;

namespace Unach.Gastosdiarios.WPF
{
	/// <summary>
	/// Lógica de interacción para AltaAdministrador.xaml
	/// </summary>
	public partial class AltaAdministrador : Window
	{


		public AltaAdministrador()
		{
			InitializeComponent();
			CargarOperadoras();
		}
		private void CargarOperadoras()
		{
			try
			{
				List<OperadoraEntidad> operadoras = OperadoraDAL.ObtenerOperadoras();
				cmbOperadora.ItemsSource = operadoras;
				cmbOperadora.DisplayMemberPath = "Nombre"; // Nombre de la propiedad en OperadoraEntidad
				cmbOperadora.SelectedValuePath = "Id"; // Nombre de la propiedad en OperadoraEntidad
				cmbOperadora.Items.Refresh(); // Forzar la actualización del ComboBox
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error al cargar operadoras: " + ex.Message);
			}
		}


		private void BtnGuardar_Click(object sender, RoutedEventArgs e)
		{
			// Captura los datos de los campos
			string nombre = txtNombre.Text;
			string usuario = txtUsuario.Text;
			string contrasena = txtPassword.Password; // Para PasswordBox
			int operadoraId = (cmbOperadora.SelectedItem as OperadoraEntidad)?.Id ?? 0;

			// Validar que todos los campos estén llenos
			if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena) || operadoraId == 0)
			{
				MessageBox.Show("Por favor, completa todos los campos.");
				return;
			}

			// Lógica para guardar el administrador en la base de datos
			try
			{
				using (var conn = Unach.Gastosdiarios.Conexion.Conexion.ObtenerConexion())
				{
					conn.Open();
					string query = "INSERT INTO administrador (nombre, usuario, contrasena, id_operadora) VALUES (@nombre, @usuario, @contrasena, @id_operadora)";
					MySqlCommand cmd = new MySqlCommand(query, conn);
					cmd.Parameters.AddWithValue("@nombre", nombre);
					cmd.Parameters.AddWithValue("@usuario", usuario);
					cmd.Parameters.AddWithValue("@contrasena", contrasena);
					cmd.Parameters.AddWithValue("@id_operadora", operadoraId);

					cmd.ExecuteNonQuery();
					MessageBox.Show("Administrador registrado exitosamente.");
				}
			}
			catch (MySqlException ex)
			{
				MessageBox.Show("Error al registrar el administrador: " + ex.Message);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error inesperado: " + ex.Message);
			}
		}


	}
}
