using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Utils;

namespace backend.Config
{
	public class ApplicationDbContext: DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

		public DbSet<User> Users { get; set; }
		public DbSet<Audio> Audios { get; set; }
		public DbSet<Playlist> Playlists { get; set; }
		public DbSet<PlaylistAudio> PlaylistAudios { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(EnvVariables.SUPABASE_URI, 
            npgsqlOptions => npgsqlOptions.EnableRetryOnFailure())
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine, LogLevel.Information);
    }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// define relation between playlist and audio
			modelBuilder.Entity<PlaylistAudio>()
				.HasKey(pa => new { pa.PlaylistId, pa.AudioId });

			modelBuilder.Entity<PlaylistAudio>()
				.HasOne(pa => pa.Playlist)
				.WithMany(p => p.PlaylistAudios)
				.HasForeignKey(pa => pa.PlaylistId);

			modelBuilder.Entity<PlaylistAudio>()
				.HasOne(pa => pa.Audio)
				.WithMany(a => a.PlaylistsAudios)
				.HasForeignKey(pa => pa.AudioId);

			// relation between playlist and user
			modelBuilder.Entity<Playlist>()
				.HasOne(p => p.Creator)
				.WithMany(u => u.Playlists)
				.HasForeignKey(p => p.CreatorId);
		}
	}
}