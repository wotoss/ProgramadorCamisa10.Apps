using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.ProgramadorCamisa10.Core.Model;

namespace Todo.ProgramadorCamisa10.Data.Contexto
{
    //classe publica TodoAppContexto vai herdar de DbContext
    public class TodoAppContexto : DbContext
    {
        //como não vou ter injeção de dependencia eu deixo o construtor vazio.
        public TodoAppContexto()
        {
        
        }
        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //se tiver configurado ele da o retorno
            if (optionsBuilder.IsConfigured)
                return;

            //preciso colocar o banco em um lugar que seja comum a todos os tipos de dispositivos
            var databasePath = Path
                //resolve questões do "caminho" como barra - contraBarra
                .Combine(Environment
                .GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "todoApp.db");

            optionsBuilder.UseSqlite($"Filename={databasePath}");

            base.OnConfiguring(optionsBuilder);
        }

        //para ele pegar todas as classes que herdam de EntityConfiguration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoAppContexto).Assembly);
        }
    }
    
}
