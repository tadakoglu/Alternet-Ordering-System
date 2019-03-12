using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AlternetSiparisYazilimi.Migrations
{
    public partial class Siparisler : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Siparisler",
                columns: table => new
                {
                    SiparisID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdresSatiri1 = table.Column<string>(nullable: false),
                    AdresSatiri2 = table.Column<string>(nullable: true),
                    AdresSatiri3 = table.Column<string>(nullable: true),
                    Alici = table.Column<string>(nullable: false),
                    PostaKodu = table.Column<string>(nullable: true),
                    Sehir = table.Column<string>(nullable: false),
                    Ulke = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siparisler", x => x.SiparisID);
                });

            migrationBuilder.CreateTable(
                name: "SepetSatiri",
                columns: table => new
                {
                    SatirID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Adet = table.Column<int>(nullable: false),
                    SiparisID = table.Column<int>(nullable: true),
                    UrunID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SepetSatiri", x => x.SatirID);
                    table.ForeignKey(
                        name: "FK_SepetSatiri_Siparisler_SiparisID",
                        column: x => x.SiparisID,
                        principalTable: "Siparisler",
                        principalColumn: "SiparisID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SepetSatiri_Urunler_UrunID",
                        column: x => x.UrunID,
                        principalTable: "Urunler",
                        principalColumn: "UrunID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SepetSatiri_SiparisID",
                table: "SepetSatiri",
                column: "SiparisID");

            migrationBuilder.CreateIndex(
                name: "IX_SepetSatiri_UrunID",
                table: "SepetSatiri",
                column: "UrunID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SepetSatiri");

            migrationBuilder.DropTable(
                name: "Siparisler");
        }
    }
}
