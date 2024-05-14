using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JAbarccaS6.Models;
using Newtonsoft.Json;

namespace JAbarccaS6.Vistas
{
    public partial class Empleados : ContentPage
    {
        private const string Url = "http://25.46.184.61:8000/api/empleados/listar";
        private readonly HttpClient cliente = new HttpClient();
        private ObservableCollection<Empleado> empl;

        public Empleados()
        {
            InitializeComponent();
            empl = new ObservableCollection<Empleado>();
            Obtener();
        }

        /*public async void Obtener()
        {
            var content = await cliente.GetStringAsync(Url);
            var tempObject = JsonConvert.DeserializeObject<dynamic>(content);
            List<Empleado> empleados = tempObject.data.ToObject<List<Empleado>>();

            foreach (var empleado in empleados)
            {
                AddEmployeeToGrid(empleado);
            }
        }*/
        public  void Obtener()
        {
           /* var content = await cliente.GetStringAsync(Url);
            var tempObject = JsonConvert.DeserializeObject<dynamic>(content);
            List<Empleado> empleados = tempObject.data.ToObject<List<Empleado>>();
            var employes  = grid
            foreach (var empleado in empleados)
            {
                AddEmployeeToGrid(empleado);
            }*/
        }

       /* private void AddEmployeeToGrid(Empleado empleado)
        {
            // Create new row for the employee
            int newRowIndex = gridEmpleados.RowDefinitions.Count;
            gridEmpleados.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            // Create labels for name and apellido
            var lblNombre = new Label { Text = empleado.nombre, Grid.Column = 0 };
            var lblApellido = new Label { Text = empleado.apellido, Grid.Column = 1 };

            // Add labels to the grid in the new row
            gridEmpleados.Children.Add(lblNombre, 0, newRowIndex);
            gridEmpleados.Children.Add(lblApellido, 1, newRowIndex);
        }*/
    }
}
