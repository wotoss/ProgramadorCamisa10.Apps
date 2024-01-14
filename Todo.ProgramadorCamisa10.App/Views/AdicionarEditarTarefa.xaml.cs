using Todo.ProgramadorCamisa10.Core.Model;
using Todo.ProgramadorCamisa10.Data.DAO;

namespace Todo.ProgramadorCamisa10.Views;

public partial class AdicionarEditarTarefa : ContentPage
{
    private readonly TarefaDAO _trefaDAO;
	public AdicionarEditarTarefa()
	{
		InitializeComponent();
	}

    public AdicionarEditarTarefa(TarefaDAO tarefaDAO)
    {
        this.InitializeComponent();

        _trefaDAO = tarefaDAO;
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
        //vamos pegar o botão e fazer ele adicionar a tarefa aqui dentro
        _trefaDAO.Adicionar(tarefa);

        await Navigation.PopModalAsync();
    }
}