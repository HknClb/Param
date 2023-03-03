using Domain.Entities;
using Domain.Entities.Common;
using Domain.Entities.CrossTables;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class MovieDbContext : IdentityDbContext<User, Role, string>
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Director> Directors { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<MovieStaff> MovieStaff { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Star> Stars { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Movie>()
                .Property(e => e.Price)
                .HasColumnType("decimal(18,2)");
            builder.Entity<Movie>()
                .HasMany(e => e.Genres)
                .WithMany(e => e.Movies)
                .UsingEntity(entity => entity.ToTable("MovieGenres"));
            builder.Entity<Movie>()
                .HasMany(e => e.Stars)
                .WithMany(e => e.Movies)
                .UsingEntity<MovieStar>(entity =>
                {
                    entity.HasKey(e => new { e.MovieId, e.StarId });
                    entity.HasOne<Movie>().WithMany().HasForeignKey(e => e.MovieId);
                    entity.HasOne<Star>().WithMany().HasForeignKey(e => e.StarId).OnDelete(DeleteBehavior.Restrict);
                    entity.ToTable("MovieStars");
                });

            builder.Entity<Genre>()
                .HasKey(e => e.Id);
            builder.Entity<Genre>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
            builder.Entity<Genre>()
                .HasMany(e => e.FavoriteCustomers)
                .WithMany(e => e.FavoriteGenres)
                .UsingEntity(e => e.ToTable("UsersFavoriteGenres"));

            builder.Entity<Order>()
                .HasKey(e => new { e.UserId, e.MovieId });
            builder.Entity<Order>()
                .Property(e => e.Price)
                .HasColumnType("decimal(18,2)");

            builder.Entity<User>()
                .HasMany(e => e.Roles)
                .WithMany(e => e.Users)
                .UsingEntity<IdentityUserRole<string>>(entity =>
                {
                    entity.HasKey("UserId", "RoleId"); // Important! Sorting of keys should be "UserId", "RoleId".
                    entity.HasOne<User>().WithMany().HasForeignKey("UserId");
                    entity.HasOne<Role>().WithMany().HasForeignKey("RoleId");
                    entity.ToTable("AspNetUserRoles");
                });

            #region Default Genres Inıtialize
            Genre[] genres = new Genre[]
            {
                new("Action") { Id = 1 },
                new("Comedy") { Id = 2 },
                new("Drama") { Id = 3 },
                new("Fantasy") { Id = 4 },
                new("Horror") { Id = 5 },
                new("Mystery") { Id = 6 },
                new("Romance") { Id = 7 },
                new("Thriller") { Id = 8 },
                new("Western") { Id = 9 }
             };
            builder.Entity<Genre>().HasData(genres);
            #endregion

            #region Default Account Initialize
            Role[] roles = new Role[]
            {
                new()
                {
                    Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    Name = "customer",
                    NormalizedName = "CUSTOMER".ToUpper()
                },
                new()
                {
                    Id = "7f3s213a-9l2q-321f-24ea-321f12op1453",
                    Name = "admin",
                    NormalizedName = "ADMIN".ToUpper()
                }
            };
            builder.Entity<Role>().HasData(roles);

            var hasher = new PasswordHasher<User>();
            User user = new()
            {
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                Name = "Hakan",
                Surname = "ÇELEBİ",
                Email = "hknclb00@gmail.com",
                NormalizedEmail = "HKNCLB00@GMAIL.COM",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                PhoneNumber = "+90 538 017 8740",
                SecurityStamp = "QTC5PI3U6GFOVY2KSSYWA32ZYUMFBPF6",
                ConcurrencyStamp = "d8599b1a-40e3-40a5-a033-6624d2123d21",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            user.PasswordHash = hasher.HashPassword(user, "admin");
            builder.Entity<User>().HasData(user);

            IdentityUserRole<string>[] userRoles = new IdentityUserRole<string>[]
            {
                new()
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
                },
                new()
                {
                    RoleId = "7f3s213a-9l2q-321f-24ea-321f12op1453",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
            #endregion                             

            base.OnModelCreating(builder);
        }

        private void Interceptor()
        {
            var entityEntries = ChangeTracker.Entries<Entity>();

            foreach (var entry in entityEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Deleted:
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    default:
                        break;
                }
            }
        }

        #region SaveChanges Overrides
        public override int SaveChanges()
        {
            Interceptor();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            Interceptor();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            Interceptor();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            Interceptor();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        #endregion
    }
}
