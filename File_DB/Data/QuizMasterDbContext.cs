namespace File_DB.Data
{
	using File_DB.BaseProvider.Config;
	using File_DB.Data.Models;
	using Microsoft.EntityFrameworkCore;

	public class QuizMasterDbContext : DbContext
	{
		public QuizMasterDbContext()
		{			
		}

		public QuizMasterDbContext(DbContextOptions dbContextOptions)
			: base(dbContextOptions)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(Config.ConnectionString);
			}
		}

		//protected override void OnModelCreating(ModelBuilder modelBuilder)
		//{
		//	base.OnModelCreating(modelBuilder);
		//	modelBuilder.Entity<Quiz>().ToTable("Quizzes");
		//	modelBuilder.Entity<Question>().ToTable("Questions");
		//	modelBuilder.Entity<Answer>().ToTable("Answers");
		//	modelBuilder.Entity<User>().ToTable("Users");
		//	modelBuilder.Entity<GameSession>().ToTable("GameSessions");
		//}
		public DbSet<ConfigTable> ConfigTables { get; set; }
		public DbSet<Question> Questions { get; set; }
		public DbSet<Answer> Answers { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<GameSession> GameSessions { get; set; }
	}
	{
	}
}
