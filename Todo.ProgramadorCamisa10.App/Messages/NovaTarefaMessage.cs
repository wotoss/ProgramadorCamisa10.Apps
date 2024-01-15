using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.ProgramadorCamisa10.Core.Model;

namespace Todo.ProgramadorCamisa10.Messages;

public class NovaTarefaMessage : ValueChangedMessage<Tarefa>
{
    public NovaTarefaMessage(Tarefa value) : base(value)
    {

    }
}
