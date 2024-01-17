using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Todo.ProgramadorCamisa10.Core.Model;

namespace Todo.ProgramadorCamisa10.Storages;

public static class TarefaSecureStorage
{
    private const string TAREFAS = "tarefas";

    public static async Task Salvar(Tarefa tarefa)
    {
        List<Tarefa> tarefas;

        //estou buscando as minhas tarefas
        var tarefasJson = await SecureStorage.Default.GetAsync(TAREFAS);

        //se o (tarefasJson) não for vazio eu entro no if e dou um (JsonSerializer.Deserialize)
        //no caso se tiver tarefa.
        //ele valida três condições - texto vazios, nulos ou texto em brancos.
        if (!string.IsNullOrWhiteSpace(tarefasJson))
        {
            tarefas = JsonSerializer.Deserialize<List<Tarefa>>(tarefasJson);
        }
        else
        {
            tarefas = new List<Tarefa>();
        }
        tarefas.Add(tarefa);
        await SecureStorage.Default.SetAsync(TAREFAS, JsonSerializer.Serialize(tarefas));
    }

    //vamos fazer o update 
    public static async Task Atualizar(Tarefa tarefa)
    {
        //busca as tarefas através da GetAsync(TAREFAS); e a chave (TAREFAS)
        var tarefasJson = await SecureStorage.Default.GetAsync(TAREFAS);
        //ele valida três condições - texto vazios, nulos ou texto em brancos.
        if (string.IsNullOrWhiteSpace(tarefasJson)) { return; }

        //tendo tarefa faço o deserializer da minha lista
        var tarefas = JsonSerializer.Deserialize<List<Tarefa>>(tarefasJson);

        //neste momento eu faço o filtro
        var tarefaAtualizada = tarefas.FirstOrDefault(x => x.Id == tarefa.Id);

        //aqui começo a preencher o combo com a atualização.
        tarefaAtualizada.Nome = tarefa.Nome;
        tarefaAtualizada.Descricao = tarefa.Descricao;
        tarefaAtualizada.DataConclusao = tarefa.DataConclusao;

        //devolvo a tarela
        await SecureStorage.Default.SetAsync(TAREFAS, JsonSerializer.Serialize(tarefas));
    }

     public static async Task Excluir(Tarefa tarefa)
      {
          var tarefaJson = await SecureStorage.Default.GetAsync(TAREFAS);
        //ele valida três condições - texto vazios, nulos ou texto em brancos.
          if (string.IsNullOrWhiteSpace(tarefaJson)) { return; }

          var tarefas = JsonSerializer.Deserialize<List<Tarefa>>(tarefaJson);

          var tarefasExistente = tarefas.FirstOrDefault(x => x.Id == tarefa.Id);

        //ele dá o remove e depois seta a tarefa novamente no storage.
          tarefas.Remove(tarefasExistente);

        //ele seta a tarefa no secureStorage
        await SecureStorage.Default.SetAsync(TAREFAS, JsonSerializer.Serialize(tarefas));
      } 

    //vamos construir o listar tarefas
    public static async Task<List<Tarefa>> Listar()
    {
        //faz um get com a chave TAREFAS e trás a lista
        var tarefasJson = await SecureStorage.Default.GetAsync(TAREFAS);

        //aqui é diferente dos outros método - se tiver vazia ele retorna uma nova lista
        //lista de tarefas
        //ele valida três condições - texto vazios, nulos ou texto em brancos.
        if (string.IsNullOrWhiteSpace(tarefasJson))
            return new List<Tarefa>();

        //se não ele faz o "Deserialize"
        return JsonSerializer.Deserialize<List<Tarefa>>(tarefasJson);
    }

    //este limpar é um booleano
    public static void Limpar()
        //aqui eu poderia aplicar um (remove)
        => SecureStorage.Default.Remove(TAREFAS);
}
