using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JAbarccaS6.Models;
using System.Net;

namespace JAbarccaS6.Vistas;

public partial class Employe : ContentPage
{
    private const string Url = "http://192.168.1.4:8000/api/empleados/listar";
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
        int newRowIndex = grdEmployes.RowDefinitions.Count;
        grdEmployes.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto});
        var lblCodigo = new Label { Text = empleado.id + " ", AnchorX = 10 };
        Grid.SetColumn(lblCodigo, 0); 
        var lblNombre = new Label { Text = empleado.nombre+" ", AnchorX = 10 };
        Grid.SetColumn(lblNombre, 1); 
        var lblApellido = new Label { Text = empleado.apellido + " ", AnchorX = 10 };
        Grid.SetColumn(lblApellido, 2); 
        var lblEdad = new Label { Text = empleado.edad.ToString() + " ", AnchorX = 10 };
        Grid.SetColumn(lblEdad, 3);
        // Crear el botón y configurar el evento Clicked
        var btnEditar = new Button { Text = "\u270E", CommandParameter = empleado.id , CornerRadius = 10, FontSize = 24, BackgroundColor = Color.FromHex("#EDFF21"), TextColor = Color.FromHex("#000000") };
        btnEditar.Clicked += (sender, e) =>
        {
            Button button = (Button)sender;
            string parametro = button.CommandParameter.ToString();
            EditarEmpleado(parametro);
        };

        Grid.SetColumn(btnEditar, 4);
        // Crear el botón y configurar el evento Clicked
        var btnEliminar = new Button { Text = "\u2620", CornerRadius = 10, FontSize = 24, CommandParameter = empleado.id, BackgroundColor=Color.FromHex("#FF0000") , TextColor= Color.FromHex("#000000")};
        btnEliminar.Clicked += (sender, e) =>
        {
            Button button = (Button)sender;
            string parametro = button.CommandParameter.ToString();
            EliminarEmpleado(parametro);
        };

        Grid.SetColumn(btnEliminar, 4);

        grdEmployes.Add(lblCodigo, 0, newRowIndex);
        grdEmployes.Add(lblNombre, 1, newRowIndex);
        grdEmployes.Add(lblApellido, 2, newRowIndex);
        grdEmployes.Add(lblEdad, 3, newRowIndex);
        grdEmployes.Add(btnEditar, 4, newRowIndex);
        grdEmployes.Add(btnEliminar, 5, newRowIndex);

    }
    void EditarEmpleado(string parametro)
    {
        //DisplayAlert("Parámetro enviado", "Se envió el parámetro: " + parametro, "OK");
        Navigation.PushAsync(new AgEmpleados(parametro));
    }
    void EliminarEmpleado(string parametro)
    {
        try
        {
            string id = parametro;
           
            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();
            parametros.Add("id", id);
            cliente.UploadValues("http://192.168.1.4:8000/api/empleados/eliminar", "DELETE", parametros);
            grdEmployes.Children.Clear();
            Obtener();
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "Ok");
        }
    }

    void btnNuevoEmpleado_Clicked(System.Object sender, System.EventArgs e)
    {
        Navigation.PushAsync(new AgEmpleados());
    }
}
