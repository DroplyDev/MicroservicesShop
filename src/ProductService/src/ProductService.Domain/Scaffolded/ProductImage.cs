namespace ProductService.Domain;

public partial class ProductImage
{
    public int Id { get; set; }

    public byte[] Icon { get; set; } = null!;

    public int ProductId { get; set; }

    /// <summary>
    ///     Many to one navigation for Product table
    /// </summary>
    public virtual Product Product { get; set; } = null!;
}