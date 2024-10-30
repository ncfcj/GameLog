using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class Create_GameLog : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "todo_items",
            schema: "public");

        migrationBuilder.CreateTable(
            name: "game_logs",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                game_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                start_date = table.Column<DateTime>(type: "Date", nullable: true),
                end_date = table.Column<DateTime>(type: "Date", nullable: true),
                review = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                review_like_count = table.Column<long>(type: "bigint", nullable: false),
                created_at = table.Column<DateTime>(type: "Date", nullable: true),
                platform = table.Column<int>(type: "int", nullable: false),
                rating = table.Column<int>(type: "int", nullable: true),
                log_status = table.Column<int>(type: "int", nullable: false),
                genres = table.Column<string>(type: "text", nullable: false),
                steam_app_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                user_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_game_logs", x => x.id);
                table.ForeignKey(
                    name: "fk_game_logs_users_user_id",
                    column: x => x.user_id,
                    principalSchema: "public",
                    principalTable: "users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_game_logs_user_id",
            schema: "public",
            table: "game_logs",
            column: "user_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "game_logs",
            schema: "public");

        migrationBuilder.CreateTable(
            name: "todo_items",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                completed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                description = table.Column<string>(type: "text", nullable: false),
                due_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                is_completed = table.Column<bool>(type: "boolean", nullable: false),
                labels = table.Column<List<string>>(type: "text[]", nullable: false),
                priority = table.Column<int>(type: "integer", nullable: false),
                user_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_todo_items", x => x.id);
                table.ForeignKey(
                    name: "fk_todo_items_users_user_id",
                    column: x => x.user_id,
                    principalSchema: "public",
                    principalTable: "users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_todo_items_user_id",
            schema: "public",
            table: "todo_items",
            column: "user_id");
    }
}
