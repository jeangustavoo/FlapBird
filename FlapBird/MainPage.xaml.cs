
namespace FlapBird;

public partial class MainPage : ContentPage
{
	const int gravidade = 30;
	const int tempoEntreframes = 25;
	bool estaMorto = true;
	double larguraJanela = 0;
	double AlturaJanela = 0;
	int velocidade = 10;

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
			If(VerificaColisao());
			{
				estaMorto = true;
				frameGameOver.IsVisible = true;
				break;
			}
			await Task.Delay(tempoEntreframes);

		}
	}

	private void If(bool v)
	{
		throw new NotImplementedException();
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
			if (VerificaColisaoTeto() ||
			VerificaColisao())
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





}


