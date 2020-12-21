﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ph_UserEnv.Authentication;

namespace ph_UserEnv.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Value")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ph_UserEnv.Authentication.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("ph_UserEnv.Models.Contact", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("contact_1_id")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("contact_2_id")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("ph_UserEnv.Models.GamePlayer", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("gameSession_id")
                        .HasColumnType("int");

                    b.Property<int?>("gameSessionid")
                        .HasColumnType("int");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("userId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("user_id")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("id");

                    b.HasIndex("gameSessionid");

                    b.HasIndex("userId");

                    b.ToTable("GamePlayers");
                });

            modelBuilder.Entity("ph_UserEnv.Models.GameSession", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("creatorId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("creator_id")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("current_turnId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("current_turn_id")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("game_name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("game_state")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("moves_left")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("winnerId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("winner_id")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("id");

                    b.HasIndex("creatorId");

                    b.HasIndex("current_turnId");

                    b.HasIndex("winnerId");

                    b.ToTable("GameSessions");
                });

            modelBuilder.Entity("ph_UserEnv.Models.Message", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("message")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("receiver_id")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("sender_id")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ph_UserEnv.Authentication.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ph_UserEnv.Authentication.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ph_UserEnv.Authentication.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ph_UserEnv.Authentication.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ph_UserEnv.Models.GamePlayer", b =>
                {
                    b.HasOne("ph_UserEnv.Models.GameSession", "gameSession")
                        .WithMany()
                        .HasForeignKey("gameSessionid");

                    b.HasOne("ph_UserEnv.Authentication.ApplicationUser", "user")
                        .WithMany()
                        .HasForeignKey("userId");
                });

            modelBuilder.Entity("ph_UserEnv.Models.GameSession", b =>
                {
                    b.HasOne("ph_UserEnv.Authentication.ApplicationUser", "creator")
                        .WithMany()
                        .HasForeignKey("creatorId");

                    b.HasOne("ph_UserEnv.Authentication.ApplicationUser", "current_turn")
                        .WithMany()
                        .HasForeignKey("current_turnId");

                    b.HasOne("ph_UserEnv.Authentication.ApplicationUser", "winner")
                        .WithMany()
                        .HasForeignKey("winnerId");
                });
#pragma warning restore 612, 618
        }
    }
}
