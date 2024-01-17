using Microsoft.EntityFrameworkCore;
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
    private static readonly TodoAppContexto _contexto;

    static TarefaDAO()
    {
        _contexto = new TodoAppContexto();
    }
    public async Task Adicionar(Tarefa tarefa)
    {
       await _contexto.Tarefas.AddAsync(tarefa);
       await _contexto.SaveChangesAsync();
    }
    //adicionar já em lista
    public async Task Adicionar(List<Tarefa> tarefaList)
    {
        await _contexto.Tarefas.AddRangeAsync(tarefaList);
        await _contexto.SaveChangesAsync();
    }

    public async Task Atualizar(Tarefa tarefa)
    {
         //_contexto.Tarefas.Update(tarefa);
         _contexto.Update(tarefa);
        await _contexto.SaveChangesAsync();
    }
    public async Task Remover(Tarefa tarefa)
    {
        _contexto.Remove(tarefa);
        await _contexto.SaveChangesAsync();
    }

    public async Task<List<Tarefa>> Lista()
        => await _contexto.Tarefas.ToListAsync();


    #region métodos que criam e deletam o banco de dados
    public void CriarBancoDeDados()
    {
        _contexto.Database.EnsureCreated();
    }

    public void AplicarAtualizacoesBancoDeDados()
    {
        _contexto.Database.Migrate();
    }
    #endregion
}


#region DAO - feita com dados fake 

//public class TarefaDAO
//{
//    private static readonly TodoAppContextoFake _contexto;

//    static TarefaDAO()
//    {
//        _contexto = new TodoAppContextoFake();
//    }
//    public void Adicionar(Tarefa tarefa)
//    {
//        _contexto.Tarefas.Add(tarefa);
//    }
//    //adicionar já em lista
//    public void Adicionar(List<Tarefa> tarefaList)
//    {
//        _contexto.Tarefas.AddRange(tarefaList);
//    }

//    public void Atualizar(Tarefa tarefa)
//    {
//        var tarefasExistente = _contexto.Tarefas.FirstOrDefault(x => x.Nome.Equals(tarefa.Nome));
//        if (tarefasExistente != null)
//        {
//            tarefasExistente.Descricao = tarefa.Descricao;
//            tarefasExistente.DataConclusao = tarefa.DataConclusao;
//            tarefasExistente.Concluida = tarefa.Concluida;
//        }
//    }
//    public void Remover(Tarefa tarefa)
//    {
//        var tarefasExistente = _contexto.Tarefas.FirstOrDefault(x => x.Nome.Equals(tarefa.Nome));

//        if (tarefasExistente != null)
//        {
//            _contexto.Tarefas.Remove(tarefasExistente);
//        }
//    }

//    public List<Tarefa> Lista()
//        => _contexto.Tarefas;
//}

#endregion