using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update61624 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produit_LigneFacture_LigneFactureId",
                table: "Produit");

            migrationBuilder.DropIndex(
                name: "IX_Produit_LigneFactureId",
                table: "Produit");

            migrationBuilder.DropColumn(
                name: "LigneFactureId",
                table: "Produit");

            migrationBuilder.AddColumn<Guid>(
                name: "ProduitId",
                table: "LigneFacture",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_LigneFacture_ProduitId",
                table: "LigneFacture",
                column: "ProduitId");

            migrationBuilder.AddForeignKey(
                name: "FK_LigneFacture_Produit_ProduitId",
                table: "LigneFacture",
                column: "ProduitId",
                principalTable: "Produit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LigneFacture_Produit_ProduitId",
                table: "LigneFacture");

            migrationBuilder.DropIndex(
                name: "IX_LigneFacture_ProduitId",
                table: "LigneFacture");

            migrationBuilder.DropColumn(
                name: "ProduitId",
                table: "LigneFacture");

            migrationBuilder.AddColumn<Guid>(
                name: "LigneFactureId",
                table: "Produit",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produit_LigneFactureId",
                table: "Produit",
                column: "LigneFactureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produit_LigneFacture_LigneFactureId",
                table: "Produit",
                column: "LigneFactureId",
                principalTable: "LigneFacture",
                principalColumn: "Id");
        }
    }
}
