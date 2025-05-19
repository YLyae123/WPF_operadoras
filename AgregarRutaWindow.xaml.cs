using System;
using System.Windows;
using Unach.Gastosdiarios.Conexion;
using Unach.Gastosdiarios.Logica;

namespace Unach.Gastosdiarios.WPF
{
	public partial class AgregarRutaWindow : Window
	{
		public AgregarRutaWindow()
		{
			InitializeComponent();

			// Cargar las operadoras en el ComboBox
			cbOperadoras.ItemsSource = OperadoraDAL.ObtenerOperadoras();
		}

		private void BtnAgregar_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RutaEntidad ruta = new RutaEntidad
				{
					NombreRuta = txtNombreRuta.Text,
					Origen = txtOrigen.Text,
					Destino = txtDestino.Text,
					IdOperadora = (int)cbOperadoras.SelectedValue
				};

				bool resultado = RutaDAL.AgregarRuta(ruta);

				if (resultado)
				{
					MessageBox.Show("Ruta agregada exitosamente.");
					this.Close();
				}
				else
				{
					MessageBox.Show("Ocurrió un error al agregar la ruta.");
				}
			}
			catch (FormatException)
			{
				MessageBox.Show("El ID de operadora debe ser un número entero.");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error inesperado: " + ex.Message);
			}
		}
	}
}
