// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ProductService.Contracts.Dtos.Products;

namespace ProductService.Contracts.Responses.Paged;

public sealed record CategoryDtoPagedResponse(IEnumerable<ProductDto> Data, int TotalCount)
    : PagedResponse<ProductDto>(Data, TotalCount);
