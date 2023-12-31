﻿// <auto-generated />
using System;
using MeuProjeto.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MeuProjeto.Infrastructure.Migrations
{
    [DbContext(typeof(MeuDbContext))]
    [Migration("20220905133723_adding-settings")]
    partial class addingsettings
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MeuProjeto.Business.Models.Identity.CustomRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<bool>("Admin")
                        .HasColumnType("bit");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("MeuProjeto.Business.Models.Identity.CustomRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("MeuProjeto.Business.Models.Identity.CustomUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("CommonName")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("DistinguishedName")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<Guid?>("IconId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("ObjectCategory")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ObjectClass")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ObjectGuid")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ObjectSid")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("IconId")
                        .IsUnique()
                        .HasFilter("[IconId] IS NOT NULL");

                    b.HasIndex("ImageId")
                        .IsUnique()
                        .HasFilter("[ImageId] IS NOT NULL");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("MeuProjeto.Business.Models.Identity.CustomUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("MeuProjeto.Business.Models.Identity.CustomUserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("MeuProjeto.Business.Models.Identity.CustomUserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("MeuProjeto.Business.Models.Identity.CustomUserToken", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("varchar(100)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MeuProjeto.Business.Models.Log", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Exception")
                        .HasColumnType("ntext");

                    b.Property<string>("Header")
                        .HasColumnType("ntext");

                    b.Property<string>("IP")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Level")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Logger")
                        .HasColumnType("ntext");

                    b.Property<string>("Message")
                        .HasColumnType("ntext");

                    b.Property<string>("NewState")
                        .HasColumnType("ntext");

                    b.Property<string>("OldState")
                        .HasColumnType("ntext");

                    b.Property<string>("StackTracking")
                        .HasColumnType("ntext");

                    b.Property<string>("TableName")
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TableName");

                    b.HasIndex("UserId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("MeuProjeto.Business.Models.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Type", "Value")
                        .IsUnique();

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("MeuProjeto.Business.Models.Picture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pictures", (string)null);
                });

            modelBuilder.Entity("MeuProjeto.Business.Models.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedByIp")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReplacedByToken")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("datetime2");

                    b.Property<string>("RevokedByIp")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Token")
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("MeuProjeto.Business.Models.Settings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Contents")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Settings", (string)null);
                });

            modelBuilder.Entity("MeuProjeto.Business.Models.Text", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Texts");
                });

            modelBuilder.Entity("MeuProjeto.Business.Models.UserManual", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("ContentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LastReviewerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ReviewDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Revision")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("ContentId");

                    b.HasIndex("LastReviewerId");

                    b.ToTable("UserManuals", (string)null);
                });

            modelBuilder.Entity("MeuProjeto.Business.Models.Identity.CustomRoleClaim", b =>
                {
                    b.HasOne("MeuProjeto.Business.Models.Identity.CustomRole", null)
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MeuProjeto.Business.Models.Identity.CustomUser", b =>
                {
                    b.HasOne("MeuProjeto.Business.Models.Picture", "Icon")
                        .WithOne()
                        .HasForeignKey("MeuProjeto.Business.Models.Identity.CustomUser", "IconId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("MeuProjeto.Business.Models.Picture", "Image")
                        .WithOne()
                        .HasForeignKey("MeuProjeto.Business.Models.Identity.CustomUser", "ImageId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Icon");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("MeuProjeto.Business.Models.Identity.CustomUserClaim", b =>
                {
                    b.HasOne("MeuProjeto.Business.Models.Identity.CustomUser", null)
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MeuProjeto.Business.Models.Identity.CustomUserLogin", b =>
                {
                    b.HasOne("MeuProjeto.Business.Models.Identity.CustomUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MeuProjeto.Business.Models.Identity.CustomUserRole", b =>
                {
                    b.HasOne("MeuProjeto.Business.Models.Identity.CustomRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MeuProjeto.Business.Models.Identity.CustomUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MeuProjeto.Business.Models.Identity.CustomUserToken", b =>
                {
                    b.HasOne("MeuProjeto.Business.Models.Identity.CustomUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MeuProjeto.Business.Models.Log", b =>
                {
                    b.HasOne("MeuProjeto.Business.Models.Identity.CustomUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MeuProjeto.Business.Models.RefreshToken", b =>
                {
                    b.HasOne("MeuProjeto.Business.Models.Identity.CustomUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MeuProjeto.Business.Models.UserManual", b =>
                {
                    b.HasOne("MeuProjeto.Business.Models.Text", "Content")
                        .WithMany()
                        .HasForeignKey("ContentId")
                        .IsRequired();

                    b.HasOne("MeuProjeto.Business.Models.Identity.CustomUser", "LastReviewer")
                        .WithMany()
                        .HasForeignKey("LastReviewerId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Content");

                    b.Navigation("LastReviewer");
                });

            modelBuilder.Entity("MeuProjeto.Business.Models.Identity.CustomRole", b =>
                {
                    b.Navigation("Claims");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("MeuProjeto.Business.Models.Identity.CustomUser", b =>
                {
                    b.Navigation("Claims");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
