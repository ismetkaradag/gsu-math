﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace gsu_math.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20230321184758_secondmig")]
    partial class secondmig
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("gsu_math.Models.Bildirim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("At_created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Yazi")
                        .HasColumnType("longtext");

                    b.Property<string>("action")
                        .HasColumnType("longtext");

                    b.Property<string>("controller")
                        .HasColumnType("longtext");

                    b.Property<string>("from")
                        .HasColumnType("longtext");

                    b.Property<bool>("is_read")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("otherid")
                        .HasColumnType("longtext");

                    b.Property<string>("to")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Bildirim");
                });

            modelBuilder.Entity("gsu_math.Models.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AtCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Author")
                        .HasColumnType("longtext");

                    b.Property<string>("Metin")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("duzenlememetni")
                        .HasColumnType("longtext");

                    b.Property<string>("in_editing")
                        .HasColumnType("longtext");

                    b.Property<bool>("is_active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("slug")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Blog");
                });

            modelBuilder.Entity("gsu_math.Models.Duyuru", b =>
                {
                    b.Property<int>("DuyuruId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Baslik")
                        .HasColumnType("longtext");

                    b.Property<string>("Foto")
                        .HasColumnType("longtext");

                    b.Property<string>("Metin")
                        .HasColumnType("longtext");

                    b.HasKey("DuyuruId");

                    b.ToTable("Duyuru");
                });

            modelBuilder.Entity("gsu_math.Models.Etkinlik", b =>
                {
                    b.Property<int>("EtkinlikId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Baslik")
                        .HasColumnType("longtext");

                    b.Property<string>("Foto")
                        .HasColumnType("longtext");

                    b.Property<string>("Metin")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("atCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("endDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("startDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("EtkinlikId");

                    b.ToTable("Etkinlik");
                });

            modelBuilder.Entity("gsu_math.Models.ForumBaslik", b =>
                {
                    b.Property<int>("ForumBaslikId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AtCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Baslik")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("begenisayisi")
                        .HasColumnType("int");

                    b.Property<int>("cevapsayisi")
                        .HasColumnType("int");

                    b.Property<string>("creater")
                        .HasColumnType("longtext");

                    b.Property<string>("slug")
                        .HasColumnType("longtext");

                    b.HasKey("ForumBaslikId");

                    b.ToTable("ForumBaslik");
                });

            modelBuilder.Entity("gsu_math.Models.ForumCevap", b =>
                {
                    b.Property<int>("ForumCevapId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BegeniSayisi")
                        .HasColumnType("int");

                    b.Property<int>("ForumBaslikId")
                        .HasColumnType("int");

                    b.Property<string>("Metin")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("at_created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("faydalibulanlar")
                        .HasColumnType("longtext");

                    b.Property<string>("username")
                        .HasColumnType("longtext");

                    b.HasKey("ForumCevapId");

                    b.ToTable("ForumCevap");
                });

            modelBuilder.Entity("gsu_math.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AdSoyad")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("AtCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ConfirmPwd")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Is_admin")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("is_mail_validated")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("is_validate")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("unique_string")
                        .HasColumnType("longtext");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
