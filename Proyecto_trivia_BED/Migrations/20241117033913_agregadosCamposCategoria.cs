using Microsoft.EntityFrameworkCore.Migrations;

namespace Proyecto_trivia_BED.Migrations
{
    public partial class agregadosCamposCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "IdWeb",
                table: "Categorias",
                newName: "externalAPI");

            migrationBuilder.AddColumn<int>(
                name: "externalAPI",
                table: "Dificultades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "webId",
                table: "Dificultades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WebId",
                table: "Categorias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Dificultades",
                keyColumn: "IdDificultad",
                keyValue: 1,
                columns: new[] { "externalAPI", "webId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Dificultades",
                keyColumn: "IdDificultad",
                keyValue: 2,
                columns: new[] { "externalAPI", "webId" },
                values: new object[] { 1, 3 });

            migrationBuilder.UpdateData(
                table: "Dificultades",
                keyColumn: "IdDificultad",
                keyValue: 3,
                columns: new[] { "externalAPI", "webId" },
                values: new object[] { 1, 5 });

            migrationBuilder.AddForeignKey(
                name: "FK_Respuestas_Preguntas_EPreguntaIdPregunta",
                table: "Respuestas",
                column: "EPreguntaIdPregunta",
                principalTable: "Preguntas",
                principalColumn: "IdPregunta",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Respuestas_Preguntas_EPreguntaIdPregunta",
                table: "Respuestas");

            migrationBuilder.DropColumn(
                name: "externalAPI",
                table: "Dificultades");

            migrationBuilder.DropColumn(
                name: "webId",
                table: "Dificultades");

            migrationBuilder.DropColumn(
                name: "WebId",
                table: "Categorias");

            migrationBuilder.RenameColumn(
                name: "EPreguntaIdPregunta",
                table: "Respuestas",
                newName: "PreguntaIdPregunta");

            migrationBuilder.RenameIndex(
                name: "IX_Respuestas_EPreguntaIdPregunta",
                table: "Respuestas",
                newName: "IX_Respuestas_PreguntaIdPregunta");

            migrationBuilder.RenameColumn(
                name: "externalAPI",
                table: "Categorias",
                newName: "IdWeb");

            migrationBuilder.AddForeignKey(
                name: "FK_Respuestas_Preguntas_PreguntaIdPregunta",
                table: "Respuestas",
                column: "PreguntaIdPregunta",
                principalTable: "Preguntas",
                principalColumn: "IdPregunta",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
