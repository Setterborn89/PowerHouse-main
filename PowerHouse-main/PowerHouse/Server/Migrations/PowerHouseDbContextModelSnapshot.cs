﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PowerHouse.Server.Data;

#nullable disable

namespace PowerHouse.Server.Migrations
{
    [DbContext(typeof(PowerHouseDbContext))]
    partial class PowerHouseDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ConversationUser", b =>
                {
                    b.Property<Guid>("ConversationsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ConversationsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("ConversationUser");
                });

            modelBuilder.Entity("PowerHouse.Server.Models.Conversation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Conversation");

                    b.HasData(
                        new
                        {
                            Id = new Guid("01f424df-72cd-4dd0-a0a5-929ddbbda931"),
                            AuthorId = new Guid("b1873f2f-81f0-4683-aa7f-33c3e9845df2"),
                            IsBlocked = false,
                            IsPublic = true,
                            Topic = "Exploring the World of Microorganisms: The Hidden Life in Your Backyard"
                        },
                        new
                        {
                            Id = new Guid("a1d66b82-3313-485e-b464-79ad3bd1f84e"),
                            AuthorId = new Guid("b1873f2f-81f0-4683-aa7f-33c3e9845df2"),
                            IsBlocked = false,
                            IsPublic = true,
                            Topic = "The Science of Sleep: Understanding the Importance of a Good Night's Rest"
                        },
                        new
                        {
                            Id = new Guid("a322d519-142c-44ad-adc4-fd0be0b5b752"),
                            AuthorId = new Guid("b1873f2f-81f0-4683-aa7f-33c3e9845df2"),
                            IsBlocked = false,
                            IsPublic = true,
                            Topic = "Revolutionizing Agriculture: The Future of Sustainable Farming Practices"
                        },
                        new
                        {
                            Id = new Guid("7ca6277d-f149-4d92-bfec-aeeb47881689"),
                            AuthorId = new Guid("b1873f2f-81f0-4683-aa7f-33c3e9845df2"),
                            IsBlocked = false,
                            IsPublic = true,
                            Topic = "Beyond the Horizon: The Fascinating World of Space Exploration"
                        },
                        new
                        {
                            Id = new Guid("9990e740-142d-4a11-bb5d-8fb262fdf74a"),
                            AuthorId = new Guid("b1873f2f-81f0-4683-aa7f-33c3e9845df2"),
                            IsBlocked = false,
                            IsPublic = true,
                            Topic = "The Power of the Mind: Understanding the Science of Memory and Cognition"
                        });
                });

            modelBuilder.Entity("PowerHouse.Server.Models.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ConversationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EditDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEdited")
                        .HasColumnType("bit");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ConversationId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("PowerHouse.Server.Models.Report", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Action")
                        .HasColumnType("int");

                    b.Property<Guid?>("ConversationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Decision")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsChecked")
                        .HasColumnType("bit");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("bit");

                    b.Property<Guid?>("MessageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Reported")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ReporterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConversationId");

                    b.HasIndex("MessageId");

                    b.ToTable("Reports");

                    b.HasData(
                        new
                        {
                            Id = new Guid("309a4766-5a12-4211-bb99-9635b7ed1038"),
                            Action = 0,
                            ConversationId = new Guid("a322d519-142c-44ad-adc4-fd0be0b5b752"),
                            IsChecked = false,
                            IsClosed = false,
                            Reason = "Bad text",
                            Reported = new DateTime(2023, 2, 13, 13, 5, 18, 443, DateTimeKind.Utc).AddTicks(7616),
                            ReporterId = new Guid("b1873f2f-81f0-4683-aa7f-33c3e9845df2"),
                            Type = 0
                        },
                        new
                        {
                            Id = new Guid("9b7f4c45-940d-4b4f-90ae-9548c2c091b4"),
                            Action = 0,
                            ConversationId = new Guid("a1d66b82-3313-485e-b464-79ad3bd1f84e"),
                            IsChecked = false,
                            IsClosed = false,
                            Reason = "Hurt my feelings",
                            Reported = new DateTime(2023, 2, 13, 13, 5, 18, 443, DateTimeKind.Utc).AddTicks(7623),
                            ReporterId = new Guid("b1873f2f-81f0-4683-aa7f-33c3e9845df2"),
                            Type = 0
                        });
                });

            modelBuilder.Entity("PowerHouse.Server.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b1873f2f-81f0-4683-aa7f-33c3e9845df2"),
                            Email = "carlo.goretti@live.se",
                            Username = "Carlo Goretti"
                        },
                        new
                        {
                            Id = new Guid("a6bd1e41-b8f0-4434-8387-39329ae2e1a1"),
                            Email = "Bambi.goretti@live.se",
                            Username = "Bambi Goretti"
                        });
                });

            modelBuilder.Entity("ConversationUser", b =>
                {
                    b.HasOne("PowerHouse.Server.Models.Conversation", null)
                        .WithMany()
                        .HasForeignKey("ConversationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PowerHouse.Server.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PowerHouse.Server.Models.Message", b =>
                {
                    b.HasOne("PowerHouse.Server.Models.User", "Author")
                        .WithMany("Messages")
                        .HasForeignKey("AuthorId");

                    b.HasOne("PowerHouse.Server.Models.Conversation", "Conversation")
                        .WithMany("Messages")
                        .HasForeignKey("ConversationId");

                    b.Navigation("Author");

                    b.Navigation("Conversation");
                });

            modelBuilder.Entity("PowerHouse.Server.Models.Report", b =>
                {
                    b.HasOne("PowerHouse.Server.Models.Conversation", "Conversation")
                        .WithMany()
                        .HasForeignKey("ConversationId");

                    b.HasOne("PowerHouse.Server.Models.Message", "Message")
                        .WithMany()
                        .HasForeignKey("MessageId");

                    b.Navigation("Conversation");

                    b.Navigation("Message");
                });

            modelBuilder.Entity("PowerHouse.Server.Models.Conversation", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("PowerHouse.Server.Models.User", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}