

namespace FlapBird;

public partial class MainPage : ContentPage
{
	const int gravidade = 10;
	const int tempoEntreframes = 50;
	bool estaMorto = true;
	double larguraJanela = 0;
	double AlturaJanela = 0;
	int velocidade = 10;
	const int maxTempoPulando = 3;
	int TempoPulando = 0;
	bool estaPulando = false;
	const int forcaPulo = 30;

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
			if (estaPulando)
				AplicaPulo();
			else
				AplicaGravidade();
			GerenciarCanos();
			if (VerificaColisao())
			{
				estaMorto = true;
				frameGameOver.IsVisible = true;
				break;
			}
			await Task.Delay(tempoEntreframes);

		}
	}

	
	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		larguraJanela = w;
		AlturaJanela = h;
	}
	void GerenciarCanos()
	{
		imgCima.TranslationX -= velocidade;
		imgBaixo.TranslationX -= velocidade;
		if (imgBaixo.TranslationX < -larguraJanela)
		{
			imgBaixo.TranslationX = 0;
			imgCima.TranslationX = 0;
		}

	}
	void OnGameOverClicked(object s, TappedEventArgs a)
	{
		frameGameOver.IsVisible = false;
		Inicializar();
		Desenhar();
	}
	void Inicializar()
	{
		estaMorto = false;
		pipaaa.TranslationY = 0;
	}
	bool VerificaColisao()
	{
		if (!estaMorto)
		{
			if (VerificaColisaoTeto() || VerificaColisaoChao())
			{
				return true;
			}
		}
		return false;
	}
	bool VerificaColisaoTeto()
	{
		var minY = -AlturaJanela / 2;
		if (pipaaa.TranslationY <= minY)
			return true;
		else
			return false;
	}
	bool VerificaColisaoChao()
	{
		var maxY = AlturaJanela / 2 - imgChao.HeightRequest;
		if (pipaaa.TranslationY >= maxY)
			return true;
		else
			return false;
	}

	 void AplicaPulo()
	{
		pipaaa.TranslationY -= forcaPulo;
		TempoPulando++;
		if (TempoPulando >= maxTempoPulando)
		{
			estaPulando = false;
			TempoPulando = 0;
		}
	}
	void OnGridClicked (object s, TappedEventArgs a)
	{
		estaPulando = true;
	}





}


