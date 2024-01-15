using CommunityToolkit.Mvvm.Messaging;
using Javax.Security.Auth;
using System.Collections.ObjectModel;
using Todo.ProgramadorCamisa10.Core.Model;
using Todo.ProgramadorCamisa10.Data.DAO;
using Todo.ProgramadorCamisa10.Messages;
using Todo.ProgramadorCamisa10.Views;

namespace Todo.ProgramadorCamisa10.App.Views;

public partial class PaginaInicial : ContentPage
{
    //ele esta iniciando com tema escuro.
    private bool _isDarkTheme = true;

    //lembrando: foi inserido a (referencia de projeto) do App para camada Data
    private readonly TarefaDAO _tarefasDAO;

    //lista de tarefas monitoradas
    private ObservableCollection<Tarefa> _tarefasObservada;
	public PaginaInicial()
	{
		InitializeComponent();

        _tarefasDAO = new TarefaDAO();

        _tarefasObservada = new ObservableCollection<Tarefa>(_tarefasDAO.Lista());

        TarefasCollectionView.ItemsSource = _tarefasObservada;

        WeakReferenceMessenger.Default.Register<NovaTarefaMessage>(this, (x, y) =>
        {
            _tarefasObservada.Add(y.Value);
        });

        WeakReferenceMessenger.Default.Register<EditarTarefaMessage>(this, (x, y) =>
        {
            var tarefa = _tarefasObservada.Where(x => x.Id != y.Value.Id).ToList();
            _tarefasObservada = new ObservableCollection<Tarefa>(tarefa)
            {
                y.Value
            };

            //troco a referencia passando como (nullo)
            TarefasCollectionView.ItemsSource = null;

            TarefasCollectionView.ItemsSource = _tarefasObservada;
        });
    }

	//Code bin rade => codigo por detrás da paginas.
    private void PesquisaEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(e.NewTextValue))
        {
            TarefasCollectionView.ItemsSource = _tarefasObservada;
        }
        else
        {
            TarefasCollectionView.ItemsSource = _tarefasObservada.Where(x => x.Nome.Contains(e.NewTextValue));
        }

    }

    private async void AdicionarTarefa_ButtonClicked(object sender, EventArgs e)
    {
        var modal = new AdicionarEditarTarefa();

        #region exemplo de código
        //quando o modal estiver desaparecendo, este evento é acionado (Disappearing) e nos mostara a lista
        //modal.Disappearing += (s, args) =>
        //{
        //    //e trago a lista que foi adicionada, como a refencia da lista mudo, ele consegue trazer a lista
        //    //1- observarção se eu fizem assim não preciso dar o new e recriar a lista => TarefasCollectionView.ItemsSource = _tarefasObservada;
        //    TarefasCollectionView.ItemsSource = new ObservableCollection<Tarefa>(_tarefasDAO.Lista());
        //};
        #endregion

        await Navigation.PushModalAsync(modal);

        //quando eu clikar no botão Adicionar Tarefa - este método adiciona a tela adicionar navegador
        //por ser pilha estrutura (LIFO) eu não estou navegando para outra pagina.
        //eu estou tirando a (primeira pagina da estrutura e adicionando a outra).
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
            App.Current.UserAppTheme = AppTheme.Dark;
            _isDarkTheme = !_isDarkTheme;
        }
        else
        {
            App.Current.UserAppTheme = AppTheme.Light;
            _isDarkTheme = !_isDarkTheme;
        }
    }

    private async void AoTocarTarefaIrParaEditar(object sender, TappedEventArgs e)
    {
        var tarefa = (Tarefa)e.Parameter;
        var modal = new AdicionarEditarTarefa(tarefa);

        await Navigation.PushModalAsync(modal);
        
    }
}