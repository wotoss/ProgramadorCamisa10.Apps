using CommunityToolkit.Mvvm.Messaging;
using Todo.ProgramadorCamisa10.Core.Model;
using Todo.ProgramadorCamisa10.Data.DAO;
using Todo.ProgramadorCamisa10.Messages;

namespace Todo.ProgramadorCamisa10.Views;

public partial class AdicionarEditarTarefa : ContentPage
{
    private Tarefa _tarefa;
	public AdicionarEditarTarefa()
	{
		InitializeComponent();
	}
    //construtor para preencher as (Label,s)
    public AdicionarEditarTarefa(Tarefa tarefa)
    {
        //ele vai receber a (tarefa) e vai preencher as label
        InitializeComponent();
        nomeTarefaEntry.Text = tarefa.Nome;
        descricaoTarefaEditor.Text = tarefa.Descricao;
        dataTarefaDatePicker.Date = tarefa.DataConclusao;

        _tarefa = tarefa;
    }

    private async void BtnFechar_Clicked(object sender, EventArgs e)
    {
        //vai remover o modal da stack, por que ele trabalha com a estrutura de pilha.
        await Navigation.PopModalAsync();
    }

    private async void BtnSalvar_Clicked(object sender, EventArgs e)
    {
        var tarefa = new Tarefa
        {
            Nome = nomeTarefaEntry.Text,
            Descricao = descricaoTarefaEditor.Text,
            DataConclusao = dataTarefaDatePicker.Date,
            Concluida = false
        };

        //este if a tarefa esta sendo editada.
        if (_tarefa is not null)
        {
            _tarefa.Nome = tarefa.Nome;
            _tarefa.Descricao = tarefa.Descricao;
            _tarefa.DataConclusao = tarefa.DataConclusao;

         WeakReferenceMessenger.Default.Send(new EditarTarefaMessage(tarefa));
        }
        //neste else a tarefa esta sendo criada.
        else
        {
            _tarefa = tarefa;
        }      
        //vou ter que disparar uma mensagem
        WeakReferenceMessenger.Default.Send(new NovaTarefaMessage(tarefa));

        await Navigation.PopModalAsync();
    }
}