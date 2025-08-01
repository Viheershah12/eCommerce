﻿using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Inventory.MongoDB;

[ConnectionStringName(InventoryDbProperties.ConnectionStringName)]
public interface IInventoryMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */

    IMongoCollection<Models.Inventory> Inventory { get; }   

    IMongoCollection<Models.StockMovement> StockMovement { get; }
}
