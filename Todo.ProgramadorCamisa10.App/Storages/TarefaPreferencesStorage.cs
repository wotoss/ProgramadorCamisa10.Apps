using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Todo.ProgramadorCamisa10.Core.Model;

namespace Todo.ProgramadorCamisa10.Storages;

  public static class TarefaPreferencesStorage
    {
    //boa pratica=> como estou repetindo muito a key "tarefas", vou refatorar.
    //criando uma private só poderá ser usada dentro desta classe
    private const string TAREFAS = "tarefas";
        public static void Salvar(Tarefa tarefa)
        {
            List<Tarefa> tarefas;

            if (Preferences.Default.ContainsKey(TAREFAS))
            {
                tarefas = JsonSerializer
                    .Deserialize<List<Tarefa>>(Preferences.Default.Get(TAREFAS, string.Empty));
            }
            else
            {
                tarefas = new List<Tarefa>();
            }

            tarefas.Add(tarefa);

            Preferences.Default.Set(TAREFAS, JsonSerializer.Serialize(tarefas));
        }

        public static void Atualizar (Tarefa tarefa)
        {
        //se o atualizar não tiver eu vou apenas dar um retorn
        if (!Preferences.Default.ContainsKey(TAREFAS))
            return;

        //aqui ele busca as tarefas...
        var tarefas = JsonSerializer
              .Deserialize<List<Tarefa>>(Preferences.Default.Get(TAREFAS, string.Empty));

        //irá pegar a primeira tarefa comparando o id
        var tarefaAtualizada = tarefas.FirstOrDefault(x => x.Id == tarefa.Id);

        tarefaAtualizada.Nome = tarefa.Nome;
        tarefaAtualizada.Descricao = tarefa.Descricao;
        tarefaAtualizada.DataConclusao = tarefa.DataConclusao;

        //depois ele serializa a tarefa novamente.
        Preferences.Default.Set(TAREFAS, JsonSerializer.Serialize(tarefas));
      }


        //montar o método excluir
        public static void Excluir(Tarefa tarefa)
        {
        //se não tiver nenhuma tarefa ele retorna
        if (!Preferences.Default.ContainsKey(TAREFAS))
            return;

        //tendo ele vai trazer a tarefa
        var tarefas = JsonSerializer
             .Deserialize<List<Tarefa>>(Preferences.Default.Get(TAREFAS, string.Empty));

        var tarefasExistentes = tarefas.FirstOrDefault(x => x.Id == tarefa.Id);

        //aqui ele remove as tarefa
        tarefas.Remove(tarefasExistentes);

        //e seta novamente
        Preferences.Default.Set(TAREFAS, JsonSerializer.Serialize(tarefas));
        }

        //Listar todas as tarefas
        public static List<Tarefa> Lista()
        {
        if (!Preferences.Default.ContainsKey(TAREFAS))
            return new List<Tarefa>();

            return JsonSerializer
                .Deserialize<List<Tarefa>>(Preferences.Default.Get(TAREFAS, string.Empty));
        }
        //não quero que esta chave exista mais
        public static void Limpar()
        {
        //se não contiver a chave retorna 
            if (!Preferences.Default.ContainsKey(TAREFAS))
                return;

        //se tiver quero que remova a key/chave
            Preferences.Default.Remove(TAREFAS);
        }

        //temos tambem o limpartodos ou reset
        //public static void Reset()
        //{
        //    Preferences.Default.Clear();
        //}
   }

   

