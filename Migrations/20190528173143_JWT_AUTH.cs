using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CommercantsAPI.Migrations
{
    public partial class JWT_AUTH : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_roleId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Shop_shopId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "shopId",
                table: "User",
                newName: "ShopId");

            migrationBuilder.RenameColumn(
                name: "roleId",
                table: "User",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_User_shopId",
                table: "User",
                newName: "IX_User_ShopId");

            migrationBuilder.RenameIndex(
                name: "IX_User_roleId",
                table: "User",
                newName: "IX_User_RoleId");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "User",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "User",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Shop_ShopId",
                table: "User",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Shop_ShopId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "ShopId",
                table: "User",
                newName: "shopId");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "User",
                newName: "roleId");

            migrationBuilder.RenameIndex(
                name: "IX_User_ShopId",
                table: "User",
                newName: "IX_User_shopId");

            migrationBuilder.RenameIndex(
                name: "IX_User_RoleId",
                table: "User",
                newName: "IX_User_roleId");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "User",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_roleId",
                table: "User",
                column: "roleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Shop_shopId",
                table: "User",
                column: "shopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
