namespace Quiz_Master_Game_Play.Questions
{
	using Common.Enums;
	using Common.IO.Contract;
	using System.Collections.Generic;
	using System.Net.Security;
	using System.Reflection.PortableExecutable;

	public class Question
	{
		private bool isTest;
		private byte numOfAnswers;
		private uint points;
		private QuestionType qt;
		private string correctAnswer;
		private string description;

		private IWriter writer;
		private IReader reader;

		public Question(IWriter writer, IReader reader, string description, string correctAnswer, uint points, bool isTest, byte numOfAnswers)
		{
			this.writer = writer;
			this.reader = reader;
			this.description = description;
			this.correctAnswer = correctAnswer;
			this.points = points;
			this.isTest = isTest;
			this.numOfAnswers = numOfAnswers;
		}

		protected string Description => this.description;

		protected uint Points
		{
			get => this.points;
			set => this.points = value;
		}

		protected QuestionType Qt
		{
			get => this.qt;
			set => this.qt = value;
		}

		protected string CorrectAnswer => this.correctAnswer;

		protected bool IsTest => this.isTest;

		protected byte NumOfAnswers => this.numOfAnswers;

		protected IReader Reader => this.reader;

		protected IWriter Writer => this.writer;

		protected void PrintTestCondition()
		{
			if (this.IsTest)
			{
				this.writer.WriteLine($"The correct answer is: {this.correctAnswer}");
			}
		}

		protected virtual void PrintQuestion()
		{
			this.writer.Write("Enter your answer here: ");
		}

		protected virtual bool AnswerAQuestion() => true;
	}
}
