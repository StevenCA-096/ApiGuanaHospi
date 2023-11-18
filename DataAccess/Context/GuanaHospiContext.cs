using DataAccess.Models;
using DataAccess.RequestObjects;
using DataAccess.RequestViews;
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
        public DbSet<Especialidad> especialidad { get; set; } = default!;
        public DbSet<EspecialidadRequest> especialidadRequest { get; set; } = default!;
        public DbSet<Sintoma> sintoma { get; set; } = default!;
        public DbSet<Doctor> doctor { get; set; } = default!;
        public DbSet<Enfermedad_Sintoma> enfermedad_Sintoma { get; set; } = default!;
        public DbSet<Enfermedad> enfermedad { get; set; } = default!;
        public DbSet<Usuario> usuario { get; set; } = default!;
        public DbSet<Rol> rol { get; set; } = default!;
        public DbSet<Unidad> unidad { get; set; } = default!;
        public DbSet<Paciente> paciente { get; set; } = default!;
        public DbSet<Intervencion> intervencion { get; set; } = default!;
        public DbSet<IntervencionRequest> intervencionRequests { get; set; } = default!;
        public DbSet<TipoIntervencion> tipoIntervencion { get; set; } = default!;
        public DbSet<UnidadRequest> unidadRequests { get; set; } = default!;
        public DbSet<Paciente_Unidad> paciente_unidad { get; set; } = default!;
        public DbSet<PacienteUnidadRequest> pacienteUnidadRequests { get; set; } = default!;
        public DbSet<HistorialAcciones> HistorialAcciones { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PacienteUnidadRequest>().HasKey(ur => ur.id_paciente_unidad);

            modelBuilder.Entity<UnidadRequest>().HasKey(ur => ur.ID_Unidad);
            
            modelBuilder.Entity<EspecialidadRequest>().HasKey(er => er.ID_Especialidad);
            modelBuilder.Entity<IntervencionRequest>().HasKey(ir => ir.ID_Intervencion);

            //pk doctor
            modelBuilder.Entity<Doctor>()
                .HasKey(d => d.ID_Doctor);

            //pk unidad
            modelBuilder.Entity<Unidad>().HasKey(u => u.ID_Unidad);

            //relacion unidad - doctor
            modelBuilder.Entity<Doctor>().HasMany(d => d.unidad).WithOne(u => u.Doctor);

            //indicamos que la tabla doctor tiene una fk de la tabla especialidad que es su pk en dicha tabla
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.especialidad)
                .WithMany(e => e.doctores)
                .HasForeignKey(d => d.ID_Especialidad);

            //indicar por aca la fk de doctor en unidad o por el modelo (cualquiera de las 2 funciona)
            modelBuilder.Entity<Unidad>()
                .HasOne(d => d.Doctor)
                .WithMany(e => e.unidad)
                .HasForeignKey(d => d.Id_Doctor);

            //pk paciente
            modelBuilder.Entity<Paciente>().HasKey(p => p.ID_Paciente);

            //pk intervencion
            modelBuilder.Entity<Intervencion>().HasKey(p => p.ID_Intervencion);

            //pk especialidad
            modelBuilder.Entity<Especialidad>().HasKey(e => e.ID_Especialidad);

            //pk sintoma
            modelBuilder.Entity<Sintoma>().HasKey(s => s.ID_Sintoma);

            //pk usuario
            modelBuilder.Entity<Usuario>().HasKey(u => u.Id_Usuario);

            //pk rol
            modelBuilder.Entity<Rol>().HasKey(r => r.Id_Rol);

            //relacion usuario - rol
            modelBuilder.Entity<Usuario>()
                .HasOne(f => f.rol)
                .WithMany(e => e.usuarios)
                .HasForeignKey(f => f.ID_Rol);

            //pk enfermedad
            modelBuilder.Entity<Enfermedad>().HasKey(e => e.Id_Enfermedad);

            //pk enfermedad_sintoma
            modelBuilder.Entity<Enfermedad_Sintoma>().HasKey(es => es.Id_Enfermedad_Sintoma);

            //fk
            // modelBuilder.Entity<Enfermedad_Sintoma>()
            // .HasOne(f => f.enfermedad)
            // .WithMany(e => e.enfermedad_Sintoma)
            // .HasForeignKey(f => f.Id_Enfermedad);

            // modelBuilder.Entity<Enfermedad_Sintoma>()
            //.HasOne(f => f.sintoma)
            //.WithMany(e => e.enfermedad_Sintoma)
            //.HasForeignKey(f => f.Id_Sintoma);

            //
            modelBuilder.Entity<Paciente_Unidad>().HasKey(pa => pa.ID_Paciente_Unidad);


            //Definiendo PK paciente e intervencion
            modelBuilder.Entity<Paciente>().HasKey(p => p.ID_Paciente);
            modelBuilder.Entity<TipoIntervencion>().HasKey(ti => ti.iD_TipoIntervencion);

            //Relaciones de intervencion y configurando su PK
            modelBuilder.Entity<Intervencion>().HasKey(i => i.ID_Intervencion);

            modelBuilder.Entity<Intervencion>().HasOne(i => i.tipoIntervencion).WithMany(ti => ti.intervenciones);
            modelBuilder.Entity<TipoIntervencion>().HasMany(ti => ti.intervenciones).WithOne(i => i.tipoIntervencion);

            modelBuilder.Entity<Intervencion>().HasOne(i => i.paciente).WithMany(p => p.intervenciones);
            modelBuilder.Entity<Paciente>().HasMany(p => p.intervenciones).WithOne(i => i.paciente);

            modelBuilder.Entity<Intervencion>().HasOne(i => i.enfermedad).WithMany(e => e.intervenciones);
            modelBuilder.Entity<Enfermedad>().HasMany(e => e.intervenciones).WithOne(i => i.enfermedad);

            modelBuilder.Entity<Intervencion>().HasOne(i => i.doctor).WithMany(d => d.intervenciones);
            modelBuilder.Entity<Doctor>().HasMany(d => d.intervenciones).WithOne(i => i.doctor);
            modelBuilder.Entity<HistorialAcciones>().HasKey(p => p.Id);

        }



    }
}