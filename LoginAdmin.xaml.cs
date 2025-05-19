using System;
using System.Windows;
using Unach.Gastosdiarios.Logica;
using Unach.Gastosdiarios.Conexion;

namespace Unach.Gastosdiarios.WPF
{
	public partial class LoginAdmin : Window
	{
		public LoginAdmin()
		{
			InitializeComponent();
		}

		private void BtnIniciarSesion_Click(object sender, RoutedEventArgs e)
		{
			var administrador = AdminLogica.VerificarLogin(txtUsuario.Text, txtContrasena.Password);
			if (administrador != null)
			{
				MainWindow mainWindow = new MainWindow(administrador);
				mainWindow.Show();
				this.Close(); // Cierra la ventana de inicio de sesión
			}
			else
			{
				MessageBox.Show("Usuario o contraseña incorrectos.");
			}
		}


	}
}
