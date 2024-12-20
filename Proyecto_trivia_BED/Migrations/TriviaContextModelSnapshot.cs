﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Proyecto_trivia_BED.ContextoDB;

namespace Proyecto_trivia_BED.Migrations
{
    [DbContext(typeof(TriviaContext))]
    partial class TriviaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Proyecto_trivia_BED.ContextoDB.Entidad.Categoria", b =>
                {
                    b.Property<int>("IdCategoria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NombreCategoria")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WebId")
                        .HasColumnType("int");

                    b.Property<int>("externalAPI")
                        .HasColumnType("int");

                    b.HasKey("IdCategoria");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Proyecto_trivia_BED.ContextoDB.Entidad.Dificultad", b =>
                {
                    b.Property<int>("IdDificultad")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NombreDificultad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Valor")
                        .HasColumnType("int");

                    b.Property<int>("externalAPI")
                        .HasColumnType("int");

                    b.Property<int>("webId")
                        .HasColumnType("int");

                    b.HasKey("IdDificultad");

                    b.ToTable("Dificultades");

                    b.HasData(
                        new
                        {
                            IdDificultad = 1,
                            NombreDificultad = "easy",
                            Valor = 1,
                            externalAPI = 1,
                            webId = 1
                        },
                        new
                        {
                            IdDificultad = 2,
                            NombreDificultad = "medium",
                            Valor = 3,
                            externalAPI = 1,
                            webId = 3
                        },
                        new
                        {
                            IdDificultad = 3,
                            NombreDificultad = "hard",
                            Valor = 5,
                            externalAPI = 1,
                            webId = 5
                        });
                });

            modelBuilder.Entity("Proyecto_trivia_BED.ContextoDB.Entidad.Pregunta", b =>
                {
                    b.Property<int>("IdPregunta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoriaIdCategoria")
                        .HasColumnType("int");

                    b.Property<int?>("DificultadIdDificultad")
                        .HasColumnType("int");

                    b.Property<string>("LaPregunta")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPregunta");

                    b.HasIndex("CategoriaIdCategoria");

                    b.HasIndex("DificultadIdDificultad");

                    b.ToTable("Preguntas");
                });

            modelBuilder.Entity("Proyecto_trivia_BED.ContextoDB.Entidad.Puntaje", b =>
                {
                    b.Property<int>("IdPuntaje")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("Tiempo")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioIdUsuario")
                        .HasColumnType("int");

                    b.Property<float>("ValorPuntaje")
                        .HasColumnType("real");

                    b.HasKey("IdPuntaje");

                    b.HasIndex("UsuarioIdUsuario");

                    b.ToTable("Puntajes");
                });

            modelBuilder.Entity("Proyecto_trivia_BED.ContextoDB.Entidad.Respuesta", b =>
                {
                    b.Property<int>("IdRespuesta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Correcta")
                        .HasColumnType("bit");

                    b.Property<int?>("PreguntaIdPregunta")
                        .HasColumnType("int");

                    b.Property<string>("SRespuesta")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdRespuesta");

                    b.HasIndex("PreguntaIdPregunta");

                    b.ToTable("Respuestas");
                });

            modelBuilder.Entity("Proyecto_trivia_BED.ContextoDB.Entidad.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("EsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("NombreUsuario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUsuario");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Proyecto_trivia_BED.ContextoDB.Entidad.Pregunta", b =>
                {
                    b.HasOne("Proyecto_trivia_BED.ContextoDB.Entidad.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaIdCategoria");

                    b.HasOne("Proyecto_trivia_BED.ContextoDB.Entidad.Dificultad", "Dificultad")
                        .WithMany()
                        .HasForeignKey("DificultadIdDificultad");

                    b.Navigation("Categoria");

                    b.Navigation("Dificultad");
                });

            modelBuilder.Entity("Proyecto_trivia_BED.ContextoDB.Entidad.Puntaje", b =>
                {
                    b.HasOne("Proyecto_trivia_BED.ContextoDB.Entidad.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioIdUsuario");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Proyecto_trivia_BED.ContextoDB.Entidad.Respuesta", b =>
                {
                    b.HasOne("Proyecto_trivia_BED.ContextoDB.Entidad.Pregunta", null)
                        .WithMany("Respuestas")
                        .HasForeignKey("PreguntaIdPregunta");
                });

            modelBuilder.Entity("Proyecto_trivia_BED.ContextoDB.Entidad.Pregunta", b =>
                {
                    b.Navigation("Respuestas");
                });
#pragma warning restore 612, 618
        }
    }
}
