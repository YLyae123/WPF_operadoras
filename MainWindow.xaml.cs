using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Unach.Gastosdiarios.Conexion;
using Unach.Gastosdiarios.Logica;
using AdminLogica = Unach.Gastosdiarios.Logica.AdminEntidad;

namespace Unach.Gastosdiarios.WPF
{
	public partial class MainWindow : Window
	{
		private AdminLogica admin;
		private List<Ruta> rutas = new List<Ruta>();

		// Constructor con administrador
		public MainWindow(AdminLogica administrador)
		{
			InitializeComponent();
			admin = administrador;

			MessageBox.Show($"Administrador: {admin.Nombre}\nIdOperadora: {(admin.IdOperadora.HasValue ? admin.IdOperadora.Value.ToString() : "null")}");
			lblAdminNombre.Content = "Administrador: " + admin.Nombre;

			CargarRutasPorOperadora();
			MostrarBotonesSiEsOperadora();
		}

		// Constructor vacío (opcional si solo se usa con admin)
		public MainWindow()
		{
			InitializeComponent();
			CargarRutas(); // Carga general
		}

		// Cargar rutas según operadora del admin
		private void CargarRutasPorOperadora()
		{
			if (admin?.IdOperadora != null)
			{
				var rutasEntidad = RutaDAL.ObtenerTodas(admin.IdOperadora.Value);
				rutas = ConvertirLista(rutasEntidad); // Convertir de RutaEntidad a Ruta
				dgOperadoras.ItemsSource = rutas;
			}
			else
			{
				MessageBox.Show("Este administrador no está asociado a una operadora.");
			}
		}



		// Mostrar u ocultar botones según el nombre de la operadora
		private void MostrarBotonesSiEsOperadora()
		{
			string nombreOperadora = OperadoraDAL.ObtenerNombrePorId(admin.IdOperadora ?? -1);

			MessageBox.Show($"Nombre de la operadora: {nombreOperadora}");

			bool mostrarBotones = nombreOperadora == "OCC" || nombreOperadora == "ADO" || nombreOperadora == "MOCHILEROS" || nombreOperadora == "LOBO";

			Agregar.Visibility = mostrarBotones ? Visibility.Visible : Visibility.Collapsed;
			Editar.Visibility = mostrarBotones ? Visibility.Visible : Visibility.Collapsed;
			Eliminar.Visibility = mostrarBotones ? Visibility.Visible : Visibility.Collapsed;
		}

		// Botón: Alta administrador
		private void BtnAgregarAdmin_Click(object sender, RoutedEventArgs e)
		{
			AltaAdministrador altaAdminWindow = new AltaAdministrador();
			altaAdminWindow.ShowDialog();
		}

		// Botón: Login admin
		private void BtnLoginAdmin_Click(object sender, RoutedEventArgs e)
		{
			LoginAdmin loginWindow = new LoginAdmin();
			loginWindow.ShowDialog();
		}

		// Botón: Agregar nueva ruta
		private void BtnAgregarRuta_Click(object sender, RoutedEventArgs e)
		{
			AgregarRutaWindow ventana = new AgregarRutaWindow();
			ventana.ShowDialog();

			CargarRutasPorOperadora(); // Actualizar tabla
		}

		// Botón: Editar ruta seleccionada
		// Botón: Editar ruta seleccionada
		// Botón: Editar ruta seleccionada
		private void BtnEditarRuta_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (dgOperadoras.SelectedItem is Ruta rutaSeleccionada)
				{
					// Crear una nueva instancia de EditarRutaWindow y pasar la ruta seleccionada
					EditarRutaWindow editarRutaWindow = new EditarRutaWindow(rutaSeleccionada);
					bool? resultado = editarRutaWindow.ShowDialog();

					if (resultado == true)
					{
						MessageBox.Show("Ruta actualizada correctamente.");
						CargarRutasPorOperadora(); // Recargar rutas después de la actualización
					}
				}
				else
				{
					MessageBox.Show("Selecciona una ruta para editar.");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error al actualizar: " + ex.Message);
			}
		}


		// Botón: Eliminar ruta seleccionada
		private void BtnEliminarRuta_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (dgOperadoras.SelectedItem == null)
				{
					MessageBox.Show("Selecciona una ruta para eliminar.");
					return;
				}

				if (dgOperadoras.SelectedItem is Ruta rutaSeleccionada)
				{
					var resultado = MessageBox.Show("¿Está seguro de eliminar la ruta seleccionada?",
													"Confirmar eliminación",
													MessageBoxButton.YesNo,
													MessageBoxImage.Warning);

					if (resultado == MessageBoxResult.Yes)
					{
						bool eliminado = RutaDAL.EliminarRuta(rutaSeleccionada.IdRuta);
						MessageBox.Show(eliminado ? "Ruta eliminada exitosamente." : "Ocurrió un error al eliminar la ruta.");

						// Recargar
						CargarRutasPorOperadora();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error inesperado: " + ex.Message);
			}
		}


		// Carga todas las rutas (sin filtro)
		private void CargarRutas()
		{
			int idOperadoraActual = 1; // cambia este valor por el ID del administrador logueado
			var rutasEntidad = RutaDAL.ObtenerTodas(idOperadoraActual);
			dgOperadoras.ItemsSource = rutasEntidad;
		}






		// Cuando se selecciona una fila del DataGrid
		private void dgOperadoras_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (dgOperadoras.SelectedItem is Ruta rutaSeleccionada)
			{
				txtIdRuta.Text = rutaSeleccionada.IdRuta.ToString();
				txtNombreRuta.Text = rutaSeleccionada.NombreRuta;
				txtIdOperadora.Text = rutaSeleccionada.IdOperadora.ToString();
			}
		}

		private List<Ruta> ConvertirLista(List<RutaEntidad> entidades)
		{
			var lista = new List<Ruta>();

			foreach (var entidad in entidades)
			{
				lista.Add(new Ruta
				{
					IdRuta = entidad.IdRuta,
					NombreRuta = entidad.NombreRuta,
					Origen = entidad.Origen,
					Destino = entidad.Destino,
					IdOperadora = entidad.IdOperadora
				});
			}

			return lista;
		}



	}
}
