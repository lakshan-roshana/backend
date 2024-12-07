using MongoDB.Driver;
using SimpleCrudApi.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleCrudApi.Services
{
    public class ItemService
    {
        private readonly IMongoCollection<Item> _items;

        public ItemService(IOptions<DatabaseSettings> dbSettings)
        {
            var client = new MongoClient(dbSettings.Value.ConnectionString);
            var database = client.GetDatabase(dbSettings.Value.DatabaseName);
            _items = database.GetCollection<Item>(dbSettings.Value.CollectionName);
        }

        public async Task<List<Item>> GetAllAsync() => 
            await _items.Find(_ => true).ToListAsync();

        public async Task<Item> GetAsync(string id) => 
            await _items.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Item item) => 
            await _items.InsertOneAsync(item);

        public async Task UpdateAsync(string id, Item item) => 
            await _items.ReplaceOneAsync(x => x.Id == id, item);

        public async Task DeleteAsync(string id) => 
            await _items.DeleteOneAsync(x => x.Id == id);
    }
}
