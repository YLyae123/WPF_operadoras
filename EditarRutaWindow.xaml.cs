using System;
using System.Windows;
using Unach.Gastosdiarios.Conexion;
using Unach.Gastosdiarios.Logica;

namespace Unach.Gastosdiarios.WPF
{
	public partial class EditarRutaWindow : Window
	{
		private Ruta ruta;

		public EditarRutaWindow(Ruta rutaSeleccionada)
		{
			InitializeComponent();
			ruta = rutaSeleccionada;

			txtIdRuta.Text = ruta.IdRuta.ToString();
			txtNombreRuta.Text = ruta.NombreRuta;

			// Asegúrate de que estas propiedades existan y tengan datos:
			txtOrigen.Text = ruta.Origen ?? string.Empty;
			txtDestino.Text = ruta.Destino ?? string.Empty;

			txtIdOperadora.Text = ruta.IdOperadora.ToString();
		}


		private void BtnActualizar_Click(object sender, RoutedEventArgs e)
		{
			// Validar datos (simplificado)
			if (string.IsNullOrWhiteSpace(txtNombreRuta.Text) ||
				string.IsNullOrWhiteSpace(txtOrigen.Text) ||
				string.IsNullOrWhiteSpace(txtDestino.Text) ||
				string.IsNullOrWhiteSpace(txtIdOperadora.Text))
			{
				MessageBox.Show("Por favor, completa todos los campos.");
				return;
			}

			try
			{
				ruta.NombreRuta = txtNombreRuta.Text;
				ruta.Origen = txtOrigen.Text;
				ruta.Destino = txtDestino.Text;
				ruta.IdOperadora = int.Parse(txtIdOperadora.Text);

				// Crear objeto RutaEntidad para DAL
				var rutaEntidad = new RutaEntidad
				{
					IdRuta = ruta.IdRuta,
					NombreRuta = ruta.NombreRuta,
					Origen = ruta.Origen,
					Destino = ruta.Destino,
					IdOperadora = ruta.IdOperadora
				};

				bool actualizado = RutaDAL.EditarRuta(rutaEntidad);

				if (actualizado)
				{
					MessageBox.Show("Ruta actualizada correctamente.");
					this.DialogResult = true; // Indica éxito y cierra ventana
				}
				else
				{
					MessageBox.Show("Error al actualizar la ruta.");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.Message);
			}
		}
	}
}
