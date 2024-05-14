using JAbarccaS6.Vistas;

namespace JAbarccaS6;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new Employe();
	}
}

