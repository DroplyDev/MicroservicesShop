namespace ProductService.Domain;

public partial class Category
{
	public int Id { get; set; }

	public string Name { get; set; } = null!;

	public string? Description { get; set; }

	public byte[]? Icon { get; set; }

	/// <summary>
	///     One to many navigation for Product table
	/// </summary>
	public virtual ICollection<Product> Products { get; } = new HashSet<Product>();
}