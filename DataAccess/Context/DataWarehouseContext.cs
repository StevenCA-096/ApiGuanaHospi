using DataAccess.Models;
using DataAccess.RequestViews;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class DataWarehouseContext : DbContext
    {
        public DataWarehouseContext(DbContextOptions<DataWarehouseContext> DWoptions)
           : base(DWoptions)
        {

        }
        public DbSet<TopUnidadesPorPacientes> TopUnidadesPorPacientes { get; set; } = default!;
        public DbSet<TopEnfermedadesPorIntervenciones> TopEnfermedadesPorIntervenciones { get; set; } = default!;
        public DbSet<TopIntervencionesPorDoctor> TopIntervencionesPorDoctor { get; set; } = default!;
        public DbSet<TopIntervencionesPorTipo> TopIntervencionesPorTipo { get; set; } = default!;
        public DbSet<TopIntervencionesPorPaciente> TopIntervencionesPorPaciente { get; set; } = default!;
        public DbSet<TopSintomasPorEnfermedad> TopSintomasPorEnfermedad { get; set; } = default!;
        public DbSet<TopSintomasAtendidos> TopSintomasAtendidos { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TopUnidadesPorPacientes>().HasNoKey();
            modelBuilder.Entity<TopEnfermedadesPorIntervenciones>().HasNoKey();
            modelBuilder.Entity<TopIntervencionesPorDoctor>().HasNoKey();
            modelBuilder.Entity<TopIntervencionesPorTipo>().HasNoKey();
            modelBuilder.Entity<TopIntervencionesPorPaciente>().HasNoKey();
            modelBuilder.Entity<TopSintomasPorEnfermedad>().HasNoKey();
            modelBuilder.Entity<TopSintomasAtendidos>().HasNoKey();

        }


    }
}
