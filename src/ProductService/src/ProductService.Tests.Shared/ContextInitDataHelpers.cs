using AutoBogus;
using Microsoft.EntityFrameworkCore;
using ProductService.Domain;

namespace ProductService.Tests.Shared;

public static class ContextInitDataHelpers
{
	public static List<Category> InitCategories(this DbContext context, int count = 5)
	{
		var data = new AutoFaker<Category>()
			.RuleFor(c => c.Id, s => s.IndexFaker)
			.RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0])
			.RuleFor(p => p.Description, f => f.Lorem.Sentence())
			.Generate(count);
		context.AddRange(data);
		return data;
	}

	public static List<Product> InitProducts(this DbContext context, int count = 5)
	{
		var categories = context.InitCategories();
		var data = new AutoFaker<Product>()
			.RuleFor(c => c.Id, s => s.IndexFaker)
			.RuleFor(p => p.Name, f => f.Commerce.ProductName())
			.RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
			.RuleFor(p => p.Quantity, f => f.Random.Int(1, 100))
			.RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
			.Generate(count);
		foreach (var item in data)
			item.Category = categories.PickRandom();
		context.AddRange(data);
		return data;
	}

	public static List<ProductImage> InitProductImages(this DbContext context, int count = 5)
	{
		var data = new AutoFaker<ProductImage>()
			.RuleFor(c => c.Id, s => s.IndexFaker)
			.Generate(count);
		context.AddRange(data);
		return data;
	}

	public static TItem PickRandom<TItem>(this List<TItem> items)
	{
		var rnd = new Random();
		return items[rnd.Next(0, items.Count - 1)];
	}

	public static async Task<byte[]> DownloadImageAsync(string url)
	{
		using var httpClient = new HttpClient();
		var response = await httpClient.GetAsync(url);
		if (!response.IsSuccessStatusCode)
			throw new IOException($"Failed to download image from {url}: {response.ReasonPhrase}");
		return await response.Content.ReadAsByteArrayAsync();
	}
}