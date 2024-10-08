﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PEMIRA.Models;

#nullable disable

namespace PEMIRA.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240808154035_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_general_ci")
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");
            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("PEMIRA.Models.Candidate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint(20)")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(6)
                        .HasColumnType("char(6)")
                        .HasColumnName("color")
                        .HasDefaultValueSql("'00bcd4'")
                        .IsFixedLength();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("current_timestamp()");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("deleted_at");

                    b.Property<long?>("DeletedBy")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("deleted_by");

                    b.Property<long>("ElectionId")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("election_id");

                    b.Property<string>("Img")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("img")
                        .HasDefaultValueSql("'nophoto.jpg'");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("current_timestamp()");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("UpdatedAt"));

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("updated_by");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "ElectionId" }, "candidates_elections_FK");

                    b.HasIndex(new[] { "UserId" }, "candidates_users_FK");

                    b.HasIndex(new[] { "CreatedBy" }, "fk_candidates_created_by");

                    b.HasIndex(new[] { "DeletedBy" }, "fk_candidates_deleted_by");

                    b.HasIndex(new[] { "UpdatedBy" }, "fk_candidates_updated_by");

                    b.ToTable("candidates", (string)null);
                });

            modelBuilder.Entity("PEMIRA.Models.CandidateUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint(20)")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("CandidateId")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("candidate_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "CandidateId" }, "user_candidate_candidates_FK");

                    b.HasIndex(new[] { "UserId" }, "user_candidate_users_FK");

                    b.ToTable("candidate_user", (string)null);
                });

            modelBuilder.Entity("PEMIRA.Models.Election", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint(20)")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("current_timestamp()");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("deleted_at");

                    b.Property<long?>("DeletedBy")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("deleted_by");

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name")
                        .HasDefaultValueSql("'New Candidate'");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("current_timestamp()");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("UpdatedAt"));

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("updated_by");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "CreatedBy" }, "fk_elections_created_by");

                    b.HasIndex(new[] { "DeletedBy" }, "fk_elections_deleted_by");

                    b.HasIndex(new[] { "UpdatedBy" }, "fk_elections_updated_by");

                    b.ToTable("elections", (string)null);
                });

            modelBuilder.Entity("PEMIRA.Models.ElectionUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint(20)")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("ElectionId")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("election_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "ElectionId" }, "election_user_elections_FK");

                    b.HasIndex(new[] { "UserId" }, "election_user_users_FK");

                    b.ToTable("election_user", (string)null);
                });

            modelBuilder.Entity("PEMIRA.Models.Menu", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint(20)")
                        .HasColumnName("id")
                        .HasComment("Primary Key");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Icon")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("icon")
                        .HasDefaultValueSql("'file-earmark'");

                    b.Property<long>("MenusegmentId")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("menusegment_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name")
                        .HasDefaultValueSql("'New Menu'");

                    b.Property<string>("Url")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("url")
                        .HasDefaultValueSql("'/'");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "MenusegmentId" }, "fk_menusegment_id");

                    b.ToTable("menus", (string)null);
                });

            modelBuilder.Entity("PEMIRA.Models.MenuRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint(20)")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("MenuId")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("menu_id");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "MenuId" }, "menu_role_ibfk_1");

                    b.HasIndex(new[] { "RoleId" }, "menu_role_ibfk_2");

                    b.ToTable("menu_role", (string)null);
                });

            modelBuilder.Entity("PEMIRA.Models.Menusegment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint(20)")
                        .HasColumnName("id")
                        .HasComment("Primary Key");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name")
                        .HasDefaultValueSql("'New Segment'");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("menusegments", (string)null);
                });

            modelBuilder.Entity("PEMIRA.Models.Permission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint(20)")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name")
                        .HasDefaultValueSql("'New Permission'");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("permissions", (string)null);
                });

            modelBuilder.Entity("PEMIRA.Models.PermissionUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint(20)")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("PermissionId")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("permission_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "PermissionId" }, "fk_permission_id");

                    b.HasIndex(new[] { "UserId" }, "fk_user_id");

                    b.ToTable("permission_user", (string)null);
                });

            modelBuilder.Entity("PEMIRA.Models.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint(20)")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name")
                        .HasDefaultValueSql("'New Role'");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("roles", (string)null);
                });

            modelBuilder.Entity("PEMIRA.Models.RoleUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint(20)")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("ElectionId")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("election_id");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("role_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "ElectionId" }, "role_user_elections_FK");

                    b.HasIndex(new[] { "RoleId" }, "role_user_roles_FK");

                    b.HasIndex(new[] { "UserId" }, "role_user_users_FK");

                    b.ToTable("role_user", (string)null);
                });

            modelBuilder.Entity("PEMIRA.Models.Tag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint(20)")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("current_timestamp()");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("deleted_at");

                    b.Property<long?>("DeletedBy")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("deleted_by");

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name")
                        .HasDefaultValueSql("'New Tag'");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("current_timestamp()");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("UpdatedAt"));

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("updated_by");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "CreatedBy" }, "fk_tags_created_by");

                    b.HasIndex(new[] { "DeletedBy" }, "fk_tags_deleted_by");

                    b.HasIndex(new[] { "UpdatedBy" }, "fk_tags_updated_by");

                    b.ToTable("tags", (string)null);
                });

            modelBuilder.Entity("PEMIRA.Models.TagUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint(20)")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("TagId")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("tag_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "TagId" }, "tag_user_tags_FK");

                    b.HasIndex(new[] { "UserId" }, "tag_user_users_FK");

                    b.ToTable("tag_user", (string)null);
                });

            modelBuilder.Entity("PEMIRA.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint(20)")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("code");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("current_timestamp()");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("deleted_at");

                    b.Property<long?>("DeletedBy")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("deleted_by");

                    b.Property<bool?>("Gender")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("gender")
                        .HasDefaultValueSql("'1'");

                    b.Property<string>("More")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("more")
                        .HasDefaultValueSql("'-'");

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name")
                        .HasDefaultValueSql("'New User'");

                    b.Property<string>("Password")
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("current_timestamp()");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("UpdatedAt"));

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("updated_by");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Code" }, "code")
                        .IsUnique();

                    b.HasIndex(new[] { "CreatedBy" }, "fk_users_created_by");

                    b.HasIndex(new[] { "DeletedBy" }, "fk_users_deleted_by");

                    b.HasIndex(new[] { "UpdatedBy" }, "fk_users_updated_by");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("PEMIRA.Models.Candidate", b =>
                {
                    b.HasOne("PEMIRA.Models.User", "CreatedByNavigation")
                        .WithMany("CandidateCreatedByNavigations")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_candidates_created_by");

                    b.HasOne("PEMIRA.Models.User", "DeletedByNavigation")
                        .WithMany("CandidateDeletedByNavigations")
                        .HasForeignKey("DeletedBy")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_candidates_deleted_by");

                    b.HasOne("PEMIRA.Models.Election", "Election")
                        .WithMany("Candidates")
                        .HasForeignKey("ElectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("candidates_elections_FK");

                    b.HasOne("PEMIRA.Models.User", "UpdatedByNavigation")
                        .WithMany("CandidateUpdatedByNavigations")
                        .HasForeignKey("UpdatedBy")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_candidates_updated_by");

                    b.HasOne("PEMIRA.Models.User", "User")
                        .WithMany("CandidateUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("candidates_users_FK");

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("DeletedByNavigation");

                    b.Navigation("Election");

                    b.Navigation("UpdatedByNavigation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PEMIRA.Models.CandidateUser", b =>
                {
                    b.HasOne("PEMIRA.Models.Candidate", "Candidate")
                        .WithMany("CandidateUsers")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("user_candidate_candidates_FK");

                    b.HasOne("PEMIRA.Models.User", "User")
                        .WithMany("CandidateUsersNavigation")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("user_candidate_users_FK");

                    b.Navigation("Candidate");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PEMIRA.Models.Election", b =>
                {
                    b.HasOne("PEMIRA.Models.User", "CreatedByNavigation")
                        .WithMany("ElectionCreatedByNavigations")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_elections_created_by");

                    b.HasOne("PEMIRA.Models.User", "DeletedByNavigation")
                        .WithMany("ElectionDeletedByNavigations")
                        .HasForeignKey("DeletedBy")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_elections_deleted_by");

                    b.HasOne("PEMIRA.Models.User", "UpdatedByNavigation")
                        .WithMany("ElectionUpdatedByNavigations")
                        .HasForeignKey("UpdatedBy")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_elections_updated_by");

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("DeletedByNavigation");

                    b.Navigation("UpdatedByNavigation");
                });

            modelBuilder.Entity("PEMIRA.Models.ElectionUser", b =>
                {
                    b.HasOne("PEMIRA.Models.Election", "Election")
                        .WithMany("ElectionUsers")
                        .HasForeignKey("ElectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("election_user_elections_FK");

                    b.HasOne("PEMIRA.Models.User", "User")
                        .WithMany("ElectionUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("election_user_users_FK");

                    b.Navigation("Election");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PEMIRA.Models.Menu", b =>
                {
                    b.HasOne("PEMIRA.Models.Menusegment", "Menusegment")
                        .WithMany("Menus")
                        .HasForeignKey("MenusegmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_menusegment_id");

                    b.Navigation("Menusegment");
                });

            modelBuilder.Entity("PEMIRA.Models.MenuRole", b =>
                {
                    b.HasOne("PEMIRA.Models.Menu", "Menu")
                        .WithMany("MenuRoles")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("menu_role_ibfk_1");

                    b.HasOne("PEMIRA.Models.Role", "Role")
                        .WithMany("MenuRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("menu_role_ibfk_2");

                    b.Navigation("Menu");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("PEMIRA.Models.PermissionUser", b =>
                {
                    b.HasOne("PEMIRA.Models.Permission", "Permission")
                        .WithMany("PermissionUsers")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_permission_id");

                    b.HasOne("PEMIRA.Models.User", "User")
                        .WithMany("PermissionUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_id");

                    b.Navigation("Permission");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PEMIRA.Models.RoleUser", b =>
                {
                    b.HasOne("PEMIRA.Models.Election", "Election")
                        .WithMany("RoleUsers")
                        .HasForeignKey("ElectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("role_user_elections_FK");

                    b.HasOne("PEMIRA.Models.Role", "Role")
                        .WithMany("RoleUsers")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("role_user_roles_FK");

                    b.HasOne("PEMIRA.Models.User", "User")
                        .WithMany("RoleUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("role_user_users_FK");

                    b.Navigation("Election");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PEMIRA.Models.Tag", b =>
                {
                    b.HasOne("PEMIRA.Models.User", "CreatedByNavigation")
                        .WithMany("TagCreatedByNavigations")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_tags_created_by");

                    b.HasOne("PEMIRA.Models.User", "DeletedByNavigation")
                        .WithMany("TagDeletedByNavigations")
                        .HasForeignKey("DeletedBy")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_tags_deleted_by");

                    b.HasOne("PEMIRA.Models.User", "UpdatedByNavigation")
                        .WithMany("TagUpdatedByNavigations")
                        .HasForeignKey("UpdatedBy")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_tags_updated_by");

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("DeletedByNavigation");

                    b.Navigation("UpdatedByNavigation");
                });

            modelBuilder.Entity("PEMIRA.Models.TagUser", b =>
                {
                    b.HasOne("PEMIRA.Models.Tag", "Tag")
                        .WithMany("TagUsers")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("tag_user_tags_FK");

                    b.HasOne("PEMIRA.Models.User", "User")
                        .WithMany("TagUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("tag_user_users_FK");

                    b.Navigation("Tag");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PEMIRA.Models.User", b =>
                {
                    b.HasOne("PEMIRA.Models.User", "CreatedByNavigation")
                        .WithMany("InverseCreatedByNavigation")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_users_created_by");

                    b.HasOne("PEMIRA.Models.User", "DeletedByNavigation")
                        .WithMany("InverseDeletedByNavigation")
                        .HasForeignKey("DeletedBy")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_users_deleted_by");

                    b.HasOne("PEMIRA.Models.User", "UpdatedByNavigation")
                        .WithMany("InverseUpdatedByNavigation")
                        .HasForeignKey("UpdatedBy")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_users_updated_by");

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("DeletedByNavigation");

                    b.Navigation("UpdatedByNavigation");
                });

            modelBuilder.Entity("PEMIRA.Models.Candidate", b =>
                {
                    b.Navigation("CandidateUsers");
                });

            modelBuilder.Entity("PEMIRA.Models.Election", b =>
                {
                    b.Navigation("Candidates");

                    b.Navigation("ElectionUsers");

                    b.Navigation("RoleUsers");
                });

            modelBuilder.Entity("PEMIRA.Models.Menu", b =>
                {
                    b.Navigation("MenuRoles");
                });

            modelBuilder.Entity("PEMIRA.Models.Menusegment", b =>
                {
                    b.Navigation("Menus");
                });

            modelBuilder.Entity("PEMIRA.Models.Permission", b =>
                {
                    b.Navigation("PermissionUsers");
                });

            modelBuilder.Entity("PEMIRA.Models.Role", b =>
                {
                    b.Navigation("MenuRoles");

                    b.Navigation("RoleUsers");
                });

            modelBuilder.Entity("PEMIRA.Models.Tag", b =>
                {
                    b.Navigation("TagUsers");
                });

            modelBuilder.Entity("PEMIRA.Models.User", b =>
                {
                    b.Navigation("CandidateCreatedByNavigations");

                    b.Navigation("CandidateDeletedByNavigations");

                    b.Navigation("CandidateUpdatedByNavigations");

                    b.Navigation("CandidateUsers");

                    b.Navigation("CandidateUsersNavigation");

                    b.Navigation("ElectionCreatedByNavigations");

                    b.Navigation("ElectionDeletedByNavigations");

                    b.Navigation("ElectionUpdatedByNavigations");

                    b.Navigation("ElectionUsers");

                    b.Navigation("InverseCreatedByNavigation");

                    b.Navigation("InverseDeletedByNavigation");

                    b.Navigation("InverseUpdatedByNavigation");

                    b.Navigation("PermissionUsers");

                    b.Navigation("RoleUsers");

                    b.Navigation("TagCreatedByNavigations");

                    b.Navigation("TagDeletedByNavigations");

                    b.Navigation("TagUpdatedByNavigations");

                    b.Navigation("TagUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
