using Microsoft.EntityFrameworkCore.Migrations;

namespace Proyecto_trivia_BED.Migrations
{
    public partial class FixDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Respuestas_Preguntas_EPreguntaIdPregunta",
                table: "Respuestas");

            migrationBuilder.RenameColumn(
                name: "EPreguntaIdPregunta",
                table: "Respuestas",
                newName: "PreguntaIdPregunta");

            migrationBuilder.RenameIndex(
                name: "IX_Respuestas_EPreguntaIdPregunta",
                table: "Respuestas",
                newName: "IX_Respuestas_PreguntaIdPregunta");

            migrationBuilder.AddForeignKey(
                name: "FK_Respuestas_Preguntas_PreguntaIdPregunta",
                table: "Respuestas",
                column: "PreguntaIdPregunta",
                principalTable: "Preguntas",
                principalColumn: "IdPregunta",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Respuestas_Preguntas_PreguntaIdPregunta",
                table: "Respuestas");

            migrationBuilder.RenameColumn(
                name: "PreguntaIdPregunta",
                table: "Respuestas",
                newName: "EPreguntaIdPregunta");

            migrationBuilder.RenameIndex(
                name: "IX_Respuestas_PreguntaIdPregunta",
                table: "Respuestas",
                newName: "IX_Respuestas_EPreguntaIdPregunta");

            migrationBuilder.AddForeignKey(
                name: "FK_Respuestas_Preguntas_EPreguntaIdPregunta",
                table: "Respuestas",
                column: "EPreguntaIdPregunta",
                principalTable: "Preguntas",
                principalColumn: "IdPregunta",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
