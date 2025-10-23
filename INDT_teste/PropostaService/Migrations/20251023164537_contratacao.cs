using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropostaService.Migrations
{
    /// <inheritdoc />
    public partial class contratacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_CONTRATACAO",
                table: "PROPOSTAS",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DATA_CONTRATACAO",
                table: "PROPOSTAS");
        }
    }
}
