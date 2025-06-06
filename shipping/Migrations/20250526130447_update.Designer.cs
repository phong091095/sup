﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using shipping.DBContext;

#nullable disable

namespace shipping.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20250526130447_update")]
    partial class update
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CategoriesService.Models.DanhMuc", b =>
                {
                    b.Property<int>("IDDanhMuc")
                        .HasColumnType("int");

                    b.Property<int>("CapDanhMuc")
                        .HasColumnType("int");

                    b.Property<bool>("IsLeaf")
                        .HasColumnType("bit");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenDanhMuc")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("IDDanhMuc");

                    b.ToTable("DanhMucs");
                });

            modelBuilder.Entity("CategoriesService.Models.HinhAnhDanhMuc", b =>
                {
                    b.Property<int>("IDHinhAnhDanhMuc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDHinhAnhDanhMuc"));

                    b.Property<byte[]>("HinhAnh")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("IDDanhMuc")
                        .HasColumnType("int");

                    b.HasKey("IDHinhAnhDanhMuc");

                    b.HasIndex("IDDanhMuc");

                    b.ToTable("HinhAnhDanhMucs");
                });

            modelBuilder.Entity("shipping.Model.AspNetUsers", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailConfirmed")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("LockoutEnd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumberConfirmed")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("shipping.Model.BienTheSanPham", b =>
                {
                    b.Property<string>("IDBienTheSanPham")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Gia")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<byte[]>("HinhAnh")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("IDSanPham")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SKU")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("IDBienTheSanPham");

                    b.HasIndex("IDSanPham");

                    b.ToTable("BienTheSanPham");
                });

            modelBuilder.Entity("shipping.Model.ChiTietBienTheSanPham", b =>
                {
                    b.Property<int>("IDChiTietBTSanPham")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDChiTietBTSanPham"));

                    b.Property<string>("IDBienTheSanPham")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("IDGiaTriBienTheSanPham")
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDChiTietBTSanPham");

                    b.HasIndex("IDBienTheSanPham");

                    b.HasIndex("IDGiaTriBienTheSanPham");

                    b.ToTable("ChiTietBienTheSanPham");
                });

            modelBuilder.Entity("shipping.Model.ChiTietDVVanChuyen", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IDCuaHang")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IDDonViVanChuyen")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<int>("PhiVanChuyen")
                        .HasColumnType("int");

                    b.Property<string>("ThoiGianDuKien")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThaiSuDung")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("IDDonViVanChuyen");

                    b.ToTable("ChiTietDVVanChuyen");
                });

            modelBuilder.Entity("shipping.Model.CuaHang", b =>
                {
                    b.Property<string>("IDCuaHang")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayDangKy")
                        .HasColumnType("datetime2");

                    b.Property<string>("TenCuaHang")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDCuaHang");

                    b.ToTable("CuaHang");
                });

            modelBuilder.Entity("shipping.Model.DonViVanChuyen", b =>
                {
                    b.Property<string>("IDDonViVanChuyen")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoDienThoai")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenDonVi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDDonViVanChuyen");

                    b.ToTable("DonViVanChuyen");
                });

            modelBuilder.Entity("shipping.Model.GiaTriBTSP", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("IDThuocTinh")
                        .HasColumnType("int");

                    b.Property<string>("TenGiaTri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("IDThuocTinh");

                    b.ToTable("GiaTriBTSP");
                });

            modelBuilder.Entity("shipping.Model.Images", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("HinhAnh")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("IDSanPham")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("IDSanPham");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("shipping.Model.LogActiveties", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LogActiveties");
                });

            modelBuilder.Entity("shipping.Model.SanPham", b =>
                {
                    b.Property<string>("IDSanPham")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IDCuaHang")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IDDanhMuc")
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("TenSanPham")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrangThai")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDSanPham");

                    b.HasIndex("IDDanhMuc");

                    b.ToTable("SanPham");
                });

            modelBuilder.Entity("shipping.Model.ThuocTinhBTSP", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("TenThuocTinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("ThuocTinhBTSP");
                });

            modelBuilder.Entity("CategoriesService.Models.HinhAnhDanhMuc", b =>
                {
                    b.HasOne("CategoriesService.Models.DanhMuc", "DanhMuc")
                        .WithMany("HinhAnhDanhMucs")
                        .HasForeignKey("IDDanhMuc")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DanhMuc");
                });

            modelBuilder.Entity("shipping.Model.BienTheSanPham", b =>
                {
                    b.HasOne("shipping.Model.SanPham", "SanPham")
                        .WithMany("BienThes")
                        .HasForeignKey("IDSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("shipping.Model.ChiTietBienTheSanPham", b =>
                {
                    b.HasOne("shipping.Model.BienTheSanPham", "BienTheSanPham")
                        .WithMany("ChiTietBienThes")
                        .HasForeignKey("IDBienTheSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("shipping.Model.GiaTriBTSP", "GiaTri")
                        .WithMany("ChiTietBienThes")
                        .HasForeignKey("IDGiaTriBienTheSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BienTheSanPham");

                    b.Navigation("GiaTri");
                });

            modelBuilder.Entity("shipping.Model.ChiTietDVVanChuyen", b =>
                {
                    b.HasOne("shipping.Model.DonViVanChuyen", "DonViVanChuyen")
                        .WithMany("ChiTietDVVanChuyens")
                        .HasForeignKey("IDDonViVanChuyen")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DonViVanChuyen");
                });

            modelBuilder.Entity("shipping.Model.GiaTriBTSP", b =>
                {
                    b.HasOne("shipping.Model.ThuocTinhBTSP", "ThuocTinh")
                        .WithMany("GiaTris")
                        .HasForeignKey("IDThuocTinh")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ThuocTinh");
                });

            modelBuilder.Entity("shipping.Model.Images", b =>
                {
                    b.HasOne("shipping.Model.SanPham", "SanPham")
                        .WithMany("Images")
                        .HasForeignKey("IDSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("shipping.Model.SanPham", b =>
                {
                    b.HasOne("CategoriesService.Models.DanhMuc", "DanhMuc")
                        .WithMany()
                        .HasForeignKey("IDDanhMuc")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DanhMuc");
                });

            modelBuilder.Entity("CategoriesService.Models.DanhMuc", b =>
                {
                    b.Navigation("HinhAnhDanhMucs");
                });

            modelBuilder.Entity("shipping.Model.BienTheSanPham", b =>
                {
                    b.Navigation("ChiTietBienThes");
                });

            modelBuilder.Entity("shipping.Model.DonViVanChuyen", b =>
                {
                    b.Navigation("ChiTietDVVanChuyens");
                });

            modelBuilder.Entity("shipping.Model.GiaTriBTSP", b =>
                {
                    b.Navigation("ChiTietBienThes");
                });

            modelBuilder.Entity("shipping.Model.SanPham", b =>
                {
                    b.Navigation("BienThes");

                    b.Navigation("Images");
                });

            modelBuilder.Entity("shipping.Model.ThuocTinhBTSP", b =>
                {
                    b.Navigation("GiaTris");
                });
#pragma warning restore 612, 618
        }
    }
}
