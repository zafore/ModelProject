using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Data.Migrations.Commons
{
    public partial class InitialDbMigrationData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    AuditLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryKeyValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ControllerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.AuditLogId);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ItemInUse = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "MasterData",
                columns: table => new
                {
                    MasterDataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterDataName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ColumnsNumberCount = table.Column<int>(type: "int", nullable: false),
                    InUse = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbData", x => x.MasterDataId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleIsactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Isactive = table.Column<bool>(type: "bit", nullable: true),
                    CrDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CrUserId = table.Column<int>(type: "int", nullable: true),
                    UpDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "SelectedItems",
                columns: table => new
                {
                    SelectedItemsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterDataId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbDataItem", x => x.SelectedItemsId);
                    table.ForeignKey(
                        name: "FK_TbDataItem_Items",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId");
                    table.ForeignKey(
                        name: "FK_TbDataItem_TbMasterData",
                        column: x => x.MasterDataId,
                        principalTable: "MasterData",
                        principalColumn: "MasterDataId");
                });

            migrationBuilder.CreateTable(
                name: "SelectedNumber",
                columns: table => new
                {
                    SelectedNumberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterDataId = table.Column<int>(type: "int", nullable: false),
                    ColumnsNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedNumber", x => x.SelectedNumberId);
                    table.ForeignKey(
                        name: "FK_SelectedNumber_MasterData",
                        column: x => x.MasterDataId,
                        principalTable: "MasterData",
                        principalColumn: "MasterDataId");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CrDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CrUser = table.Column<int>(type: "int", nullable: true),
                    UpDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpuserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserRoleId);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_UserInfos_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfos",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SelectedItems_ItemId",
                table: "SelectedItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedItems_MasterDataId",
                table: "SelectedItems",
                column: "MasterDataId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedNumber_MasterDataId",
                table: "SelectedNumber",
                column: "MasterDataId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "SelectedItems");

            migrationBuilder.DropTable(
                name: "SelectedNumber");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "MasterData");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserInfos");
        }
    }
}
