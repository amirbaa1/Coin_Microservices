using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wallet.API.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "walletModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_walletModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WalletCoinModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameCoin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    coinPrice = table.Column<double>(type: "float", nullable: false),
                    PriceUSD = table.Column<double>(type: "float", nullable: false),
                    WalletModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletCoinModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletCoinModel_walletModels_WalletModelId",
                        column: x => x.WalletModelId,
                        principalTable: "walletModels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WalletCoinModel_WalletModelId",
                table: "WalletCoinModel",
                column: "WalletModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WalletCoinModel");

            migrationBuilder.DropTable(
                name: "walletModels");
        }
    }
}
