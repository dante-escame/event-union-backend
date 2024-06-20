﻿// <auto-generated />
using System;
using Event.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Event.Api.Migrations
{
    [DbContext(typeof(EventDbContext))]
    partial class EventDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Event.Api.Entities.Address", b =>
                {
                    b.Property<Guid>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("address_id");

                    b.Property<string>("AdditionalInfo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("additional_info");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("country");

                    b.Property<string>("Neighborhood")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("neighborhood");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("number");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("state");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("street");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("zip_code");

                    b.HasKey("AddressId")
                        .HasName("pk_address");

                    b.ToTable("address", (string)null);
                });

            modelBuilder.Entity("Event.Api.Entities.Company", b =>
                {
                    b.Property<Guid>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("company_id");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("cnpj");

                    b.Property<string>("LegalName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("legal_name");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("specialization");

                    b.Property<string>("TradeName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("trade_name");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("CompanyId")
                        .HasName("pk_company");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_company_user_id");

                    b.ToTable("company", (string)null);
                });

            modelBuilder.Entity("Event.Api.Entities.Event", b =>
                {
                    b.Property<Guid>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("event_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("end_date");

                    b.Property<int>("EventTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("event_type_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("start_date");

                    b.Property<int>("TargetId")
                        .HasColumnType("integer")
                        .HasColumnName("target_id");

                    b.Property<Guid>("UserOwnerId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_owner_id");

                    b.HasKey("EventId")
                        .HasName("pk_event");

                    b.HasIndex("EventTypeId")
                        .HasDatabaseName("ix_event_event_type_id");

                    b.HasIndex("TargetId")
                        .HasDatabaseName("ix_event_target_id");

                    b.ToTable("event", (string)null);
                });

            modelBuilder.Entity("Event.Api.Entities.EventParameter", b =>
                {
                    b.Property<int>("EventParameterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("event_parameter_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("EventParameterId"));

                    b.Property<bool>("Active")
                        .HasColumnType("boolean")
                        .HasColumnName("active");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid")
                        .HasColumnName("event_id");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("key");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("EventParameterId")
                        .HasName("pk_event_parameter");

                    b.HasIndex("EventId")
                        .HasDatabaseName("ix_event_parameter_event_id");

                    b.ToTable("event_parameter", (string)null);
                });

            modelBuilder.Entity("Event.Api.Entities.EventParticipant", b =>
                {
                    b.Property<int>("EventParticipantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("event_participant_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("EventParticipantId"));

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid")
                        .HasColumnName("event_id");

                    b.Property<string>("InviteEmail")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("invite_email");

                    b.Property<Guid>("ParticipantId")
                        .HasColumnType("uuid")
                        .HasColumnName("participant_id");

                    b.HasKey("EventParticipantId")
                        .HasName("pk_event_participant");

                    b.HasIndex("EventId")
                        .HasDatabaseName("ix_event_participant_event_id");

                    b.HasIndex("ParticipantId")
                        .HasDatabaseName("ix_event_participant_participant_id");

                    b.ToTable("event_participant", (string)null);
                });

            modelBuilder.Entity("Event.Api.Entities.EventSpace", b =>
                {
                    b.Property<int>("EventSpaceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("event_space_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("EventSpaceId"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("end_date");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid")
                        .HasColumnName("event_id");

                    b.Property<int>("PlaceId")
                        .HasColumnType("integer")
                        .HasColumnName("place_id");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("start_date");

                    b.HasKey("EventSpaceId")
                        .HasName("pk_event_space");

                    b.HasIndex("EventId")
                        .HasDatabaseName("ix_event_space_event_id");

                    b.HasIndex("PlaceId")
                        .HasDatabaseName("ix_event_space_place_id");

                    b.ToTable("event_space", (string)null);
                });

            modelBuilder.Entity("Event.Api.Entities.EventType", b =>
                {
                    b.Property<int>("EventTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("event_type_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("EventTypeId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.HasKey("EventTypeId")
                        .HasName("pk_event_type");

                    b.ToTable("event_type", (string)null);
                });

            modelBuilder.Entity("Event.Api.Entities.Interest", b =>
                {
                    b.Property<int>("InterestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("interest_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("InterestId"));

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("InterestId", "UserId")
                        .HasName("pk_interest");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_interest_user_id");

                    b.ToTable("interest", (string)null);
                });

            modelBuilder.Entity("Event.Api.Entities.Participant", b =>
                {
                    b.Property<Guid>("ParticipantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("participant_id");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("birthdate");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("cpf");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("ParticipantId")
                        .HasName("pk_participant");

                    b.ToTable("participant", (string)null);
                });

            modelBuilder.Entity("Event.Api.Entities.Person", b =>
                {
                    b.Property<Guid>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("person_id");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("birthdate");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("cpf");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("PersonId")
                        .HasName("pk_people");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_people_user_id");

                    b.ToTable("people", (string)null);
                });

            modelBuilder.Entity("Event.Api.Entities.Phone", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("UserId")
                        .HasName("pk_phone");

                    b.ToTable("phone", (string)null);
                });

            modelBuilder.Entity("Event.Api.Entities.Place", b =>
                {
                    b.Property<int>("PlaceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("place_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PlaceId"));

                    b.Property<int>("Capacity")
                        .HasColumnType("integer")
                        .HasColumnName("capacity");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("PlaceId")
                        .HasName("pk_place");

                    b.ToTable("place", (string)null);
                });

            modelBuilder.Entity("Event.Api.Entities.PlaceAddress", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("address_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AddressId"));

                    b.Property<int>("PlaceId")
                        .HasColumnType("integer")
                        .HasColumnName("place_id");

                    b.HasKey("AddressId")
                        .HasName("pk_place_address");

                    b.HasIndex("PlaceId")
                        .IsUnique()
                        .HasDatabaseName("ix_place_address_place_id");

                    b.ToTable("place_address", (string)null);
                });

            modelBuilder.Entity("Event.Api.Entities.Sponsor", b =>
                {
                    b.Property<int>("SponsorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("sponsor_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SponsorId"));

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid")
                        .HasColumnName("event_id");

                    b.HasKey("SponsorId")
                        .HasName("pk_sponsor");

                    b.HasIndex("EventId")
                        .HasDatabaseName("ix_sponsor_event_id");

                    b.ToTable("sponsor", (string)null);
                });

            modelBuilder.Entity("Event.Api.Entities.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("tag_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TagId"));

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid")
                        .HasColumnName("event_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("TagId")
                        .HasName("pk_tag");

                    b.HasIndex("EventId")
                        .HasDatabaseName("ix_tag_event_id");

                    b.ToTable("tag", (string)null);
                });

            modelBuilder.Entity("Event.Api.Entities.Target", b =>
                {
                    b.Property<int>("TargetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("target_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TargetId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.HasKey("TargetId")
                        .HasName("pk_target");

                    b.ToTable("target", (string)null);
                });

            modelBuilder.Entity("Event.Api.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<string>("CryptKey")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("crypt_key");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.HasKey("UserId")
                        .HasName("pk_user");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("Event.Api.Entities.UserAddress", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uuid")
                        .HasColumnName("address_id");

                    b.HasKey("UserId", "AddressId")
                        .HasName("pk_user_address");

                    b.HasIndex("AddressId")
                        .IsUnique()
                        .HasDatabaseName("ix_user_address_address_id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_user_address_user_id");

                    b.ToTable("user_address", (string)null);
                });

            modelBuilder.Entity("Event.Api.Entities.Company", b =>
                {
                    b.HasOne("Event.Api.Entities.User", "User")
                        .WithOne("Company")
                        .HasForeignKey("Event.Api.Entities.Company", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_company_user_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Event.Api.Entities.Event", b =>
                {
                    b.HasOne("Event.Api.Entities.EventType", "EventType")
                        .WithMany()
                        .HasForeignKey("EventTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_event_type_event_type_id");

                    b.HasOne("Event.Api.Entities.Target", "Target")
                        .WithMany()
                        .HasForeignKey("TargetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_target_target_id");

                    b.Navigation("EventType");

                    b.Navigation("Target");
                });

            modelBuilder.Entity("Event.Api.Entities.EventParameter", b =>
                {
                    b.HasOne("Event.Api.Entities.Event", "Event")
                        .WithMany("EventParameters")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_parameter_event_event_id");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Event.Api.Entities.EventParticipant", b =>
                {
                    b.HasOne("Event.Api.Entities.Event", "Event")
                        .WithMany("EventParticipants")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_participant_event_event_id");

                    b.HasOne("Event.Api.Entities.Participant", "Participant")
                        .WithMany()
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_participant_participant_participant_id");

                    b.Navigation("Event");

                    b.Navigation("Participant");
                });

            modelBuilder.Entity("Event.Api.Entities.EventSpace", b =>
                {
                    b.HasOne("Event.Api.Entities.Event", "Event")
                        .WithMany("EventSpaces")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_space_event_event_id");

                    b.HasOne("Event.Api.Entities.Place", "Place")
                        .WithMany()
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_space_place_place_id");

                    b.Navigation("Event");

                    b.Navigation("Place");
                });

            modelBuilder.Entity("Event.Api.Entities.Interest", b =>
                {
                    b.HasOne("Event.Api.Entities.User", "User")
                        .WithMany("Interests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_interest_user_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Event.Api.Entities.Person", b =>
                {
                    b.HasOne("Event.Api.Entities.User", "User")
                        .WithOne("Person")
                        .HasForeignKey("Event.Api.Entities.Person", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_people_user_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Event.Api.Entities.Phone", b =>
                {
                    b.HasOne("Event.Api.Entities.User", "User")
                        .WithOne("Phone")
                        .HasForeignKey("Event.Api.Entities.Phone", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_phone_user_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Event.Api.Entities.PlaceAddress", b =>
                {
                    b.HasOne("Event.Api.Entities.Place", "Place")
                        .WithOne("PlaceAddress")
                        .HasForeignKey("Event.Api.Entities.PlaceAddress", "PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_place_address_place_place_id");

                    b.Navigation("Place");
                });

            modelBuilder.Entity("Event.Api.Entities.Sponsor", b =>
                {
                    b.HasOne("Event.Api.Entities.Event", "Event")
                        .WithMany("Sponsors")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_sponsor_event_event_id");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Event.Api.Entities.Tag", b =>
                {
                    b.HasOne("Event.Api.Entities.Event", "Event")
                        .WithMany("Tags")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tag_event_event_id");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Event.Api.Entities.UserAddress", b =>
                {
                    b.HasOne("Event.Api.Entities.Address", "Address")
                        .WithOne("UserAddress")
                        .HasForeignKey("Event.Api.Entities.UserAddress", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_address_address_address_id");

                    b.HasOne("Event.Api.Entities.User", "User")
                        .WithOne("UserAddress")
                        .HasForeignKey("Event.Api.Entities.UserAddress", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_address_user_user_id");

                    b.Navigation("Address");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Event.Api.Entities.Address", b =>
                {
                    b.Navigation("UserAddress")
                        .IsRequired();
                });

            modelBuilder.Entity("Event.Api.Entities.Event", b =>
                {
                    b.Navigation("EventParameters");

                    b.Navigation("EventParticipants");

                    b.Navigation("EventSpaces");

                    b.Navigation("Sponsors");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("Event.Api.Entities.Place", b =>
                {
                    b.Navigation("PlaceAddress")
                        .IsRequired();
                });

            modelBuilder.Entity("Event.Api.Entities.User", b =>
                {
                    b.Navigation("Company")
                        .IsRequired();

                    b.Navigation("Interests");

                    b.Navigation("Person")
                        .IsRequired();

                    b.Navigation("Phone")
                        .IsRequired();

                    b.Navigation("UserAddress")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
