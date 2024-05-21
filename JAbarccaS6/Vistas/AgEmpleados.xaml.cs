using System.Net;

namespace JAbarccaS6.Vistas;

public partial class AgEmpleados : ContentPage
{
    private string _id = "";
	public AgEmpleados()
	{
		InitializeComponent();
	}
    public AgEmpleados(string id )
    {
        InitializeComponent();
        _id = id;
    }

    void btnGuardar_Clicked(System.Object sender, System.EventArgs e)
    {
        try
        {
            string nombre = txtNombre.Text;
            string apellido = txtApellido.Text;
            string edad = txtEdad.Text;
            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();
            parametros.Add("name", nombre);
            parametros.Add("apellido", apellido);
            parametros.Add("edad", edad);

            if (_id != "")
            {
                parametros.Add("id", _id);
                cliente.UploadValues("http://192.168.1.4:8000/api/empleados/actualizar", "PUT", parametros);

            }
            else {
                cliente.UploadValues("http://192.168.1.4:8000/api/empleados/crear", "POST", parametros);

            }
            Navigation.PushAsync(new Employe());
        }
        catch (Exception ex) {
            DisplayAlert("Error",ex.Message,"Ok");
        }
        
    }
}
