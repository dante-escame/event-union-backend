﻿// <auto-generated />
using System;
using EventUnion.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EventUnion.Infrastructure.Migrations
{
    [DbContext(typeof(EventUnionDbContext))]
    [Migration("20241118215947_InitIdentityAggregate")]
    partial class InitIdentityAggregate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EventUnion.Domain.Addresses.Address", b =>
                {
                    b.Property<Guid>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("address_id");

                    b.Property<string>("AdditionalInfo")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("additional_info");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("country");

                    b.Property<string>("Neighborhood")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("neighborhood");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("number");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("character varying(2)")
                        .HasColumnName("state");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("street");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("zip_code");

                    b.HasKey("AddressId")
                        .HasName("pk_address");

                    b.ToTable("address", (string)null);
                });

            modelBuilder.Entity("EventUnion.Domain.Addresses.Phone", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid>("PhoneId")
                        .HasColumnType("uuid")
                        .HasColumnName("phone_id");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("value");

                    b.HasKey("UserId")
                        .HasName("pk_phone");

                    b.ToTable("phone", (string)null);
                });

            modelBuilder.Entity("EventUnion.Domain.Events.Event", b =>
                {
                    b.Property<Guid>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("event_id");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uuid")
                        .HasColumnName("address_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("EventTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("event_type_id");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("image");

                    b.Property<bool>("Private")
                        .HasColumnType("boolean")
                        .HasColumnName("private");

                    b.Property<int>("TargetId")
                        .HasColumnType("integer")
                        .HasColumnName("target_id");

                    b.Property<Guid>("UserOwnerId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_owner_id");

                    b.HasKey("EventId")
                        .HasName("pk_event");

                    b.HasIndex("AddressId")
                        .HasDatabaseName("ix_event_address_id");

                    b.HasIndex("EventTypeId")
                        .HasDatabaseName("ix_event_event_type_id");

                    b.HasIndex("TargetId")
                        .HasDatabaseName("ix_event_target_id");

                    b.HasIndex("UserOwnerId")
                        .HasDatabaseName("ix_event_user_owner_id");

                    b.ToTable("event", (string)null);
                });

            modelBuilder.Entity("EventUnion.Domain.Events.EventAddress", b =>
                {
                    b.Property<Guid>("EventAddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("event_address_id");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uuid")
                        .HasColumnName("address_id");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid")
                        .HasColumnName("event_id");

                    b.HasKey("EventAddressId")
                        .HasName("pk_event_address");

                    b.HasIndex("AddressId")
                        .HasDatabaseName("ix_event_address_address_id");

                    b.HasIndex("EventId")
                        .HasDatabaseName("ix_event_address_event_id");

                    b.ToTable("event_address", (string)null);
                });

            modelBuilder.Entity("EventUnion.Domain.Events.EventTag", b =>
                {
                    b.Property<Guid>("EventTagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("event_tag_id");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid")
                        .HasColumnName("event_id");

                    b.Property<int>("TagId")
                        .HasColumnType("integer")
                        .HasColumnName("tag_id");

                    b.HasKey("EventTagId")
                        .HasName("pk_event_tag");

                    b.HasIndex("EventId")
                        .HasDatabaseName("ix_event_tag_event_id");

                    b.HasIndex("TagId")
                        .HasDatabaseName("ix_event_tag_tag_id");

                    b.ToTable("event_tag", (string)null);
                });

            modelBuilder.Entity("EventUnion.Domain.Events.EventType", b =>
                {
                    b.Property<int>("EventTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("event_type_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("EventTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("EventTypeId")
                        .HasName("pk_event_type");

                    b.ToTable("event_type", (string)null);

                    b.HasData(
                        new
                        {
                            EventTypeId = 1,
                            Name = "Festivo"
                        },
                        new
                        {
                            EventTypeId = 2,
                            Name = "Esportivo"
                        },
                        new
                        {
                            EventTypeId = 3,
                            Name = "Formal"
                        });
                });

            modelBuilder.Entity("EventUnion.Domain.Events.EventUser", b =>
                {
                    b.Property<Guid>("EventUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("event_user_id");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid")
                        .HasColumnName("event_id");

                    b.Property<string>("InviteEmail")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("invite_email");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("EventUserId")
                        .HasName("pk_event_user");

                    b.HasIndex("EventId")
                        .HasDatabaseName("ix_event_user_event_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_event_user_user_id");

                    b.ToTable("event_user", (string)null);
                });

            modelBuilder.Entity("EventUnion.Domain.Events.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("tag_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TagId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.HasKey("TagId")
                        .HasName("pk_tag");

                    b.ToTable("tag", (string)null);

                    b.HasData(
                        new
                        {
                            TagId = 1,
                            Name = "Programação"
                        },
                        new
                        {
                            TagId = 2,
                            Name = "Música"
                        },
                        new
                        {
                            TagId = 3,
                            Name = "Arte"
                        },
                        new
                        {
                            TagId = 4,
                            Name = "Esportes"
                        },
                        new
                        {
                            TagId = 5,
                            Name = "Tecnologia"
                        },
                        new
                        {
                            TagId = 6,
                            Name = "Educação"
                        },
                        new
                        {
                            TagId = 7,
                            Name = "Saúde"
                        },
                        new
                        {
                            TagId = 8,
                            Name = "Negócios"
                        },
                        new
                        {
                            TagId = 9,
                            Name = "Viagem"
                        },
                        new
                        {
                            TagId = 10,
                            Name = "Culinária"
                        },
                        new
                        {
                            TagId = 11,
                            Name = "Ciência"
                        },
                        new
                        {
                            TagId = 12,
                            Name = "Meio Ambiente"
                        },
                        new
                        {
                            TagId = 13,
                            Name = "Moda"
                        },
                        new
                        {
                            TagId = 14,
                            Name = "Literatura"
                        },
                        new
                        {
                            TagId = 15,
                            Name = "Jogos"
                        },
                        new
                        {
                            TagId = 16,
                            Name = "Cinema"
                        },
                        new
                        {
                            TagId = 17,
                            Name = "Fotografia"
                        },
                        new
                        {
                            TagId = 18,
                            Name = "História"
                        },
                        new
                        {
                            TagId = 19,
                            Name = "Caridade"
                        },
                        new
                        {
                            TagId = 20,
                            Name = "Networking"
                        },
                        new
                        {
                            TagId = 21,
                            Name = "Workshops"
                        },
                        new
                        {
                            TagId = 22,
                            Name = "Conferências"
                        },
                        new
                        {
                            TagId = 23,
                            Name = "Webinars"
                        },
                        new
                        {
                            TagId = 24,
                            Name = "Meetups"
                        },
                        new
                        {
                            TagId = 25,
                            Name = "Festivais"
                        },
                        new
                        {
                            TagId = 26,
                            Name = "Competições"
                        });
                });

            modelBuilder.Entity("EventUnion.Domain.Events.Target", b =>
                {
                    b.Property<int>("TargetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("target_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TargetId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("TargetId")
                        .HasName("pk_target");

                    b.ToTable("target", (string)null);

                    b.HasData(
                        new
                        {
                            TargetId = 1,
                            Name = "Infantil"
                        },
                        new
                        {
                            TargetId = 2,
                            Name = "Jovem"
                        },
                        new
                        {
                            TargetId = 3,
                            Name = "Jovem Adulto"
                        },
                        new
                        {
                            TargetId = 4,
                            Name = "Adulto"
                        },
                        new
                        {
                            TargetId = 5,
                            Name = "Idoso"
                        });
                });

            modelBuilder.Entity("EventUnion.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("CriptKey")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("cript_key");

                    b.Property<string>("Iv")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("iv");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("user", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("EventUnion.Domain.Users.UserAddress", b =>
                {
                    b.Property<Guid>("UserAddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("user_address_id");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uuid")
                        .HasColumnName("address_id");

                    b.HasKey("UserAddressId")
                        .HasName("pk_user_address");

                    b.HasIndex("AddressId")
                        .IsUnique()
                        .HasDatabaseName("ix_user_address_address_id");

                    b.ToTable("user_address", (string)null);
                });

            modelBuilder.Entity("EventUnion.Domain.Users.UserTag", b =>
                {
                    b.Property<Guid>("UserTagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("user_tag_id");

                    b.Property<int>("TagId")
                        .HasColumnType("integer")
                        .HasColumnName("tag_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("UserTagId")
                        .HasName("pk_user_tag");

                    b.HasIndex("TagId")
                        .HasDatabaseName("ix_user_tag_tag_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_tag_user_id");

                    b.ToTable("user_tag", (string)null);
                });

            modelBuilder.Entity("EventUnion.Domain.Users.Company", b =>
                {
                    b.HasBaseType("EventUnion.Domain.Users.User");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid")
                        .HasColumnName("company_id");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasMaxLength(90)
                        .HasColumnType("character varying(90)")
                        .HasColumnName("specialization");

                    b.ToTable("company", (string)null);
                });

            modelBuilder.Entity("EventUnion.Domain.Users.Person", b =>
                {
                    b.HasBaseType("EventUnion.Domain.Users.User");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uuid")
                        .HasColumnName("person_id");

                    b.ToTable("person", (string)null);
                });

            modelBuilder.Entity("EventUnion.Domain.Addresses.Phone", b =>
                {
                    b.HasOne("EventUnion.Domain.Users.User", "User")
                        .WithOne()
                        .HasForeignKey("EventUnion.Domain.Addresses.Phone", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_phone_user_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EventUnion.Domain.Events.Event", b =>
                {
                    b.HasOne("EventUnion.Domain.Addresses.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_address_address_id");

                    b.HasOne("EventUnion.Domain.Events.EventType", "EventType")
                        .WithMany()
                        .HasForeignKey("EventTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_event_type_event_type_id");

                    b.HasOne("EventUnion.Domain.Events.Target", "Target")
                        .WithMany()
                        .HasForeignKey("TargetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_target_target_id");

                    b.HasOne("EventUnion.Domain.Users.User", "UserOwner")
                        .WithMany()
                        .HasForeignKey("UserOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_user_user_owner_id");

                    b.OwnsOne("EventUnion.Domain.ValueObjects.FullName", "Name", b1 =>
                        {
                            b1.Property<Guid>("EventId")
                                .HasColumnType("uuid")
                                .HasColumnName("event_id");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("character varying(256)")
                                .HasColumnName("name");

                            b1.HasKey("EventId");

                            b1.ToTable("event");

                            b1.WithOwner()
                                .HasForeignKey("EventId")
                                .HasConstraintName("fk_event_event_event_id");
                        });

                    b.OwnsOne("EventUnion.Domain.ValueObjects.Period", "Period", b1 =>
                        {
                            b1.Property<Guid>("EventId")
                                .HasColumnType("uuid")
                                .HasColumnName("event_id");

                            b1.Property<DateTime>("EndDate")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("end_date");

                            b1.Property<DateTime>("StartDate")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("start_date");

                            b1.HasKey("EventId");

                            b1.ToTable("event");

                            b1.WithOwner()
                                .HasForeignKey("EventId")
                                .HasConstraintName("fk_event_event_event_id");
                        });

                    b.Navigation("Address");

                    b.Navigation("EventType");

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("Period")
                        .IsRequired();

                    b.Navigation("Target");

                    b.Navigation("UserOwner");
                });

            modelBuilder.Entity("EventUnion.Domain.Events.EventAddress", b =>
                {
                    b.HasOne("EventUnion.Domain.Addresses.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_address_address_address_id");

                    b.HasOne("EventUnion.Domain.Events.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_address_event_event_id");

                    b.Navigation("Address");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("EventUnion.Domain.Events.EventTag", b =>
                {
                    b.HasOne("EventUnion.Domain.Events.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_tag_event_event_id");

                    b.HasOne("EventUnion.Domain.Events.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_tag_tag_tag_id");

                    b.Navigation("Event");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("EventUnion.Domain.Events.EventUser", b =>
                {
                    b.HasOne("EventUnion.Domain.Events.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_user_event_event_id");

                    b.HasOne("EventUnion.Domain.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_user_user_user_id");

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EventUnion.Domain.Users.User", b =>
                {
                    b.OwnsOne("EventUnion.Domain.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(128)
                                .HasColumnType("character varying(128)")
                                .HasColumnName("email");

                            b1.HasKey("UserId");

                            b1.ToTable("user");

                            b1.WithOwner()
                                .HasForeignKey("UserId")
                                .HasConstraintName("fk_user_user_id");
                        });

                    b.Navigation("Email")
                        .IsRequired();
                });

            modelBuilder.Entity("EventUnion.Domain.Users.UserAddress", b =>
                {
                    b.HasOne("EventUnion.Domain.Addresses.Address", "Address")
                        .WithOne()
                        .HasForeignKey("EventUnion.Domain.Users.UserAddress", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_address_address_address_id");

                    b.HasOne("EventUnion.Domain.Users.User", "User")
                        .WithOne()
                        .HasForeignKey("EventUnion.Domain.Users.UserAddress", "UserAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_address_user_user_address_id");

                    b.Navigation("Address");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EventUnion.Domain.Users.UserTag", b =>
                {
                    b.HasOne("EventUnion.Domain.Events.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_tag_tag_tag_id");

                    b.HasOne("EventUnion.Domain.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_tag_user_user_id");

                    b.Navigation("Tag");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EventUnion.Domain.Users.Company", b =>
                {
                    b.HasOne("EventUnion.Domain.Users.User", null)
                        .WithOne()
                        .HasForeignKey("EventUnion.Domain.Users.Company", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_company_user_id");

                    b.OwnsOne("EventUnion.Domain.ValueObjects.FullName", "LegalName", b1 =>
                        {
                            b1.Property<Guid>("CompanyId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("character varying(256)")
                                .HasColumnName("legal_name");

                            b1.HasKey("CompanyId");

                            b1.ToTable("company");

                            b1.WithOwner()
                                .HasForeignKey("CompanyId")
                                .HasConstraintName("fk_company_company_id");
                        });

                    b.OwnsOne("EventUnion.Domain.ValueObjects.FullName", "TradeName", b1 =>
                        {
                            b1.Property<Guid>("CompanyId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("character varying(256)")
                                .HasColumnName("trade_name");

                            b1.HasKey("CompanyId");

                            b1.ToTable("company");

                            b1.WithOwner()
                                .HasForeignKey("CompanyId")
                                .HasConstraintName("fk_company_company_id");
                        });

                    b.OwnsOne("EventUnion.Domain.ValueObjects.Cnpj", "Cnpj", b1 =>
                        {
                            b1.Property<Guid>("CompanyId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(14)
                                .HasColumnType("character varying(14)")
                                .HasColumnName("cnpj");

                            b1.HasKey("CompanyId");

                            b1.ToTable("company");

                            b1.WithOwner()
                                .HasForeignKey("CompanyId")
                                .HasConstraintName("fk_company_company_id");
                        });

                    b.Navigation("Cnpj")
                        .IsRequired();

                    b.Navigation("LegalName")
                        .IsRequired();

                    b.Navigation("TradeName")
                        .IsRequired();
                });

            modelBuilder.Entity("EventUnion.Domain.Users.Person", b =>
                {
                    b.HasOne("EventUnion.Domain.Users.User", null)
                        .WithOne()
                        .HasForeignKey("EventUnion.Domain.Users.Person", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_person_user_id");

                    b.OwnsOne("EventUnion.Domain.ValueObjects.FullName", "Name", b1 =>
                        {
                            b1.Property<Guid>("PersonId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("character varying(256)")
                                .HasColumnName("name");

                            b1.HasKey("PersonId");

                            b1.ToTable("person");

                            b1.WithOwner()
                                .HasForeignKey("PersonId")
                                .HasConstraintName("fk_person_person_id");
                        });

                    b.OwnsOne("EventUnion.Domain.ValueObjects.Birthdate", "Birthdate", b1 =>
                        {
                            b1.Property<Guid>("PersonId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<DateTime>("Value")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("birthdate");

                            b1.HasKey("PersonId");

                            b1.ToTable("person");

                            b1.WithOwner()
                                .HasForeignKey("PersonId")
                                .HasConstraintName("fk_person_person_id");
                        });

                    b.OwnsOne("EventUnion.Domain.ValueObjects.Cpf", "Cpf", b1 =>
                        {
                            b1.Property<Guid>("PersonId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(11)
                                .HasColumnType("character varying(11)")
                                .HasColumnName("cpf");

                            b1.HasKey("PersonId");

                            b1.ToTable("person");

                            b1.WithOwner()
                                .HasForeignKey("PersonId")
                                .HasConstraintName("fk_person_person_id");
                        });

                    b.Navigation("Birthdate")
                        .IsRequired();

                    b.Navigation("Cpf")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}