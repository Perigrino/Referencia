using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RefsGuy.Application.Migrations
{
    /// <inheritdoc />
    public partial class updated_models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReferralCodes_Users_UsersId",
                table: "ReferralCodes");

            migrationBuilder.DropIndex(
                name: "IX_ReferralCodes_UsersId",
                table: "ReferralCodes");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "ReferralCodes");

            migrationBuilder.CreateIndex(
                name: "IX_ReferralCodes_UserId",
                table: "ReferralCodes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReferralCodes_Users_UserId",
                table: "ReferralCodes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReferralCodes_Users_UserId",
                table: "ReferralCodes");

            migrationBuilder.DropIndex(
                name: "IX_ReferralCodes_UserId",
                table: "ReferralCodes");

            migrationBuilder.AddColumn<Guid>(
                name: "UsersId",
                table: "ReferralCodes",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReferralCodes_UsersId",
                table: "ReferralCodes",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReferralCodes_Users_UsersId",
                table: "ReferralCodes",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
