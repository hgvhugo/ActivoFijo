﻿// <auto-generated />
using System;
using ActivoFijo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ActivoFijo.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240903185228_cambiomodelo")]
    partial class cambiomodelo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ActivoFijo.Models.Camb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModifica")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("cat_cambs");
                });

            modelBuilder.Entity("ActivoFijo.Models.Contrato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModifica")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tbl_contratos");
                });

            modelBuilder.Entity("ActivoFijo.Models.Cucop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModifica")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("cat_cucop");
                });

            modelBuilder.Entity("ActivoFijo.Models.Empleado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("ApellidoMaterno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApellidoPaterno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rfc")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UbicacionId")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioModifica")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("Rfc")
                        .IsUnique();

                    b.HasIndex("UbicacionId");

                    b.ToTable("tbl_empleados");
                });

            modelBuilder.Entity("ActivoFijo.Models.EmpleadoUnidadAdministrativa", b =>
                {
                    b.Property<int>("EmpleadoId")
                        .HasColumnType("int");

                    b.Property<int>("UnidadAdministrativaId")
                        .HasColumnType("int");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModifica")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmpleadoId", "UnidadAdministrativaId");

                    b.HasIndex("UnidadAdministrativaId");

                    b.ToTable("tbr_empleado_unidad_administrativa");
                });

            modelBuilder.Entity("ActivoFijo.Models.FotosBienActivo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FotoBien")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RegistroBienesId")
                        .HasColumnType("int");

                    b.Property<int>("TipoFotoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RegistroBienesId");

                    b.ToTable("tbl_fotos_bien_activo");
                });

            modelBuilder.Entity("ActivoFijo.Models.Partida", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModifica")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("cat_partidas");
                });

            modelBuilder.Entity("ActivoFijo.Models.RegistroBienes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Activo")
                        .HasColumnType("bit");

                    b.Property<int?>("CambId")
                        .HasColumnType("int");

                    b.Property<string>("CodigoBien")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int?>("CucopId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int?>("EmpleadoId")
                        .HasColumnType("int");

                    b.Property<int>("EstatusId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaEfectos")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaFactura")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("FotoBien")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FotosId")
                        .HasColumnType("int");

                    b.Property<string>("IPAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Marca")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Modelo")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NombreBien")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NumeroContrato")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroFactura")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PartidaId")
                        .HasColumnType("int");

                    b.Property<string>("Serie")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("UbicacionId")
                        .HasColumnType("int");

                    b.Property<int?>("UnidadAdministrativaId")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioModifica")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("ValorDepreciado")
                        .HasColumnType("float");

                    b.Property<double?>("ValorFactura")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CambId");

                    b.HasIndex("CucopId");

                    b.HasIndex("EmpleadoId");

                    b.HasIndex("PartidaId");

                    b.HasIndex("UbicacionId");

                    b.HasIndex("UnidadAdministrativaId");

                    b.ToTable("tbl_registro_bienes");
                });

            modelBuilder.Entity("ActivoFijo.Models.RegistroBienesTemp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CambId")
                        .HasColumnType("int");

                    b.Property<string>("CargaId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodigoBien")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CucopId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EmpleadoId")
                        .HasColumnType("int");

                    b.Property<string>("ErrorValidacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EstatusId")
                        .HasColumnType("int");

                    b.Property<bool?>("Exito")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("FechaEfectos")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaFactura")
                        .HasColumnType("datetime2");

                    b.Property<string>("FotoBien")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Marca")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Modelo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreBien")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroContrato")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroFactura")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PartidaId")
                        .HasColumnType("int");

                    b.Property<bool?>("Procesado")
                        .HasColumnType("bit");

                    b.Property<string>("Serie")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UbicacionId")
                        .HasColumnType("int");

                    b.Property<int?>("UnidadAdministrativaId")
                        .HasColumnType("int");

                    b.Property<double?>("ValorDepreciado")
                        .HasColumnType("float");

                    b.Property<double?>("ValorFactura")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("tmp_registro_bienes");
                });

            modelBuilder.Entity("ActivoFijo.Models.Ubicacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModifica")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("cat_ubicaciones");
                });

            modelBuilder.Entity("ActivoFijo.Models.UnidadAdministrativa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoUnidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModifica")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("cat_unidades_administrativas");
                });

            modelBuilder.Entity("ActivoFijo.Models.Empleado", b =>
                {
                    b.HasOne("ActivoFijo.Models.Ubicacion", "Ubicacion")
                        .WithMany()
                        .HasForeignKey("UbicacionId");

                    b.Navigation("Ubicacion");
                });

            modelBuilder.Entity("ActivoFijo.Models.EmpleadoUnidadAdministrativa", b =>
                {
                    b.HasOne("ActivoFijo.Models.Empleado", null)
                        .WithMany("EmpleadoUnidadesAdministrativas")
                        .HasForeignKey("EmpleadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ActivoFijo.Models.UnidadAdministrativa", null)
                        .WithMany("EmpleadoUnidadAdministrativas")
                        .HasForeignKey("UnidadAdministrativaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ActivoFijo.Models.FotosBienActivo", b =>
                {
                    b.HasOne("ActivoFijo.Models.RegistroBienes", null)
                        .WithMany("FotosBienActivo")
                        .HasForeignKey("RegistroBienesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ActivoFijo.Models.RegistroBienes", b =>
                {
                    b.HasOne("ActivoFijo.Models.Camb", "Camb")
                        .WithMany()
                        .HasForeignKey("CambId");

                    b.HasOne("ActivoFijo.Models.Cucop", "Cucop")
                        .WithMany()
                        .HasForeignKey("CucopId");

                    b.HasOne("ActivoFijo.Models.Empleado", "Empleado")
                        .WithMany()
                        .HasForeignKey("EmpleadoId");

                    b.HasOne("ActivoFijo.Models.Partida", "Partida")
                        .WithMany()
                        .HasForeignKey("PartidaId");

                    b.HasOne("ActivoFijo.Models.Ubicacion", "Ubicacion")
                        .WithMany()
                        .HasForeignKey("UbicacionId");

                    b.HasOne("ActivoFijo.Models.UnidadAdministrativa", "UnidadAdministrativa")
                        .WithMany()
                        .HasForeignKey("UnidadAdministrativaId");

                    b.Navigation("Camb");

                    b.Navigation("Cucop");

                    b.Navigation("Empleado");

                    b.Navigation("Partida");

                    b.Navigation("Ubicacion");

                    b.Navigation("UnidadAdministrativa");
                });

            modelBuilder.Entity("ActivoFijo.Models.Empleado", b =>
                {
                    b.Navigation("EmpleadoUnidadesAdministrativas");
                });

            modelBuilder.Entity("ActivoFijo.Models.RegistroBienes", b =>
                {
                    b.Navigation("FotosBienActivo");
                });

            modelBuilder.Entity("ActivoFijo.Models.UnidadAdministrativa", b =>
                {
                    b.Navigation("EmpleadoUnidadAdministrativas");
                });
#pragma warning restore 612, 618
        }
    }
}
