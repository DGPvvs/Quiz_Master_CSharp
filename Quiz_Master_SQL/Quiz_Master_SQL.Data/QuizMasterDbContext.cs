namespace Quiz_Master_SQL.Quiz_Master_SQL.Data
{
	using Common.Classes;
	using global::Quiz_Master_SQL.Data.Configuration.EntityConfiguration;
	using global::Quiz_Master_SQL.Data.Configuration.EntitySeed;
	using global::Quiz_Master_SQL.Data.Models;
	using Microsoft.EntityFrameworkCore;

	public class QuizMasterDbContext : DbContext
	{
		private bool isSeeded = false;

		public QuizMasterDbContext()
		{
		}

		public QuizMasterDbContext(bool isSeeded) : base()
		{
			this.isSeeded = isSeeded;
		}

		public QuizMasterDbContext(
			DbContextOptions dbContextOptions
			, bool isSeeded
			) : base(dbContextOptions)
		{
			this.isSeeded = isSeeded;
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
			//Debugger.Launch();
			
			modelBuilder.ApplyConfiguration(new LikedQuizzeDBConfiguration());
			modelBuilder.ApplyConfiguration(new FavoritedQuizzeDBConfiguration());
			modelBuilder.ApplyConfiguration(new MultiplyAnswersDBConfigure());

			if (this.isSeeded)
			{
				// Зареждане на тестовите данни в базата
				modelBuilder.ApplyConfiguration(new ConfigTableDBSeeder());
				modelBuilder.ApplyConfiguration(new UserDBTableSeeder());


				//modelBuilder.ApplyConfiguration(new TechnologicalPositionSeeder());
				//modelBuilder.ApplyConfiguration(new WorkingShiftSeeder());
				//modelBuilder.ApplyConfiguration(new RoleSeeder());
				//modelBuilder.ApplyConfiguration(new ApplicationUserSeeder());
				//modelBuilder.ApplyConfiguration(new UserRoleSeeder());
				//Debugger.Launch();
				//modelBuilder.ApplyConfiguration(new ChangedScheduleSeede());
				//modelBuilder.ApplyConfiguration(new ApplicationUserPlantInstalationSeeder());
			}

			base.OnModelCreating(modelBuilder);
		}
		public DbSet<ConfigTableDB> ConfigTablesDB { get; set; }

		public DbSet<UserDB> UsersDB { get; set; }

		public DbSet<FinishedChallengeDB> FinishedChallengesDB { get; set; }

		public DbSet<LikedQuizzeDB> LikedQuizzesDB { get; set; }

		public DbSet<QuizDB> QuizzesDB { get; set; }

		public DbSet<FavoritedQuizzeDB> FavoritedQuizzes { get; set; }

		public DbSet<MessagesDB> Messages { get; set; }
		
		public DbSet<MultiplyAnswersDB> MultiplyAnswers { get; set; }
		
		public DbSet<TrueOrFalseQuestionDB> TrueOrFalseQuestions { get; set; }
		
		public DbSet<SingleChoiceQuestionDB> SingleChoiceQuestions { get; set; }
		
		public DbSet<ShortAnswerQuestionDB> ShortAnswerQuestions { get; set; }
	}
}
