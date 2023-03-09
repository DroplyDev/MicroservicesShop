namespace ProductService.Infrastructure.Attributes;

public sealed class ImageExtensionFilterAttribute : FileExtensionFilterAttribute
{
	public ImageExtensionFilterAttribute() : base(".jpg", ".jpeg", ".png")
	{
	}
}