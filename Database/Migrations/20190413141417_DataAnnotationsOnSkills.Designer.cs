﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shadowrun.DataAccess;

namespace Shadowrun.DataAccess.Migrations
{
    [DbContext(typeof(ShadowDBContext))]
    [Migration("20190413141417_DataAnnotationsOnSkills")]
    partial class DataAnnotationsOnSkills
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Shadowrun.Model.Skill", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CanDefault");

                    b.Property<string>("Description");

                    b.Property<int?>("GroupID");

                    b.Property<int>("LinkedTo");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<int>("Type");

                    b.HasKey("ID");

                    b.HasIndex("GroupID");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("Shadowrun.Model.SkillGroup", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("SkillGroup");
                });

            modelBuilder.Entity("Shadowrun.Model.Specialization", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<int?>("SkillID");

                    b.HasKey("ID");

                    b.HasIndex("SkillID");

                    b.ToTable("Specializations");
                });

            modelBuilder.Entity("Shadowrun.Model.Skill", b =>
                {
                    b.HasOne("Shadowrun.Model.SkillGroup", "Group")
                        .WithMany("Skills")
                        .HasForeignKey("GroupID");
                });

            modelBuilder.Entity("Shadowrun.Model.Specialization", b =>
                {
                    b.HasOne("Shadowrun.Model.Skill", "Skill")
                        .WithMany("Specializations")
                        .HasForeignKey("SkillID");
                });
#pragma warning restore 612, 618
        }
    }
}