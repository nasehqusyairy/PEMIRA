using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace PEMIRA.Models;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Candidate> Candidates { get; set; }

    public virtual DbSet<CandidateUser> CandidateUsers { get; set; }

    public virtual DbSet<Election> Elections { get; set; }

    public virtual DbSet<ElectionUser> ElectionUsers { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuRole> MenuRoles { get; set; }

    public virtual DbSet<Menusegment> Menusegments { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<PermissionUser> PermissionUsers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleUser> RoleUsers { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TagUser> TagUsers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = configuration?.GetConnectionString("MySQL") ?? "server=localhost;database=pemira;user=root";

        optionsBuilder.UseMySql(connectionString, ServerVersion.Parse("10.4.20-mariadb"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("candidates");

            entity.HasIndex(e => e.ElectionId, "candidates_elections_FK");

            entity.HasIndex(e => e.UserId, "candidates_users_FK");

            entity.HasIndex(e => e.CreatedBy, "fk_candidates_created_by");

            entity.HasIndex(e => e.DeletedBy, "fk_candidates_deleted_by");

            entity.HasIndex(e => e.UpdatedBy, "fk_candidates_updated_by");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("id");
            entity.Property(e => e.Color)
                .HasMaxLength(6)
                .HasDefaultValueSql("'00bcd4'")
                .IsFixedLength()
                .HasColumnName("color");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("bigint(20)")
                .HasColumnName("created_by");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy)
                .HasColumnType("bigint(20)")
                .HasColumnName("deleted_by");
            entity.Property(e => e.ElectionId)
                .HasColumnType("bigint(20)")
                .HasColumnName("election_id");
            entity.Property(e => e.Img)
                .HasMaxLength(100)
                .HasDefaultValueSql("'nophoto.jpg'")
                .HasColumnName("img");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("bigint(20)")
                .HasColumnName("updated_by");
            entity.Property(e => e.UserId)
                .HasColumnType("bigint(20)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.CandidateCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_candidates_created_by");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.CandidateDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_candidates_deleted_by");

            entity.HasOne(d => d.Election).WithMany(p => p.Candidates)
                .HasForeignKey(d => d.ElectionId)
                .HasConstraintName("candidates_elections_FK");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.CandidateUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_candidates_updated_by");

            entity.HasOne(d => d.User).WithMany(p => p.CandidateUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("candidates_users_FK");
        });

        modelBuilder.Entity<CandidateUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("candidate_user");

            entity.HasIndex(e => e.CandidateId, "user_candidate_candidates_FK");

            entity.HasIndex(e => e.UserId, "user_candidate_users_FK");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("id");
            entity.Property(e => e.CandidateId)
                .HasColumnType("bigint(20)")
                .HasColumnName("candidate_id");
            entity.Property(e => e.UserId)
                .HasColumnType("bigint(20)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.Candidate).WithMany(p => p.CandidateUsers)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("user_candidate_candidates_FK");

            entity.HasOne(d => d.User).WithMany(p => p.CandidateUsersNavigation)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_candidate_users_FK");
        });

        modelBuilder.Entity<Election>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("elections");

            entity.HasIndex(e => e.CreatedBy, "fk_elections_created_by");

            entity.HasIndex(e => e.DeletedBy, "fk_elections_deleted_by");

            entity.HasIndex(e => e.UpdatedBy, "fk_elections_updated_by");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("bigint(20)")
                .HasColumnName("created_by");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy)
                .HasColumnType("bigint(20)")
                .HasColumnName("deleted_by");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValueSql("'New Candidate'")
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("bigint(20)")
                .HasColumnName("updated_by");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ElectionCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_elections_created_by");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.ElectionDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_elections_deleted_by");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ElectionUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_elections_updated_by");
        });

        modelBuilder.Entity<ElectionUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("election_user");

            entity.HasIndex(e => e.ElectionId, "election_user_elections_FK");

            entity.HasIndex(e => e.UserId, "election_user_users_FK");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("id");
            entity.Property(e => e.ElectionId)
                .HasColumnType("bigint(20)")
                .HasColumnName("election_id");
            entity.Property(e => e.UserId)
                .HasColumnType("bigint(20)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.Election).WithMany(p => p.ElectionUsers)
                .HasForeignKey(d => d.ElectionId)
                .HasConstraintName("election_user_elections_FK");

            entity.HasOne(d => d.User).WithMany(p => p.ElectionUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("election_user_users_FK");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("menus");

            entity.HasIndex(e => e.MenusegmentId, "fk_menusegment_id");

            entity.Property(e => e.Id)
                .HasComment("Primary Key")
                .HasColumnType("bigint(20)")
                .HasColumnName("id");
            entity.Property(e => e.Icon)
                .HasMaxLength(100)
                .HasDefaultValueSql("'file-earmark'")
                .HasColumnName("icon");
            entity.Property(e => e.MenusegmentId)
                .HasColumnType("bigint(20)")
                .HasColumnName("menusegment_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValueSql("'New Menu'")
                .HasColumnName("name");
            entity.Property(e => e.Url)
                .HasMaxLength(100)
                .HasDefaultValueSql("'/'")
                .HasColumnName("url");

            entity.HasOne(d => d.Menusegment).WithMany(p => p.Menus)
                .HasForeignKey(d => d.MenusegmentId)
                .HasConstraintName("fk_menusegment_id");
        });

        modelBuilder.Entity<MenuRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("menu_role");

            entity.HasIndex(e => e.MenuId, "menu_role_ibfk_1");

            entity.HasIndex(e => e.RoleId, "menu_role_ibfk_2");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("id");
            entity.Property(e => e.MenuId)
                .HasColumnType("bigint(20)")
                .HasColumnName("menu_id");
            entity.Property(e => e.RoleId)
                .HasColumnType("bigint(20)")
                .HasColumnName("role_id");

            entity.HasOne(d => d.Menu).WithMany(p => p.MenuRoles)
                .HasForeignKey(d => d.MenuId)
                .HasConstraintName("menu_role_ibfk_1");

            entity.HasOne(d => d.Role).WithMany(p => p.MenuRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("menu_role_ibfk_2");
        });

        modelBuilder.Entity<Menusegment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("menusegments");

            entity.Property(e => e.Id)
                .HasComment("Primary Key")
                .HasColumnType("bigint(20)")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValueSql("'New Segment'")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("permissions");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValueSql("'New Permission'")
                .HasColumnName("name");
        });

        modelBuilder.Entity<PermissionUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("permission_user");

            entity.HasIndex(e => e.PermissionId, "fk_permission_id");

            entity.HasIndex(e => e.UserId, "fk_user_id");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("id");
            entity.Property(e => e.PermissionId)
                .HasColumnType("bigint(20)")
                .HasColumnName("permission_id");
            entity.Property(e => e.UserId)
                .HasColumnType("bigint(20)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.Permission).WithMany(p => p.PermissionUsers)
                .HasForeignKey(d => d.PermissionId)
                .HasConstraintName("fk_permission_id");

            entity.HasOne(d => d.User).WithMany(p => p.PermissionUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_user_id");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValueSql("'New Role'")
                .HasColumnName("name");
        });

        modelBuilder.Entity<RoleUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("role_user");

            entity.HasIndex(e => e.ElectionId, "role_user_elections_FK");

            entity.HasIndex(e => e.RoleId, "role_user_roles_FK");

            entity.HasIndex(e => e.UserId, "role_user_users_FK");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("id");
            entity.Property(e => e.ElectionId)
                .HasColumnType("bigint(20)")
                .HasColumnName("election_id");
            entity.Property(e => e.RoleId)
                .HasColumnType("bigint(20)")
                .HasColumnName("role_id");
            entity.Property(e => e.UserId)
                .HasColumnType("bigint(20)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.Election).WithMany(p => p.RoleUsers)
                .HasForeignKey(d => d.ElectionId)
                .HasConstraintName("role_user_elections_FK");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleUsers)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("role_user_roles_FK");

            entity.HasOne(d => d.User).WithMany(p => p.RoleUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("role_user_users_FK");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tags");

            entity.HasIndex(e => e.CreatedBy, "fk_tags_created_by");

            entity.HasIndex(e => e.DeletedBy, "fk_tags_deleted_by");

            entity.HasIndex(e => e.UpdatedBy, "fk_tags_updated_by");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("bigint(20)")
                .HasColumnName("created_by");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy)
                .HasColumnType("bigint(20)")
                .HasColumnName("deleted_by");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValueSql("'New Tag'")
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("bigint(20)")
                .HasColumnName("updated_by");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TagCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_tags_created_by");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.TagDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_tags_deleted_by");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.TagUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_tags_updated_by");
        });

        modelBuilder.Entity<TagUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tag_user");

            entity.HasIndex(e => e.TagId, "tag_user_tags_FK");

            entity.HasIndex(e => e.UserId, "tag_user_users_FK");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("id");
            entity.Property(e => e.TagId)
                .HasColumnType("bigint(20)")
                .HasColumnName("tag_id");
            entity.Property(e => e.UserId)
                .HasColumnType("bigint(20)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.Tag).WithMany(p => p.TagUsers)
                .HasForeignKey(d => d.TagId)
                .HasConstraintName("tag_user_tags_FK");

            entity.HasOne(d => d.User).WithMany(p => p.TagUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("tag_user_users_FK");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Code, "code").IsUnique();

            entity.HasIndex(e => e.CreatedBy, "fk_users_created_by");

            entity.HasIndex(e => e.DeletedBy, "fk_users_deleted_by");

            entity.HasIndex(e => e.UpdatedBy, "fk_users_updated_by");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(100)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("bigint(20)")
                .HasColumnName("created_by");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy)
                .HasColumnType("bigint(20)")
                .HasColumnName("deleted_by");
            entity.Property(e => e.Gender)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("gender");
            entity.Property(e => e.More)
                .HasMaxLength(50)
                .HasDefaultValueSql("'-'")
                .HasColumnName("more");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValueSql("'New User'")
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasColumnType("text")
                .HasColumnName("password");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("bigint(20)")
                .HasColumnName("updated_by");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_users_created_by");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.InverseDeletedByNavigation)
                .HasForeignKey(d => d.DeletedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_users_deleted_by");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.InverseUpdatedByNavigation)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_users_updated_by");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
