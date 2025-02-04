using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "WatchRoomUsers");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "WatchRoomInvites");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "UserStatistics");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "UserPreferences");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "SupportTickets");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Subtitles");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Seasons");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "LikeDislikes");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Histories");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Friendships");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "FriendRequests");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "FAQs");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "DownloadLists");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "DownloadListItems");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "CustomLists");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "CustomListItems");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "AudioTracks");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Analytics");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "ActivityLogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "WatchRoomUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "WatchRoomInvites",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "UserStatistics",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "UserPreferences",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "SupportTickets",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Subtitles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Subscriptions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Series",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Seasons",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Reviews",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Rentals",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Recommendations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Ratings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Plans",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Payments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Notifications",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Movies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "LikeDislikes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Languages",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Histories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Genres",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Friendships",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "FriendRequests",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "FAQs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Episodes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "DownloadLists",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "DownloadListItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Directors",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "CustomLists",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "CustomListItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "ChatMessages",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "AudioTracks",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Analytics",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Actors",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "ActivityLogs",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
