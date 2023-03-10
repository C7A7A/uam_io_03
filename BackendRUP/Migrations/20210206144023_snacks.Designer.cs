// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WatchTogetherAPI.Data;

namespace WatchTogetherAPI.Migrations
{
    [DbContext(typeof(WatchTogetherContext))]
    [Migration("20210206144023_snacks")]
    partial class snacks
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("LobbyUser", b =>
                {
                    b.Property<int>("LobbiesParticipatedId")
                        .HasColumnType("int");

                    b.Property<int>("Participantsid")
                        .HasColumnType("int");

                    b.HasKey("LobbiesParticipatedId", "Participantsid");

                    b.HasIndex("Participantsid");

                    b.ToTable("LobbyUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MovieVotesUser", b =>
                {
                    b.Property<int>("VotedForMovieId")
                        .HasColumnType("int");

                    b.Property<int>("alreadyVotedid")
                        .HasColumnType("int");

                    b.HasKey("VotedForMovieId", "alreadyVotedid");

                    b.HasIndex("alreadyVotedid");

                    b.ToTable("MovieVotesUser");
                });

            modelBuilder.Entity("TermPropositionUser", b =>
                {
                    b.Property<int>("VotedForTimeId")
                        .HasColumnType("int");

                    b.Property<int>("alreadyVotedid")
                        .HasColumnType("int");

                    b.HasKey("VotedForTimeId", "alreadyVotedid");

                    b.HasIndex("alreadyVotedid");

                    b.ToTable("TermPropositionUser");
                });

            modelBuilder.Entity("WatchTogetherAPI.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("WatchTogetherAPI.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("WatchTogetherAPI.Models.Lobby", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("HostId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ProposedTermIndex")
                        .HasColumnType("int");

                    b.Property<int?>("SelectedMovieId")
                        .HasColumnType("int");

                    b.Property<int?>("SnackId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Term")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("HostId");

                    b.HasIndex("SelectedMovieId");

                    b.HasIndex("SnackId");

                    b.ToTable("Lobbies");
                });

            modelBuilder.Entity("WatchTogetherAPI.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<string>("Poster_path")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<float>("Rating")
                        .HasColumnType("float");

                    b.Property<DateTime>("Release_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Stills_path")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("WatchTogetherAPI.Models.MovieVotes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("LobbyId")
                        .HasColumnType("int");

                    b.Property<int?>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("votes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LobbyId");

                    b.HasIndex("MovieId");

                    b.ToTable("MovieVotes");
                });

            modelBuilder.Entity("WatchTogetherAPI.Models.Movies_genres", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("Movies_Genres");
                });

            modelBuilder.Entity("WatchTogetherAPI.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("Fromid")
                        .HasColumnType("int");

                    b.Property<int?>("LobbyId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int?>("Userid")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Fromid");

                    b.HasIndex("LobbyId");

                    b.HasIndex("Userid");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("WatchTogetherAPI.Models.PreferredHours", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("EndTime")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("StartTime")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("day")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<int>("userID")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("PreferredHours");
                });

            modelBuilder.Entity("WatchTogetherAPI.Models.Snack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Link")
                        .HasColumnType("longtext");

                    b.Property<string>("Thumbnail")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Snacks");
                });

            modelBuilder.Entity("WatchTogetherAPI.Models.TermProposition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("LobbyId")
                        .HasColumnType("int");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("LobbyId");

                    b.ToTable("TermPropostions");
                });

            modelBuilder.Entity("WatchTogetherAPI.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("IdentityId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("friends")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<bool>("is_deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("preferredgenres")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("refresh_token")
                        .HasColumnType("longtext");

                    b.Property<string>("surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("watchedmovies")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.HasIndex("IdentityId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LobbyUser", b =>
                {
                    b.HasOne("WatchTogetherAPI.Models.Lobby", null)
                        .WithMany()
                        .HasForeignKey("LobbiesParticipatedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WatchTogetherAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("Participantsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WatchTogetherAPI.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WatchTogetherAPI.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WatchTogetherAPI.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WatchTogetherAPI.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieVotesUser", b =>
                {
                    b.HasOne("WatchTogetherAPI.Models.MovieVotes", null)
                        .WithMany()
                        .HasForeignKey("VotedForMovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WatchTogetherAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("alreadyVotedid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TermPropositionUser", b =>
                {
                    b.HasOne("WatchTogetherAPI.Models.TermProposition", null)
                        .WithMany()
                        .HasForeignKey("VotedForTimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WatchTogetherAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("alreadyVotedid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WatchTogetherAPI.Models.Lobby", b =>
                {
                    b.HasOne("WatchTogetherAPI.Models.User", "Host")
                        .WithMany("LobbiesHosted")
                        .HasForeignKey("HostId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WatchTogetherAPI.Models.Movie", "SelectedMovie")
                        .WithMany()
                        .HasForeignKey("SelectedMovieId");

                    b.HasOne("WatchTogetherAPI.Models.Snack", "Snack")
                        .WithMany()
                        .HasForeignKey("SnackId");

                    b.Navigation("Host");

                    b.Navigation("SelectedMovie");

                    b.Navigation("Snack");
                });

            modelBuilder.Entity("WatchTogetherAPI.Models.MovieVotes", b =>
                {
                    b.HasOne("WatchTogetherAPI.Models.Lobby", null)
                        .WithMany("ProposedMovies")
                        .HasForeignKey("LobbyId");

                    b.HasOne("WatchTogetherAPI.Models.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("WatchTogetherAPI.Models.Movies_genres", b =>
                {
                    b.HasOne("WatchTogetherAPI.Models.Genre", "Genre")
                        .WithMany("Movies_Genres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WatchTogetherAPI.Models.Movie", "Movie")
                        .WithMany("Movies_Genres")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("WatchTogetherAPI.Models.Notification", b =>
                {
                    b.HasOne("WatchTogetherAPI.Models.User", "From")
                        .WithMany()
                        .HasForeignKey("Fromid");

                    b.HasOne("WatchTogetherAPI.Models.Lobby", "Lobby")
                        .WithMany()
                        .HasForeignKey("LobbyId");

                    b.HasOne("WatchTogetherAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Userid");

                    b.Navigation("From");

                    b.Navigation("Lobby");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WatchTogetherAPI.Models.TermProposition", b =>
                {
                    b.HasOne("WatchTogetherAPI.Models.Lobby", null)
                        .WithMany("ProposedTerms")
                        .HasForeignKey("LobbyId");
                });

            modelBuilder.Entity("WatchTogetherAPI.Models.User", b =>
                {
                    b.HasOne("WatchTogetherAPI.Models.AppUser", "Identity")
                        .WithMany()
                        .HasForeignKey("IdentityId");

                    b.Navigation("Identity");
                });

            modelBuilder.Entity("WatchTogetherAPI.Models.Genre", b =>
                {
                    b.Navigation("Movies_Genres");
                });

            modelBuilder.Entity("WatchTogetherAPI.Models.Lobby", b =>
                {
                    b.Navigation("ProposedMovies");

                    b.Navigation("ProposedTerms");
                });

            modelBuilder.Entity("WatchTogetherAPI.Models.Movie", b =>
                {
                    b.Navigation("Movies_Genres");
                });

            modelBuilder.Entity("WatchTogetherAPI.Models.User", b =>
                {
                    b.Navigation("LobbiesHosted");
                });
#pragma warning restore 612, 618
        }
    }
}
