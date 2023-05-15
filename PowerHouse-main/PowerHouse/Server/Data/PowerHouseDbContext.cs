using Microsoft.EntityFrameworkCore;
using PowerHouse.Server.Models;

namespace PowerHouse.Server.Data
{
    public class PowerHouseDbContext : DbContext
    {
        public PowerHouseDbContext(DbContextOptions<PowerHouseDbContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Conversation> Conversation { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
			#region SeedData
			builder.Entity<User>().HasData(
				new List<User>()
				{
					new User {
						Id = Guid.Parse("b1873f2f-81f0-4683-aa7f-33c3e9845df2"),
						Username = "Carlo Goretti",
						Email = "carlo.goretti@live.se"
					},
					new User {
						Id = Guid.Parse("a6bd1e41-b8f0-4434-8387-39329ae2e1a1"),
						Username = "Bambi Goretti",
						Email = "Bambi.goretti@live.se"
					}
				});

            builder.Entity<Conversation>().HasData(

				new List<Conversation>
				{
					new Conversation
					{
						Id = Guid.Parse("01f424df-72cd-4dd0-a0a5-929ddbbda931"),
						Topic = "Exploring the World of Microorganisms: The Hidden Life in Your Backyard",
						IsPublic = true,
						AuthorId = Guid.Parse("b1873f2f-81f0-4683-aa7f-33c3e9845df2"),
					},
					new Conversation
					{
						Id = Guid.Parse("a1d66b82-3313-485e-b464-79ad3bd1f84e"),
						Topic = "The Science of Sleep: Understanding the Importance of a Good Night's Rest",
						IsPublic = true,
						AuthorId = Guid.Parse("b1873f2f-81f0-4683-aa7f-33c3e9845df2")
					},
					new Conversation
					{
						Id = Guid.Parse("a322d519-142c-44ad-adc4-fd0be0b5b752"),
						Topic = "Revolutionizing Agriculture: The Future of Sustainable Farming Practices",
						IsPublic = true,
						AuthorId = Guid.Parse("b1873f2f-81f0-4683-aa7f-33c3e9845df2")
					},
					new Conversation
					{
						Id = Guid.Parse("7ca6277d-f149-4d92-bfec-aeeb47881689"),
						Topic = "Beyond the Horizon: The Fascinating World of Space Exploration",
						IsPublic = true,
						AuthorId = Guid.Parse("b1873f2f-81f0-4683-aa7f-33c3e9845df2")
					},
					new Conversation
					{
						Id = Guid.Parse("9990e740-142d-4a11-bb5d-8fb262fdf74a"),
						Topic = "The Power of the Mind: Understanding the Science of Memory and Cognition",
						IsPublic = true,
						AuthorId = Guid.Parse("b1873f2f-81f0-4683-aa7f-33c3e9845df2")
					}
				}
			);

           

            builder.Entity<Report>().HasData(new List<Report>            {
                 
                new Report
                {
                    Id = Guid.Parse("309a4766-5a12-4211-bb99-9635b7ed1038"),
                    Reason = "Bad text",
                    Reported = DateTime.UtcNow,
                    ConversationId = Guid.Parse("a322d519-142c-44ad-adc4-fd0be0b5b752"),
                    ReporterId = Guid.Parse("b1873f2f-81f0-4683-aa7f-33c3e9845df2"),
                    Type = Shared.Enums.Type.Conversation
                },
                new Report
                {
                    Id = Guid.Parse("9b7f4c45-940d-4b4f-90ae-9548c2c091b4"),
                    Reason = "Hurt my feelings",
                    Reported = DateTime.UtcNow,
                    ConversationId = Guid.Parse("a1d66b82-3313-485e-b464-79ad3bd1f84e"),
                    ReporterId = Guid.Parse("b1873f2f-81f0-4683-aa7f-33c3e9845df2"),
                    Type = Shared.Enums.Type.Conversation
                }
            });
            #endregion
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        private void OnBeforeSaving()
        {
            foreach (var entry in ChangeTracker.Entries<Message>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
						entry.CurrentValues["IsDeleted"] = false;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                    case EntityState.Modified:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsEdited"] = true;
                        entry.CurrentValues["EditDate"] = DateTime.UtcNow;
                        break;

                }
            }

            foreach (var entry in ChangeTracker.Entries<Report>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["Reported"] = DateTime.UtcNow;
                        break;
                }
            }
        }
    }
}
