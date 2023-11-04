﻿// <auto-generated />
using System;
using Data_Access;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data_Access.Migrations
{
    [DbContext(typeof(MiContexto))]
    [Migration("20231008191212_add-range-EstadoConservacion")]
    partial class addrangeEstadoConservacion
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Amenaza", b =>
                {
                    b.Property<int>("AmenazaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AmenazaId"));

                    b.Property<int>("EcosistemaMarinoId")
                        .HasColumnType("int");

                    b.Property<int>("GradoPeligrosidad")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AmenazaId");

                    b.HasIndex("EcosistemaMarinoId");

                    b.ToTable("Amenazas");
                });

            modelBuilder.Entity("Domain.Entities.Audit", b =>
                {
                    b.Property<int>("AuditId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuditId"));

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdEntidad")
                        .HasColumnType("int");

                    b.Property<string>("TipoEntidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Usuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AuditId");

                    b.ToTable("Audits");
                });

            modelBuilder.Entity("Domain.Entities.EcosistemaMarino", b =>
                {
                    b.Property<int>("EcosistemaMarinoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EcosistemaMarinoId"));

                    b.Property<double>("Area")
                        .HasColumnType("float");

                    b.Property<int?>("EspecieId")
                        .HasColumnType("int");

                    b.Property<int>("EstadoConservacionId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaisId")
                        .HasColumnType("int");

                    b.HasKey("EcosistemaMarinoId");

                    b.HasIndex("EspecieId");

                    b.HasIndex("EstadoConservacionId");

                    b.HasIndex("PaisId");

                    b.ToTable("Ecosistemas");
                });

            modelBuilder.Entity("Domain.Entities.Especie", b =>
                {
                    b.Property<int>("EspecieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EspecieId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EcosistemaMarinoId")
                        .HasColumnType("int");

                    b.Property<int>("EstadoConservacionId")
                        .HasColumnType("int");

                    b.Property<string>("NombreCientifico")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreVulgar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PesoMax")
                        .HasColumnType("float");

                    b.Property<double>("PesoMin")
                        .HasColumnType("float");

                    b.HasKey("EspecieId");

                    b.HasIndex("EcosistemaMarinoId");

                    b.HasIndex("EstadoConservacionId");

                    b.ToTable("Especies");
                });

            modelBuilder.Entity("Domain.Entities.EstadoConservacion", b =>
                {
                    b.Property<int>("EstadoConservacionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EstadoConservacionId"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ValorDesde")
                        .HasColumnType("int");

                    b.Property<int>("ValorHasta")
                        .HasColumnType("int");

                    b.HasKey("EstadoConservacionId");

                    b.HasAlternateKey("Nombre");

                    b.ToTable("EstadosConservacion");
                });

            modelBuilder.Entity("Domain.Entities.Pais", b =>
                {
                    b.Property<int>("PaisId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaisId"));

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaisId");

                    b.ToTable("Paises");
                });

            modelBuilder.Entity("Domain.Entities.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsuarioId"));

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Domain.Entities.Amenaza", b =>
                {
                    b.HasOne("Domain.Entities.EcosistemaMarino", null)
                        .WithMany("Amenazas")
                        .HasForeignKey("EcosistemaMarinoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.EcosistemaMarino", b =>
                {
                    b.HasOne("Domain.Entities.Especie", null)
                        .WithMany("EcosistemasHabitados")
                        .HasForeignKey("EspecieId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.Entities.EstadoConservacion", "EstadoConservacion")
                        .WithMany()
                        .HasForeignKey("EstadoConservacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Pais", null)
                        .WithMany("ecosistemaMarinos")
                        .HasForeignKey("PaisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.Entities.UbiGeografica", "UbicacionGeografica", b1 =>
                        {
                            b1.Property<int>("EcosistemaMarinoId")
                                .HasColumnType("int");

                            b1.Property<int>("GradoPeligro")
                                .HasColumnType("int");

                            b1.Property<double>("Latitud")
                                .HasColumnType("float");

                            b1.Property<double>("Longitud")
                                .HasColumnType("float");

                            b1.HasKey("EcosistemaMarinoId");

                            b1.ToTable("Ecosistemas");

                            b1.WithOwner()
                                .HasForeignKey("EcosistemaMarinoId");
                        });

                    b.Navigation("EstadoConservacion");

                    b.Navigation("UbicacionGeografica")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Especie", b =>
                {
                    b.HasOne("Domain.Entities.EcosistemaMarino", null)
                        .WithMany("Especies")
                        .HasForeignKey("EcosistemaMarinoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.Entities.EstadoConservacion", "EstadoConservacion")
                        .WithMany()
                        .HasForeignKey("EstadoConservacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EstadoConservacion");
                });

            modelBuilder.Entity("Domain.Entities.EcosistemaMarino", b =>
                {
                    b.Navigation("Amenazas");

                    b.Navigation("Especies");
                });

            modelBuilder.Entity("Domain.Entities.Especie", b =>
                {
                    b.Navigation("EcosistemasHabitados");
                });

            modelBuilder.Entity("Domain.Entities.Pais", b =>
                {
                    b.Navigation("ecosistemaMarinos");
                });
#pragma warning restore 612, 618
        }
    }
}
