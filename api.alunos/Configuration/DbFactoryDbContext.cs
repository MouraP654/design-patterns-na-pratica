using api.alunos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.alunos.Configuration
{
    public class DbFactoryDbContext : IDesignTimeDbContextFactory<CursoDbContext>
    {
        public CursoDbContext CreateDbContext(string[] args)
        {

            var optionsBuilder = new DbContextOptionsBuilder<CursoDbContext>();

            optionsBuilder.UseSqlServer("Server=DESKTOP-92OR4DK;Database=ProjetoDDD;Integrated Security=True;Context Connection=false");

            CursoDbContext contexto = new CursoDbContext(optionsBuilder.Options);

            return contexto;
        }
    }
}
