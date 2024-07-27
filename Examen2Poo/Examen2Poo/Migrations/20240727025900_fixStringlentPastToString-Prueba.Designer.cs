﻿// <auto-generated />
using System;
using Examen2Poo.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Examen2Poo.Migrations
{
    [DbContext(typeof(Examen2PooContext))]
    [Migration("20240727025900_fixStringlentPastToString-Prueba")]
    partial class fixStringlentPastToStringPrueba
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Examen2Poo.Database.Entities.AmortizationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<double>("AbonoExtraordinario")
                        .HasColumnType("float")
                        .HasColumnName("abono_extraordinario");

                    b.Property<double>("Amount")
                        .HasColumnType("float")
                        .HasColumnName("abono");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<double>("CuotaConSvcd")
                        .HasColumnType("float")
                        .HasColumnName("cuota_con_seguro");

                    b.Property<double>("Interest")
                        .HasColumnType("float")
                        .HasColumnName("tasa_interes");

                    b.Property<int>("NCuota")
                        .HasColumnType("int")
                        .HasColumnName("NCuota");

                    b.Property<string>("NombreClave")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("identidad_cliente");

                    b.Property<double>("SaldoPrincipal")
                        .HasColumnType("float")
                        .HasColumnName("Saldo Principal");

                    b.Property<double>("Svcd")
                        .HasColumnType("float")
                        .HasColumnName("seguro");

                    b.Property<string>("UpdateBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("updated_by");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_date");

                    b.Property<double>("cuotaSinSvcd")
                        .HasColumnType("float")
                        .HasColumnName("cuota_sin_Seguro");

                    b.HasKey("Id");

                    b.ToTable("amortitation", "dbo");
                });

            modelBuilder.Entity("Examen2Poo.Database.Entities.ClientAmortitationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("AmortizationId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("amortitation_id");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("client_id");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<string>("UpdateBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("updated_by");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_date");

                    b.HasKey("Id");

                    b.HasIndex("AmortizationId");

                    b.HasIndex("ClientId");

                    b.ToTable("client_amortitation", "dbo");
                });

            modelBuilder.Entity("Examen2Poo.Database.Entities.ClientEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<double>("ComisionRate")
                        .HasColumnType("float")
                        .HasColumnName("tasa_comision");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<DateTime>("DateTimeDesembolso")
                        .HasColumnType("datetime2")
                        .HasColumnName("fecha_desembolso");

                    b.Property<DateTime>("DeteTimePrimerPago")
                        .HasColumnType("datetime2")
                        .HasColumnName("Fecha del Primer Pago");

                    b.Property<string>("IdentytyNumber")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)")
                        .HasColumnName("identidad");

                    b.Property<double>("InteresRest")
                        .HasColumnType("float")
                        .HasColumnName("tasa_interes");

                    b.Property<double>("LoadAmount")
                        .HasColumnType("float")
                        .HasColumnName("monto_desembolsado");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("nombre");

                    b.Property<int>("Ter")
                        .HasColumnType("int")
                        .HasColumnName("termino_pagos");

                    b.Property<string>("UpdateBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("updated_by");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_date");

                    b.HasKey("Id");

                    b.ToTable("client", "dbo");
                });

            modelBuilder.Entity("Examen2Poo.Database.Entities.ClientAmortitationEntity", b =>
                {
                    b.HasOne("Examen2Poo.Database.Entities.AmortizationEntity", "Amortization")
                        .WithMany("Clients")
                        .HasForeignKey("AmortizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Examen2Poo.Database.Entities.ClientEntity", "Clients")
                        .WithMany("Amortitations")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Amortization");

                    b.Navigation("Clients");
                });

            modelBuilder.Entity("Examen2Poo.Database.Entities.AmortizationEntity", b =>
                {
                    b.Navigation("Clients");
                });

            modelBuilder.Entity("Examen2Poo.Database.Entities.ClientEntity", b =>
                {
                    b.Navigation("Amortitations");
                });
#pragma warning restore 612, 618
        }
    }
}