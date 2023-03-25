// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
