namespace FlapBird;

public partial class MainPage : ContentPage
{
	const int gravidade = 30;
	const int tempoEntreframes = 25;
	bool estaMorto = false;

	public MainPage()
	{
		InitializeComponent();
	}
	void AplicaGravidade()
	{
		pipaaa.TranslationY += gravidade;
	}
	async Task Desenhar()
	{
		while (!estaMorto)
		{
			AplicaGravidade();
			await Task.Delay(tempoEntreframes);
		}
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		Desenhar();
	}




}


