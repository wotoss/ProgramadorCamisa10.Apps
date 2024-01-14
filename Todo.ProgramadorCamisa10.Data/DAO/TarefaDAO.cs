using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.ProgramadorCamisa10.Core.Model;
using Todo.ProgramadorCamisa10.Data.Contexto;

namespace Todo.ProgramadorCamisa10.Data.DAO;

public class TarefaDAO
{
    private static readonly TodoAppContextoFake _contexto;

    static TarefaDAO()
    {
        _contexto = new TodoAppContextoFake();
    }
    public void Adicionar(Tarefa tarefa)
    {
        _contexto.Tarefas.Add(tarefa);
    }

    public void Atualizar(Tarefa tarefa)
    {
        var tarefasExistente = _contexto.Tarefas.FirstOrDefault(x => x.Nome.Equals(tarefa.Nome));
        if(tarefasExistente != null)
        {
            tarefasExistente.Descricao = tarefa.Descricao;
            tarefasExistente.DataConclusao = tarefa.DataConclusao;
            tarefasExistente.Concluida = tarefa.Concluida;
        }
    }
    public void Remover(Tarefa tarefa)
    {
        var tarefasExistente = _contexto.Tarefas.FirstOrDefault(x => x.Nome.Equals(tarefa.Nome));

        if (tarefasExistente != null)
        {
            _contexto.Tarefas.Remove(tarefasExistente);
        }
    }

    public List<Tarefa> Lista()
        => _contexto.Tarefas;
}
