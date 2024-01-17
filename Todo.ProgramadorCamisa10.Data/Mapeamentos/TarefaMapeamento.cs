using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.ProgramadorCamisa10.Core.Model;

namespace Todo.ProgramadorCamisa10.Data.Mapeamentos;

public class TarefaMapeamento : IEntityTypeConfiguration<Tarefa>
{
    public void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        //modelo/formato da minha tabela e coluna para persistir ao banco SQLite
        builder.ToTable("Tarefas");

        builder.HasKey(x => x.Id);
        //lembrando que meu Id é um Guid estou passando com string
        builder.Property(x => x.Id)
            .HasConversion<string>();

        builder.Property(x=> x.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Descricao)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.DataConclusao)
            .IsRequired();

        builder.Property(x => x.Concluida)
            .IsRequired();
    }
}
