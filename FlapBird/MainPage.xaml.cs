

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
	const int aberturaMinima = 200;
	int score = 0;

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
			imgBaixo.TranslationX = imgBaixo.WidthRequest;
			imgCima.TranslationX = imgCima.WidthRequest;
			var alturaMax = -imgCima.HeightRequest + 200;
			var alturaMin = -imgCima.HeightRequest;
			imgCima.TranslationY = Random.Shared.Next((int)alturaMin, (int)alturaMax);
			imgBaixo.TranslationY = imgCima.TranslationY + aberturaMinima;
			score++;
			labelScore.Text = "Canos:" + score.ToString("D3");
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
		imgCima.TranslationX = -larguraJanela;
		imgBaixo.TranslationX = -larguraJanela;
		pipaaa.TranslationX = 0;
		pipaaa.TranslationY = 0;
		score = 0;

		GerenciarCanos();
	}
	bool VerificaColisao()
	{
		if (!estaMorto)
		{
			if (VerificaColisaoTeto() ||
			 VerificaColisaoChao() ||
			 VerificaColisaoCanoCima())
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
	bool VerificaColisaoCanoCima()
	{
		var posHpipaaa = (larguraJanela / 2) - (pipaaa.WidthRequest / 2);
		var posVpipaaa = (AlturaJanela / 2) - (pipaaa.HeightRequest / 2) + pipaaa.TranslationY;
		if (posHpipaaa >= Math.Abs(imgCima.TranslationX) - imgCima.WidthRequest &&
		posHpipaaa <= Math.Abs(imgChao.TranslationX) + imgCima.WidthRequest &&
		posVpipaaa <= imgCima.HeightRequest + imgCima.TranslationY)
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
	void OnGridClicked(object s, TappedEventArgs a)
	{
		estaPulando = true;
	}





}


