﻿// <auto-generated />
using System;
using GraphQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GraphQL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210615003520_Refactoring")]
    partial class Refactoring
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("GraphQL.Data.Models.Attendee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("EmailAddress")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Attendees");
                });

            modelBuilder.Entity("GraphQL.Data.Models.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Abstract")
                        .HasMaxLength(4000)
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int?>("TrackId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TrackId1")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TrackId1");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("GraphQL.Data.Models.SessionAttendee", b =>
                {
                    b.Property<int>("SessionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AttendeeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("SessionId", "AttendeeId");

                    b.HasIndex("AttendeeId");

                    b.ToTable("SessionAttendee");
                });

            modelBuilder.Entity("GraphQL.Data.Models.SessionSpeaker", b =>
                {
                    b.Property<int>("SessionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SpeakerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("SessionId", "SpeakerId");

                    b.HasIndex("SpeakerId");

                    b.ToTable("SessionSpeaker");
                });

            modelBuilder.Entity("GraphQL.Data.Models.Track", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("GraphQL.Data.Speaker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bio")
                        .HasMaxLength(4000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("WebSite")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Speakers");
                });

            modelBuilder.Entity("GraphQL.Data.Models.Session", b =>
                {
                    b.HasOne("GraphQL.Data.Models.Track", "Track")
                        .WithMany("Sessions")
                        .HasForeignKey("TrackId1");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("GraphQL.Data.Models.SessionAttendee", b =>
                {
                    b.HasOne("GraphQL.Data.Models.Attendee", "Attendee")
                        .WithMany("SessionAttendees")
                        .HasForeignKey("AttendeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GraphQL.Data.Models.Session", "Session")
                        .WithMany("SessionAttendees")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attendee");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("GraphQL.Data.Models.SessionSpeaker", b =>
                {
                    b.HasOne("GraphQL.Data.Models.Session", "Session")
                        .WithMany("SessionSpeakers")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GraphQL.Data.Speaker", "Speaker")
                        .WithMany("SessionSpeakers")
                        .HasForeignKey("SpeakerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Session");

                    b.Navigation("Speaker");
                });

            modelBuilder.Entity("GraphQL.Data.Models.Attendee", b =>
                {
                    b.Navigation("SessionAttendees");
                });

            modelBuilder.Entity("GraphQL.Data.Models.Session", b =>
                {
                    b.Navigation("SessionAttendees");

                    b.Navigation("SessionSpeakers");
                });

            modelBuilder.Entity("GraphQL.Data.Models.Track", b =>
                {
                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("GraphQL.Data.Speaker", b =>
                {
                    b.Navigation("SessionSpeakers");
                });
#pragma warning restore 612, 618
        }
    }
}
