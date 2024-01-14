using Todo.ProgramadorCamisa10.Core.Model;
using Todo.ProgramadorCamisa10.Data.DAO;
using Todo.ProgramadorCamisa10.Views;

namespace Todo.ProgramadorCamisa10.App.Views;

public partial class PaginaInicial : ContentPage
{
    //ele esta iniciando com tema escuro.
    private bool _isDarkTheme = true;
    //lembrando: foi inserido a (referencia de projeto) do App para camada Data
    private readonly TarefaDAO _trefaDAO;
	public PaginaInicial()
	{
		InitializeComponent();

        TarefasCollectionView.ItemsSource = _trefaDAO.Lista();
    }

	//Code bin rade => codigo por detrás da paginas.
    private void PesquisaEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
		

    }

    private async void AdicionarTarefa_ButtonClicked(object sender, EventArgs e)
    {
        //quando eu clikar no botão Adicionar Tarefa - este método adiciona a tela adicionar navegador
        //por ser pilha estrutura (LIFO) eu não estou navegando para outra pagina.
        //eu estou tirando a (primeira pagina da estrutura e adicionando a outra).
       await Navigation.PushModalAsync(new AdicionarEditarTarefa(_trefaDAO));
        //ao fechar o modal em pilha ele esta passando as tarefas atraves do _tarefaDAO
    }

    #region Exemplo de código
    private void ExemplosCompilacaoCondicional()
    {
        //compilação condicional
        #if ANDROID
                DisplayAlert("Android", "Adicionar tarefa", "Ok");
        #endif

        #if WINDOWS
                DisplayAlert("Windows", "Adicionar tarefa", "Ok");
        #endif
    }


    //protected override void OnSizeAllocated(double width, double height)
    //{
    //    //o componente de pesquisa (pesquisaEntry) fica 100px; menor que a tela toda
    //    //conforme redimensiona o tamanho da tela ele (pesquisaEntry) permance espaçado em 100px;

    //    pesquisaEntry.WidthRequest = width - 100;
    //    pesquisaEntry.HorizontalOptions = LayoutOptions.Center;
    //    base.OnSizeAllocated(width, height);
    //}

    #endregion

    private void Switch_Toggled(object sender, ToggledEventArgs e)
    {
        //logica referente ao thema
        if (_isDarkTheme)
        {
            App.Current.UserAppTheme = AppTheme.Light;
            _isDarkTheme = !_isDarkTheme;
        }
        else
        {
            App.Current.UserAppTheme = AppTheme.Dark;
            _isDarkTheme = !_isDarkTheme;
        }
    }
}