#region

using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace ProductService.Infrastructure.Database;

public static class DbInitializer
{
	public static async Task InitializeDatabaseDataAsync(this IServiceProvider services)
	{
		await using var scope = services.CreateAsyncScope();
	}


	public static async Task MigrateDatabaseAsync(this IServiceProvider services)
	{
		using var scope = services.CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
		await context.Database.MigrateAsync();
	}


	public static async Task CreateDatabaseFromContextIfNotExistsAsync(this IServiceProvider services)
	{
		using var scope = services.CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
		await context.Database.EnsureCreatedAsync();
	}


	private static string LoremIpsum(int minWords, int maxWords,
		int minSentences, int maxSentences,
		int numParagraphs)
	{
		var words = new[]
		{
			"lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
			"adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
			"tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"
		};

		var rand = new Random();
		var numSentences = rand.Next(maxSentences - minSentences)
		                   + minSentences + 1;
		var numWords = rand.Next(maxWords - minWords) + minWords + 1;

		var result = new StringBuilder();

		for (var p = 0; p < numParagraphs; p++)
		for (var s = 0; s < numSentences; s++)
		{
			for (var w = 0; w < numWords; w++)
			{
				if (w > 0)
					result.Append(" ");
				result.Append(words[rand.Next(words.Length)]);
			}

			result.Append(". ");
		}

		return result.ToString();
	}
}