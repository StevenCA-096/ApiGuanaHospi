using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class GuanaHospiContext : DbContext
    {
        public GuanaHospiContext(DbContextOptions<GuanaHospiContext> options)
           : base(options)
        {
            // Deshabilita el uso de proxies de carga diferida, es como utilizar .AsNoTracking() en el controlador
            //this.ChangeTracker.LazyLoadingEnabled = false;
        }

        //public DbSet<Especialidad> especialidad { get; set; } = default!;
        //public DbSet<Sintoma> sintoma { get; set; } = default!;
        //public DbSet<Doctor> doctor { get; set; } = default!;
        //public DbSet<Enfermedad_Sintoma> enfermedad_Sintoma { get; set; } = default!;
        //public DbSet<Enfermedad> enfermedad { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Especialidad>().HasKey(e => e.ID_Especialidad);

            modelBuilder.Entity<Sintoma>().HasKey(s => s.ID_Sintoma);

            modelBuilder.Entity<Doctor>()
                .HasKey(d => d.ID_Doctor);

            //indicamos que la tabla doctor tiene una fk de la tabla especialidad que es su pk en dicha tabla
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.especialidad)
                .WithMany(e => e.doctores)
                .HasForeignKey(d => d.ID_Especialidad);

            modelBuilder.Entity<Enfermedad>().HasKey(e => e.Id_Enfermedad);

            modelBuilder.Entity<Enfermedad_Sintoma>().HasKey(es => es.Id_Enfermedad);
        }



    }
}