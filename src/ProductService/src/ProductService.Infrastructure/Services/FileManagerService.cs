using Microsoft.AspNetCore.Http;

namespace ProductService.Infrastructure.Services;

public static class FileManagerService
{
	public static async Task<byte[]> FormFileToByteArrayAsync(IFormFile file)
	{
		using var memoryStream = new MemoryStream();
		await file.CopyToAsync(memoryStream);
		return memoryStream.ToArray();
	}
}