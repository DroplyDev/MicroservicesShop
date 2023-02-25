using System;
using System.Collections.Generic;

namespace ProductService.Domain;

public partial class ProductImage
{
    public int Id { get; set; }

    public byte[] Icon { get; set; } = null!;

    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
