// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProductService.Contracts.Dtos.Categories;

public sealed class CategoryUpdateDto
{
	public int Id { get; set; }

	public string Name { get; set; } = null!;

	public string? Description { get; set; }
	public byte[]? Icon { get; set; }
}
