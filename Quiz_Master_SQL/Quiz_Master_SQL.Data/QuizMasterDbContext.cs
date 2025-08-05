namespace Quiz_Master_SQL.Quiz_Master_SQL.Data
{
	using Common.Classes;
	using File_DB.Data.Models;
	using global::Quiz_Master_SQL.Data.Models;
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
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(DBConfig.ConnectionString);
			}

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<ConfigTableDB>(e => e.HasNoKey());
			//	modelBuilder.Entity<Quiz>().ToTable("Quizzes");
			//	modelBuilder.Entity<Question>().ToTable("Questions");
			//	modelBuilder.Entity<Answer>().ToTable("Answers");
			//	modelBuilder.Entity<User>().ToTable("Users");
			//	modelBuilder.Entity<GameSession>().ToTable("GameSessions");
		}
		public DbSet<ConfigTableDB> ConfigTablesDB { get; set; }
		public DbSet<UserDB> UsersDB { get; set; }
		public DbSet<UserDataDB> UsersDataDB { get; set; }
		//public DbSet<Answer> Answers { get; set; }
		//public DbSet<GameSession> GameSessions { get; set; }
	}
}
