using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.ProgramadorCamisa10.Core.Model;
    public class Tarefa
    {
      public string Nome { get; set; } = string.Empty;
      public string Descricao { get; set; } = string.Empty;
      public DateTime DataConclusao { get; set; }
      public bool Concluida { get; set; }
}

