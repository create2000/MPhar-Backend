using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthcareAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixHealthDuplicate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Illnesses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "AssignedHealthProfessionalId",
                table: "Illnesses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientId",
                table: "Illnesses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Recommendation",
                table: "Illnesses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RecommendationDate",
                table: "Illnesses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmissionDate",
                table: "Illnesses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "HealthProfessionals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.CreateIndex(
                name: "IX_Illnesses_AssignedHealthProfessionalId",
                table: "Illnesses",
                column: "AssignedHealthProfessionalId");

            migrationBuilder.CreateIndex(
                name: "IX_Illnesses_PatientId",
                table: "Illnesses",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Illnesses_AspNetUsers_PatientId",
                table: "Illnesses",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Illnesses_HealthProfessionals_AssignedHealthProfessionalId",
                table: "Illnesses",
                column: "AssignedHealthProfessionalId",
                principalTable: "HealthProfessionals",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Illnesses_AspNetUsers_PatientId",
                table: "Illnesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Illnesses_HealthProfessionals_AssignedHealthProfessionalId",
                table: "Illnesses");

            migrationBuilder.DropIndex(
                name: "IX_Illnesses_AssignedHealthProfessionalId",
                table: "Illnesses");

            migrationBuilder.DropIndex(
                name: "IX_Illnesses_PatientId",
                table: "Illnesses");

            migrationBuilder.DropColumn(
                name: "AssignedHealthProfessionalId",
                table: "Illnesses");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Illnesses");

            migrationBuilder.DropColumn(
                name: "Recommendation",
                table: "Illnesses");

            migrationBuilder.DropColumn(
                name: "RecommendationDate",
                table: "Illnesses");

            migrationBuilder.DropColumn(
                name: "SubmissionDate",
                table: "Illnesses");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "HealthProfessionals");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Illnesses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
