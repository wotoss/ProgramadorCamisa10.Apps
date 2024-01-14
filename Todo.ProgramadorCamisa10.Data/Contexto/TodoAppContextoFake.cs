using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.ProgramadorCamisa10.Core.Model;

namespace Todo.ProgramadorCamisa10.Data.Contexto;
    public  class TodoAppContextoFake
    {
       public static TodoAppContextoFake Instance { get; } = new ();
       //criando lista de tarefas para dados fake
       public  List<Tarefa> Tarefas { get; set; } 

        public TodoAppContextoFake()
        {
          Tarefas = new List<Tarefa>();
        }

    }

