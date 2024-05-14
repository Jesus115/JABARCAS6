using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JAbarccaS6.Models;
namespace JAbarccaS6.Vistas;

public partial class Employe : ContentPage
{
    private const string Url = "http://25.46.184.61:8000/api/empleados/listar";
    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<Empleado> empl;

    public Employe()
	{
		InitializeComponent();
        empl = new ObservableCollection<Empleado>();
        Obtener();

    }
    public async void Obtener()
    {
        var content = await cliente.GetStringAsync(Url);
        var tempObject = JsonConvert.DeserializeObject<dynamic>(content);
        List<Empleado> empleados = tempObject.data.ToObject<List<Empleado>>();

        foreach (var empleado in empleados)
        {
            AddEmployeeToGrid(empleado);
        }
    }
    private void AddEmployeeToGrid(Empleado empleado)
    {
        // Create new row for the employee
        int newRowIndex = grdEmployes.RowDefinitions.Count;
        grdEmployes.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto});
        var lblCodigo = new Label { Text = empleado.id + " ", AnchorX = 10 };
        Grid.SetColumn(lblCodigo, 0); // Set column index to 0

        var lblNombre = new Label { Text = empleado.nombre+" ", AnchorX = 10 };
        Grid.SetColumn(lblNombre, 1); // Set column index to 0

        var lblApellido = new Label { Text = empleado.apellido + " ", AnchorX = 10 };
        Grid.SetColumn(lblApellido, 2); // Set column index to 1

        var lblEdad = new Label { Text = empleado.edad.ToString() + " ", AnchorX = 10 };
        Grid.SetColumn(lblEdad, 3); // Set column index to 1
        // Add labels to the grid in the new row
        grdEmployes.Add(lblCodigo, 0, newRowIndex);
        grdEmployes.Add(lblNombre, 1, newRowIndex);
        grdEmployes.Add(lblApellido, 2, newRowIndex);
        grdEmployes.Add(lblEdad, 3, newRowIndex);
    }
}
