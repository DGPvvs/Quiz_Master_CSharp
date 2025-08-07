namespace Quiz_Master_SQL.Quiz_Master_SQL.Data
{
	using Common.Classes;
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

			modelBuilder.Entity<LikedQuizzeDB>(e => e.HasKey(lq =>new { lq.QuizId, lq.UserId }));
			modelBuilder.Entity<LikedQuizzeDB>(e => e.HasOne(x=>x.QuizDBs).WithMany(b => b.LikedQuizzes).OnDelete(DeleteBehavior.NoAction));
			modelBuilder.Entity<FavoritedQuizzeDB>(e => e.HasKey(fq => new { fq.QuizId, fq.UserId }));
			modelBuilder.Entity<FavoritedQuizzeDB>(e => e.HasOne(x => x.Quiz).WithMany(b => b.FavoritedQuizzes).OnDelete(DeleteBehavior.NoAction));

			//modelBuilder.Entity<LikedQuizzeDB>(
			//	kasO
			//	)
			//	.HasOne(x => x.. .Instalation)
			//	.WithMany(b => b.ApplicationUserPlantInstalations)
			//	.OnDelete(DeleteBehavior.NoAction);
			//	modelBuilder.Entity<Quiz>().ToTable("Quizzes");
			//	modelBuilder.Entity<Question>().ToTable("Questions");
			//	modelBuilder.Entity<Answer>().ToTable("Answers");
			//	modelBuilder.Entity<User>().ToTable("Users");
			//	modelBuilder.Entity<GameSession>().ToTable("GameSessions");
		}
		public DbSet<ConfigTableDB> ConfigTablesDB { get; set; }

		public DbSet<UserDB> UsersDB { get; set; }

		public DbSet<FinishedChallengeDB> FinishedChallengesDB { get; set; }

		public DbSet<LikedQuizzeDB> LikedQuizzesDB { get; set; }

		public DbSet<QuizDB> QuizzesDB { get; set; }

		public DbSet<FavoritedQuizzeDB> FavoritedQuizzes { get; set; }

		public DbSet<MessagesDB> Messages { get; set; }
	}
}
