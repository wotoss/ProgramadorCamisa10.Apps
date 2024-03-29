using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using Todo.ProgramadorCamisa10.Core.Model;
using Todo.ProgramadorCamisa10.Data.DAO;
using Todo.ProgramadorCamisa10.Messages;
using Todo.ProgramadorCamisa10.Storages;
using Todo.ProgramadorCamisa10.Views;

namespace Todo.ProgramadorCamisa10.App.Views;

public partial class PaginaInicial : ContentPage
{
    //ele esta iniciando com tema escuro.
    private bool _isDarkTheme = true;

    private bool _tarefasCarregadas = false;

    //lembrando: foi inserido a (referencia de projeto) do App para camada Data
    private TarefaDAO _tarefasDAO;

    //lista de tarefas monitoradas
    private ObservableCollection<Tarefa> _tarefasObservada;
	public PaginaInicial()
	{
        //inicio a aplica��o
		InitializeComponent();
        _tarefasDAO = new TarefaDAO();
        //ser� iniciado no construtor
        //se eu quiser inicializar a lista vazia
        //var tarefasSalvas = new List<Tarefa>();



        WeakReferenceMessenger.Default.Register<NovaTarefaMessage>(this, async (x, y) =>
        {
            _tarefasObservada.Add(y.Value);
            await _tarefasDAO.Adicionar(y.Value);
        });

        WeakReferenceMessenger.Default.Register<EditarTarefaMessage>(this, async (x, y) =>
        {
            var tarefa = _tarefasObservada.Where(x => x.Id != y.Value.Id).ToList();
            _tarefasObservada = new ObservableCollection<Tarefa>(tarefa)
            {
                y.Value
            };

            //troco a referencia passando como (nullo)
            TarefasCollectionView.ItemsSource = null;

            TarefasCollectionView.ItemsSource = _tarefasObservada;

            await _tarefasDAO.Atualizar(y.Value);
        });
    }

    //este m�todo execulta ao carregar junto com inicializa��o da tela a tela 
    //ele � um override que esta sobescrevendo - tambem um delegate
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (!_tarefasCarregadas)
        {
            //vindo direto do banco
            var tarefasSalvas = await _tarefasDAO.Lista();

            _tarefasObservada = new ObservableCollection<Tarefa>(tarefasSalvas);

            TarefasCollectionView.ItemsSource = _tarefasObservada;

            _tarefasCarregadas = true;
        }
    }

    //Code bin rade => codigo por detr�s da paginas.
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

        #region exemplo de c�digo
        //quando o modal estiver desaparecendo, este evento � acionado (Disappearing) e nos mostara a lista
        //modal.Disappearing += (s, args) =>
        //{
        //    //e trago a lista que foi adicionada, como a refencia da lista mudo, ele consegue trazer a lista
        //    //1- observar��o se eu fizem assim n�o preciso dar o new e recriar a lista => TarefasCollectionView.ItemsSource = _tarefasObservada;
        //    TarefasCollectionView.ItemsSource = new ObservableCollection<Tarefa>(_tarefasDAO.Lista());
        //};
        #endregion

        await Navigation.PushModalAsync(modal);

        //quando eu clikar no bot�o Adicionar Tarefa - este m�todo adiciona a tela adicionar navegador
        //por ser pilha estrutura (LIFO) eu n�o estou navegando para outra pagina.
        //eu estou tirando a (primeira pagina da estrutura e adicionando a outra).
        //ao fechar o modal em pilha ele esta passando as tarefas atraves do _tarefaDAO
    }

    #region Exemplo de c�digo
    private void ExemplosCompilacaoCondicional()
    {
        //compila��o condicional
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
    //    //conforme redimensiona o tamanho da tela ele (pesquisaEntry) permance espa�ado em 100px;

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

    #region exemplo Deletar funcionando mas n�o usaremos 
    //private void DeletarTarefaLeftSwiped(object sender, SwipedEventArgs e)
    //{
    //    //tarefa recebe parameter (objeto-completo) porque l� no front eu estou enviando o conteudo-completo atrav�s do (.)
    //    var tarefa = (Tarefa)e.Parameter;
    //    _tarefasObservada.Remove(tarefa);
    //}
    #endregion

    private async void DeletarTarefaSwipeInvoke(object sender, EventArgs e)
    {
        //aqui vou implemetar via sender 
        var swipeItem = (SwipeItem)sender;
        //esta � mais uma forma de pegar (.) (conteudo/objeto-completo)
        var tarefa = (Tarefa)swipeItem.CommandParameter;

        _tarefasObservada.Remove(tarefa);

        await _tarefasDAO.Remover(tarefa);

    }

}



#region Exemplo-PreferencieStorage

//public partial class PaginaInicial : ContentPage
//{
//    //ele esta iniciando com tema escuro.
//    private bool _isDarkTheme = true;

//    //lembrando: foi inserido a (referencia de projeto) do App para camada Data
//    private readonly TarefaDAO _tarefasDAO;

//    //lista de tarefas monitoradas
//    private ObservableCollection<Tarefa> _tarefasObservada;
//    public PaginaInicial()
//    {
//        //inicio a aplica��o
//        InitializeComponent();
//        //aqui loga na entrada do app-que est�o aramazenadas eu tenho as listas salvas
//        var tarefasSalvar = TarefaPreferencesStorage.Lista();

//        _tarefasDAO = new TarefaDAO();

//        //se tiver alguma tarefa eu vou adicionar dentro do meu storage
//        if (tarefasSalvar.Any())
//        {
//            _tarefasDAO.Adicionar(tarefasSalvar);
//        }

//        _tarefasObservada = new ObservableCollection<Tarefa>(_tarefasDAO.Lista());

//        TarefasCollectionView.ItemsSource = _tarefasObservada;

//        WeakReferenceMessenger.Default.Register<NovaTarefaMessage>(this, (x, y) =>
//        {
//            _tarefasObservada.Add(y.Value);
//            TarefaPreferencesStorage.Salvar(y.Value);
//        });

//        WeakReferenceMessenger.Default.Register<EditarTarefaMessage>(this, (x, y) =>
//        {
//            var tarefa = _tarefasObservada.Where(x => x.Id != y.Value.Id).ToList();
//            _tarefasObservada = new ObservableCollection<Tarefa>(tarefa)
//            {
//                y.Value
//            };

//            //troco a referencia passando como (nullo)
//            TarefasCollectionView.ItemsSource = null;

//            TarefasCollectionView.ItemsSource = _tarefasObservada;

//            TarefaPreferencesStorage.Atualizar(y.Value);
//        });
//    }

//    //Code bin rade => codigo por detr�s da paginas.
//    private void PesquisaEntry_TextChanged(object sender, TextChangedEventArgs e)
//    {
//        if (string.IsNullOrWhiteSpace(e.NewTextValue))
//        {
//            TarefasCollectionView.ItemsSource = _tarefasObservada;
//        }
//        else
//        {
//            TarefasCollectionView.ItemsSource = _tarefasObservada.Where(x => x.Nome.Contains(e.NewTextValue));
//        }

//    }

//    private async void AdicionarTarefa_ButtonClicked(object sender, EventArgs e)
//    {
//        var modal = new AdicionarEditarTarefa();

//        #region exemplo de c�digo
//        //quando o modal estiver desaparecendo, este evento � acionado (Disappearing) e nos mostara a lista
//        //modal.Disappearing += (s, args) =>
//        //{
//        //    //e trago a lista que foi adicionada, como a refencia da lista mudo, ele consegue trazer a lista
//        //    //1- observar��o se eu fizem assim n�o preciso dar o new e recriar a lista => TarefasCollectionView.ItemsSource = _tarefasObservada;
//        //    TarefasCollectionView.ItemsSource = new ObservableCollection<Tarefa>(_tarefasDAO.Lista());
//        //};
//        #endregion

//        await Navigation.PushModalAsync(modal);

//        //quando eu clikar no bot�o Adicionar Tarefa - este m�todo adiciona a tela adicionar navegador
//        //por ser pilha estrutura (LIFO) eu n�o estou navegando para outra pagina.
//        //eu estou tirando a (primeira pagina da estrutura e adicionando a outra).
//        //ao fechar o modal em pilha ele esta passando as tarefas atraves do _tarefaDAO
//    }

//    #region Exemplo de c�digo
//    private void ExemplosCompilacaoCondicional()
//    {
//        //compila��o condicional
//#if ANDROID
//        DisplayAlert("Android", "Adicionar tarefa", "Ok");
//#endif

//#if WINDOWS
//                DisplayAlert("Windows", "Adicionar tarefa", "Ok");
//#endif
//    }


//    //protected override void OnSizeAllocated(double width, double height)
//    //{
//    //    //o componente de pesquisa (pesquisaEntry) fica 100px; menor que a tela toda
//    //    //conforme redimensiona o tamanho da tela ele (pesquisaEntry) permance espa�ado em 100px;

//    //    pesquisaEntry.WidthRequest = width - 100;
//    //    pesquisaEntry.HorizontalOptions = LayoutOptions.Center;
//    //    base.OnSizeAllocated(width, height);
//    //}

//    #endregion

//    private void Switch_Toggled(object sender, ToggledEventArgs e)
//    {
//        //logica referente ao thema
//        if (_isDarkTheme)
//        {
//            App.Current.UserAppTheme = AppTheme.Dark;
//            _isDarkTheme = !_isDarkTheme;
//        }
//        else
//        {
//            App.Current.UserAppTheme = AppTheme.Light;
//            _isDarkTheme = !_isDarkTheme;
//        }
//    }

//    private async void AoTocarTarefaIrParaEditar(object sender, TappedEventArgs e)
//    {
//        var tarefa = (Tarefa)e.Parameter;
//        var modal = new AdicionarEditarTarefa(tarefa);

//        await Navigation.PushModalAsync(modal);

//    }

//    #region exemplo Deletar funcionando mas n�o usaremos 
//    //private void DeletarTarefaLeftSwiped(object sender, SwipedEventArgs e)
//    //{
//    //    //tarefa recebe parameter (objeto-completo) porque l� no front eu estou enviando o conteudo-completo atrav�s do (.)
//    //    var tarefa = (Tarefa)e.Parameter;
//    //    _tarefasObservada.Remove(tarefa);
//    //}
//    #endregion

//    private void DeletarTarefaSwipeInvoke(object sender, EventArgs e)
//    {
//        //aqui vou implemetar via sender 
//        var swipeItem = (SwipeItem)sender;
//        //esta � mais uma forma de pegar (.) (conteudo/objeto-completo)
//        var tarefa = (Tarefa)swipeItem.CommandParameter;

//        _tarefasObservada.Remove(tarefa);

//        TarefaPreferencesStorage.Excluir(tarefa);
//    }
//}

#endregion