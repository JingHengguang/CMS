﻿// <auto-generated />
using System;
using Cms.Backend.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cms.Backend.Api.Migrations
{
    [DbContext(typeof(CmsBackendDb))]
    partial class CmsBackendDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Cms.Backend.Api.Model.Articles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ContentsId")
                        .HasColumnType("int");

                    b.Property<string>("CoverPictureUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedTime")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActived")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("IsCheck")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsRecommend")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("LikesNum")
                        .HasColumnType("int");

                    b.Property<int>("ReadingsNum")
                        .HasColumnType("int");

                    b.Property<string>("Remarks")
                        .HasColumnType("longtext");

                    b.Property<string>("Synopsis")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedTime")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContentsId");

                    b.HasIndex("TypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("Cms.Backend.Api.Model.AuditInfos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("BrowserInfo")
                        .HasColumnType("longtext");

                    b.Property<string>("ClientIpAddress")
                        .HasColumnType("longtext");

                    b.Property<string>("ClientName")
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedTime")
                        .HasColumnType("longtext");

                    b.Property<string>("CustomData")
                        .HasColumnType("longtext");

                    b.Property<string>("Exception")
                        .HasColumnType("longtext");

                    b.Property<int>("ExecutionDuration")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExecutionTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsActived")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("MethodName")
                        .HasColumnType("longtext");

                    b.Property<string>("Parameters")
                        .HasColumnType("longtext");

                    b.Property<string>("Remarks")
                        .HasColumnType("longtext");

                    b.Property<string>("ReturnValue")
                        .HasColumnType("longtext");

                    b.Property<string>("ServiceName")
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedTime")
                        .HasColumnType("longtext");

                    b.Property<string>("UserInfo")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("AuditInfos");
                });

            modelBuilder.Entity("Cms.Backend.Api.Model.Broadcast_map", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatedTime")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActived")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Picture")
                        .HasColumnType("longtext");

                    b.Property<string>("Remarks")
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedTime")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Broadcast_map");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedTime = "2023-07-29 16:23:04",
                            IsActived = true,
                            IsDeleted = false,
                            Picture = "UploadFiles\\DefaultImage\\bj001.jpg",
                            Remarks = "种子数据",
                            UpdatedTime = "2023-07-29 16:23:04"
                        });
                });

            modelBuilder.Entity("Cms.Backend.Api.Model.Comments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ArticlesId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedTime")
                        .HasColumnType("longtext");

                    b.Property<int>("FromUserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActived")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Remarks")
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedTime")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ArticlesId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Cms.Backend.Api.Model.Contents", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatedTime")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActived")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Picture")
                        .HasColumnType("longtext");

                    b.Property<string>("Remarks")
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedTime")
                        .HasColumnType("longtext");

                    b.Property<string>("Video")
                        .HasColumnType("longtext");

                    b.Property<string>("Words")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Contents");
                });

            modelBuilder.Entity("Cms.Backend.Api.Model.Corporate_information", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("About")
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedTime")
                        .HasColumnType("longtext");

                    b.Property<string>("FTR")
                        .HasColumnType("longtext");

                    b.Property<string>("ICP")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActived")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Official_account")
                        .HasColumnType("longtext");

                    b.Property<string>("QQ")
                        .HasColumnType("longtext");

                    b.Property<string>("Recruitment")
                        .HasColumnType("longtext");

                    b.Property<string>("Remarks")
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedTime")
                        .HasColumnType("longtext");

                    b.Property<string>("WeChat")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Corporate_information");
                });

            modelBuilder.Entity("Cms.Backend.Api.Model.Likes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

                    b.Property<int?>("ArticlesId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedTime")
                        .HasColumnType("longtext");

                    b.Property<int>("FromUserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActived")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsPraise")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Remarks")
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedTime")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ArticlesId");

                    b.HasIndex("FromUserId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("Cms.Backend.Api.Model.Types", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatedTime")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActived")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Remarks")
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedTime")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("Cms.Backend.Api.Model.UserRoles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatedTime")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActived")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Permission")
                        .HasColumnType("longtext");

                    b.Property<string>("Remarks")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedTime")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedTime = "2023-07-29 16:23:04",
                            IsActived = true,
                            IsDeleted = false,
                            Permission = "",
                            Remarks = "种子数据",
                            RoleName = "超级管理员",
                            UpdatedTime = "2023-07-29 16:23:04"
                        },
                        new
                        {
                            Id = 2,
                            CreatedTime = "2023-07-29 16:23:04",
                            IsActived = true,
                            IsDeleted = false,
                            Permission = "",
                            Remarks = "种子数据",
                            RoleName = "管理员",
                            UpdatedTime = "2023-07-29 16:23:04"
                        },
                        new
                        {
                            Id = 3,
                            CreatedTime = "2023-07-29 16:23:04",
                            IsActived = true,
                            IsDeleted = false,
                            Permission = "",
                            Remarks = "种子数据",
                            RoleName = "普通用户",
                            UpdatedTime = "2023-07-29 16:23:04"
                        });
                });

            modelBuilder.Entity("Cms.Backend.Api.Model.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedTime")
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActived")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("QQNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("Remarks")
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedTime")
                        .HasColumnType("longtext");

                    b.Property<string>("UserPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.Property<int>("UserRoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("WeChat")
                        .HasColumnType("longtext");

                    b.Property<string>("eMail")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("UserRoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Age = 20,
                            Avatar = "UploadFiles\\DefaultImage\\default.jpg",
                            CreatedTime = "2023-07-29 16:23:04",
                            IsActived = true,
                            IsDeleted = false,
                            Password = "123",
                            Remarks = "种子数据",
                            UpdatedTime = "2023-07-29 16:23:04",
                            UserPhoneNumber = "18295778879",
                            UserRoleId = 1,
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("Cms.Backend.Api.Model.Articles", b =>
                {
                    b.HasOne("Cms.Backend.Api.Model.Contents", "Contents")
                        .WithMany("Article")
                        .HasForeignKey("ContentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cms.Backend.Api.Model.Types", "Type")
                        .WithMany("Article")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cms.Backend.Api.Model.Users", "User")
                        .WithMany("Article")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contents");

                    b.Navigation("Type");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cms.Backend.Api.Model.Comments", b =>
                {
                    b.HasOne("Cms.Backend.Api.Model.Articles", "Articles")
                        .WithMany("Comments")
                        .HasForeignKey("ArticlesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Articles");
                });

            modelBuilder.Entity("Cms.Backend.Api.Model.Likes", b =>
                {
                    b.HasOne("Cms.Backend.Api.Model.Articles", "Articles")
                        .WithMany("Likes")
                        .HasForeignKey("ArticlesId");

                    b.HasOne("Cms.Backend.Api.Model.Users", "FromUser")
                        .WithMany("Likes")
                        .HasForeignKey("FromUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Articles");

                    b.Navigation("FromUser");
                });

            modelBuilder.Entity("Cms.Backend.Api.Model.Users", b =>
                {
                    b.HasOne("Cms.Backend.Api.Model.UserRoles", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("Cms.Backend.Api.Model.Articles", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Likes");
                });

            modelBuilder.Entity("Cms.Backend.Api.Model.Contents", b =>
                {
                    b.Navigation("Article");
                });

            modelBuilder.Entity("Cms.Backend.Api.Model.Types", b =>
                {
                    b.Navigation("Article");
                });

            modelBuilder.Entity("Cms.Backend.Api.Model.UserRoles", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Cms.Backend.Api.Model.Users", b =>
                {
                    b.Navigation("Article");

                    b.Navigation("Likes");
                });
#pragma warning restore 612, 618
        }
    }
}
