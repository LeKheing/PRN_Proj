using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ManageInventory.Models;

public partial class QuanLyKhoContext : DbContext
{
    public QuanLyKhoContext()
    {
    }

    public QuanLyKhoContext(DbContextOptions<QuanLyKhoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Input> Inputs { get; set; }

    public virtual DbSet<InputInfo> InputInfos { get; set; }

    public virtual DbSet<Object> Objects { get; set; }

    public virtual DbSet<Output> Outputs { get; set; }

    public virtual DbSet<OutputInfo> OutputInfos { get; set; }

    public virtual DbSet<Suplier> Supliers { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=QuanLyKho;User ID=sa;Password=sa;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC074FBD99BA");

            entity.ToTable("Customer");

            entity.Property(e => e.ContractDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        modelBuilder.Entity<Input>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Input__3214EC07AD6124D3");

            entity.ToTable("Input");

            entity.Property(e => e.Id).HasMaxLength(128);
            entity.Property(e => e.DateInput).HasColumnType("datetime");
        });

        modelBuilder.Entity<InputInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__InputInf__3214EC076EFD1B30");

            entity.ToTable("InputInfo");

            entity.Property(e => e.Id).HasMaxLength(128);
            entity.Property(e => e.IdInput).HasMaxLength(128);
            entity.Property(e => e.IdObject).HasMaxLength(128);
            entity.Property(e => e.InputPrice).HasDefaultValue(0.0);
            entity.Property(e => e.OutputPrice).HasDefaultValue(0.0);

            entity.HasOne(d => d.IdInputNavigation).WithMany(p => p.InputInfos)
                .HasForeignKey(d => d.IdInput)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InputInfo__IdInp__37A5467C");

            entity.HasOne(d => d.IdObjectNavigation).WithMany(p => p.InputInfos)
                .HasForeignKey(d => d.IdObject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InputInfo__Statu__36B12243");
        });

        modelBuilder.Entity<Object>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Object__3214EC074FEBBAAF");

            entity.ToTable("Object");

            entity.Property(e => e.Id).HasMaxLength(128);

            entity.HasOne(d => d.IdSuplierNavigation).WithMany(p => p.Objects)
                .HasForeignKey(d => d.IdSuplier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Object__IdSuplie__2B3F6F97");

            entity.HasOne(d => d.IdUnitNavigation).WithMany(p => p.Objects)
                .HasForeignKey(d => d.IdUnit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Object__BarCode__2A4B4B5E");
        });

        modelBuilder.Entity<Output>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Output__3214EC07D03AEB37");

            entity.ToTable("Output");

            entity.Property(e => e.Id).HasMaxLength(128);
            entity.Property(e => e.DateOutput).HasColumnType("datetime");
        });

        modelBuilder.Entity<OutputInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OutputIn__3214EC076323DB5D");

            entity.ToTable("OutputInfo");

            entity.Property(e => e.Id).HasMaxLength(128);
            entity.Property(e => e.IdObject).HasMaxLength(128);
            entity.Property(e => e.IdOutput).HasMaxLength(128);

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.OutputInfos)
                .HasForeignKey(d => d.IdCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OutputInf__IdCus__3E52440B");

            entity.HasOne(d => d.IdObjectNavigation).WithMany(p => p.OutputInfos)
                .HasForeignKey(d => d.IdObject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OutputInf__Statu__3C69FB99");

            entity.HasOne(d => d.IdOutputNavigation).WithMany(p => p.OutputInfos)
                .HasForeignKey(d => d.IdOutput)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OutputInf__IdOut__3D5E1FD2");
        });

        modelBuilder.Entity<Suplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Suplier__3214EC0739485C61");

            entity.ToTable("Suplier");

            entity.Property(e => e.ContractDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Unit__3214EC0708295FF3");

            entity.ToTable("Unit");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07FE73E818");

            entity.Property(e => e.UserName).HasMaxLength(100);

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__IdRole__300424B4");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserRole__3214EC070D9C4A47");

            entity.ToTable("UserRole");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
