using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MovieApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CreatedAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Biography = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false),
                    IsMale = table.Column<bool>(type: "boolean", nullable: false),
                    UserStatisticsId = table.Column<int>(type: "integer", nullable: true),
                    LikedMoviesCount = table.Column<int>(type: "integer", nullable: false),
                    DislikedMoviesCount = table.Column<int>(type: "integer", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Directors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Surname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: true),
                    Biography = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Icon = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DurationTime = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Action = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LogDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityLogs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    ListName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomLists_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DownloadLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownloadLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DownloadLists_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FAQs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Question = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Answer = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "date", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "date", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FAQs_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "FriendRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SenderId = table.Column<string>(type: "text", nullable: false),
                    ReceiverId = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    SentAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FriendRequests_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FriendRequests_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Friendships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    FriendId = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friendships_AspNetUsers_FriendId",
                        column: x => x.FriendId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Friendships_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    SendDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    PaymentMethod = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    TransactionId = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupportTickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    ResolvedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    AssignedUserId = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportTickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportTickets_AspNetUsers_AssignedUserId",
                        column: x => x.AssignedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SupportTickets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    TotalMoviesWatched = table.Column<int>(type: "integer", nullable: false),
                    TotalSeriesWatched = table.Column<int>(type: "integer", nullable: false),
                    TotalWatchTime = table.Column<int>(type: "integer", nullable: false),
                    MostWatchedMovieGenre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    GenreWatchTime = table.Column<string>(type: "jsonb", nullable: false),
                    WeeklyWatchTime = table.Column<string>(type: "jsonb", nullable: false),
                    MonthlyWatchTime = table.Column<string>(type: "jsonb", nullable: false),
                    TopWatchedMovies = table.Column<string>(type: "jsonb", nullable: false),
                    TopWatchedSeries = table.Column<string>(type: "jsonb", nullable: false),
                    TotalFavorites = table.Column<int>(type: "integer", nullable: false),
                    TotalWatchLater = table.Column<int>(type: "integer", nullable: false),
                    TotalComments = table.Column<int>(type: "integer", nullable: false),
                    MovieRatings = table.Column<string>(type: "jsonb", nullable: false),
                    SerieRatings = table.Column<string>(type: "jsonb", nullable: false),
                    TotalDownloads = table.Column<int>(type: "integer", nullable: false),
                    DownloadedMovies = table.Column<string>(type: "jsonb", nullable: false),
                    DownloadedSeries = table.Column<string>(type: "jsonb", nullable: false),
                    TotalOfflineWatchTime = table.Column<int>(type: "integer", nullable: false),
                    DailyActivity = table.Column<string>(type: "jsonb", nullable: false),
                    MonthlyActivity = table.Column<string>(type: "jsonb", nullable: false),
                    LastActivity = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TotalLikes = table.Column<int>(type: "integer", nullable: false),
                    TotalDislikes = table.Column<int>(type: "integer", nullable: false),
                    MovieLikes = table.Column<string>(type: "jsonb", nullable: false),
                    SerieLikes = table.Column<string>(type: "jsonb", nullable: false),
                    TotalFriends = table.Column<int>(type: "integer", nullable: false),
                    TotalFriendRequestsSent = table.Column<int>(type: "integer", nullable: false),
                    TotalFriendRequestsReceived = table.Column<int>(type: "integer", nullable: false),
                    AverageWatchTimePerDay = table.Column<int>(type: "integer", nullable: false),
                    AverageWatchTimePerWeek = table.Column<int>(type: "integer", nullable: false),
                    LongestWatchSession = table.Column<int>(type: "integer", nullable: false),
                    MostActiveDay = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserStatistics_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PosterUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    TrailerUrl = table.Column<string>(type: "text", nullable: true),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "date", nullable: false),
                    DirectorId = table.Column<int>(type: "integer", nullable: true),
                    LikeCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    DislikeCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Directors_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Directors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserPreferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    GenreId = table.Column<int>(type: "integer", nullable: false),
                    PreferenceWeight = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPreferences_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPreferences_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subtitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LanguageId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subtitles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subtitles_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    PlanId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieActors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActorId = table.Column<int>(type: "integer", nullable: true),
                    MovieId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieActors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieActors_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieActors_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MovieId = table.Column<int>(type: "integer", nullable: true),
                    GenreId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MovieId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RentalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentals_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rentals_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieSubtitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MovieId = table.Column<int>(type: "integer", nullable: false),
                    SubtitleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieSubtitles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieSubtitles_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovieSubtitles_Subtitles_SubtitleId",
                        column: x => x.SubtitleId,
                        principalTable: "Subtitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Analytics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TotalUsers = table.Column<int>(type: "integer", nullable: false),
                    TotalSeries = table.Column<int>(type: "integer", nullable: false),
                    TotalMovies = table.Column<int>(type: "integer", nullable: false),
                    TotalRentals = table.Column<int>(type: "integer", nullable: false),
                    TotalSubscriptions = table.Column<int>(type: "integer", nullable: false),
                    TotalViews = table.Column<int>(type: "integer", nullable: false),
                    TotalFavorites = table.Column<int>(type: "integer", nullable: false),
                    TotalReviews = table.Column<int>(type: "integer", nullable: false),
                    TotalRatings = table.Column<int>(type: "integer", nullable: false),
                    TotalDownloads = table.Column<int>(type: "integer", nullable: false),
                    ActiveUsers = table.Column<int>(type: "integer", nullable: false),
                    NewUsers = table.Column<int>(type: "integer", nullable: false),
                    TotalRevenue = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    AverageRating = table.Column<double>(type: "numeric(4,2)", nullable: false),
                    MostPopularMovieId = table.Column<int>(type: "integer", nullable: true),
                    MostPopularSerieId = table.Column<int>(type: "integer", nullable: true),
                    MostRentedMovieId = table.Column<int>(type: "integer", nullable: true),
                    MostRentedSerieId = table.Column<int>(type: "integer", nullable: true),
                    MostDownloadedMovieId = table.Column<int>(type: "integer", nullable: true),
                    MostDownloadedSerieId = table.Column<int>(type: "integer", nullable: true),
                    MostReviewedMovieId = table.Column<int>(type: "integer", nullable: true),
                    MostReviewedSerieId = table.Column<int>(type: "integer", nullable: true),
                    MostRatedMovieId = table.Column<int>(type: "integer", nullable: true),
                    MostRatedSerieId = table.Column<int>(type: "integer", nullable: true),
                    AnalyticsDate = table.Column<DateTime>(type: "date", nullable: false),
                    AnalyticsType = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analytics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Analytics_Movies_MostDownloadedMovieId",
                        column: x => x.MostDownloadedMovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Analytics_Movies_MostPopularMovieId",
                        column: x => x.MostPopularMovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Analytics_Movies_MostRatedMovieId",
                        column: x => x.MostRatedMovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Analytics_Movies_MostRentedMovieId",
                        column: x => x.MostRentedMovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Analytics_Movies_MostReviewedMovieId",
                        column: x => x.MostReviewedMovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AudioTracks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MovieId = table.Column<int>(type: "integer", nullable: false),
                    SerieId = table.Column<int>(type: "integer", nullable: false),
                    LanguageId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioTracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AudioTracks_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AudioTracks_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WatchRoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    MessageType = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    LinkUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SystemMessage = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Message = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    SentAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomListItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomListId = table.Column<int>(type: "integer", nullable: false),
                    MovieId = table.Column<int>(type: "integer", nullable: true),
                    SeriesId = table.Column<int>(type: "integer", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomListItems_CustomLists_CustomListId",
                        column: x => x.CustomListId,
                        principalTable: "CustomLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomListItems_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "DownloadListItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DownloadListId = table.Column<int>(type: "integer", nullable: false),
                    MovieId = table.Column<int>(type: "integer", nullable: false),
                    EpisodeId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DownloadDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DownloadQuality = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownloadListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DownloadListItems_DownloadLists_DownloadListId",
                        column: x => x.DownloadListId,
                        principalTable: "DownloadLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DownloadListItems_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    EpisodeNumber = table.Column<int>(type: "integer", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "date", nullable: false),
                    LikeCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    DislikeCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    SeasonId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    MovieId = table.Column<int>(type: "integer", nullable: true),
                    SerieId = table.Column<int>(type: "integer", nullable: true),
                    EpisodeId = table.Column<int>(type: "integer", nullable: true),
                    WatchedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    StoppedAt = table.Column<int>(type: "int", nullable: true),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    WatchedDuration = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Histories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Histories_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Histories_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    PosterUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    TrailerUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    HistoryId = table.Column<int>(type: "integer", nullable: true),
                    DirectorId = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Series_Directors_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Directors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Series_Histories_HistoryId",
                        column: x => x.HistoryId,
                        principalTable: "Histories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    MovieId = table.Column<int>(type: "integer", nullable: false),
                    SerieId = table.Column<int>(type: "integer", nullable: false),
                    EpisodeId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Ratings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Ratings_Series_SerieId",
                        column: x => x.SerieId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recommendations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MovieId = table.Column<int>(type: "integer", nullable: false),
                    SerieId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RecommendedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recommendations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recommendations_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recommendations_Series_SerieId",
                        column: x => x.SerieId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    SeasonNumber = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    SerieId = table.Column<int>(type: "integer", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seasons_Series_SerieId",
                        column: x => x.SerieId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SerieActors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActorId = table.Column<int>(type: "integer", nullable: true),
                    SerieId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerieActors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SerieActors_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SerieActors_Series_SerieId",
                        column: x => x.SerieId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SerieGenres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SerieId = table.Column<int>(type: "integer", nullable: true),
                    GenreId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerieGenres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SerieGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SerieGenres_Series_SerieId",
                        column: x => x.SerieId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SerieSubtitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SerieId = table.Column<int>(type: "integer", nullable: false),
                    SubtitleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerieSubtitles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SerieSubtitles_Series_SerieId",
                        column: x => x.SerieId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SerieSubtitles_Subtitles_SubtitleId",
                        column: x => x.SubtitleId,
                        principalTable: "Subtitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WatchRooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    HostUserId = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    EndedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MovieId = table.Column<int>(type: "integer", nullable: true),
                    SerieId = table.Column<int>(type: "integer", nullable: true),
                    ShareCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatchRooms_AspNetUsers_HostUserId",
                        column: x => x.HostUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WatchRooms_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_WatchRooms_Series_SerieId",
                        column: x => x.SerieId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: false),
                    MovieId = table.Column<int>(type: "integer", nullable: true),
                    SerieId = table.Column<int>(type: "integer", nullable: true),
                    EpisodeId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    RatingId = table.Column<int>(type: "integer", nullable: true),
                    ReviewDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LikeCount = table.Column<int>(type: "integer", nullable: false),
                    DislikeCount = table.Column<int>(type: "integer", nullable: false),
                    ParentReviewId = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Ratings_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Reviews_ParentReviewId",
                        column: x => x.ParentReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Series_SerieId",
                        column: x => x.SerieId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WatchRoomInvites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InviterId = table.Column<string>(type: "text", nullable: false),
                    InviteeId = table.Column<string>(type: "text", nullable: false),
                    WatchRoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchRoomInvites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatchRoomInvites_AspNetUsers_InviteeId",
                        column: x => x.InviteeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WatchRoomInvites_AspNetUsers_InviterId",
                        column: x => x.InviterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WatchRoomInvites_WatchRooms_WatchRoomId",
                        column: x => x.WatchRoomId,
                        principalTable: "WatchRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WatchRoomUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    WatchRoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchRoomUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatchRoomUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WatchRoomUsers_WatchRooms_WatchRoomId",
                        column: x => x.WatchRoomId,
                        principalTable: "WatchRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikeDislikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    MovieId = table.Column<int>(type: "integer", nullable: true),
                    EpisodeId = table.Column<int>(type: "integer", nullable: true),
                    ReviewId = table.Column<int>(type: "integer", nullable: true),
                    IsLike = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeDislikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikeDislikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeDislikes_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeDislikes_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeDislikes_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_UserId",
                table: "ActivityLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Analytics_AnalyticsDate_AnalyticsType",
                table: "Analytics",
                columns: new[] { "AnalyticsDate", "AnalyticsType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Analytics_MostDownloadedMovieId",
                table: "Analytics",
                column: "MostDownloadedMovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Analytics_MostDownloadedSerieId",
                table: "Analytics",
                column: "MostDownloadedSerieId");

            migrationBuilder.CreateIndex(
                name: "IX_Analytics_MostPopularMovieId",
                table: "Analytics",
                column: "MostPopularMovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Analytics_MostPopularSerieId",
                table: "Analytics",
                column: "MostPopularSerieId");

            migrationBuilder.CreateIndex(
                name: "IX_Analytics_MostRatedMovieId",
                table: "Analytics",
                column: "MostRatedMovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Analytics_MostRatedSerieId",
                table: "Analytics",
                column: "MostRatedSerieId");

            migrationBuilder.CreateIndex(
                name: "IX_Analytics_MostRentedMovieId",
                table: "Analytics",
                column: "MostRentedMovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Analytics_MostRentedSerieId",
                table: "Analytics",
                column: "MostRentedSerieId");

            migrationBuilder.CreateIndex(
                name: "IX_Analytics_MostReviewedMovieId",
                table: "Analytics",
                column: "MostReviewedMovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Analytics_MostReviewedSerieId",
                table: "Analytics",
                column: "MostReviewedSerieId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AudioTracks_LanguageId",
                table: "AudioTracks",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_AudioTracks_MovieId_SerieId_LanguageId",
                table: "AudioTracks",
                columns: new[] { "MovieId", "SerieId", "LanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AudioTracks_SerieId",
                table: "AudioTracks",
                column: "SerieId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_MessageType",
                table: "ChatMessages",
                column: "MessageType");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_SentAt",
                table: "ChatMessages",
                column: "SentAt");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_UserId",
                table: "ChatMessages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_WatchRoomId",
                table: "ChatMessages",
                column: "WatchRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomListItems_CustomListId",
                table: "CustomListItems",
                column: "CustomListId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomListItems_MovieId",
                table: "CustomListItems",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomListItems_SeriesId",
                table: "CustomListItems",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomLists_ListName",
                table: "CustomLists",
                column: "ListName");

            migrationBuilder.CreateIndex(
                name: "IX_CustomLists_UserId",
                table: "CustomLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DownloadListItems_DownloadListId",
                table: "DownloadListItems",
                column: "DownloadListId");

            migrationBuilder.CreateIndex(
                name: "IX_DownloadListItems_EpisodeId",
                table: "DownloadListItems",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_DownloadListItems_MovieId",
                table: "DownloadListItems",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_DownloadLists_UserId",
                table: "DownloadLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_SeasonId",
                table: "Episodes",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_FAQs_CreatedById",
                table: "FAQs",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_ReceiverId",
                table: "FriendRequests",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_SenderId",
                table: "FriendRequests",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_SentAt",
                table: "FriendRequests",
                column: "SentAt");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_Status",
                table: "FriendRequests",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_FriendId",
                table: "Friendships",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_UserId_FriendId",
                table: "Friendships",
                columns: new[] { "UserId", "FriendId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Name",
                table: "Genres",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Histories_EpisodeId",
                table: "Histories",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_MovieId",
                table: "Histories",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_SerieId",
                table: "Histories",
                column: "SerieId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Histories_UserId",
                table: "Histories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_Code",
                table: "Languages",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LikeDislikes_EpisodeId",
                table: "LikeDislikes",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeDislikes_MovieId",
                table: "LikeDislikes",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeDislikes_ReviewId",
                table: "LikeDislikes",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeDislikes_UserId_EpisodeId",
                table: "LikeDislikes",
                columns: new[] { "UserId", "EpisodeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LikeDislikes_UserId_MovieId",
                table: "LikeDislikes",
                columns: new[] { "UserId", "MovieId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LikeDislikes_UserId_ReviewId",
                table: "LikeDislikes",
                columns: new[] { "UserId", "ReviewId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieActors_ActorId",
                table: "MovieActors",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieActors_MovieId_ActorId",
                table: "MovieActors",
                columns: new[] { "MovieId", "ActorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_GenreId",
                table: "MovieGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_MovieId_GenreId",
                table: "MovieGenres",
                columns: new[] { "MovieId", "GenreId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieSubtitles_MovieId",
                table: "MovieSubtitles",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieSubtitles_MovieId_SubtitleId",
                table: "MovieSubtitles",
                columns: new[] { "MovieId", "SubtitleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieSubtitles_SubtitleId",
                table: "MovieSubtitles",
                column: "SubtitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_DirectorId",
                table: "Movies",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_ReleaseDate",
                table: "Movies",
                column: "ReleaseDate");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentDate",
                table: "Payments",
                column: "PaymentDate");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId",
                table: "Payments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_Name",
                table: "Plans",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_EpisodeId",
                table: "Ratings",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_MovieId",
                table: "Ratings",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_SerieId",
                table: "Ratings",
                column: "SerieId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId_EpisodeId",
                table: "Ratings",
                columns: new[] { "UserId", "EpisodeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId_MovieId",
                table: "Ratings",
                columns: new[] { "UserId", "MovieId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId_SerieId",
                table: "Ratings",
                columns: new[] { "UserId", "SerieId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_MovieId",
                table: "Recommendations",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_SerieId",
                table: "Recommendations",
                column: "SerieId");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_UserId",
                table: "Recommendations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_MovieId",
                table: "Rentals",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_UserId",
                table: "Rentals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_EpisodeId",
                table: "Reviews",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MovieId",
                table: "Reviews",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ParentReviewId",
                table: "Reviews",
                column: "ParentReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RatingId",
                table: "Reviews",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_SerieId",
                table: "Reviews",
                column: "SerieId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_ReleaseDate",
                table: "Seasons",
                column: "ReleaseDate");

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_SerieId",
                table: "Seasons",
                column: "SerieId");

            migrationBuilder.CreateIndex(
                name: "IX_SerieActors_ActorId",
                table: "SerieActors",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_SerieActors_SerieId",
                table: "SerieActors",
                column: "SerieId");

            migrationBuilder.CreateIndex(
                name: "IX_SerieActors_SerieId_ActorId",
                table: "SerieActors",
                columns: new[] { "SerieId", "ActorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SerieGenres_GenreId",
                table: "SerieGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_SerieGenres_SerieId",
                table: "SerieGenres",
                column: "SerieId");

            migrationBuilder.CreateIndex(
                name: "IX_SerieGenres_SerieId_GenreId",
                table: "SerieGenres",
                columns: new[] { "SerieId", "GenreId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SerieSubtitles_SerieId",
                table: "SerieSubtitles",
                column: "SerieId");

            migrationBuilder.CreateIndex(
                name: "IX_SerieSubtitles_SerieId_SubtitleId",
                table: "SerieSubtitles",
                columns: new[] { "SerieId", "SubtitleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SerieSubtitles_SubtitleId",
                table: "SerieSubtitles",
                column: "SubtitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_DirectorId",
                table: "Series",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_HistoryId",
                table: "Series",
                column: "HistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_Title",
                table: "Series",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_EndTime",
                table: "Subscriptions",
                column: "EndTime");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_PlanId",
                table: "Subscriptions",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_UserId",
                table: "Subscriptions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subtitles_LanguageId",
                table: "Subtitles",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Subtitles_LanguageId_Id",
                table: "Subtitles",
                columns: new[] { "LanguageId", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_SupportTickets_AssignedUserId",
                table: "SupportTickets",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportTickets_UserId",
                table: "SupportTickets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferences_GenreId",
                table: "UserPreferences",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferences_PreferenceWeight",
                table: "UserPreferences",
                column: "PreferenceWeight");

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferences_UserId_GenreId",
                table: "UserPreferences",
                columns: new[] { "UserId", "GenreId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserStatistics_LastActivity",
                table: "UserStatistics",
                column: "LastActivity");

            migrationBuilder.CreateIndex(
                name: "IX_UserStatistics_MostWatchedMovieGenre",
                table: "UserStatistics",
                column: "MostWatchedMovieGenre");

            migrationBuilder.CreateIndex(
                name: "IX_UserStatistics_TotalMoviesWatched",
                table: "UserStatistics",
                column: "TotalMoviesWatched");

            migrationBuilder.CreateIndex(
                name: "IX_UserStatistics_TotalSeriesWatched",
                table: "UserStatistics",
                column: "TotalSeriesWatched");

            migrationBuilder.CreateIndex(
                name: "IX_UserStatistics_TotalWatchTime",
                table: "UserStatistics",
                column: "TotalWatchTime");

            migrationBuilder.CreateIndex(
                name: "IX_UserStatistics_UserId",
                table: "UserStatistics",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WatchRoomInvites_InviteeId",
                table: "WatchRoomInvites",
                column: "InviteeId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchRoomInvites_InviterId",
                table: "WatchRoomInvites",
                column: "InviterId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchRoomInvites_WatchRoomId",
                table: "WatchRoomInvites",
                column: "WatchRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchRoomUsers_UserId",
                table: "WatchRoomUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchRoomUsers_UserId_WatchRoomId",
                table: "WatchRoomUsers",
                columns: new[] { "UserId", "WatchRoomId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WatchRoomUsers_WatchRoomId",
                table: "WatchRoomUsers",
                column: "WatchRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchRooms_HostUserId",
                table: "WatchRooms",
                column: "HostUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchRooms_MovieId",
                table: "WatchRooms",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchRooms_RoomName",
                table: "WatchRooms",
                column: "RoomName");

            migrationBuilder.CreateIndex(
                name: "IX_WatchRooms_SerieId",
                table: "WatchRooms",
                column: "SerieId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchRooms_ShareCode",
                table: "WatchRooms",
                column: "ShareCode",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Analytics_Series_MostDownloadedSerieId",
                table: "Analytics",
                column: "MostDownloadedSerieId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Analytics_Series_MostPopularSerieId",
                table: "Analytics",
                column: "MostPopularSerieId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Analytics_Series_MostRatedSerieId",
                table: "Analytics",
                column: "MostRatedSerieId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Analytics_Series_MostRentedSerieId",
                table: "Analytics",
                column: "MostRentedSerieId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Analytics_Series_MostReviewedSerieId",
                table: "Analytics",
                column: "MostReviewedSerieId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AudioTracks_Series_SerieId",
                table: "AudioTracks",
                column: "SerieId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_WatchRooms_WatchRoomId",
                table: "ChatMessages",
                column: "WatchRoomId",
                principalTable: "WatchRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomListItems_Series_SeriesId",
                table: "CustomListItems",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DownloadListItems_Episodes_EpisodeId",
                table: "DownloadListItems",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Seasons_SeasonId",
                table: "Episodes",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_Series_SerieId",
                table: "Histories",
                column: "SerieId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_AspNetUsers_UserId",
                table: "Histories");

            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Movies_MovieId",
                table: "Histories");

            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Series_SerieId",
                table: "Histories");

            migrationBuilder.DropForeignKey(
                name: "FK_Seasons_Series_SerieId",
                table: "Seasons");

            migrationBuilder.DropTable(
                name: "ActivityLogs");

            migrationBuilder.DropTable(
                name: "Analytics");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AudioTracks");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "CustomListItems");

            migrationBuilder.DropTable(
                name: "DownloadListItems");

            migrationBuilder.DropTable(
                name: "FAQs");

            migrationBuilder.DropTable(
                name: "FriendRequests");

            migrationBuilder.DropTable(
                name: "Friendships");

            migrationBuilder.DropTable(
                name: "LikeDislikes");

            migrationBuilder.DropTable(
                name: "MovieActors");

            migrationBuilder.DropTable(
                name: "MovieGenres");

            migrationBuilder.DropTable(
                name: "MovieSubtitles");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Recommendations");

            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "SerieActors");

            migrationBuilder.DropTable(
                name: "SerieGenres");

            migrationBuilder.DropTable(
                name: "SerieSubtitles");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "SupportTickets");

            migrationBuilder.DropTable(
                name: "UserPreferences");

            migrationBuilder.DropTable(
                name: "UserStatistics");

            migrationBuilder.DropTable(
                name: "WatchRoomInvites");

            migrationBuilder.DropTable(
                name: "WatchRoomUsers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CustomLists");

            migrationBuilder.DropTable(
                name: "DownloadLists");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Subtitles");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "WatchRooms");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropTable(
                name: "Directors");

            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "Seasons");
        }
    }
}
