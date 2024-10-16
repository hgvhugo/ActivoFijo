using ActivoFijo.Models;
using Microsoft.EntityFrameworkCore;

namespace ActivoFijo.Data
{
    public class AppDbContext : DbContext

    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<RegistroBienes> RegistroBienes { get; set; }
        public DbSet<Partida> Partidas { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Camb> Camps { get; set; }

        public DbSet<Cucop> Cucops { get; set; }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<UnidadAdministrativa> UnidadesAdministrativas { get; set; }
        public DbSet<EmpleadoUnidadAdministrativa> EmpleadoUnidadesAdministrativas { get; set; }

        public DbSet<RegistroBienesTemp> RegistroBienesTemp { get; set; }

        public DbSet<RegistroBienesLog> RegistroBienesLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            // Configuración de la relación Empleados-UnidadesAdministrativas por tabla intermedia m-m
            modelBuilder.Entity<EmpleadoUnidadAdministrativa>()
                .HasKey(eua => new { eua.EmpleadoId, eua.UnidadAdministrativaId });

            //modelBuilder.Entity<EmpleadoUnidadAdministrativa>()
            //    .HasOne(eua => eua.Empleado)
            //    .WithMany(e => e.EmpleadoUnidadesAdministrativas)
            //    .HasForeignKey(eua => eua.EmpleadoId);

            //modelBuilder.Entity<EmpleadoUnidadAdministrativa>()
            //    .HasOne(eua => eua.UnidadAdministrativa)
            //    .WithMany(ua => ua.EmpleadoUnidadAdministrativas)
            //    .HasForeignKey(eua => eua.UnidadAdministrativaId);


            //// Configuración de la relación Empleados-Contratos uno a muchos
            //modelBuilder.Entity<Empleado>()
            //    .HasOne(e => e.Ubicacion)
            //    .WithMany(u => u.Empleados)
            //    .HasForeignKey(e => e.UbicacionId);


            //// Configuración de la relación RegistroBienes-Partidas uno a uno
            //modelBuilder.Entity<RegistroBienes>()
            //    .HasOne(rb => rb.Partida)
            //    .WithMany(p => p.RegistroBienes)
            //    .HasForeignKey<RegistroBienes>(rb => rb.PartidaId);

            //// Configuración de la relación RegistroBienes-Cambs uno a uno
            //modelBuilder.Entity<RegistroBienes>()
            //    .HasOne(rb => rb.Camb)
            //    .WithMany(c => c.RegistroBienes)
            //    .HasForeignKey<RegistroBienes>(rb => rb.CambId);

            //// Configuración de la relación RegistroBienes-Cucops uno a uno
            //modelBuilder.Entity<RegistroBienes>()
            //    .HasOne(rb => rb.Cucop)
            //    .WithMany(c => c.RegistroBienes)
            //    .HasForeignKey<RegistroBienes>(rb => rb.CucopId);

            //// Configuración de la relación RegistroBienes-UnidadesAdministrativas uno a uno
            //modelBuilder.Entity<RegistroBienes>()
            //    .HasOne(rb => rb.UnidadAdministrativa)
            //    .WithMany(ua => ua.RegistroBienes)
            //    .HasForeignKey<RegistroBienes>(rb => rb.UnidadAdministrativaId);

            //// Configuración de la relación RegistroBienes-Ubicaciones uno a muchos
            //modelBuilder.Entity<RegistroBienes>()
            //    .HasOne(rb => rb.Ubicacion)
            //    .WithMany(u => u.RegistroBienes)
            //    .HasForeignKey(rb => rb.UbicacionId);

            //// Configuración de la relación RegistroBienes-Empleados uno a uno
            //modelBuilder.Entity<RegistroBienes>()
            //    .HasOne(rb => rb.Empleado)
            //    .WithMany(e => e.RegistroBienes)
            //    .HasForeignKey<RegistroBienes>(rb => rb.EmpleadoId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //// Configuración de la relación RegistroBienes-FotosBienActivo uno a muchos
            //modelBuilder.Entity<RegistroBienes>()
            //    .HasMany(rb => rb.FotosBienActivo)
            //    .WithOne(f => f.RegistroBienes)
            //    .HasForeignKey(f => f.RegistroBienesId);



            base.OnModelCreating(modelBuilder);
        }





    }
}
