using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventUnion.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitIdentityAggregate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    address_id = table.Column<Guid>(type: "uuid", nullable: false),
                    zip_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    street = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    neighborhood = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    additional_info = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    state = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    city = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_address", x => x.address_id);
                });

            migrationBuilder.CreateTable(
                name: "EventType",
                columns: table => new
                {
                    event_type_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_event_type", x => x.event_type_id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    tag_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tag", x => x.tag_id);
                });

            migrationBuilder.CreateTable(
                name: "Target",
                columns: table => new
                {
                    target_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_target", x => x.target_id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    cript_key = table.Column<string>(type: "text", nullable: false),
                    iv = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    company_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    legal_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    trade_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    specialization = table.Column<string>(type: "character varying(90)", maxLength: 90, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.id);
                    table.ForeignKey(
                        name: "fk_company_company_id",
                        column: x => x.id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_company_user_id",
                        column: x => x.id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    event_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    @private = table.Column<bool>(name: "private", type: "boolean", nullable: false),
                    image = table.Column<string>(type: "text", nullable: false),
                    user_owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    event_type_id = table.Column<int>(type: "integer", nullable: false),
                    target_id = table.Column<int>(type: "integer", nullable: false),
                    address_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_event", x => x.event_id);
                    table.ForeignKey(
                        name: "fk_event_address_address_id",
                        column: x => x.address_id,
                        principalTable: "Address",
                        principalColumn: "address_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_event_event_type_event_type_id",
                        column: x => x.event_type_id,
                        principalTable: "EventType",
                        principalColumn: "event_type_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_event_target_target_id",
                        column: x => x.target_id,
                        principalTable: "Target",
                        principalColumn: "target_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_event_user_user_owner_id",
                        column: x => x.user_owner_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    person_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    birthdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.id);
                    table.ForeignKey(
                        name: "fk_person_person_id",
                        column: x => x.id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_person_user_id",
                        column: x => x.id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phone",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    phone_id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_phone", x => x.user_id);
                    table.ForeignKey(
                        name: "fk_phone_user_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAddress",
                columns: table => new
                {
                    user_address_id = table.Column<Guid>(type: "uuid", nullable: false),
                    address_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_address", x => x.user_address_id);
                    table.ForeignKey(
                        name: "fk_user_address_address_address_id",
                        column: x => x.address_id,
                        principalTable: "Address",
                        principalColumn: "address_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_address_user_user_address_id",
                        column: x => x.user_address_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTag",
                columns: table => new
                {
                    user_tag_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tag_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_tag", x => x.user_tag_id);
                    table.ForeignKey(
                        name: "fk_user_tag_tag_tag_id",
                        column: x => x.tag_id,
                        principalTable: "Tag",
                        principalColumn: "tag_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_tag_user_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventAddress",
                columns: table => new
                {
                    event_address_id = table.Column<Guid>(type: "uuid", nullable: false),
                    event_id = table.Column<Guid>(type: "uuid", nullable: false),
                    address_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_event_address", x => x.event_address_id);
                    table.ForeignKey(
                        name: "fk_event_address_address_address_id",
                        column: x => x.address_id,
                        principalTable: "Address",
                        principalColumn: "address_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_event_address_event_event_id",
                        column: x => x.event_id,
                        principalTable: "Event",
                        principalColumn: "event_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventTag",
                columns: table => new
                {
                    event_tag_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tag_id = table.Column<int>(type: "integer", nullable: false),
                    event_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_event_tag", x => x.event_tag_id);
                    table.ForeignKey(
                        name: "fk_event_tag_event_event_id",
                        column: x => x.event_id,
                        principalTable: "Event",
                        principalColumn: "event_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_event_tag_tag_tag_id",
                        column: x => x.tag_id,
                        principalTable: "Tag",
                        principalColumn: "tag_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventUser",
                columns: table => new
                {
                    event_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    event_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    invite_email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_event_user", x => x.event_user_id);
                    table.ForeignKey(
                        name: "fk_event_user_event_event_id",
                        column: x => x.event_id,
                        principalTable: "Event",
                        principalColumn: "event_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_event_user_user_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EventType",
                columns: new[] { "event_type_id", "name" },
                values: new object[,]
                {
                    { 1, "Festivo" },
                    { 2, "Esportivo" },
                    { 3, "Formal" }
                });

            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "tag_id", "name" },
                values: new object[,]
                {
                    { 1, "Programação" },
                    { 2, "Música" },
                    { 3, "Arte" },
                    { 4, "Esportes" },
                    { 5, "Tecnologia" },
                    { 6, "Educação" },
                    { 7, "Saúde" },
                    { 8, "Negócios" },
                    { 9, "Viagem" },
                    { 10, "Culinária" },
                    { 11, "Ciência" },
                    { 12, "Meio Ambiente" },
                    { 13, "Moda" },
                    { 14, "Literatura" },
                    { 15, "Jogos" },
                    { 16, "Cinema" },
                    { 17, "Fotografia" },
                    { 18, "História" },
                    { 19, "Caridade" },
                    { 20, "Networking" },
                    { 21, "Workshops" },
                    { 22, "Conferências" },
                    { 23, "Webinars" },
                    { 24, "Meetups" },
                    { 25, "Festivais" },
                    { 26, "Competições" }
                });

            migrationBuilder.InsertData(
                table: "Target",
                columns: new[] { "target_id", "name" },
                values: new object[,]
                {
                    { 1, "Infantil" },
                    { 2, "Jovem" },
                    { 3, "Jovem Adulto" },
                    { 4, "Adulto" },
                    { 5, "Idoso" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_event_address_id",
                table: "Event",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "ix_event_event_type_id",
                table: "Event",
                column: "event_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_event_target_id",
                table: "Event",
                column: "target_id");

            migrationBuilder.CreateIndex(
                name: "ix_event_user_owner_id",
                table: "Event",
                column: "user_owner_id");

            migrationBuilder.CreateIndex(
                name: "ix_event_address_address_id",
                table: "EventAddress",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "ix_event_address_event_id",
                table: "EventAddress",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "ix_event_tag_event_id",
                table: "EventTag",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "ix_event_tag_tag_id",
                table: "EventTag",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "ix_event_user_event_id",
                table: "EventUser",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "ix_event_user_user_id",
                table: "EventUser",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_address_address_id",
                table: "UserAddress",
                column: "address_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_tag_tag_id",
                table: "UserTag",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_tag_user_id",
                table: "UserTag",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "EventAddress");

            migrationBuilder.DropTable(
                name: "EventTag");

            migrationBuilder.DropTable(
                name: "EventUser");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Phone");

            migrationBuilder.DropTable(
                name: "UserAddress");

            migrationBuilder.DropTable(
                name: "UserTag");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "EventType");

            migrationBuilder.DropTable(
                name: "Target");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
