// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoBogus;
using Microsoft.EntityFrameworkCore;
using ProductService.Domain;

namespace ProductService.Tests.Shared;

public static class ContextInitDataHelpers
{
    public static List<Category> InitCategories(this DbContext context, int categoryCount = 5)
    {
        var data = new AutoFaker<Category>()
            .RuleFor(c => c.Id, _ => 0)
            .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0])
            .RuleFor(p => p.Description, f => f.Lorem.Letter(500))
            .Generate(categoryCount);
        context.AddRange(data);
        context.SaveChanges();

        return data;
    }

    public static List<Product> InitProducts(this DbContext context, int productCount = 5, int categoryCount = 5)
    {
        var categories = context.InitCategories(categoryCount);
        var data = new AutoFaker<Product>()
            .RuleFor(c => c.Id, _ => 0)
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Lorem.Letter(500))
            .RuleFor(p => p.Quantity, f => f.Random.Int(1, 100))
            .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
            .Generate(productCount);
        foreach (var item in data)
        {
            item.Category = categories.PickRandom();
        }

        context.AddRange(data);
        context.SaveChanges();

        return data;
    }

    public static List<ProductImage> InitProductImages(this DbContext context, int count = 5)
    {
        var data = new AutoFaker<ProductImage>()
            .RuleFor(c => c.Id, _ => 0)
            .Generate(count);
        context.AddRange(data);
        context.SaveChanges();

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
        {
            throw new IOException($"Failed to download image from {url}: {response.ReasonPhrase}");
        }

        return await response.Content.ReadAsByteArrayAsync();
    }

    public static void Clear(this DbContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

    public static async Task ClearAsync(this DbContext context)
    {
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
    }
}
