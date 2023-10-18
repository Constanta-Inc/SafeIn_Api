using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityTest.Migrations
{
    /// <inheritdoc />
    public partial class DoorsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentEmployee_Departments_DepartmentsDepartmentId",
                table: "DepartmentEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_Doors_Companies_CompanyId",
                table: "Doors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doors_Departments_DepartmentId",
                table: "Doors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doors",
                table: "Doors");

            migrationBuilder.DropIndex(
                name: "IX_Doors_CompanyId",
                table: "Doors");

            migrationBuilder.DropColumn(
                name: "DoorId",
                table: "Doors");

            migrationBuilder.RenameColumn(
                name: "EntranceId",
                table: "Entrances",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Doors",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Departments",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DepartmentsDepartmentId",
                table: "DepartmentEmployee",
                newName: "DepartmentsId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Companies",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentId",
                table: "Doors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doors",
                table: "Doors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentEmployee_Departments_DepartmentsId",
                table: "DepartmentEmployee",
                column: "DepartmentsId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doors_Departments_DepartmentId",
                table: "Doors",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentEmployee_Departments_DepartmentsId",
                table: "DepartmentEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_Doors_Departments_DepartmentId",
                table: "Doors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doors",
                table: "Doors");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Entrances",
                newName: "EntranceId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Doors",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Departments",
                newName: "DepartmentId");

            migrationBuilder.RenameColumn(
                name: "DepartmentsId",
                table: "DepartmentEmployee",
                newName: "DepartmentsDepartmentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Companies",
                newName: "CompanyId");

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentId",
                table: "Doors",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "DoorId",
                table: "Doors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doors",
                table: "Doors",
                column: "DoorId");

            migrationBuilder.CreateIndex(
                name: "IX_Doors_CompanyId",
                table: "Doors",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentEmployee_Departments_DepartmentsDepartmentId",
                table: "DepartmentEmployee",
                column: "DepartmentsDepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doors_Companies_CompanyId",
                table: "Doors",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doors_Departments_DepartmentId",
                table: "Doors",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId");
        }
    }
}
