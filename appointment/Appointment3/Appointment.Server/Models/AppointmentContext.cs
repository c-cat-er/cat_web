﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Server.Models;

public partial class AppointmentContext : DbContext
{
    public AppointmentContext()
    {
    }

    public AppointmentContext(DbContextOptions<AppointmentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<WBEAURE> WBEAUREs { get; set; }

    public virtual DbSet<WBRSTOR> WBRSTORs { get; set; }

    public virtual DbSet<WCUSTOM> WCUSTOMs { get; set; }

    public virtual DbSet<WSUBSAM> WSUBSAMs { get; set; }

    public virtual DbSet<WUSER> WUSERs { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=台南SERVER\\YIFEI;Initial Catalog=Appointment;User ID=YIFEI2;Password=YIFEI2123;Connect Timeout=30");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WBEAURE>(entity =>
        {
            entity.HasKey(e => e.WBID);

            entity.ToTable("WBEAURES");

            entity.Property(e => e.ACUTE)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CDOC)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CDOCP)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CDOCS)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CMESS).HasMaxLength(50);
            entity.Property(e => e.CNUM)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CSTAT)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CUPDATE)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CURECNT).HasColumnType("decimal(7, 1)");
            entity.Property(e => e.CURECNTS).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.CURENAME).HasMaxLength(20);
            entity.Property(e => e.CURENO)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.OPERUSER)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.RDATE)
                .IsRequired()
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.RTIME)
                .IsRequired()
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SDATE)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SERNO)
                .HasMaxLength(11)
                .IsUnicode(false);
        });

        modelBuilder.Entity<WBRSTOR>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("WBRSTOR");

            entity.Property(e => e.CNAME)
                .IsRequired()
                .HasMaxLength(20);
            entity.Property(e => e.CNUM)
                .IsRequired()
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<WCUSTOM>(entity =>
        {
            entity.HasKey(e => e.CNUM);

            entity.ToTable("WCUSTOM");

            entity.Property(e => e.CNUM)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CBIRTH)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CMOBILE)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.CNAME)
                .IsRequired()
                .HasMaxLength(20);
        });

        modelBuilder.Entity<WSUBSAM>(entity =>
        {
            entity.HasKey(e => e.SID).HasName("PK__WSUBSAM__CA1959704AE8A947");

            entity.ToTable("WSUBSAM");

            entity.Property(e => e.CNAME)
                .IsRequired()
                .HasMaxLength(20);
            entity.Property(e => e.CNUM)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CURECNT).HasColumnType("decimal(7, 1)");
            entity.Property(e => e.CURECNTS).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.CURENAME)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CURENO)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SDATE)
                .IsRequired()
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SERNO)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SUBCNT).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.SUBQTY).HasColumnType("decimal(11, 2)");
        });

        modelBuilder.Entity<WUSER>(entity =>
        {
            entity.HasKey(e => e.USERID);

            entity.ToTable("WUSERS");

            entity.Property(e => e.USERID)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PASSWD)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SHOPNO)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.UEMPDEG)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.UOUTDATE)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.USERNAME)
                .IsRequired()
                .HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}