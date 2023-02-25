using System;
using System.Collections.Generic;

namespace ProductService.Domain;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public byte[]? Icon { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
