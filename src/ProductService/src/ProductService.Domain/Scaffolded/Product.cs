// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProductService.Domain;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public byte[]? Thumbnail { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public bool IsAvailable { get; set; }

    public int CategoryId { get; set; }

    /// <summary>
    ///     Many to one navigation for Category table
    /// </summary>
    public virtual Category Category { get; set; } = null!;

    /// <summary>
    ///     One to many navigation for ProductImage table
    /// </summary>
    public virtual ICollection<ProductImage> ProductImages { get; } = new HashSet<ProductImage>();
}
