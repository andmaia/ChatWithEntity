using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Migrations
{
    /// <inheritdoc />
    public partial class MessageTalkToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_TalksToUsers_TalkToUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Talks_TalkId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_TalkToUserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "TalkToUserId",
                table: "Messages");

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "TalkId",
                keyValue: null,
                column: "TalkId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "TalkId",
                table: "Messages",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MessageTallkToUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TalkToUserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MessageId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageTallkToUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageTallkToUsers_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageTallkToUsers_TalksToUsers_TalkToUserId",
                        column: x => x.TalkToUserId,
                        principalTable: "TalksToUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MessageTallkToUsers_MessageId",
                table: "MessageTallkToUsers",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageTallkToUsers_TalkToUserId",
                table: "MessageTallkToUsers",
                column: "TalkToUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Talks_TalkId",
                table: "Messages",
                column: "TalkId",
                principalTable: "Talks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Talks_TalkId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "MessageTallkToUsers");

            migrationBuilder.AlterColumn<string>(
                name: "TalkId",
                table: "Messages",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TalkToUserId",
                table: "Messages",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_TalkToUserId",
                table: "Messages",
                column: "TalkToUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_TalksToUsers_TalkToUserId",
                table: "Messages",
                column: "TalkToUserId",
                principalTable: "TalksToUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Talks_TalkId",
                table: "Messages",
                column: "TalkId",
                principalTable: "Talks",
                principalColumn: "Id");
        }
    }
}
