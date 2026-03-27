using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dogadjaji",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumOd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumDo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dogadjaji", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Agende",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agende", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agende_Dogadjaji_Id",
                        column: x => x.Id,
                        principalTable: "Dogadjaji",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Delovi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tip = table.Column<int>(type: "int", nullable: true),
                    AgendaId = table.Column<int>(type: "int", nullable: true),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VremeOd = table.Column<TimeSpan>(type: "time", nullable: false),
                    VremeDo = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delovi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Delovi_Agende_AgendaId",
                        column: x => x.AgendaId,
                        principalTable: "Agende",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mejl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojTelefona = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    BrojIzvrsenihZahteva = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Clan_DogadjajId = table.Column<int>(type: "int", nullable: true),
                    Tip = table.Column<int>(type: "int", nullable: true),
                    DogadjajId = table.Column<int>(type: "int", nullable: true),
                    AgendaId = table.Column<int>(type: "int", nullable: true),
                    Komitet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alerigije = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ishrana = table.Column<int>(type: "int", nullable: true),
                    VremeDolaska = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VremeOdlaska = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Participant_DogadjajId = table.Column<int>(type: "int", nullable: true),
                    Predavac_Komitet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Predavac_Alerigije = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Predavac_Ishrana = table.Column<int>(type: "int", nullable: true),
                    Predavac_VremeDolaska = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Predavac_VremeOdlaska = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Predavac_DogadjajId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Korisnici_Agende_AgendaId",
                        column: x => x.AgendaId,
                        principalTable: "Agende",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Korisnici_Dogadjaji_Clan_DogadjajId",
                        column: x => x.Clan_DogadjajId,
                        principalTable: "Dogadjaji",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Korisnici_Dogadjaji_DogadjajId",
                        column: x => x.DogadjajId,
                        principalTable: "Dogadjaji",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Korisnici_Dogadjaji_Participant_DogadjajId",
                        column: x => x.Participant_DogadjajId,
                        principalTable: "Dogadjaji",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Korisnici_Dogadjaji_Predavac_DogadjajId",
                        column: x => x.Predavac_DogadjajId,
                        principalTable: "Dogadjaji",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: true),
                    ReciverId = table.Column<int>(type: "int", nullable: true),
                    DogadjajId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Dogadjaji_DogadjajId",
                        column: x => x.DogadjajId,
                        principalTable: "Dogadjaji",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatMessages_Korisnici_ReciverId",
                        column: x => x.ReciverId,
                        principalTable: "Korisnici",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatMessages_Korisnici_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Korisnici",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notifikacije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimalacId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tip = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Read = table.Column<bool>(type: "bit", nullable: true),
                    KoordinatorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifikacije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifikacije_Korisnici_KoordinatorId",
                        column: x => x.KoordinatorId,
                        principalTable: "Korisnici",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifikacije_Korisnici_PrimalacId",
                        column: x => x.PrimalacId,
                        principalTable: "Korisnici",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sesije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeSesije = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    VremePocetka = table.Column<TimeSpan>(type: "time", nullable: true),
                    VremeKraja = table.Column<TimeSpan>(type: "time", nullable: true),
                    PredavacId = table.Column<int>(type: "int", nullable: true),
                    DeoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sesije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sesije_Delovi_DeoId",
                        column: x => x.DeoId,
                        principalTable: "Delovi",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sesije_Korisnici_PredavacId",
                        column: x => x.PredavacId,
                        principalTable: "Korisnici",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TabeleDostupnosti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VremeOd = table.Column<TimeSpan>(type: "time", nullable: false),
                    VremeDo = table.Column<TimeSpan>(type: "time", nullable: false),
                    JeDostupan = table.Column<int>(type: "int", nullable: false),
                    ClanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabeleDostupnosti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TabeleDostupnosti_Korisnici_ClanId",
                        column: x => x.ClanId,
                        principalTable: "Korisnici",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Zahtevi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PosiljalacId = table.Column<int>(type: "int", nullable: true),
                    KoordinatorId = table.Column<int>(type: "int", nullable: true),
                    StatusZahteva = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DogadjajId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zahtevi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zahtevi_Dogadjaji_DogadjajId",
                        column: x => x.DogadjajId,
                        principalTable: "Dogadjaji",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Zahtevi_Korisnici_KoordinatorId",
                        column: x => x.KoordinatorId,
                        principalTable: "Korisnici",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Zahtevi_Korisnici_PosiljalacId",
                        column: x => x.PosiljalacId,
                        principalTable: "Korisnici",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClanZahtev",
                columns: table => new
                {
                    ZaduzeniClanId = table.Column<int>(type: "int", nullable: false),
                    ZahteviId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClanZahtev", x => new { x.ZaduzeniClanId, x.ZahteviId });
                    table.ForeignKey(
                        name: "FK_ClanZahtev_Korisnici_ZaduzeniClanId",
                        column: x => x.ZaduzeniClanId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClanZahtev_Zahtevi_ZahteviId",
                        column: x => x.ZahteviId,
                        principalTable: "Zahtevi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_DogadjajId",
                table: "ChatMessages",
                column: "DogadjajId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ReciverId",
                table: "ChatMessages",
                column: "ReciverId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_SenderId",
                table: "ChatMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ClanZahtev_ZahteviId",
                table: "ClanZahtev",
                column: "ZahteviId");

            migrationBuilder.CreateIndex(
                name: "IX_Delovi_AgendaId",
                table: "Delovi",
                column: "AgendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_AgendaId",
                table: "Korisnici",
                column: "AgendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_Clan_DogadjajId",
                table: "Korisnici",
                column: "Clan_DogadjajId");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_DogadjajId",
                table: "Korisnici",
                column: "DogadjajId");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_Participant_DogadjajId",
                table: "Korisnici",
                column: "Participant_DogadjajId");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_Predavac_DogadjajId",
                table: "Korisnici",
                column: "Predavac_DogadjajId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifikacije_KoordinatorId",
                table: "Notifikacije",
                column: "KoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifikacije_PrimalacId",
                table: "Notifikacije",
                column: "PrimalacId");

            migrationBuilder.CreateIndex(
                name: "IX_Sesije_DeoId",
                table: "Sesije",
                column: "DeoId");

            migrationBuilder.CreateIndex(
                name: "IX_Sesije_PredavacId",
                table: "Sesije",
                column: "PredavacId");

            migrationBuilder.CreateIndex(
                name: "IX_TabeleDostupnosti_ClanId",
                table: "TabeleDostupnosti",
                column: "ClanId");

            migrationBuilder.CreateIndex(
                name: "IX_Zahtevi_DogadjajId",
                table: "Zahtevi",
                column: "DogadjajId");

            migrationBuilder.CreateIndex(
                name: "IX_Zahtevi_KoordinatorId",
                table: "Zahtevi",
                column: "KoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Zahtevi_PosiljalacId",
                table: "Zahtevi",
                column: "PosiljalacId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "ClanZahtev");

            migrationBuilder.DropTable(
                name: "Notifikacije");

            migrationBuilder.DropTable(
                name: "Sesije");

            migrationBuilder.DropTable(
                name: "TabeleDostupnosti");

            migrationBuilder.DropTable(
                name: "Zahtevi");

            migrationBuilder.DropTable(
                name: "Delovi");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "Agende");

            migrationBuilder.DropTable(
                name: "Dogadjaji");
        }
    }
}
