﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WisePoll.Data;

namespace WisePoll.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220108110016_add_user_id")]
    partial class add_user_id
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("MembersPollFields", b =>
                {
                    b.Property<int>("MembersId")
                        .HasColumnType("int");

                    b.Property<int>("PollFieldsId")
                        .HasColumnType("int");

                    b.HasKey("MembersId", "PollFieldsId");

                    b.HasIndex("PollFieldsId");

                    b.ToTable("MembersPollFields");
                });

            modelBuilder.Entity("MembersPolls", b =>
                {
                    b.Property<int>("MembersId")
                        .HasColumnType("int");

                    b.Property<int>("PollsId")
                        .HasColumnType("int");

                    b.HasKey("MembersId", "PollsId");

                    b.HasIndex("PollsId");

                    b.ToTable("MembersPolls");
                });

            modelBuilder.Entity("WisePoll.Data.Models.Members", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsersId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("WisePoll.Data.Models.PollFields", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("PollsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PollsId");

                    b.ToTable("PollFields");
                });

            modelBuilder.Entity("WisePoll.Data.Models.Polls", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Is_active")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Multiple")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsersId");

                    b.ToTable("Polls");
                });

            modelBuilder.Entity("WisePoll.Data.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Pseudo")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MembersPollFields", b =>
                {
                    b.HasOne("WisePoll.Data.Models.Members", null)
                        .WithMany()
                        .HasForeignKey("MembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WisePoll.Data.Models.PollFields", null)
                        .WithMany()
                        .HasForeignKey("PollFieldsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MembersPolls", b =>
                {
                    b.HasOne("WisePoll.Data.Models.Members", null)
                        .WithMany()
                        .HasForeignKey("MembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WisePoll.Data.Models.Polls", null)
                        .WithMany()
                        .HasForeignKey("PollsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WisePoll.Data.Models.Members", b =>
                {
                    b.HasOne("WisePoll.Data.Models.Users", null)
                        .WithMany("Members")
                        .HasForeignKey("UsersId");
                });

            modelBuilder.Entity("WisePoll.Data.Models.PollFields", b =>
                {
                    b.HasOne("WisePoll.Data.Models.Polls", null)
                        .WithMany("PollFields")
                        .HasForeignKey("PollsId");
                });

            modelBuilder.Entity("WisePoll.Data.Models.Polls", b =>
                {
                    b.HasOne("WisePoll.Data.Models.Users", null)
                        .WithMany("Polls")
                        .HasForeignKey("UsersId");
                });

            modelBuilder.Entity("WisePoll.Data.Models.Polls", b =>
                {
                    b.Navigation("PollFields");
                });

            modelBuilder.Entity("WisePoll.Data.Models.Users", b =>
                {
                    b.Navigation("Members");

                    b.Navigation("Polls");
                });
#pragma warning restore 612, 618
        }
    }
}
