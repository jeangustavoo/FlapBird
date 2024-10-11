namespace FlapBird;

public partial class MainPage : ContentPage
{
	const int gravidade = 30;
	const int tempoEntreframes = 25;
	bool estaMorto = true;
	double larguraJanela=0;
	double AlturaJanela=0;
	int velocidade=10;

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
			GerenciarCanos();
			await Task.Delay(tempoEntreframes);
		
		}
	}
    protected override void OnSizeAllocated(double w, double h)
    {
        base.OnSizeAllocated(w, h);
		larguraJanela= w;
		AlturaJanela= h;
    }
	void GerenciarCanos()
	{
		imgCima.TranslationX -= velocidade;
		imgBaixo.TranslationX -= velocidade;
		if(imgBaixo.TranslationX <-larguraJanela)
		{
			imgBaixo.TranslationX =0;
			imgCima.TranslationX =0;
		}

	}
	void OnGameOverClicked (object s, TappedEventArgs a)
	{
		frameGameOver.IsVisible =false; 
		Inicializar();
		Desenhar();
	}
	void Inicializar()
	{
		estaMorto= false;
		pipaaa.TranslationY= 0;
	}





}


