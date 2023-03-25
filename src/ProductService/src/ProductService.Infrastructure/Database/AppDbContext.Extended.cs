// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Microsoft.EntityFrameworkCore;

#endregion

namespace ProductService.Infrastructure.Database;

public partial class AppDbContext
{
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyGlobalFilters("IsDeleted", false);
        modelBuilder.ApplyGlobalFilters<DateTime?>("DeleteDate", null);
    }

    public override int SaveChanges()
    {
        UpdateDefaultActionStatuses();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        UpdateDefaultActionStatuses();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateDefaultActionStatuses();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        UpdateDefaultActionStatuses();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void UpdateDefaultActionStatuses()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            var entryPropertyNames = entry.CurrentValues.Properties.Select(item => item.Name).ToList();
            switch (entry.State)
            {
                case EntityState.Added when entryPropertyNames.Contains("CreateDate"):
                    entry.CurrentValues["CreateDate"] = DateTime.UtcNow;
                    break;
                case EntityState.Modified when entryPropertyNames.Contains("UpdateDate"):
                    entry.CurrentValues["UpdateDate"] = DateTime.UtcNow;
                    break;
                case EntityState.Deleted:
                    if (entryPropertyNames.Contains("IsDeleted"))
                    {
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                    }
                    else if (entryPropertyNames.Contains("DeleteDate"))
                    {
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["DeleteDate"] = DateTime.UtcNow;
                    }

                    break;
            }
        }
    }
#pragma warning disable IDE0060
    [DbFunction("JSON_VALUE", IsBuiltIn = true, IsNullable = false)]
    public static string JsonValue(string expression, string path)
    {
        throw new NotSupportedException();
    }

    [DbFunction("JSON_QUERY", IsBuiltIn = true, IsNullable = false)]
    public static string JsonQuery(string expression, string path)
    {
        throw new NotSupportedException();
    }
#pragma warning restore IDE0060
}
